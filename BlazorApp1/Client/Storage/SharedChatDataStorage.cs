using System.Collections.Concurrent;
using System.Net.Http.Json;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Responses.Chats;
using BlazorApp1.Shared.Responses.Users;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorApp1.Client.Storage;

public class SharedChatDataStorage
{
    private readonly HttpClient _httpClient;
    private readonly IDictionary<int, UserModel> _users = new ConcurrentDictionary<int, UserModel>();
    private readonly IDictionary<int, StoredChatData> _chats = new ConcurrentDictionary<int, StoredChatData>();
    private readonly IDictionary<int, MessageModel> _messages = new ConcurrentDictionary<int, MessageModel>();
    private bool _allUsersLoaded;
    private int? _currentUserId;

    public SharedChatDataStorage(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserModel?> GetCurrentUserAsync()
    {
        if (_currentUserId is null)
        {
            await FetchCurrentUserAsync();
        }
        
        return _currentUserId is not null ? _users[_currentUserId.Value] : null;
    }

    private async Task FetchCurrentUserAsync()
    {
        var response = await SafeGetHttp<GetCurrentUserResponse>("Users/current");
        if (response.CurrentUser is null)
        {
            return;
        }

        _currentUserId = response.CurrentUser?.Id;
        _users[_currentUserId!.Value] = response.CurrentUser!;
        CurrentUserUpdated?.Invoke(response.CurrentUser!);
    }

    public event Action<UserModel>? CurrentUserUpdated;

    public async Task<IReadOnlyCollection<UserModel>> GetUsersAsync()
    {
        if (_allUsersLoaded)
        {
            return _users.Values.ToArray();
        }

        var response = await SafeGetHttp<GetAllUsersResponse>("api/Users");
        ArgumentNullException.ThrowIfNull(response);
        var users = response.AllUsers;
        foreach (var user in users)
        {
            _users[user.Id] = user;
        }

        _allUsersLoaded = true;
        return _users.Values.ToArray();
    }

    public async Task<IReadOnlyCollection<ChatModel>> GetChatsAsync()
    {
        if (_chats.Any())
        {
            return _chats.Values.Select(c => c.Chat).ToArray();
        }

        var response = await SafeGetHttp<GetAllChatsResponse>("api/Chats");
        var chats = response.AllChats;
        foreach (var chat in chats)
        {
            _chats.TryAdd(chat.Id, new StoredChatData { Chat = chat, IsPreviewLoaded = true });
            foreach (var msg in chat.Messages)
            {
                _messages[msg.Id] = msg;
            }
        }

        return chats;
    }
    
    public async Task<ChatModel> GetChatAsync(int id)
    {
        if (_chats.TryGetValue(id, out var data) && data.IsPreviewLoaded is false)
        {
            return data.Chat;
        }

        var result = await _httpClient.GetFromJsonAsync<GetChatResponse>($"api/Chats/{id}");
        var chat = result!.Chat;
        
        _chats[id] = new StoredChatData { Chat = chat, IsPreviewLoaded = false };
        
        foreach (var msg in chat.Messages)
        {
            _messages[msg.Id] = msg;
        }
        
        return chat;
    }

    private void AddMessage(SendMessageResponse response) => _messages.Add(response.Message.Id, response.Message);

    private async Task<TResult> SafeGetHttp<TResult>(string uri)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<TResult>(uri);
            ArgumentNullException.ThrowIfNull(result);
            return result;
        }
        catch (AccessTokenNotAvailableException e)
        {
            e.Redirect();
            
            await Task.Delay(-1);
            return default!;
        }
    }
}
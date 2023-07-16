using System.Collections.Concurrent;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Chats;

namespace BlazorApp1.Client.Storage;

public class SharedChatDataStorage
{
    private readonly IUsersControllerClient _usersControllerClient;
    private readonly IChatsControllerClient _chatsControllerClient;
    private readonly ChatHubClient _chatHubClient;
    private readonly IDictionary<int, UserModel> _users = new ConcurrentDictionary<int, UserModel>();
    private readonly IDictionary<int, ChatModel> _chats = new ConcurrentDictionary<int, ChatModel>();
    private readonly IDictionary<int, MessageModel> _messages = new ConcurrentDictionary<int, MessageModel>();
    private bool _allUsersLoaded;
    private int? _currentUserId;

    public SharedChatDataStorage(ChatHubClient chatHubClient, 
        IChatsControllerClient chatsControllerClient, 
        IUsersControllerClient usersControllerClient)
    {
        _chatHubClient = chatHubClient;
        _chatsControllerClient = chatsControllerClient;
        _usersControllerClient = usersControllerClient;
        _chatHubClient.OnMessageSent += AddMessage;
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
        var response = await _usersControllerClient.GetCurrentUser();
        if (response.CurrentUser is null)
        {
            _currentUserId = null;
            CurrentUserUpdated?.Invoke(null!);
            return;
        }

        _currentUserId = response.CurrentUser?.Id;
        _users[_currentUserId!.Value] = response.CurrentUser!;
        CurrentUserUpdated?.Invoke(response.CurrentUser!);
    }

    public async Task LogOutUserAsync()
    {
        await _usersControllerClient.LogOutUser(new LogOutUserRequest());
        _currentUserId = null;
        CurrentUserUpdated?.Invoke(null!);
    }

    public event Action<UserModel>? CurrentUserUpdated;

    public async Task<IReadOnlyCollection<UserModel>> GetUsersAsync()
    {
        if (_allUsersLoaded)
        {
            return _users.Values.ToArray();
        }

        var response = await _usersControllerClient.GetAllUsers(new GetAllUsersRequest());
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
            return _chats.Values.ToArray();
        }

        var response = await _chatsControllerClient.GetAllChats(new GetAllChatsRequest());
        var chats = response.AllChats;
        foreach (var chat in chats)
        {
            _chats.TryAdd(chat.Id, chat);
            foreach (var msg in chat.Messages)
            {
                _messages[msg.Id] = msg;
            }
        }

        return chats;
    }

    private void AddMessage(SendMessageResponse response) => _messages.Add(response.Message.Id, response.Message);
}
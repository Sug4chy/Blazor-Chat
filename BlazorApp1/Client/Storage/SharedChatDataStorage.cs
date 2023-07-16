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
    private readonly IDictionary<int, IDictionary<int, MessageModel>> _messages =
        new ConcurrentDictionary<int, IDictionary<int, MessageModel>>();
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
        if (_users.TryGetValue(_currentUserId!.Value, out _))
        {
            _users[_currentUserId!.Value] = response.CurrentUser!;
        }
        else
        {
            _users.Add(_currentUserId!.Value, response.CurrentUser!);
        }
        CurrentUserUpdated?.Invoke(response.CurrentUser!);
    }

    public async Task LogOutUserAsync()
    {
        await _usersControllerClient.LogOutUser(new LogOutUserRequest());
        _currentUserId = null;
        CurrentUserUpdated?.Invoke(null!);
    }

    public async Task CreateUserAsync(string name, string password)
    {
        await _usersControllerClient.CreateUser(new CreateUserRequest { Name = name, Password = password });
    }

    public async Task AuthorizeUserAsync(string name, string password)
    {
        var response = await _usersControllerClient.AuthorizeUser(new AuthorizeUserRequest { UserName = name, Password = password });
        _currentUserId = response.AuthorizedUser.Id;
    }

    public async Task<(IReadOnlyCollection<ChatModel>, string?)> GetUserChatsAsync(int userId)
    {
        var response = await _usersControllerClient.GetUserChats(new GetUserChatsRequest { UserId = userId });
        return (response.UserChats, response.UserName);
    }
    
    public event Action<UserModel>? CurrentUserUpdated;

    public async Task<ChatModel> GetChatAsync(int chatId)
    {
        if (_chats.TryGetValue(chatId, out var result))
        {
            return result;
        }

        var response = await _chatsControllerClient.GetChat(new GetChatRequest { ChatId = chatId });
        var chat = response.Chat;
        _messages.Add(chat.Id, chat.Messages.ToDictionary(msg => msg.Id, msg => msg));
        _chats.Add(chat.Id, chat);
        return chat;
    }
    
    public async Task<ChatModel> CreateChatAsync(string chatName)
    {
        var response = await _chatsControllerClient.CreateChat(new CreateChatRequest { Name = chatName });
        _chats.Add(response.Chat.Id, response.Chat);
        return response.Chat;
    }

    public async Task AddUserInChatAsync(int chatId, int userId)
    {
        var response = await _chatsControllerClient.AddUserInChat(new AddUserInChatRequest
            { ChatId = chatId, UserId = userId });
        _chats[chatId] = response.UpdatedChat;
    } 
    
    private void AddMessage(SendMessageResponse response) => _messages[response.Message.ChatId].Add(response.Message.Id, response.Message);
}
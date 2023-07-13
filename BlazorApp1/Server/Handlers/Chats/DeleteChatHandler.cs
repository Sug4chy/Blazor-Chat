using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Server.Handlers.Chats;

public class DeleteChatHandler : IRequestHandler<DeleteChatRequest, DeleteChatResponse>
{
    private readonly IChatService _chatService;

    public DeleteChatHandler(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<DeleteChatResponse> Handle(DeleteChatRequest request, CancellationToken cancellationToken)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чата с id {request.ChatId} не существует");
        }

        await _chatService.DeleteChat(chat);
        return new DeleteChatResponse();
    }
}
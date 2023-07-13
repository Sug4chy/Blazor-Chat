using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public Task<CreateChatResponse> CreateChat([FromBody] CreateChatRequest request) => _mediator.Send(request);

    [HttpGet]
    [Authorize]
    public Task<GetAllChatsResponse> GetAllChats([FromQuery] GetAllChatsRequest request) => _mediator.Send(request);

    [HttpDelete]
    [Authorize]
    public Task<DeleteChatResponse> DeleteChat([FromQuery] DeleteChatRequest request) => _mediator.Send(request);

    [HttpGet("{chatId:int}")]
    [Authorize]
    public Task<GetChatResponse> GetChat([FromRoute, FromBody] GetChatRequest request) => _mediator.Send(request);

    [HttpPut("{chatId:int}/users")]
    [Authorize]
    public Task<AddUserInChatResponse> AddUserInChat([FromBody] AddUserInChatRequest request) =>
        _mediator.Send(request);

    [HttpGet("{chatId:int}/users")]
    [Authorize]
    public Task<GetAllUsersInChatResponse> GetAllUsersInChat([FromRoute, FromBody] GetAllUsersInChatRequest request) =>
        _mediator.Send(request);

    [HttpPost("{chatId:int}")]
    [Authorize]
    public Task<SendMessageResponse> SendMessage([FromBody] SendMessageRequest request) => _mediator.Send(request);
}
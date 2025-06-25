using Application.Abstractions.Messaging;

namespace Application.Todos.Get;

public sealed record GetTodosQuery(int UserId) : IQuery<List<TodoResponse>>;

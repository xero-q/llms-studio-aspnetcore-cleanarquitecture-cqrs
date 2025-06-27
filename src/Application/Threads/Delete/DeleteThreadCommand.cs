using Application.Abstractions.Messaging;

namespace Application.Threads.Delete;

public sealed record DeleteThreadCommand(int ThreadId) : ICommand;

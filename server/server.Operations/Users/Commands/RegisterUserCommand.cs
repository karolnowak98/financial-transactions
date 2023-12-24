using server.Operations.Users.Dtos;

namespace server.Operations.Users.Commands;

public record RegisterUserCommand(RegisterDto RegisterDto) : ICommand<Result>;
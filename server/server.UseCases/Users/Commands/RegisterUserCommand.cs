using server.UseCases.Users.Dtos;

namespace server.UseCases.Users.Commands;

public record RegisterUserCommand(RegisterDto RegisterDto) : ICommand<Result>;
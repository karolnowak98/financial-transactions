using server.UseCases.Users.Dtos;

namespace server.UseCases.Users.Commands;

public record LoginUserCommand(LoginDto LoginDto) : ICommand<Result<string>>;
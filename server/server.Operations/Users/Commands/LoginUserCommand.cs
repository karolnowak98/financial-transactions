using server.Operations.Users.Dtos;

namespace server.Operations.Users.Commands;

public record LoginUserCommand(LoginDto LoginDto) : ICommand<Result<string>>;
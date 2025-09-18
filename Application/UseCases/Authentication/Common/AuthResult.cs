namespace Application.UseCases.Authentication.Common;

public record AuthResult(
    string AccessToken,
    string RefreshToken,
    string? Email = "",
    string? Name = "",
    string? FirstLastName = null,
    string? SecondLastName = null
    );

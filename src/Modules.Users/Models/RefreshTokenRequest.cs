namespace ModularMonolith.Modules.Users.Models;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);
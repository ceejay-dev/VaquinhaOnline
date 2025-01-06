namespace VaquinhaOnline.Application.Dtos.User;

public record UserTokenDto (
    string Id,
    string Username,
    string Email,
    string PhoneNumber
);

namespace VaquinhaOnline.Application.Dtos.User;

public record UserUpdateDto (Guid Id, string Name,
    string Email, string PhoneNumber, string Password);
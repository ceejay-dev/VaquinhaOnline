namespace VaquinhaOnline.Application.Dtos.User;

public record UserUpdateDto (Guid Id, string Name, string ProfilePhoto,
    string Email, string PhoneNumber, string Password);
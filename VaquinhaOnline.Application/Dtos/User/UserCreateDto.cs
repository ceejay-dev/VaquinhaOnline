namespace VaquinhaOnline.Application.Dtos.User;

public record UserCreateDto (string Name, DateTime CreationDate, string ProfilePhoto,
    string Email, string PhoneNumber, string Password);

namespace VaquinhaOnline.Application.Dtos.User;

public record UserCreateDto (string Name,
    string Email, string PhoneNumber, string Type, string Password);
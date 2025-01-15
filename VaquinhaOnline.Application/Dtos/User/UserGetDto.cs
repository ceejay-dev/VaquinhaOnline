namespace VaquinhaOnline.Application.Dtos.User;

public record UserGetDto (Guid Id, string Name, DateTime CreationDate,
    string Email, string PhoneNumber, string Password);

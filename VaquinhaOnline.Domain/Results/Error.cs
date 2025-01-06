namespace VaquinhaOnline.Domain.Results;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty, EErrorType.None);
    public static readonly Error NullValue = new("Generic Null", "valor null", EErrorType.NotFound);
    public Error(string code, string description, EErrorType errorType)
    {
        Description = description;
        Code = code;
        ErrorType = errorType;
    }

    public string Description { get; }
    public string Code { get; }
    public EErrorType ErrorType { get; }

    public static Error NotFound(string code, string description)
        => new(code, description, EErrorType.NotFound);
    public static Error Conflit(string code, string description)
        => new(code, description, EErrorType.Conflict);
    public static Error Failure(string code, string description)
        => new(code, description, EErrorType.Failure);
    public static Error Invalid(string code, string description)
        => new(code, description, EErrorType.Validation);
}



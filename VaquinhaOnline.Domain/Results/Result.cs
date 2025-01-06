namespace VaquinhaOnline.Domain.Results;

public class Result
{
    public Result(bool isSucess, params Error[] errors)
    {
        if (errors is null)
            throw new ArgumentNullException(nameof(errors));

        if (isSucess && errors.Any(x => x.ErrorType != EErrorType.None))
            throw new InvalidOperationException();

        if (!isSucess && errors.Contains(Error.None))
            throw new InvalidOperationException();

        IsSucess = isSucess;
        Errors = errors.AsReadOnly();
    }
    public bool IsSucess { get; }
    public bool IsFailure => !IsSucess;
    public IReadOnlyCollection<Error> Errors { get; }

    public static Result Succeed()
        => new Result(true, Error.None);

    public static Result Failure(params Error[] errors)
    {
        if (errors == null || errors.Length == 0)
            throw new ArgumentException("Pelo menos um erro deve ser fornecido para um resultado de falha.", nameof(errors));

        return new Result(false, errors);
    }

    public static Result Created(bool condition)
        => condition ? Succeed() : Failure(Error.Conflit("Error ao Criar", "Não foi possivel criar um objecto"));

    public static Result<TValue> Succeed<TValue>(TValue value)
    => new(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error)
        => new(default, false, error);

    public static Result<TValue> Created<TValue>(TValue value)
        => value != null ? Succeed(value) : Failure<TValue>(Error.NullValue);

    public static ResultPaginated<List<TValue>> Success<TValue>(List<TValue> values, int currentPage, int pageSize, int totalCount)
           => new(values, totalCount, currentPage, pageSize, true, Error.None);
    public static ResultPaginated<List<TValue>> Failure<TValue>(Error error, int currentPage, int pageSize, int totalCount)
        => new(default, totalCount, currentPage, pageSize, false, error);
    public static ResultPaginated<List<TValue>> Create<TValue>(List<TValue>? values, int currentPage, int pageSize, int totalCount)
        => values.Count > 0 ? new ResultPaginated<List<TValue>>(values, totalCount, currentPage, pageSize, true, Error.None)
        : new ResultPaginated<List<TValue>>(default, 0, currentPage, pageSize, false, Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    public Result(TValue? value, bool isSucess, params Error[] errors)
        : base(isSucess, errors)
    {
        _value = value;
    }
    public TValue Value => IsSucess ? _value! : default!;
}

public class ResultPaginated<TValue> : Result<TValue>
{
    public ResultPaginated(TValue? value,
        int totalCount,
        int currentPage,
        int pageSize,
        bool isSucess,
        params Error[] errors)
        : base(value, isSucess, errors)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPage => (int)Math.Ceiling(TotalCount / (decimal)PageSize);
}



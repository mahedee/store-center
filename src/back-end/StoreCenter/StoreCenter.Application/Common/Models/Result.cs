namespace StoreCenter.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; protected set; } = string.Empty;
        public string Code { get; protected set; } = string.Empty;

        protected Result(bool isSuccess, string error, string code = "")
        {
            IsSuccess = isSuccess;
            Error = error;
            Code = code;
        }

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error, string code = "") => new(false, error, code);
        
        public static Result<T> Success<T>(T value) => new(value, true, string.Empty);
        public static Result<T> Failure<T>(string error, string code = "") => new(default, false, error, code);
    }

    public class Result<T> : Result
    {
        public T? Value { get; protected set; }

        protected internal Result(T? value, bool isSuccess, string error, string code = "") 
            : base(isSuccess, error, code)
        {
            Value = value;
        }
    }
}
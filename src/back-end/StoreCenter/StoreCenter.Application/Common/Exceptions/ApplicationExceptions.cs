namespace StoreCenter.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors) : this()
        {
            Errors = errors;
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
namespace Minimal_API.Domain.Validations
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DomainExceptionValidation(error);
            }
        }

        public static void ThrowIfNullOrEmpty(string value, string error)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainExceptionValidation(error);
            }
        }
    }
}

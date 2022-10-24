namespace GetData.Domain.Validation
{
    public class DomainException : Exception
    {
        public DomainException(string error) : base(error)
        {           
        }

        public static void When(bool hasError, string message)
        {
            if(hasError)
                throw new DomainException(message);
        }
    }
}
namespace SwordTech.Melodart.Helper.Error;

public class DomainException : Exception, ICustomException
{
    public DomainException(string message) : base(message)
    {
    }
}

namespace SwordTech.Melodart.Helper.Error;

public class BusinessException : Exception, ICustomException
{
    public BusinessException(string message) : base(message)
    {
    }
}

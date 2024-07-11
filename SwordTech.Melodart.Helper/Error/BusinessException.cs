namespace SwordTech.Melodart.Helper.Error;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}

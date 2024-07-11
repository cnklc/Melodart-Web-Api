namespace SwordTech.Melodart.Application.Contract.Users.Models;

public class ChangePasswordRequest
{
    public string Code { get; set; }
    public string NewPassword { get; set; }
}

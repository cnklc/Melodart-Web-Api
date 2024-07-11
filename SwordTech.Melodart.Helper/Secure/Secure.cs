using System.Text;

namespace SwordTech.Melodart.Helper.Secure;

public class Secure
{
    public static string GenerateNewPassword()
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
        StringBuilder newPassword = new StringBuilder();
        Random random = new Random();

        // En az bir büyük harf ekle
        newPassword.Append(validChars[random.Next(validChars.Length / 2, validChars.Length / 2 + 26)]);
        // En az bir küçük harf ekle
        newPassword.Append(validChars[random.Next(0, validChars.Length / 2)]);
        // En az bir rakam ekle
        newPassword.Append(validChars[random.Next(validChars.Length / 2 + 26, validChars.Length / 2 + 36)]);
        // En az bir özel karakter ekle
        newPassword.Append(validChars[random.Next(validChars.Length / 2 + 36, validChars.Length)]);

        // Geri kalan karakterleri rastgele ekle
        for (int i = 4; i < 8; i++)
        {
            newPassword.Append(validChars[random.Next(validChars.Length)]);
        }

        // Şifreyi karıştır
        for (int i = 0; i < newPassword.Length; i++)
        {
            int swapIndex = random.Next(i, newPassword.Length);
            char temp = newPassword[i];
            newPassword[i] = newPassword[swapIndex];
            newPassword[swapIndex] = temp;
        }

        return newPassword.ToString();
    }
}

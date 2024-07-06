using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.EFCore.Repositories;

namespace SwordTech.Melodart.Application.Users;

public class UserAppService : AppService<AppUser, UserDto, UserDto, UserCreateDto, UserUpdateDto>, IUserAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHostEnvironment _env;

    public UserAppService(IEfBaseRepository<AppUser> repository,IMapper mapper, UserManager<AppUser> userManager, IHostEnvironment env) : base(repository,mapper)
    {
        _userManager = userManager;
        _env = env;
    }

    public override async Task<IList<UserDto>> GetAll()
    {
        var list = await base.GetAll();

        foreach (var userDto in list)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            var lists = await _userManager.GetClaimsAsync(user);

            userDto.Authorizations = lists.Select(x => x.Value).ToList();
        }

        return list;
    }

    public override async Task<UserDto> GetById(Guid id)
    {
        var user = await base.GetById(id);

        user.Authorizations = (await _userManager.GetClaimsAsync(await _userManager.FindByIdAsync(id.ToString()))).Select(x => x.Value).ToList();

        return user;
    }

    public override async Task<UserDto> Create(UserCreateDto input)
    {
        var user = new AppUser()
        {
            Email = input.Email,
            UserName = input.Email,
            Name = input.Name,
            LastName = input.LastName,
            ImageUrl = "",
            Title = input.Title
        };

        var result = await _userManager.CreateAsync(user, input.Password);

        if (result.Succeeded)
        {
            if (input.Authorizations != null && input.Authorizations.Any())
            {
                var claimList = input.Authorizations.Select(x => new Claim(ClaimTypes.Authentication, x)).ToList();
                await _userManager.AddClaimsAsync(user, claimList);
            }

            return await GetById(user.Id);
        }

        return null;
    }

    public override async Task<UserDto> Update(Guid id, UserUpdateDto input)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        user.Name = input.Name;
        user.LastName = input.LastName;
        user.Email = input.Email;
        user.Title = input.Title;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            if (input.Authorizations != null && input.Authorizations.Any())
            {
                var claimList = input.Authorizations.Select(x => new Claim(ClaimTypes.Authentication, x)).ToList();
                var claims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, claims);
                await _userManager.AddClaimsAsync(user, claimList);
            }

            if (input.Password != null)
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, input.Password);
            }

            if (input.Image != null)
            {
                user.ImageUrl = await SaveImage(_env, input.Image);
                await _userManager.UpdateAsync(user);
            }

            return await GetById(user.Id);
        }

        return null;
    }

    public async Task<UserDto> Login(LoginDto login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);

        var result = await _userManager.CheckPasswordAsync(user, login.Password);

        if (result)
        {
            return await GetById(user.Id);
        }

        return null;
    }

    public async Task<bool> ResetPassword(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Yeni bir şifre oluştur
                string newPassword = GenerateNewPassword();

                // Yeni şifreyi kullanıcıya ata
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                // Şifre sıfırlama işlemi başarılı olduysa true döndür, aksi halde false döndür
                return result.Succeeded;
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> ChangePassword(ChangePasswordDto input)
    {
        var user = await _userManager.FindByIdAsync(input.UserId.ToString());

        var result = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);

        return result.Succeeded;
    }

    public async Task<List<Authorization>> GetAuthorization()
    {
        return new List<Authorization>()
        {
            new Authorization() { Id = "profile", Name = "Profil" },
            new Authorization() { Id = "dashboard", Name = "Ana Sayfa" },
            new Authorization() { Id = "reservations", Name = "Rezervasyonlar" },
            new Authorization() { Id = "users", Name = "Kullanıcılar" },
            new Authorization() { Id = "customers", Name = "Müşteriler" },
            new Authorization() { Id = "yachts", Name = "Yatlar" },
            new Authorization() { Id = "yachtTypes", Name = "Yat Tipleri" },
            new Authorization() { Id = "tourTypes", Name = "Tur Tipleri" },
            new Authorization() { Id = "menus", Name = "Menüler" },
            new Authorization() { Id = "locations", Name = "Lokasyonlar" },
            new Authorization() { Id = "countries", Name = "Ülkeler" },
            new Authorization() { Id = "cities", Name = "Şehirler" },
            new Authorization() { Id = "financials", Name = "Mali Hesaplar" }
        };
    }

    // public async Task<UserDto> GetMyProfile()
    // {
    //     var user = await _userManager.FindByIdAsync(  User.Identity.GetUserId());
    //     var userDto = new UserDto()
    //     {
    //         Id = user.Id,
    //         Name = user.Name,
    //         LastName = user.LastName,
    //         Email = user.Email,
    //         Authorizations = (await _userManager.GetClaimsAsync(user)).Select(x => x.Value).ToList()
    //     };
    //
    //     return userDto;
    // }

    private string GenerateNewPassword()
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

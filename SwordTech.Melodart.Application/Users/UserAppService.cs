using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Users;

public class UserAppService : AppService<AppUser, UserDto, UserDto, UserCreateDto, UserUpdateDto>, IUserAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHostEnvironment _env;

    public UserAppService(IEfBaseRepository<AppUser> repository, IMapper mapper, UserManager<AppUser> userManager, IHostEnvironment env) : base(repository, mapper)
    {
        _userManager = userManager;
        _env = env;

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
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
            Title = input.Title
        };
        
        if (input.Image != null)
        {
            user.ImageUrl = await base.SaveImage(_env, input.Image);
        }
        
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

    public async Task<List<AuthorizationDto>> GetAuthorization()
    {
        return new List<AuthorizationDto>()
        {
            new AuthorizationDto() { Id = "profile", Name = "Profil" },
            new AuthorizationDto() { Id = "dashboard", Name = "Ana Sayfa" },
            new AuthorizationDto() { Id = "reservations", Name = "Rezervasyonlar" },
            new AuthorizationDto() { Id = "users", Name = "Kullanıcılar" },
            new AuthorizationDto() { Id = "customers", Name = "Müşteriler" },
            new AuthorizationDto() { Id = "yachts", Name = "Yatlar" },
            new AuthorizationDto() { Id = "yachtTypes", Name = "Yat Tipleri" },
            new AuthorizationDto() { Id = "tourTypes", Name = "Tur Tipleri" },
            new AuthorizationDto() { Id = "menus", Name = "Menüler" },
            new AuthorizationDto() { Id = "locations", Name = "Lokasyonlar" },
            new AuthorizationDto() { Id = "countries", Name = "Ülkeler" },
            new AuthorizationDto() { Id = "cities", Name = "Şehirler" },
            new AuthorizationDto() { Id = "financials", Name = "Mali Hesaplar" }
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
}

using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;
using System.Text.RegularExpressions;

namespace SwordTech.Melodart.Application.Users;

public class UserAppService : AppService<AppUser, UserDto, UserDto, UserCreateDto, UserUpdateDto>, IUserAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IWebHostEnvironment _env;


    public UserAppService(IEfBaseRepository<AppUser> repository, IMapper mapper, UserManager<AppUser> userManager, IWebHostEnvironment env) : base(repository, mapper)
    {
        _userManager = userManager;
        _env = env;


        // mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    public override async Task<IList<UserDto>> GetAll()
    {
        var list = await base.GetAll();

        foreach (var userDto in list)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            var lists = await _userManager.GetClaimsAsync(user);

            userDto.Authorizations = lists.Select(x => x.Value).ToList();

            if (userDto.Authorizations[0].Contains(","))
            {
                userDto.Authorizations = Regex.Split(userDto.Authorizations[0], ",").ToList();
            }
        }

        return list;
    }

    public override async Task<UserDto> GetById(Guid id)
    {
        var user = await base.GetById(id);

        user.Authorizations = (await _userManager.GetClaimsAsync(await _userManager.FindByIdAsync(id.ToString()))).Select(x => x.Value).ToList();

        if (user.Authorizations[0].Contains(","))
        {
            user.Authorizations = Regex.Split(user.Authorizations[0], ",").ToList();
        }

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
            new AuthorizationDto() { Id = "users", Name = "Kullanıcı Yönetimi" },
            new AuthorizationDto() { Id = "students", Name = "Öğrenci Yönetimi" },
            new AuthorizationDto() { Id = "teahcers", Name = "Öğretmen Yönetimi" },
            new AuthorizationDto() { Id = "lessens", Name = "Ders Programı Yönetimi" },
            new AuthorizationDto() { Id = "d", Name = "Ders Programı Yönetimi" },
            new AuthorizationDto() { Id = "dd", Name = "Devamsızlık Takibi" },
            new AuthorizationDto() { Id = "financial", Name = "Finans" },
            new AuthorizationDto() { Id = "definations", Name = "Tanımlar" },
            new AuthorizationDto() { Id = "settings", Name = "Ayarlar" },
        };
    }


}

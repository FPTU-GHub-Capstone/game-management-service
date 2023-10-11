﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/users")]
public class UsersController : BaseController
{
    private readonly IUserServices _userServices;
    private readonly IGenericRepository<UserEntity> _userRepo;
    public UsersController(IUserServices userServices, IGenericRepository<UserEntity> userRepo)
    {
        _userServices = userServices;
        _userRepo = userRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userServices.List();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userServices.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest cUser)
    {
        UserEntity user = new UserEntity();
        Mapper.Map(cUser, user);
        await _userServices.Create(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest user)
    {
        var currentUser = await _userServices.GetById(id);
        Mapper.Map(user, currentUser);
        await _userServices.Update(id, currentUser);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userServices.Delete(id);
        return Ok();
    }
}

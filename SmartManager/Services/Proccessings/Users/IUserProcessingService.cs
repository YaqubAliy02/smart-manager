﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Applicants;
using System.Linq;
using System.Threading.Tasks;
using System;
using SmartManager.Models.Users;

namespace SmartManager.Services.Proccessings.Users
{
    public interface IUserProcessingService
    {
        ValueTask<User> AddUserAsync(User user);
        ValueTask<User> RetrieveUserByIdAsync(Guid userid);
        ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string password);
        IQueryable RetrieveAllUsers();
        ValueTask<User> ModifyUserAsync(User user);
        ValueTask<User> RemoveUserAsync(Guid userid);
    }
}

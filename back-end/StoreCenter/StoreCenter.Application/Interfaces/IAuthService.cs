﻿using StoreCenter.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Application.Interfaces
{
    public interface IAuthService
    {
        //Task<string> GenerateToken(User user);
        Task<(bool Success, List<string> Errors)> SignUpAsync(SignUpDto signUpDto);
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}

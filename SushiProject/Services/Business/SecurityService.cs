﻿using SushiProject.Models;
using SushiProject.Services.Data;

namespace SushiProject.Services.Business
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();

        public bool Authenticate(VMLogin user)
        {
            return daoService.FindByUser(user);
        }
    }
}

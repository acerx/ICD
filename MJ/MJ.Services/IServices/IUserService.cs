﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Common.DTO;

namespace MJ.Services.IServices
{
    public interface IUserService
    {

        bool AddUser(UserDto userDto);

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Application.Common.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string firstName, string lastName);
    }
}
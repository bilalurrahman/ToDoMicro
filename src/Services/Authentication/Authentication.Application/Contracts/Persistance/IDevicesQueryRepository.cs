﻿using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Contracts.Persistance
{
   public interface IDevicesQueryRepository
    {
        Task<List<UserNotificationDevices>> getAllDevices(int userId);
    }
}
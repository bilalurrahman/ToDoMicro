﻿using Announcement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Contracts.Integration
{
    public interface IEmailIntegration
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}

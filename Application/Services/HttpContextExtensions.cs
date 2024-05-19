using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class HttpContextExtensions
    {
        public static string GetIpAddress(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}

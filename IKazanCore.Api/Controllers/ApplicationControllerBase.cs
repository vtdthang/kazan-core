using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Controllers
{
    public abstract class ApplicationControllerBase : ControllerBase
    {
        protected int GetUserId()
        {
            var userId = this.User.Claims.FirstOrDefault(i => i.Type == "Id")?.Value;
            if(userId == null)
            {
                return 0;
            }

            return Convert.ToInt32(userId);
        }
    }
}

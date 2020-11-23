using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessProfileController : ControllerBase
    {
        [HttpPost]
        public void CreateProfile([FromBody]CreateProfileDto profileDto)
        {

        }
    }
}

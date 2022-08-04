using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CouponsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        [HttpGet]
        public int GetCouponDiscount([FromQuery] string coupon)
        {
            return 10;
        }
    }
}
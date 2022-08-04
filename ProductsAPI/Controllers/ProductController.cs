using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ProductController(IConfiguration config)
        {
            _config = config;

        }
        List<Product> lst = new List<Product>{
                new Product{Name="Boots",Description="Long Boots",Price=100.0,ProductId=1},
                new Product{Name="Socks",Description="Short socks",Price=10.0,ProductId=2},
            };

        [HttpGet]
        public ActionResult<List<Product>> List()
        {
            return lst;
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> GetDiscountedList(string coupon)
        {
            List<Product> discountList = lst;
            int discount = 0;

            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(_config["ExtServices:CouponServiceBaseURL"] + $"?coupon={coupon}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    discount = JsonConvert.DeserializeObject<int>(apiResponse);
                }

            }

            foreach (var item in discountList)
            {
                item.Price = item.Price - (discount * item.Price / 100);
            }


            return discountList;

        }
    }
}
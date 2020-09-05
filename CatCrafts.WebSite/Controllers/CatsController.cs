using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatCrafts.WebSite.Models;
using CatCrafts.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatCrafts.WebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        public CatsController(JsonFileCatsService catsService)
        {
            this.CatsService = catsService;
        }

        public JsonFileCatsService CatsService { get; }

        [HttpGet]
        public IEnumerable<Cat> Get()
        {
            return CatsService.GetCats();
        }

        //[HttpPatch] "[FromBody]"
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] string CatId, 
            [FromQuery] int Rating)
        {
            CatsService.AddRating(CatId, Rating);
            return Ok();
        }
    }
}
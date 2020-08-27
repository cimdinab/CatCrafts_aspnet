using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatCrafts.WebSite.Models;
using CatCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CatCrafts.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileCatsService CatService;
        public IEnumerable<Cat> Cats { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileCatsService catService)
        {
            _logger = logger;
            CatService = catService;
        }

        public void OnGet()
        {
            Cats = CatService.GetCats();
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CatCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace CatCrafts.WebSite.Services
{
    public class JsonFileCatsService
    {
        public JsonFileCatsService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "cats.json"); }
        }

        public IEnumerable<Cat> GetCats()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Cat[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
    }

}

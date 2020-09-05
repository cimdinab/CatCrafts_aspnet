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

        public void AddRating(string catId, int rating)
        {
            var cats = GetCats();

            //LINQ
            var query = cats.First(x => x.Id == catId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Cat>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                cats
                );
            }
        }
    }

}

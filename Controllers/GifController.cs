using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using CatGifApi.Models;
using CatGifApi.Data;

namespace CatGifApi.Controllers
{
    [ApiController]
    [Route("api/gif")]
    public class GifController : ControllerBase
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly AppDbContext _context;

        public GifController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGif([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("El parámetro 'query' es obligatorio.");

            // Tomar solo las primeras 3 palabras del query
            var trimmedWords = query.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(3);
            var trimmedQuery = string.Join(" ", trimmedWords);

            string apiKey = "voaNIOg1u7ONPbckzWK71C48YqCOkhVP";
            string url = $"https://api.giphy.com/v1/gifs/search?api_key={apiKey}&q={Uri.EscapeDataString(trimmedQuery)}&limit=1";

            var giphyResponse = await _http.GetFromJsonAsync<GiphyResponse>(url);
            var gifData = giphyResponse?.data?.FirstOrDefault();
            var gifUrl = gifData?.images?.original?.url ?? "";

            var history = new SearchHistory
            {
                Date = DateTime.UtcNow,
                CatFact = query,
                QueryWords = trimmedQuery,
                GifUrl = gifUrl
            };

            _context.SearchHistories.Add(history);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                originalQuery = query,
                trimmedQuery,
                gif = gifUrl
            });
        }

        public class GiphyResponse
        {
            public List<GifData>? data { get; set; }
        }

        public class GifData
        {
            public GifImages? images { get; set; }
        }

        public class GifImages
        {
            public GifOriginal? original { get; set; }
        }

        public class GifOriginal
        {
            public string? url { get; set; }
        }
    }
}

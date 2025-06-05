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

            var gifUrl = await GetGifUrl(trimmedQuery);

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

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshGif([FromBody] CatFactRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CatFact))
                return BadRequest("CatFact is required.");

            var words = request.CatFact.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(3);
            var query = string.Join(" ", words);

            try
            {
                var gifUrl = await GetGifUrl(query);
                return Ok(new { gifUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving GIF: {ex.Message}");
            }
        }

        private async Task<string> GetGifUrl(string query)
        {
            string apiKey = "voaNIOg1u7ONPbckzWK71C48YqCOkhVP";
            string url = $"https://api.giphy.com/v1/gifs/search?api_key={apiKey}&q={Uri.EscapeDataString(query)}&limit=1";

            var giphyResponse = await _http.GetFromJsonAsync<GiphyResponse>(url);
            var gifData = giphyResponse?.data?.FirstOrDefault();
            return gifData?.images?.original?.url ?? "";
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

        public class CatFactRequest
        {
            public string CatFact { get; set; }
        }
    }
}

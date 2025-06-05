using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using CatGifApi.Data;

namespace CatGifApi.Controllers{
    [ApiController]
    [Route("api/cat/fact")]
    public class FactController : ControllerBase
    {
        private readonly HttpClient _http = new HttpClient();

        [HttpGet("random")]
        public async Task<IActionResult> GetFact()
        {
            var response = await _http.GetFromJsonAsync<CatFact>("https://catfact.ninja/fact");
            return Ok(response);
        }

        public class CatFact
        {
            public string Fact { get; set; } = "";
        }
    }
}
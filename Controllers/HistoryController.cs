using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatGifApi.Data;

namespace CatGifApi.Controllers{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HistoryController(AppDbContext context) => _context = context;

        [HttpGet("all")]
        public async Task<IActionResult> GetHistory()
        {
            var data = await _context.SearchHistories.ToListAsync();
            return Ok(data);
        }
    }
}
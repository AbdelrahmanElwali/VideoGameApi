using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> _videoGames = new List<VideoGame>
        {
            new VideoGame
            {

                Id = 1,
                Title = "Spider-Man 2",
                Platform = "PS5",
                Developer = "Insomniac Games",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame
            {

                Id = 2,
                Title = "The Legend of Zelda: Breath of the wild",
                Platform = "Nintendo Switch",
                Developer = "Nintendo EPD",
                Publisher = "Nintendo"
            },
            new VideoGame
            {

                Id = 3,
                Title = "Cyberpunk 2077",
                Platform = "PC",
                Developer = "CD Projekt Red",
                Publisher = "CD Projekt"
            }
        };

        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(_videoGames);
        }
        [HttpGet("{id}")]
        public ActionResult <VideoGame> GetVideoGameById(int id)
        {
            var game = _videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public ActionResult <VideoGame> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest();

            newGame.Id = _videoGames.Max(g => g.Id) + 1;
            _videoGames.Add(newGame);
            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame(int id, VideoGame updatedGame)
        {
            var game = _videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Developer = updatedGame.Developer;
            game.Publisher = updatedGame.Publisher;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideoGame(int id)
        {
            var game = _videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            _videoGames.Remove(game);
            return NoContent();
        }
    }
}

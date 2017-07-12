using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly TeamContext _context;

        public PlayerController(TeamContext context)
        {
            _context = context;

            if (_context.Team.Count() == 0)
            {
                _context.Team.Add(new Player { name = "David Luiz", position = "defender", number = 30, salary = 2000000, Id=1 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Player> GetTeam()
        {
            return _context.Team.ToList();
        }

        [HttpGet("{id}", Name = "GetPlayer")]
        public IActionResult GetByName(long id)
        {
            var player = _context.Team.FirstOrDefault(t => t.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return new ObjectResult(player);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest();
            }

            _context.Team.Add(player);
            _context.SaveChanges();

            return CreatedAtRoute("GetPlayer", new { name = player.name, number = player.number, position = player.position, id = player.Id, salary = player.salary,  }, player);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Player player)
        {
            if (player == null || player.Id != id)
            {
                return BadRequest();
            }

            var play = _context.Team.FirstOrDefault(t => t.Id == id);
            if (play == null)
            {
                return NotFound();
            }

            play.name = player.name;
            play.number = player.number;
            play.salary = player.salary;
            play.Id = player.Id;
            play.position = player.position;

            _context.Team.Update(play);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var player = _context.Team.First(t => t.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Team.Remove(player);
            _context.SaveChanges();
            return new NoContentResult();
        }





    }

}

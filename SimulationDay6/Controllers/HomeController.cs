using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationDay6.DataAccessLayer;
using SimulationDay6.ViewModels;
using SimulationDay6.ViewModels.Persons;
using SimulationDay6.ViewModels.Positions;

namespace SimulationDay6.Controllers
{
   
    public class HomeController(DewiDbContext _context) : Controller
    {
        public async Task< IActionResult >Index()
        {
            var persons=await _context.Persons.Select(x=>new PersonGetVM
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description,
                PositionName=x.Position.Name,
                ImageUrl = x.ImageUrl,
            }).ToListAsync();

            var positions = await _context.Positions.Select(x => new PositionGetVM
            {
                Name = x.Name,
                Id = x.Id,
            }).ToListAsync();

            HomeVM vm = new()
            {
                Positions = positions,
                Persons = persons
            };
            return View(vm);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationDay6.DataAccessLayer;
using SimulationDay6.Models;
using SimulationDay6.ViewModels.Persons;

namespace SimulationDay6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController(DewiDbContext _context) : Controller
    {
        public async Task< IActionResult> Index()
        {
            var datas=await _context.Persons.Select(x=> new PersonGetVM
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description,
                PositionName=x.Position.Name,
                ImageUrl=x.ImageUrl,
            }).ToListAsync();
            return View(datas);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Position = await _context.Positions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(PersonCreateVM vm)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();
            if (!ModelState.IsValid) 
                return View(vm);

            //if (vm.ImageFile != null)
            //{
            //    if (vm.ImageFile.ContentType.StartsWith("image"))
            //        ModelState.AddModelError("ImageFile", "File must be image");
            //    if (vm.ImageFile.Length > 2 * 1024 * 1024)
            //        ModelState.AddModelError("ImageFile", "File size must be less than 200kb ");

            //}
            if (!await _context.Positions.AnyAsync(x => x.Id == vm.PositionId))
            {
                ViewBag.Position=await _context.Positions.ToListAsync();
                ModelState.AddModelError("PositionId", "Position does not exit");
                return View(vm);
            }
            string newImgName = Guid.NewGuid().ToString() + vm.ImageFile!.FileName;
            string path = Path.Combine("wwwroot", "imgs", "persons", newImgName);
            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            await vm.ImageFile.CopyToAsync(fs);
            Person person = new()
            {
                Name = vm.Name,
                Description = vm.Description,
                PositionId = vm.PositionId,
                ImageUrl = newImgName
            };
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();
            if (!id.HasValue || id.Value <1) 
                return View();
            var person=await _context.Persons.Where(x=>x.Id==id).Select(x=>new PersonUpdateVM
            {
                Name = x.Name,
                Description = x.Description,
                PositionId = x.PositionId,
            }).FirstOrDefaultAsync();
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int?id,PersonUpdateVM vm)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();
            if (!id.HasValue || id.Value < 1)
                return View();
            if (!ModelState.IsValid)
                return View(vm);
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return NotFound();
            if (vm.ImageFile != null)
            {
                if (vm.ImageFile.ContentType.StartsWith("image"))
                    ModelState.AddModelError("ImageFile", "File must be image");
                if (vm.ImageFile.Length > 2 * 1024 * 1024)
                    ModelState.AddModelError("ImageFile", "File size must be less than 200kb ");
               
                string newImgName = Guid.NewGuid().ToString() + vm.ImageFile!.FileName;
                string path = Path.Combine("wwwroot", "imgs", "persons", newImgName);
                using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                await vm.ImageFile.CopyToAsync(fs);
                person.ImageUrl = newImgName;
            }
            if (!await _context.Positions.AnyAsync(x => x.Id == vm.PositionId))
            {
                ViewBag.Position = await _context.Positions.ToListAsync();
                ModelState.AddModelError("PositionId", "Position does not exit");
                return View(vm);
            }
           

            person.Name=vm.Name;
            person.PositionId = vm.PositionId;
            person.Description = vm.Description;
            
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();
            if (!id.HasValue || id.Value < 1)
                return View();
            var result=_context.Positions.Where(x=>x.Id==id).ExecuteDeleteAsync();
            if (result==null)
                return NotFound();
            return RedirectToAction("Index");
        }

    }
}

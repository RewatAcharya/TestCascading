using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestCascading.Data;
using TestCascading.Models;

namespace TestCascading.Controllers
{
    public class WardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Wards.Include(x => x.District).ToList() ?? null);
        }


        public async Task<IActionResult> Create()
        {

            List<District> districts = new List<District>();
            districts = await _context.Districts.ToListAsync();

            ViewBag.States = new SelectList(districts, nameof(District.Id), nameof(District.Name));

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ward ward)
        {
            //insert data
            var result = await _context.Wards.AddAsync(ward);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Ward");
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var ward = _context.Wards.FirstOrDefault(x => x.Id == Id);

            //returning the states with their id with name...
            List<District> states = new List<District>();
            states = await _context.Districts.ToListAsync();
            ViewBag.States = new SelectList(states, nameof(District.Id), nameof(District.Name));


            return PartialView(ward);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ward ward)
        {
            var preDist = _context.Wards.FirstOrDefault(x => x.Id == ward.Id);

            if (preDist != null)
            {
                preDist.Name = ward.Name;
                preDist.Description = ward.Description;
                preDist.DistrictFK = ward.DistrictFK;

                _context.Wards.Update(preDist);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Ward");
        }

        public IActionResult Delete(Guid Id)
        {
            var district = _context.Wards.FirstOrDefault(x => x.Id == Id);

            return View(district);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(Guid Id)
        {
            var state = _context.Wards.FirstOrDefault(x => x.Id == Id);
            if (state != null)
            {
                _context.Wards.Remove(state);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

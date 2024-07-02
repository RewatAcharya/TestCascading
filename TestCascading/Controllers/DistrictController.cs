using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestCascading.Data;
using TestCascading.Models;

namespace TestCascading.Controllers
{
    public class DistrictController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Districts.Include(x => x.State).ToList() ?? null);
        }


        public async Task<IActionResult> Create()
        {

            List<State> states = new List<State>();
               states = await _context.States.ToListAsync();

            ViewBag.States = new SelectList(states, nameof(State.Id), nameof(State.Name));

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(District district)
        {
            //insert data
            var result = await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "District");
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var district = _context.Districts.FirstOrDefault(x => x.Id == Id);

            //returning the states with their id with name...
            List<State> states = new List<State>();
            states = await _context.States.ToListAsync();
            ViewBag.States = new SelectList(states, nameof(State.Id), nameof(State.Name));


            return PartialView(district);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(District district)
        {
            var preDist = _context.Districts.FirstOrDefault(x => x.Id == district.Id);

            if (preDist != null)
            {
                preDist.Name = district.Name;
                preDist.Description = district.Description;
                preDist.StateFK = district.StateFK;

                _context.Districts.Update(preDist);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "District");
        }

        public IActionResult Delete(Guid Id)
        {
            var district = _context.Districts.FirstOrDefault(x => x.Id == Id);

            return View(district);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(Guid Id)
        {
            var state = _context.Districts.FirstOrDefault(x => x.Id == Id);
            if (state != null)
            {
                _context.Districts.Remove(state);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

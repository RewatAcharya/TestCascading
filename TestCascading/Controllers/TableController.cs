using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCascading.Data;
using TestCascading.Models;
using TestCascading.Utilities;

namespace TestCascading.Controllers
{
    public class TableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TableController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormOnly()
        {
            return PartialView();
        }

        public IActionResult FormData()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return PartialView(await _context.Cascadings
                .Include(x => x.State)
                .Include(x => x.District)
                .Include(x => x.Ward)
                .ToListAsync() ?? null);
        }

        public async Task<JsonResult> GetDistrict(Guid stateId)
        {
            return Json(await GetUtilityData.GetAllDistricts(stateId));
        }

        public async Task<JsonResult> GetWard(Guid districtId)
        {
            return Json(await GetUtilityData.GetAllWards(districtId));
        }

        [HttpPost]
        public IActionResult SaveData([FromBody] List<CascadingVM> Cascadings)
        {
            var transaction = _context.Database.BeginTransaction();
            foreach (var cascading in Cascadings)
            {
                if (Guid.TryParse(cascading.DistrictId, out Guid districtId) &&
                    Guid.TryParse(cascading.StateId, out Guid stateId) &&
                    Guid.TryParse(cascading.WardId, out Guid wardId))
                {
                    var newData = new Cascading()
                    {
                        DistrictId = districtId,
                        StateId = stateId,
                        WardId = wardId
                    };
                    _context.Cascadings.Add(newData);
                    _context.SaveChanges();

                }
                else
                {
                    transaction.Rollback();
                    return Json("Invalid Data");
                }
            }
            transaction.Commit();
            return RedirectToAction("Index");
        }
    }
}

using InterviewSathi.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Metadata;
using System.Security.Claims;
using TestCascading.Data;
using TestCascading.Models;

namespace TestCascading.Controllers
{
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, string? searchName)
        {
            var result = _context.States.ToList();
            if (searchName != null)
                _ = result.Where(x => (searchName == null || x.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)));
            //int pageSize = 5;
            //var blogsWithUsers = await _context.States.AsNoTracking().ToListAsync();
            //var paginatedBlogs = await PaginatedList<State>.CreateAsync(blogsWithUsers, page ?? 1, pageSize);
            //return View(paginatedBlogs);
            return View(result);
        }

        public async Task<FileContentResult> ExportExcel()
        {
            var states = _context.States.Select(u => new State
            {
                Id = u.Id,
                Name = u.Name,
            }).ToList();

            if (states == null) return null;

            //column Header name
            var columnsHeader = new List<string>{
                    "S/N",
                    "User Name"
                };
            var filecontent = ExportExcell(states, columnsHeader, "Users");
            return File(filecontent, "application/ms-excel", "users.xlsx"); 
        }

        private static byte[] ExportExcell(List<State> data, List<string> columns, string heading)
        {
            byte[] result = null;

            using (ExcelPackage package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add(heading);
                using (var cells = worksheet.Cells[1, 1, 1, 7])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.Green);
                }
                //First add the headers
                for (int i = 0; i < columns.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = columns[i];
                }

                //Add values
                var j = 2;
                var count = 1;
                foreach (var item in data)
                {
                    worksheet.Cells["A" + j].Value = count;
                    worksheet.Cells["B" + j].Value = item.Name;
    
                j++;
                    count++;
                }
                result = package.GetAsByteArray();
            }

            return result;
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(State state)
        {
            //insert data
            var result = await _context.States.AddAsync(state);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "State");
        }

        public IActionResult Edit(Guid Id)
        {
            var state = _context.States.FirstOrDefault(x => x.Id == Id);

            return PartialView(state);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(State state)
        {
            var preState = _context.States.FirstOrDefault(x => x.Id == state.Id);

            if (preState != null)
            {
                preState.Name = state.Name;
                preState.Description = state.Description;

                _context.States.Update(preState);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "State");
        }

        public IActionResult Delete(Guid Id)
        {
            var state = _context.States.FirstOrDefault(x => x.Id == Id);

            return View(state);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(Guid Id)
        {
            var state = _context.States.FirstOrDefault(x => x.Id == Id);
            if (state != null)
            {
                _context.States.Remove(state);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

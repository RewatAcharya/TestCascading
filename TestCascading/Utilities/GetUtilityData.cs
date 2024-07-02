using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mono.TextTemplating;
using TestCascading.Data;
using TestCascading.Models;
using State = TestCascading.Models.State;

namespace TestCascading.Utilities
{
    public static class GetUtilityData 
    {
        private static readonly ApplicationDbContext _context;

        static GetUtilityData()
        {
            _context = new ApplicationDbContext();
        }

        public static async Task<List<SelectListItem>> GetAllStates()
        {
            List<State> data = await _context.States.ToListAsync();
            List<SelectListItem> items = new SelectList(data, nameof(State.Id), nameof(State.Name)).ToList();
            items.Insert(0, (new SelectListItem { Text = "Select State", Value = "" }));
            return items; 
        }

        public static async Task<SelectList> GetAllDistricts(Guid id)
        {
            List<District> districts = await _context.Districts.Where(x => x.StateFK == id).ToListAsync();
            return new SelectList(districts, nameof(District.Id), nameof(District.Name));
        }
        
        public static async Task<SelectList> GetAllWards(Guid id)
        {
            List<Ward> wards = await _context.Wards.Where(x => x.DistrictFK == id).ToListAsync();
            return new SelectList(wards, nameof(Ward.Id), nameof(Ward.Name));
        }
    }
}

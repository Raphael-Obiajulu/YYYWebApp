using Core.DB;
using Core.Models;
using Logic.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class DropdownHelper : IDropdownHelper
    {
        private readonly AppDbContext _context;

        public DropdownHelper(AppDbContext context)
        {
            _context = context;
        }

        public List<CommonDropdown> DropdownOfGender()
        {
            try
            {
                var common = new CommonDropdown()
                {
                    Id = 0,
                    Name = "Select Gender"
                };
                
                var listOfGender= _context.CommonDropdowns.Where(x => x.Id > 0 && x.Active && !x.Deleted).ToList();
                var drp = listOfGender.Select(x => new CommonDropdown
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();


                drp.Insert(0, common);
                return drp;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmasList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChristmasList.Pages
{
    public class UserListModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserListModel> log;

        public UserListModel(ApplicationDbContext dbContext, ILogger<UserListModel> log)
        {
            this.dbContext = dbContext;
            this.log = log;
        }

        public List<DesiredItem> DesiredItems { get; private set; }

        public async Task OnGet(string userEmail)
        {
            log.LogInformation("{curentUser} is getting list for {userEmail}", User?.Identity?.Name ?? "[Unauthenticated]", userEmail);
            //get into database and find DesiredItems for that email
            DesiredItems = await dbContext.DesiredItems
                .Include(di => di.Item)
                .Where(di => di.ChildEmail == userEmail)
                .OrderByDescending(di => di.Item.Price)
                .ToListAsync();
        }
    }
}

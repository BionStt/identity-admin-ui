using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Community.AspNetCore.Identity.AdminUI.Areas.IdentityAdmin.Pages.Users
{
    [IdentityAdminUI(typeof(IndexModel<,>))]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }

    internal class IndexModel<TUser, TRole> : IndexModel
        where TUser : class
        where TRole : class
    {
        
    }
}
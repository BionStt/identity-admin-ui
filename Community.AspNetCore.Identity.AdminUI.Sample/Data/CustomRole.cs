namespace Community.AspNetCore.Identity.AdminUI.Sample.Data
{
    using Microsoft.AspNetCore.Identity;
    public class CustomRole : IdentityRole
    {
        public string CustomRoleField { get; set; }
    }
}
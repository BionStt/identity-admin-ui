namespace Community.AspNetCore.Identity.AdminUI.Sample.Data
{
    using Microsoft.AspNetCore.Identity;
    public class CustomUser : IdentityUser
    {
        public string CustomUserField { get; set; }
    }
}
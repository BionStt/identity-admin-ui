namespace Community.AspNetCore.Identity.AdminUI
{
    using System;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;

    internal class IdentityAdminUIConfigureOptions<TUser,TRole> :
        IPostConfigureOptions<RazorPagesOptions>,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<CookieAuthenticationOptions> 
        where TUser : class 
        where TRole :class
    {
        private const string IdentityUIAdminAreaName = "IdentityAdmin";

        public IdentityAdminUIConfigureOptions(
            IHostingEnvironment environment)
        {
            this.Environment = environment;
        }

        public IHostingEnvironment Environment { get; }

        public void PostConfigure(string name, RazorPagesOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.AllowAreas = true;
            options.Conventions.AuthorizeAreaFolder(IdentityUIAdminAreaName, "/Account/Manage");
            options.Conventions.AuthorizeAreaPage(IdentityUIAdminAreaName, "/Account/Logout");
            var convention = new IdentityPageModelConvention<TUser, TRole>();
            options.Conventions.AddAreaFolderApplicationModelConvention(
                IdentityUIAdminAreaName,
                "/",
                pam => convention.Apply(pam));
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && this.Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? this.Environment.WebRootFileProvider;

            var basePath = "wwwroot";

            // Add our provider
            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, basePath);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }

        public void PostConfigure(string name, CookieAuthenticationOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.Equals(IdentityConstants.ApplicationScheme, name, StringComparison.Ordinal))
            {
                options.LoginPath = $"/{IdentityUIAdminAreaName}/Account/Login";
                options.LogoutPath = $"/{IdentityUIAdminAreaName}/Account/Logout";
                options.AccessDeniedPath = $"/{IdentityUIAdminAreaName}/Account/AccessDenied";
            }
        }
    }
}
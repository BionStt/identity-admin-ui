namespace Community.AspNetCore.Identity.AdminUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;

    public static class ServiceCollectionExtensions
    {
        public static IdentityBuilder AddAdminUI(this IdentityBuilder builder)
        {
            return builder;
        }

        public static IdentityBuilder AddDefaultUI(this IdentityBuilder builder)
        {            
            builder.AddSignInManager();
            ServiceCollectionExtensions.AddRelatedParts(builder);
            OptionsServiceCollectionExtensions.ConfigureOptions(
                builder.Services,
                typeof(IdentityAdminUIConfigureOptions<,>).MakeGenericType(builder.UserType, builder.RoleType));
            return builder;
        }

        private static void AddRelatedParts(IdentityBuilder builder)
        {
            builder.Services.AddMvc().ConfigureApplicationPartManager(
                partManager =>
                    {
                        foreach (ApplicationPart applicationPart in RelatedAssemblyAttribute
                            .GetRelatedAssemblies(typeof(ServiceCollectionExtensions).Assembly, true).SelectMany(
                                CompiledRazorAssemblyApplicationPartFactory.GetDefaultApplicationParts))
                            partManager.ApplicationParts.Add(applicationPart);
                    });
        }
    }
}
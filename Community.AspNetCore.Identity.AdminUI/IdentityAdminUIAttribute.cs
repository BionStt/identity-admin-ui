namespace Community.AspNetCore.Identity.AdminUI
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class IdentityAdminUIAttribute : Attribute
    {
        public IdentityAdminUIAttribute(Type implementationTemplate)
        {
            Template = implementationTemplate;
        }

        public Type Template { get; }
    }
}
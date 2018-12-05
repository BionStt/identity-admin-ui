namespace Community.AspNetCore.Identity.AdminUI
{
    using System;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    internal class IdentityPageModelConvention<TUser,TRole> : IPageApplicationModelConvention 
        where TUser : class
        where TRole : class
    {
        public void Apply(PageApplicationModel model)
        {
            var defaultUIAttribute = model.ModelType.GetCustomAttribute<IdentityAdminUIAttribute>();
            if (defaultUIAttribute == null)
            {
                return;
            }

            ValidateTemplate(defaultUIAttribute.Template);
            var templateInstance = defaultUIAttribute.Template.MakeGenericType(typeof(TUser), typeof(TRole));
            model.ModelType = templateInstance.GetTypeInfo();
        }

        private void ValidateTemplate(Type template)
        {
            if (template.IsAbstract || !template.IsGenericTypeDefinition)
            {
                throw new InvalidOperationException("Implementation type can't be abstract or non generic.");
            }
            var genericArguments = template.GetGenericArguments();
            if (genericArguments.Length != 2)
            {
                throw new InvalidOperationException("Implementation type contains wrong generic arity.");
            }
        }
    }
}
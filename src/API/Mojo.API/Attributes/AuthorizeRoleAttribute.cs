using Microsoft.AspNetCore.Authorization;
using Mojo.Domain.Entities;
using Mojo.Domain.Enums;
using System.Data;

namespace Mojo.API.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params UserRole[] roles)
        {
            //Je transforme la valeur de l'enum en string parce que le midleware compare les valeurs en string 
            Roles = string.Join(",", roles.Cast<int>());
        }
    }
}
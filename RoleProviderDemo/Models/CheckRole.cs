using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleProviderDemo.Models
{
    public static class CheckRole
    {
        public static bool CheckRoleForNormalUser()
        {
            if ((HttpContext.Current.User.IsInRole("normal")) && (HttpContext.Current.User.IsInRole("Normal")))
            {
                return true;
            }
            return false;
        }

        public static bool CheckRoleForAdminUser()
        {
            if ((HttpContext.Current.User.IsInRole("admin")) && (HttpContext.Current.User.IsInRole("Admin")))
            {
                return true;
            }
            return false;
        }

        public static bool CheckRoleForHeadUser()
        {
            if ((HttpContext.Current.User.IsInRole("head")) && (HttpContext.Current.User.IsInRole("Head")))
            {
                return true;
            }
            return false;
        }
    }
}
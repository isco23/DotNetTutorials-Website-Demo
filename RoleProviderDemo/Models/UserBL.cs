using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleProviderDemo.Models
{
    public static class UserBL
    {
        public static List<User> GetUsers()
        {
            // In Realtime you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data
            List<User> userList = new List<User>();            
            userList.Add(new User()
            {
                ID = 102,
                UserName = "NormalUser",
                Password = "normaluser",
                Roles = "Admin,Superadmin",
                Email = "BothUser@a.com",
                RolesName = new string[] { "normal" , "Normal"}
            });
            userList.Add(new User()
            {
                ID = 101,
                UserName = "AdminUser",
                Password = "adminuser",
                Roles = "Admin",
                Email = "Admin@a.com",
                RolesName = new string[] { "admin", "Admin" }
            });
            userList.Add(new User()
            {
                ID = 103,
                UserName = "Headuser",
                Password = "headuser",
                Roles = "Superadmin",
                Email = "Superadmin@a.com",
                RolesName = new string[] { "head", "Head" }
            });
            return userList;
        }
    }
}
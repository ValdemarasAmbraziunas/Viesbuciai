using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace ITPPro.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public CustomPrincipal(string email)
        {
            Identity = new GenericIdentity(email);
        }
        public bool IsInRole(string role)
        {
            if (RoleId != null)
            {
                if (RoleId == 1 && role.Equals("Valdytojas"))
                    return true;
                if (RoleId == 2 && role.Equals("Buhalteris"))
                    return true;
                if (RoleId == 3 && role.Equals("Vadybininkas"))
                    return true;
            }
            return false;
        }


    }
}
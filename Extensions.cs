using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CharityKitchen
{
    public enum AccessLevel { Deny, Read, Write }

    public static class ExtensionObject
    {

        public static AccessLevel GetAccessLevel(this Page p, string role)
        {
            CharityKitchen.CharityKitchenServiceReference.User u = p.Session["user"] as CharityKitchen.CharityKitchenServiceReference.User;
            CharityKitchen.CharityKitchenServiceReference.RoleCombo userRole = (from r in u.Roles
                                  where r.Role.Contains(role)
                                  select r).FirstOrDefault();
            try
            {
                

                switch (userRole.Level)
                {
                    case 1:
                        return AccessLevel.Read;
                    case 2:
                        return AccessLevel.Write;
                    default:
                        return AccessLevel.Deny;
                }
            }
            catch
            {
                return AccessLevel.Deny;
            }
        }
    }
}
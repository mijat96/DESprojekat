using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC
{
    public class RolesConfig
    {
        private static List<string> AdminPermissions = new List<string>()
        {
            Permissions.Delete.ToString(),
            Permissions.Read.ToString(),
            Permissions.Edit.ToString(),
            Permissions.Add.ToString(),
             Permissions.Calculate.ToString()
        };

        private static List<string> ReadPermission = new List<string>()
        {
            Permissions.Read.ToString()
        };

        private static List<string> EditorPermissions = new List<string>()
        {
            Permissions.Read.ToString(),
            Permissions.Edit.ToString()
        };

        private static List<string> NoPermissions = new List<string>();

        /*
        public static List<string> GetPermissions(string role)
        {
            switch (role)
            {
                case "Customer": return PotrosacPermissions;
                case "Operator": return OperatorPermissions;
                case "Admin": return AdminPermissions;
                case "SuperUser": return SuperKorisnikPermissions;
                default: return NoPermissions; // ili null
            }
        }
        */

            /*
        public static List<string> GetPermissions(string role)
        {
            switch (role)
            {
                case "Readers": return PotrosacPermissions;
                case "Modifiers": return OperatorPermissions;
                case "Writers": return AdminPermissions;
                case "Deleters": return AdminPermissions;
                case "Super": return SuperKorisnikPermissions;
                default: return NoPermissions; // ili null
            }
        }
        */
    }
}

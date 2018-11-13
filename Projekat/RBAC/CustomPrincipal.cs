using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace RBAC
{
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity winId = null;
        private List<string> Roles = new List<string>();

        public IIdentity Identity
        {
            get { return this.winId; }
        }

        public CustomPrincipal(IIdentity winIdentity, string Certifikat)
        {
            this.winId = winIdentity;

            //isparsirati ime grupe umseto Rusove zakucane********************************************
            //string imeGrupe =winIdentity.Name;

            List<string> permissions = RBACConfigParser.GetPermissions(Certifikat);

            foreach (string p in permissions)
                Roles.Add(p);


            // ubacimo grupe sa dozvolama
            //foreach (var group in this.winId.Groups)
            //{
            //    SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
            //    var name = sid.Translate(typeof(NTAccount));

            //    // ovde bila greska, nigde veze koja glupost
            //    string groupName = string.Empty;
            //    if (name.ToString().Contains("\\"))
            //        groupName = name.ToString().Split('\\')[1];

            //    if (!Roles.Contains(groupName))
            //    {
            //        Roles.Add(groupName);
            //    }
            //}



        }

        public bool IsInRole(string role)
        {
            foreach (var group in Roles)
            {
                if (Roles.Contains(role))
                    return true;
            }

            return false;
        }
    }
}

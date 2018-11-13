using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RBAC
{
    static class RBACConfigParser
    {
        private static readonly string path = @"C:\Users\Administrator\Desktop\RBAC_citanjeGrupa\RBAC\Vezba_1_2\ServiceApp\rbac_config.xml";

        public static List<string> GetPermissions(string group)
        {
            var list = new List<string>();
            using (var reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name.Equals(group))
                        while (true)
                        {
                            if(reader.Name.Equals("permission"))
                            {
                                reader.Read();
                                if(reader.Value != "\n")
                                {
                                    list.Add(reader.Value);
                                }
                            }
                            else if(reader.NodeType == XmlNodeType.EndElement && reader.Name == group)
                            {
                                break;
                            }
                            else
                            {
                                reader.Read();
                            }
                        }
                }

                //while (reader.Read())
                //{
                //    if (reader.NodeType == XmlNodeType.Element)
                //    {
                //        if (reader.Name == "permission")
                //            list.Add(reader.ReadElementContentAsString());
                //        else
                //            break;
                //    }
                //}
            }

            return list;
        }
    }
}

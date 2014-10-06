using System;
using System.Collections.Generic;
using System.Management;

namespace AWC.WMI
{
    public class WMI
    {
        public static List<List<string>> ProcessList(int inrProID)
        {
            List<List<string>> retList = new List<List<string>>();
            List<string> lProperty;
            string strQuery = "Select * from Win32_Process ";

            if (inrProID > 0)
                strQuery += " WHERE ProcessId = " + inrProID.ToString();

            ManagementObjectSearcher objMS = new ManagementObjectSearcher(strQuery);

            if (objMS != null)
            {
                foreach (ManagementObject objMan in objMS.Get())
                {
                    if (objMan != null)
                    {
                        lProperty = new List<string>();

                        foreach (PropertyData objProp in objMan.Properties)
                        {
                            //lProperty.Clear();
                            if (objProp != null)
                            {
                                lProperty.Add(objProp.Name + ";;" + objProp.Value);
                            }
                        }
                        retList.Add(lProperty);
                    }
                }
                return retList;
            } else
            {
                throw new ArgumentNullException("objMS", "WMI Query Search has a NULL object Query:'" + strQuery + "'");
            }
        }

    }
}

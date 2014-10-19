using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AWC.Save
{
    public static class ConfigFileManager
    {
        private static List<ExternTools.ExternalToolConfig> myExternalConfigs;

        public static bool Save(string strFullFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(strFullFileName))
                {
                    using (XmlWriter writer = XmlWriter.Create(strFullFileName))
                    {
                        writer.WriteStartDocument();

                        //write the ExternalToolConfig list to the Config file
                        writer.WriteStartElement("ExternalToolConfigCollection");
                       
                        if (myExternalConfigs != null && myExternalConfigs.Count > 0)
                        {
                            foreach (ExternTools.ExternalToolConfig _ExToolCon in myExternalConfigs)
                            {
                                if (_ExToolCon != null)
                                {
                                    writer.WriteStartElement("ExternalToolConfig");

                                    writer.WriteElementString("Processname", _ExToolCon.ProcessName);
                                    writer.WriteElementString("Eventtyp", ExternTools.ExternalToolConfig.GetStringEventTypValue(_ExToolCon.ProcessEventTyp));
                                    writer.WriteElementString("Startparameter", _ExToolCon.ProcessStartParameter);
                                    writer.WriteElementString("Enable", _ExToolCon.Enable.ToString());

                                    writer.WriteEndElement();
                                }
                            }
                        }

                        writer.WriteEndElement();


                        writer.WriteEndDocument();

                        return true;
                    }
                } else
                {
                    throw new ArgumentNullException("strFullFileName");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return false;
            }
        }

        public static bool Load(string strFullFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(strFullFileName))
                {
                    using (XmlReader reader = XmlReader.Create(strFullFileName))
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement("ExternalToolConfig"))
                            {

                            }
                        }

                        return true;
                    }
                }
                else
                {
                    throw new ArgumentNullException("strFullFileName");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return false;
            }
        }

    }
}

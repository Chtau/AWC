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
        private static string myConfigFileFullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/AWCConfig.xml";
        private static string myBackupConfigFileFullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/bak_AWCConfig.xml";

        enum BackupTyp
        {
            Create = 0,
            Delete = 1,
            Restore = 2
        }

        /// <summary>
        /// Filename with Path for the Config file
        /// </summary>
        public static string ConfigFileFullName
        {
            get
            {
                return myConfigFileFullName;
            }
        }

        /// <summary>
        /// Collection of the external Tool Configuration
        /// </summary>
        public static List<ExternTools.ExternalToolConfig> ExternalToolConfigs
        {
            get 
            {
                if (myExternalConfigs == null)
                    myExternalConfigs = new List<ExternTools.ExternalToolConfig>();
                return myExternalConfigs; 
            }
            set { myExternalConfigs = value; }
        }

        /// <summary>
        /// Save to a Configuration File
        /// </summary>
        /// <param name="strFullFileName"></param>
        /// <returns></returns>
        public static bool Save(string strFullFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(strFullFileName))
                {
                    BackUpConfigFile(BackupTyp.Create);

                    XmlWriterSettings xmlSettings = new XmlWriterSettings();
                    xmlSettings.NewLineOnAttributes = true;
                    xmlSettings.Indent = true;

                    using (XmlWriter writer = XmlWriter.Create(strFullFileName, xmlSettings))
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
                                    writer.WriteElementString("ExecuteEventtyp", ExternTools.ExternalToolConfig.GetStringEventExecuteTypValue(_ExToolCon.ProcessEventExecuteTyp));
                                    writer.WriteElementString("Startparameter", _ExToolCon.ProcessStartParameter);
                                    writer.WriteElementString("Enable", _ExToolCon.Enable.ToString());

                                    writer.WriteEndElement();
                                }
                            }
                        }

                        writer.WriteEndElement();

                        writer.WriteEndDocument();

                        BackUpConfigFile(BackupTyp.Delete);

                        return true;
                    }
                } else
                {
                    BackUpConfigFile(BackupTyp.Restore);
                    throw new ArgumentNullException("strFullFileName");
                }
            } catch (Exception ex)
            {
                BackUpConfigFile(BackupTyp.Restore);
                Log.cLogger.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// Load form the Configuration File
        /// </summary>
        /// <param name="strFullFileName"></param>
        /// <returns></returns>
        public static bool Load(string strFullFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(strFullFileName) && System.IO.File.Exists(strFullFileName))
                {
                    using (XmlReader reader = XmlReader.Create(strFullFileName))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(reader);

                        if (doc != null && doc.HasChildNodes)
                        {
                            //read the external config collection
                            XmlNode nExConfigs = doc.SelectSingleNode("ExternalToolConfigCollection");
                            if (nExConfigs != null && nExConfigs.HasChildNodes)
                            {
                                XmlNodeList node = nExConfigs.SelectNodes("ExternalToolConfig");

                                if (node != null)
                                {
                                    if (myExternalConfigs == null)
                                    {
                                        myExternalConfigs = new List<ExternTools.ExternalToolConfig>();
                                    }
                                    else
                                    {
                                        myExternalConfigs.Clear();
                                    }

                                    foreach (XmlNode xnodeSingleExternalToolConfig in node)
                                    {
                                        if (xnodeSingleExternalToolConfig != null)
                                        {
                                            if (xnodeSingleExternalToolConfig.HasChildNodes)
                                            {
                                                string strPrc = xnodeSingleExternalToolConfig["Processname"].InnerText;
                                                string strEvTyp = xnodeSingleExternalToolConfig["Eventtyp"].InnerText;
                                                string strExeEvTyp = xnodeSingleExternalToolConfig["ExecuteEventtyp"].InnerText;
                                                string strParam = xnodeSingleExternalToolConfig["Startparameter"].InnerText;
                                                bool bEnable = Convert.ToBoolean(xnodeSingleExternalToolConfig["Enable"].InnerText);
                                                myExternalConfigs.Add(new ExternTools.ExternalToolConfig(strPrc, ExternTools.ExternalToolConfig.GetEnumEventTypValue(strEvTyp), strParam, bEnable, ExternTools.ExternalToolConfig.GetEnumEventExecuteTypValue(strExeEvTyp)));

                                            }
                                        }
                                    }
                                }
                            }

                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(strFullFileName))
                    {
                        throw new ArgumentNullException("strFullFileName");
                    } else
                    {
                        throw new System.IO.FileNotFoundException(string.Format("Can't Load Configuration, File:'{0}' not found", strFullFileName));
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return false;
            }
        }

        private static bool BackUpConfigFile(BackupTyp eBackuptyp)
        {
            try
            {
                if (eBackuptyp == BackupTyp.Create)
                {
                    //create backup file
                    if (System.IO.File.Exists(myConfigFileFullName))
                    {
                        System.IO.File.Copy(myConfigFileFullName, myBackupConfigFileFullName, true);
                        return true;
                    }
                    else
                        return false;
                }
                else if (eBackuptyp == BackupTyp.Delete)
                {
                    //remove backup file
                    if (System.IO.File.Exists(myBackupConfigFileFullName))
                    {
                        System.IO.File.Delete(myBackupConfigFileFullName);
                    }
                    return true;
                }
                else
                {
                    //restore backup file
                    if (System.IO.File.Exists(myBackupConfigFileFullName))
                    {
                        System.IO.File.Copy(myBackupConfigFileFullName, myConfigFileFullName, true);
                        BackUpConfigFile(BackupTyp.Delete);
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return false;
            }
        }

    }
}

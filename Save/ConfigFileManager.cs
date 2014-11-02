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
        private static bool myAutoChecker;
        private static bool myShowDebugWindow;
        private static string myConfigFileFullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/AWCConfig.xml";
        private static string myBackupConfigFileFullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/bak_AWCConfig.xml";

        public static bool ShowDebugWindow
        {
            get { return myShowDebugWindow; }
            set { myShowDebugWindow = value; }
        }

        public static bool AutoChecker
        {
            get { return myAutoChecker; }
            set { myAutoChecker = value; }
        }

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
                        writer.WriteStartElement("AWCConfig");

                        WriteExternalToolConfig(writer);
                        WriteGlobalConfig(writer);

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
                            ReadExternalToolConfig(doc);
                            ReadGlobalConfig(doc);

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

        /// <summary>
        /// handle the backup configuration file while saving changes
        /// </summary>
        /// <param name="eBackuptyp"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Writes the Configuration for the External Tool
        /// </summary>
        /// <param name="_writer">XmlWrite from the Target file</param>
        private static void WriteExternalToolConfig(XmlWriter _writer)
        {
            //write the ExternalToolConfig list to the Config file
            _writer.WriteStartElement("ExternalToolConfigCollection");
            if (myExternalConfigs != null && myExternalConfigs.Count > 0)
            {
                foreach (ExternTools.ExternalToolConfig _ExToolCon in myExternalConfigs)
                {
                    if (_ExToolCon != null)
                    {
                        _writer.WriteStartElement("ExternalToolConfig");

                        _writer.WriteElementString("Processname", _ExToolCon.ProcessName);
                        _writer.WriteElementString("Eventtyp", ExternTools.ExternalToolConfig.GetStringEventTypValue(_ExToolCon.ProcessEventTyp));
                        _writer.WriteElementString("ExecuteEventtyp", ExternTools.ExternalToolConfig.GetStringEventExecuteTypValue(_ExToolCon.ProcessEventExecuteTyp));
                        _writer.WriteElementString("Startparameter", _ExToolCon.ProcessStartParameter);
                        _writer.WriteElementString("Enable", _ExToolCon.Enable.ToString());

                        _writer.WriteEndElement();
                    }
                }
            }
            //Writes if the Autochecker should activitet
            _writer.WriteStartElement("AutoChecker");
            _writer.WriteElementString("Aktiv", myAutoChecker.ToString());
            _writer.WriteEndElement();

            _writer.WriteEndElement();
        }

        private static void WriteGlobalConfig(XmlWriter _writer)
        {
            _writer.WriteStartElement("GlobalConfig");

            _writer.WriteStartElement("Startup");
            _writer.WriteElementString("ShowDebugWindow", myShowDebugWindow.ToString());
            _writer.WriteEndElement();

            _writer.WriteEndElement();
        }

        /// <summary>
        /// Reads the Configuration for the External Tool
        /// </summary>
        /// <param name="_doc">XmlDocument from the Source File</param>
        private static void ReadExternalToolConfig(XmlDocument _doc)
        {
            //read the external config collection
            XmlNode nExConfigs = _doc.SelectSingleNode("AWCConfig");
            if (nExConfigs != null && nExConfigs.HasChildNodes)
            {
                XmlNode nEXToolC = nExConfigs.SelectSingleNode("ExternalToolConfigCollection");

                if (nEXToolC != null && nEXToolC.HasChildNodes)
                {
                    XmlNodeList node = nEXToolC.SelectNodes("ExternalToolConfig");

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

                    XmlNodeList nodeChecker = nEXToolC.SelectNodes("AutoChecker");
                    if (nodeChecker != null)
                    {
                        foreach (XmlNode item in nodeChecker)
                        {
                            myAutoChecker = Convert.ToBoolean(item["Aktiv"].InnerText);
                        }
                    }
                }
            }
        }

        private static void ReadGlobalConfig(XmlDocument _doc)
        {
            XmlNode nExConfigs = _doc.SelectSingleNode("AWCConfig");
            if (nExConfigs != null && nExConfigs.HasChildNodes)
            {
                XmlNode nEXToolC = nExConfigs.SelectSingleNode("GlobalConfig");

                 if (nEXToolC != null && nEXToolC.HasChildNodes)
                 {
                     XmlNodeList nodeChecker = nEXToolC.SelectNodes("Startup");
                     if (nodeChecker != null)
                     {
                         foreach (XmlNode item in nodeChecker)
                         {
                             myShowDebugWindow = Convert.ToBoolean(item["ShowDebugWindow"].InnerText);
                         }
                     }
                 }
            }
        }
    }
}

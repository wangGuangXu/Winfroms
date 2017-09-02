using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace EasyPhoto
{
    class FileInfo
    {
        public string ExtendName;
        public string Description;
        public string IcoPath;
        public string ExePath;
        public FileInfo(string extendname, string icopath, string descripition,string exepath)
        {
            this.ExtendName = extendname;
            this.IcoPath = icopath;
            this.Description = descripition;
            this.ExePath = exepath;
        }
    }

    class FileTypeRegister
    {
        public static void RegisterFileType(FileInfo fileinfo)
        {

            
            string RelationName = fileinfo.ExtendName.Substring(1, fileinfo.ExtendName.Length - 1).ToUpper() + "_File_Type";
            RegistryKey filetypekey = Registry.ClassesRoot.CreateSubKey(fileinfo.ExtendName);
            filetypekey.SetValue("", RelationName);
            filetypekey.Close();

            RegistryKey RelationKey = Registry.ClassesRoot.CreateSubKey(RelationName);
            RelationKey.SetValue("", fileinfo.Description);

            RegistryKey IcoPathKey = RelationKey.CreateSubKey("DefaultIco");
            IcoPathKey.SetValue("", fileinfo.IcoPath);

            RegistryKey shellKey = RelationKey.CreateSubKey("Shell");
            RegistryKey openKey = shellKey.CreateSubKey("Open");
            RegistryKey commandKey = openKey.CreateSubKey("Command");
            commandKey.SetValue("", fileinfo.ExePath + " %1"); 

            RelationKey.Close();
        }
    }
}

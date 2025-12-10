using log4net;
using System.Reflection;
using TnsNamesResolver.Services.Wrappers;

namespace CleanOracleDB
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ////*** Get Assembly name
            //string AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            ////*** Create log filename
            //string LogName = AppName + ".log";
            //string PathLogName;
            //PathLogName = Properties.Settings.Default.LOG_FOLDER;
            //PathLogName = PathLogName.Replace("%USERPROFILE%", System.IO.Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).FullName);
            //PathLogName = PathLogName.Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            ////*** Update log4net properties
            //log4net.GlobalContext.Properties["PathLogName"] = System.IO.Path.Combine(PathLogName, AppName);
            //log4net.GlobalContext.Properties["LogName"] = LogName;
            var logfile = new FileInfo(@"log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(logfile);

            TNSNameResolver.TNSNameResolver tnsNameResolver = new TNSNameResolver.TNSNameResolver();
            if ((!string.IsNullOrEmpty(tnsNameResolver.getLdapAdmin)))
            {
                //*** Vérifie l'emplacement LDAP du tnsnames.ora
                Environment.SetEnvironmentVariable("TNS_ADMIN", tnsNameResolver.getLdapAdmin);

            }
            else if ((!string.IsNullOrEmpty(tnsNameResolver.getTNS)))
            {
                //*** Vérifie l'emplacement fichier du tnsnames.ora
                Environment.SetEnvironmentVariable("TNS_ADMIN", tnsNameResolver.getTNS);

            }
            else
            {
                //*** Si aucun des 2 n'est trouvé, on cherche dans le fichier config.ini les valeurs
                TnsNamesResolver.Services.Wrappers.IniWrapper iniWrapper = new TnsNamesResolver.Services.Wrappers.IniWrapper();
                TnsNamesResolver.Services.Resolvers.TnsAdminIniResolver tnsAdminIniResolver = new TnsNamesResolver.Services.Resolvers.TnsAdminIniResolver(iniWrapper);
                tnsAdminIniResolver.iniFileName = "config.ini";
                tnsAdminIniResolver.iniSection = "TNS_ADMIN";
                tnsAdminIniResolver.iniKey = "TNS_PATH";
                string TNS_ADMIN = tnsAdminIniResolver.GetTnsAdmin();
                if (!string.IsNullOrEmpty(TNS_ADMIN))
                {
                    Environment.SetEnvironmentVariable("TNS_ADMIN", TNS_ADMIN);
                }
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        public static void DisplayErrorMessage(Exception ex) {
            MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
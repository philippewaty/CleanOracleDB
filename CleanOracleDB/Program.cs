using log4net;
using System.Reflection;

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

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            TNSNameResolver.TNSNameResolver tnsNameResolver = new TNSNameResolver.TNSNameResolver();

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
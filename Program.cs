//#define TerminalTest
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace Serial2Socket
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if TerminalTest
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
#else
            ServiceBase[] ServicesToRun;
            Serial2Socket service = new Serial2Socket();
#if true
            //add trace log here
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string logFolder = path + "\\Serial2Socket";
            if (!System.IO.Directory.Exists(logFolder))
            {
                System.Security.AccessControl.DirectorySecurity dirSecurity =
                           new System.Security.AccessControl.DirectorySecurity();
                dirSecurity.AddAccessRule(
                    new System.Security.AccessControl.FileSystemAccessRule("Everyone",
                    System.Security.AccessControl.FileSystemRights.FullControl,
                    System.Security.AccessControl.InheritanceFlags.ContainerInherit |
                    System.Security.AccessControl.InheritanceFlags.ObjectInherit,
                    System.Security.AccessControl.PropagationFlags.None,
                    System.Security.AccessControl.AccessControlType.Allow)
                );
                // Create the new folder with the custom ACL.
                System.IO.Directory.CreateDirectory(logFolder, dirSecurity);
            }
            Trace.Listeners.Add(new TextWriterTraceListener(logFolder + "\\Serial2Socket.log", "myListener"));
            Trace.AutoFlush = true;

            Trace.WriteLine(DateTime.Now + " Path: " + path);
#endif
            if (Environment.UserInteractive)
            {
                ServicesToRun = new ServiceBase[]
                {
                    new Serial2Socket()
                };
                service.RunAsConsole(args);
            }
            else
            {
                ServicesToRun = new ServiceBase[]
                {
                    new Serial2Socket()
                    //service 
                };
                ServiceBase.Run(ServicesToRun);
            }

            ServicesToRun = new ServiceBase[]
            {
                new Serial2Socket()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}

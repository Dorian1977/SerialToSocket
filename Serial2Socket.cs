using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
//using System.Threading;

namespace Serial2Socket
{
    public partial class Serial2Socket : ServiceBase
    {
        SerialPortControl _serialPort;
        string comPort = "COM4";
        string port = "11000";

        TcpTimeServer tcpServer;

        public Serial2Socket()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory +
                          @"Resource\Setting.txt";
            if (!System.IO.Directory.Exists(file))
            {//COM4,10000
                StreamReader sr = new StreamReader(file);
                //Read the first line of text
                string [] data = sr.ReadLine().Split(',');

                comPort = data[0];
                port = data[1];               
            }
            InitializeComponent();
        }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            _serialPort = new SerialPortControl();
            if (!_serialPort.openComPort(comPort))
            {
                Trace.WriteLine(DateTime.Now + " Open com port failed! ");
                return;
            }
            tcpServer = new TcpTimeServer(_serialPort, Convert.ToInt32(port)); //11000         
            StartService(this.ServiceName, 10000);
        }

        protected override void OnStop()
        {
            _serialPort.port_Close();

            tcpServer.stop();

            StopService(this.ServiceName, 10000);
        }

        public static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (InvalidOperationException e)
            {
                // ...
                Trace.WriteLine(DateTime.Now + " Could not start the {0} service.", serviceName);
            }
        }

        public void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
            catch (InvalidOperationException e)
            {
                Trace.WriteLine(DateTime.Now + " Could not stop the {0} service.", serviceName);
            }
        }

        public bool serviceExists(string ServiceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(ServiceName));
        }
    }

}

//#define USE_TCP_SCOKET
//#define USE_UDP
#define USE_TCP
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serial2Socket
{
    public class SerialPortControl
    {
        TcpTimeServer tcpTimeServer = null;

        private SerialPort serialPort = null;
        public SerialPortControl() { }
        public bool openComPort(string _port)
        {
            bool portState = false;
            Trace.WriteLine(DateTime.Now + " init Com Port: " + serialPort);

            serialPort = new SerialPort(_port, 19200, Parity.None, 8, StopBits.One);
            serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            using (SerialPort serialPort = new SerialPort(_port))
            {
                foreach (var itm in SerialPort.GetPortNames())
                {
                    if (itm.Contains(serialPort.PortName))
                    {
                        if (serialPort.IsOpen) { portState = true; }
                        else { portState = false; }
                    }
                }
            }

            try
            {
                if (!portState)
                {
                    serialPort.Open();
                    return true;
                }
                else
                {
                    Trace.WriteLine(DateTime.Now + " Com Port Open failed: " + _port);
                }
            }
            catch (IOException exp)
            {
                Trace.WriteLine(DateTime.Now + " Exception: " + exp);
            }
            return false;
        }

        public bool IsPortOpen()
        {
            return serialPort.IsOpen;
        }

        public void port_Close()
        {
            serialPort.Close();
        }
        /*
        public void port_write(string data)
        {         
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine(data);
            }
        }*/

        public void port_write(byte[] buffer, int count)
        {
            if (serialPort.IsOpen)
            {
                SendDataToInkSystem(buffer, count);
                //port.Write(buffer, offset, count);
            }         
        }

        public bool SendDataToInkSystem(byte[] data, int size)
        {
            serialPort.DiscardInBuffer();

            for (int i = 0; i < (size / 2); i++)
            {
                serialPort.Write(data, i * 2, 2);
                Thread.Sleep(10);
            }
            if (size % 2 != 0)
            {
                serialPort.Write(data, size - 1, 1);
                Thread.Sleep(10);
            }
            return true;
        }

        public void readTCPListener(TcpTimeServer _tcpTimeServer)
        {
            tcpTimeServer = _tcpTimeServer;
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[255];
            int number = serialPort.Read(buffer, 0, 255);
            string message = Encoding.UTF8.GetString(buffer, 0, number);

            if (tcpTimeServer != null)
            {
                tcpTimeServer.sendData(message);

            }

        }
    }
}


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
        
        /******************************************************************
        purpose: default constructor
        parameters:
        ********************************************************************/
        public SerialPortControl() { }
        
        /******************************************************************
        purpose: initial and look through all the com port, return true is 
                 the com port is found and open successfully, otherwise  
                 return false
        parameter:
             Input: com port in string 
             Output:  true for open com port successful
                      false for open com port failed
        ********************************************************************/
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

        /******************************************************************
        purpose: return the com port flag status 
        parameter:
             Input: 
             Output:  true if com port is opened  
                      false if com port is not opened
        ********************************************************************/
        public bool IsPortOpen()
        {
            return serialPort.IsOpen;
        }

        /******************************************************************
        purpose: close the com port 
        parameter:
        ********************************************************************/
        public void port_Close()
        {
            serialPort.Close();
        }
        
        /******************************************************************
        purpose: if com port is opened, send data through the com port 
        parameter:
             Input: buffer - data buffer in bytes like to send out
                    count - number of bytes in the buffer
             Output:
        ********************************************************************/
        public void port_write(byte[] buffer, int count)
        {
            if (serialPort.IsOpen)
            {
                SendDataToInkSystem(buffer, count);
            }         
        }

        /******************************************************************
        purpose: send data to embedded ink device, 2 bytes / once
        parameter:
             Input: data - byte array of data like to send to ink device
                    size - number of bytes in data array
             Output:  always true
        ********************************************************************/
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

        /******************************************************************
        purpose: bypass the exterial TCP server handler to local variable
        parameter:
             Input: _tcpTimeServer: tcp time server handler
             Output: 
        ********************************************************************/
        public void readTCPListener(TcpTimeServer _tcpTimeServer)
        {
            tcpTimeServer = _tcpTimeServer;
        }

        /******************************************************************
        purpose: read the data from the serial port and send them out through
                 TCP port
        parameter:
             Input: sender - the source of the event
                    e - encapsulates any additional information about the event
             Output:  
        ********************************************************************/
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


using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serial2Socket
{
    public class TcpTimeServer
    {
        SerialPortControl _serialPort;
        private Thread n_send_server;
        private TcpListener listener;
        private TcpClient client;
         private int _port;
        byte[] bufferSend = new byte[4096];
        private string _data = "";
        private readonly object _lockObj = new object();

        public TcpTimeServer(SerialPortControl serialPort, int port)
        {
            _port = port;
            _serialPort = serialPort;

            Server();           
        }

        //string strReceiveIPData = "";
        public void Server()
        {                   
            // Buffer for reading data
            Byte[] bytes = new Byte[256];     
            listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            // Enter the listening loop.
            while (true)
            {
                try
                {
                    _serialPort.readTCPListener(this);

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    client = listener.AcceptTcpClient();

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        string strReceiveIPData = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        if (_serialPort != null && _serialPort.IsPortOpen())
                        {
                            _serialPort.port_write(bytes, i);
                        } 
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(DateTime.Now + " TCP/IP Exception: " + ex.Message);
                    listener.Stop();

                    listener = new TcpListener(IPAddress.Any, _port);
                    listener.Start();
                }
            }
            
        }

        public void sendData(string data)
        {
#if true
            if (client != null && client.Connected)
            {
                NetworkStream stream = client.GetStream();
                bufferSend = Encoding.ASCII.GetBytes(data);
                stream.Write(bufferSend, 0, bufferSend.Length);
            }
#else
            _data = data;
            n_send_server = new Thread(new ThreadStart(send));
            n_send_server.IsBackground = true;
            n_send_server.Start();           
#endif
        }
#if false
        private void send()
        {
            try
            {                
                if (client != null)
                {
                    NetworkStream stream = client.GetStream();
                    bufferSend = Encoding.ASCII.GetBytes(_data);
                    stream.Write(bufferSend, 0, bufferSend.Length);
                    Thread.Sleep(10);                 
                    stream.Flush();
                }
            }
            catch (Exception exp) { Trace.WriteLine(DateTime.Now + " SEND exception: " + exp); }
        }
#endif
        public void stop()
        {
            client.Close();
            listener.Stop();
            n_send_server.Abort();
        }
      
    }
}



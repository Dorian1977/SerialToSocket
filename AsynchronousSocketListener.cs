using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serial2Socket
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.  
        public ManualResetEvent allDone = new ManualResetEvent(false);
        SerialPortControl _serialPort;

        private Socket _socket;
        private static IPEndPoint localIpEndPoint;
        private static IPEndPoint remoteIpEndPoint;
        //public static Socket listener;

        public AsynchronousSocketListener(SerialPortControl serialPort, int port)
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //ipAddress = ipHostInfo.AddressList[4];//0 is mac address
            IPAddress ipAddress = IPAddress.Any;
            localIpEndPoint = new IPEndPoint(IPAddress.Any, port);

            // Create a TCP/IP socket.  
            _socket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("socket receive timeout: " + _socket.ReceiveTimeout);
            Console.WriteLine("socket send timeout: " + _socket.SendTimeout);
            _socket.Blocking = false;

            _serialPort = serialPort;
        }

        public void StartListening()
        {
            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                _socket.Bind(localIpEndPoint);
                _socket.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    _socket.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        _socket);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool SocketConnected()
        {
            try
            {
                bool part1 = _socket.Poll(1000, SelectMode.SelectRead);
                bool part2 = (_socket.Available == 0);
                if (part1 && part2)
                    return false;
                else
                    return true;
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp);
                return false;
            }
        }

        public void SocketDisconnect()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);


           // ClientController.AddClient(handler);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;

            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.           
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = 0;
            try
            {                
                // Read data from the client socket.
                bytesRead = handler.EndReceive(ar);
            }
            catch (Exception exp)
            {
                Console.WriteLine("Read port exception " + exp.Message);
                bytesRead = 0;
            }

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                content = state.sb.ToString();
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                  content.Length, content);

                remoteIpEndPoint = (IPEndPoint)handler.RemoteEndPoint;
                string remoteIdEndPointAddress = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
                string remoteIpEndPointPort = ((IPEndPoint)handler.RemoteEndPoint).Port.ToString();
               //localIpEndPoint = handler.LocalEndPoint as IPEndPoint;

                // Using the RemoteEndPoint property.
                Console.WriteLine("I am connected to " + remoteIdEndPointAddress +
                                  " on port number " + remoteIpEndPointPort);

                // Using the LocalEndPoint property.
                Console.WriteLine("My local IpAddress is :" +
                    IPAddress.Parse(((IPEndPoint)handler.LocalEndPoint).Address.ToString()) + 
                    " I am connected on port number " + ((IPEndPoint)handler.LocalEndPoint).Port.ToString());

                Send(handler, " Echo " + content); //test ok
                //Send1(content + " Send back2");

                if (_serialPort != null && _serialPort.IsPortOpen())
                {
                    _serialPort.port_write(content);
                }
                else
                {
                    //Console.WriteLine("COM PORT not open!!!");

                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }

                state.sb.Clear();
                //handler.Shutdown(SocketShutdown.Both);
                // handler.Close();
            }
        }

        public Socket readSocket()
        {
            return _socket;
        }  

        public void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            if (!handler.Connected && remoteIpEndPoint != null)
            {/*
                 IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                 //ipAddress = ipHostInfo.AddressList[4];//0 is mac address
                 IPAddress ipAddress = IPAddress.Parse("192.168.60.21");
                 IPEndPoint sendIpEndPoint = new IPEndPoint(remoteIpEndPoint.Address, remoteIpEndPoint.Port);

                 Console.WriteLine("Send " + data + " to " +
                     remoteIpEndPoint.Address + ":" + (remoteIpEndPoint.Port + 1).ToString());

                 // Create a TCP/IP socket.  
                 Socket sendSocket = new Socket(ipAddress.AddressFamily,
                     SocketType.Stream, ProtocolType.Tcp);                     
                     */
                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }
            else
            {
                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }            
        }

        public void Send(String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            _socket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), _socket);              
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {                
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);                
                Console.WriteLine("Sent {0} byte to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

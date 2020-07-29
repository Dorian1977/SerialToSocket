using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Serial2Socket
{
    public partial class Form1 : Form
    {
        private SerialPort port = new SerialPort("COM4", 19200, Parity.None, 8, StopBits.One);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //port = new SerialPort(listBox1.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.RtsEnable = true;
            port.DtrEnable = true;
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.Open();

            string[] serialPort = SerialPort.GetPortNames();
            for (int i = 0; i < serialPort.Length; i++)
            {
                listBox1.Items.Add(serialPort[i]);
            }
        }

        private delegate void port_DataReceivedUnsafe(object sender, SerialDataReceivedEventArgs e);
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                port_DataReceivedUnsafe Invokeport_DataReceived = new port_DataReceivedUnsafe(port_DataReceived);
                this.Invoke(Invokeport_DataReceived, new object[] { sender, e });
            }
            else
            {
                // Show all the incoming data in the port's buffer
                tbRead.Text += port.ReadExisting();
            }
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            
        }


        public bool SendDataToInkSystem(byte[] data, int size)
        {
            port.DiscardInBuffer();

            for (int i = 0; i < (size / 2); i++)
            {
                port.Write(data, i * 2, 2);
                tbSend.Text += data[i] + " " + data [i+1] + Environment.NewLine;
                Thread.Sleep(10);
            }
            if(size % 2 != 0)
            {
                port.Write(data, size - 1, 1);
                tbSend.Text += data[size-1] + Environment.NewLine;
                Thread.Sleep(10);
            }                
            return true;
        }

        private void btSendWrite_Click(object sender, EventArgs e)
        {
            byte[] data2Send = Encoding.ASCII.GetBytes(tbWrite.Text+"\r");

            if (!port.IsOpen)
            {
                MessageBox.Show("Com port is not open", "Warning");
                return;
            }
            //port.DiscardInBuffer();

#if true
            SendDataToInkSystem(data2Send, data2Send.Length);
#else
            int i = 0;
            while (i < data2Send.Length)
            {
                if (i <= data2Send.Length - 2)
                {
                    //var data = new byte[] { 0, 0 };
                    //data[0] = dataBuffer[i];
                    //data[1] = dataBuffer[i + 1];
                    port.Write(data2Send, i, 2); 
                    
                    tbSend.Text += data2Send[i] + " " + data2Send[i+1] + Environment.NewLine;
                    i += 2;
                }
                else if (i <= data2Send.Length - 1)
                {
                    //var data = new byte[] { 0};
                    //data[0] = dataBuffer[i];
                    port.Write(data2Send, i, 1);
                    tbSend.Text += data2Send[i]+ Environment.NewLine;
                    i++;
                }
                Thread.Sleep(10);
            }    
#endif

        }

        private void btReadFlash_Click(object sender, EventArgs e)
        {
            tbRead.Text = "";
        }

        private void btWriteClear_Click(object sender, EventArgs e)
        {
            tbWrite.Text = "";
        }

        private void btSendClear_Click(object sender, EventArgs e)
        {
            tbSend.Text = "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            port.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                port.Close();
                port = new SerialPort(listBox1.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
                port.Handshake = Handshake.None;
                port.RtsEnable = true;
                port.DtrEnable = true;  
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Open com port got exception, " + exp.Message, "Warning");
            }
        }
    }
}

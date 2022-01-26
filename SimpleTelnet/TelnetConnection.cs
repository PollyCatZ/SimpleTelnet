using System;
using System.Text;
using System.Net.Sockets;

namespace SimpleTelnet
{ 
    public class TelnetConnection
    {
        readonly TcpClient tcpSocket;
        readonly int TimeOutMs = 100;

        public TelnetConnection(string Hostname, int Port)
        {
            tcpSocket = new TcpClient(Hostname, Port);
            System.Threading.Thread.Sleep(2500);
        }

        public string Login(string Username, string Password, int LoginTimeOutMs = 1000)
        {   
            SendCommand(Username);
            System.Threading.Thread.Sleep(LoginTimeOutMs);
            SendCommand(Password);
   
            return "authorized";
        }

        public void SendCommand(string cmd)
        {
            Write(cmd + Environment.NewLine);
            System.Threading.Thread.Sleep(TimeOutMs);         
        }

        public void Write(string cmd)
        {
            if (!tcpSocket.Connected) return;
            byte[] buf = ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }
    }
}

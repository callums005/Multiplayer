using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    internal class Server
    {

        static int PORT = 5000;
        static IPAddress IP_ADDRESS;
        static TcpListener _tcpListener;

        static byte[] buffer = new byte[1024];
        static string? recMsg = null;

        static async void Start()
        {
            IP_ADDRESS = IPAddress.Parse("127.0.0.1");

            _tcpListener = new TcpListener(IP_ADDRESS, PORT);
            _tcpListener.Start();

            using TcpClient client = _tcpListener.AcceptTcpClient();

            NetworkStream tcpStream = client.GetStream();
            int readTotal;

            while ((readTotal = tcpStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                recMsg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }

        static void Main(string[] args)
        {
            Start();
        }
    }
}

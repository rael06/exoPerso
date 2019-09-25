using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communications
{
	class SocketManager
	{
		private const int PORT = 11000;

		private static SocketManager _instance;

		public Socket Socket { get; set; }

		private SocketManager()
		{
			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			var iep = new IPEndPoint(GetHostIPAddress(), PORT);
			Socket.Connect(iep);
		}
		public static SocketManager Instance =>
			_instance ?? (_instance = new SocketManager());

		private IPAddress GetHostIPAddress()
		{
			return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(_ => _.AddressFamily == AddressFamily.InterNetwork);
		}
	}
}

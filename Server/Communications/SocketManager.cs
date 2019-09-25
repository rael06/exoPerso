using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Communications
{
	public class SocketManager
	{
		private const int PORT = 11000;

		private List<ClientManager> _clients = new List<ClientManager>();
		private Socket _socket;

		public SocketManager()
		{
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint iep = new IPEndPoint(GetHostIPAddress() , PORT);
			_socket.Bind(iep);
			_socket.Listen(0);

			while (true)
			{
				var clientSocket = _socket.Accept();
				var client = new ClientManager(clientSocket);
				_clients.Add(client);
			}
		}

		private IPAddress GetHostIPAddress()
		{
			return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(_ => _.AddressFamily == AddressFamily.InterNetwork);
		}
	}
}

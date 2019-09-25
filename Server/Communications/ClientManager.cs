using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Communications
{
	class ClientManager
	{
		private static Socket _clientSocket;
		internal ClientManager(Socket socket)
		{
			_clientSocket = socket;
			Thread listenThread = new Thread(Listen);
			listenThread.Start();
		}

		private void Listen()
		{
			while (true)
			{
				byte[] buffer = new byte[_clientSocket.ReceiveBufferSize];
				int bytesReceived = _clientSocket.Receive(buffer);
				if (bytesReceived > 0)
				{
					var jsonReceived = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
					var communicationReceived = JsonConvert.DeserializeObject<Communication>(jsonReceived);
					Console.WriteLine($"Client communication : {communicationReceived.ToString()}");

					var response = new CommunicationManager(communicationReceived).SetResponse();
					Console.WriteLine($"Server communication : {response.ToString()}");
					var jsonResponse = JsonConvert.SerializeObject(response);
					_clientSocket.Send(Encoding.UTF8.GetBytes(jsonResponse));
				}
			}
		}
	}
}

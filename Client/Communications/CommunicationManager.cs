using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communications
{
	public class CommunicationManager
	{
		#region ServerResponse
		private Communication _serverResponse = new Communication();
		public Communication ServerResponse
		{
			get { return _serverResponse; }
			set
			{
				if (_serverResponse != value)
				{
					_serverResponse = value;
					ServerResponseReceived?.Invoke(this, new PropertyChangedEventArgs(nameof(ServerResponse)));
				}
			}
		}
		#endregion

		public event PropertyChangedEventHandler ServerResponseReceived;

		private static Socket _socket = SocketManager.Instance.Socket;

		//public CommunicationManager ()
		//{
		//	ServerResponseReceived += Test;
		//}

		//private void Test(object sender, PropertyChangedEventArgs e)
		//{
		//	Debug.WriteLine("test");
		//}

		public Communication Communicate(Communication communication)
		{
			var jsonResponse = JsonConvert.SerializeObject(communication);
			_socket.Send(Encoding.UTF8.GetBytes(jsonResponse));
			return GetResponse();
		}

		private Communication GetResponse()
		{
			byte[] buffer = new byte[_socket.ReceiveBufferSize];
			int bytesReceived = _socket.Receive(buffer);
			if (bytesReceived > 0)
			{
				var jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
				ServerResponse = JsonConvert.DeserializeObject<Communication>(jsonResponse);
				return ServerResponse;
			}
			return new Communication();
		}
	}
}

using Common;
using Common.enums;
using Newtonsoft.Json;
using Server.Models;
using Server.Models.Entities;
using Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Communications
{
	class CommunicationManager
	{
		public Communication CommunicationReceived { get; set; }
		public CommunicationManager(Communication communicationReceived)
		{
			CommunicationReceived = communicationReceived;
		}

		internal Communication SetResponse()
		{
			switch (CommunicationReceived.Type)
			{
				case EType.LOGIN:
					var found = StudentRepository.FindByName(CommunicationReceived.Content as string) != null ? true : false;
					return SetCommunication(found);

				case EType.READ:
					switch (CommunicationReceived.Target)
					{
						case ETarget.STUDENT:
							return SetCommunication(StudentRepository.GetAll);

						default: return new Communication();
					}

				case EType.UPDATE:
					switch (CommunicationReceived.Target)
					{
						case ETarget.STUDENT:
							var student = JsonConvert.DeserializeObject<Student>(CommunicationReceived.Content.ToString());
							StudentRepository.Update(student);
							return SetCommunication(StudentRepository.GetAll);

						default: return new Communication();
					}


				default: return new Communication();
			}
		}

		private Communication SetCommunication (dynamic content) =>
			new Communication
			(
				CommunicationReceived.Type,
				CommunicationReceived.Target,
				content,
				CommunicationReceived.Success
			);
	}
}

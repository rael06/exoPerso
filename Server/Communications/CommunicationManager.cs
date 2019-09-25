using Common;
using Common.enums;
using Server.Models;
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
			if (CommunicationReceived.Type == EType.LOGIN)
			{
				var found = StudentRepository.FindByName(CommunicationReceived.Content as string) != null ? true : false;
				return new Communication(
						CommunicationReceived.Type,
						CommunicationReceived.Target,
						found,
						CommunicationReceived.Success
					);
			}

			if (CommunicationReceived.Type == EType.READ)
			{
				if (CommunicationReceived.Target == ETarget.STUDENT)
					return new Communication(
						CommunicationReceived.Type,
						CommunicationReceived.Target,
						StudentRepository.GetAll,
						CommunicationReceived.Success
						);
			}

			return new Communication();
		}
	}
}

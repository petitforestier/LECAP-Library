using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Library.Tools.Session.WCF
{
	public class ClientMessageInspector: IClientMessageInspector
	{

		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
		}

		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			var messageHeader = new MessageHeader<Session>(SessionContext.CurrentSession);
			var unType = messageHeader.GetUntypedHeader("HeaderMessage", "");
			request.Headers.Add(unType);
			return null;
		}
	}
}

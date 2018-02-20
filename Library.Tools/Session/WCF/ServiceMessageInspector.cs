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
using Library.Tools.Session;

namespace Library.Tools.Session.WCF
{
	public class ServiceMessageInspector : IDispatchMessageInspector
	{
		public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			if (request.Headers.FindHeader("HeaderMessage", "") == -1)
				throw new Exception("Accès refusé : L'entête d'authentification est manquant");

			var session = request.Headers.GetHeader<Session>("HeaderMessage", "");
			if (session == null)
				throw new Exception("Accès refusé : L'entête d'authentification est manquant");

			SessionContext.CurrentSession = session;
			SessionContext.CheckToken();

			return null;
		}

		public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
		{
		}
	}
}
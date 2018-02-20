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

namespace Library.HeaderMessage
{
    public class ServiceMessageInspector : IDispatchMessageInspector
	{
		public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			//todo à revoir
			Console.WriteLine("IDispatchMessageInspector.AfterReceiveRequest called.");
			return null;
		}

		public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
		{
		}
	}
}

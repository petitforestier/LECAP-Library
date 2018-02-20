using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.Session.WCF
{
	public class ServiceMessageInspectorBehavior : Attribute, IServiceBehavior
	{
		#region Public METHODS

		public void AddBindingParameters(
		  ServiceDescription description,
		  ServiceHostBase serviceHostBase,
		  System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
		  System.ServiceModel.Channels.BindingParameterCollection parameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
			{
				foreach (var eDispatcher in cDispatcher.Endpoints)
				{
					eDispatcher.DispatchRuntime.MessageInspectors.Add(new ServiceMessageInspector());
				}
			}
		}

		public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		#endregion
	}
}
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
	class ClientMessageBehaviorExtension : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get { return typeof(ClientMessageBehavior); }
		}

		protected override object CreateBehavior()
		{
			return new ClientMessageBehavior();
		}

	}
}

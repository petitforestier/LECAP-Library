using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tools.Tests.Network
{
	[TestClass]
	public class NetworkHelperTest
	{
		#region Public METHODS

		[TestMethod]
		public void Disable_Enable()
		{
			if (!IsInternetConnected()) throw new Exception("Le test ne peut pas être effectué, car réseau déjà inaccessible");

			var networkHelper = new Library.Tools.Network.NetworkHelper();
			networkHelper.Disable();

			if (IsInternetConnected()) throw new Exception();

			networkHelper.Enable();

			if (!IsInternetConnected()) throw new Exception();
		}

		//[TestMethod]
		//public Task DisconnectionTemporaryAsync()
		//{
		//	int disconnectionTimeSecond = 10;

		//	if (!IsInternetConnected()) throw new Exception("Le test ne peut pas être effectué, car réseau déjà inaccessible");

		//	var networkHelper = new Library.Tools.Network.NetworkHelper();
		//	networkHelper.DisconnectionTemporaryAsync(disconnectionTimeSecond);

		//	if (IsInternetConnected()) throw new Exception();

		//	while (networkHelper.IsRunningDisconnecting)
		//	{
		//		System.Threading.Thread.Sleep(1000);
		//	}

		//	if (!IsInternetConnected()) throw new Exception();
		//}

		#endregion Public METHODS

		#region Private METHODS

		private bool IsInternetConnected(Uri iUrl = null)
		{
			try
			{
				if (iUrl == null) iUrl = new Uri("http://www.google.com");
				using (var client = new WebClient())
				using (var stream = client.OpenRead(iUrl))
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		#endregion Private METHODS
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Library.Tools.Tasks;

namespace Library.Tools.Network
{
	public class NetworkHelper
	{
		#region Public PROPERTIES

		public bool IsRunningDisconnecting
		{
			get
			{
				return DisconnectingBoolLock.Value;
			}
		}

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		public NetworkHelper()
		{
			if (!IsAdministrator()) throw new Exception("Visual studio doit être exécuter en mode administrateur");
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		/// <summary>
		/// Désactive toutes les interfaces réseau
		/// </summary>
		public void Disable()
		{
			NetWorkAction(ActionEnum.Disable);
		}

		/// <summary>
		/// Active toutes les interfaces réseau
		/// </summary>
		public void Enable()
		{
			NetWorkAction(ActionEnum.Enable);
		}

		/// <summary>
		/// Déconnect le réseau pour un temps donnée dans un thread différent, pour continuer les tests
		/// </summary>
		public async Task DisconnectionTemporaryAsync(int iTimeSeconds)
		{
			using (new BoolLocker(ref DisconnectingBoolLock))
			{
				Disable();

				Action SleepAction = () =>
					{
						System.Threading.Thread.Sleep(1000 * iTimeSeconds);
					};

				Task sleepTask = Task.Run(() => SleepAction());
				await sleepTask;

				Enable();
			}
		}

		#endregion Public METHODS

		#region Private ENUMS

		private enum ActionEnum
		{
			Enable,
			Disable,
		}

		#endregion Private ENUMS

		#region Private FIELDS

		private BoolLock DisconnectingBoolLock = new BoolLock();

		#endregion Private FIELDS

		#region Private METHODS

		private bool IsAdministrator()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		private void NetWorkAction(ActionEnum iAction)
		{
			List<NetworkAdapter> networkAdapterList = NetworkAdapter.GetAllNetworkAdapter();

			foreach (var networkAdapterItem in networkAdapterList.Enum())
			{
				string operationString = null;
				if (iAction == ActionEnum.Disable)
					operationString = "Disable";
				else if (iAction == ActionEnum.Enable)
					operationString = "Enable";
				else
					throw new Exception("Action non supportée");

				networkAdapterItem.EnableOrDisableNetworkAdapter(operationString);
			}
		}

		#endregion Private METHODS
	}
}
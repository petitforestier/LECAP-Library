namespace Library.Tools.Misc
{
	using System.Diagnostics;
	using System.Reflection;

	public static class Deployement
	{
		#region Public METHODS

		/// <summary>
		/// Retourne la version du déploiement
		/// </summary>
		public static string GetDeployementVersion()
		{
			//Version
			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

			//Text form
			string DeployementVersion;
			if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
			{
				DeployementVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
			}
			else
			{
				DeployementVersion = "Debug";
			}

			return "V " + DeployementVersion;
		}

		#endregion Public METHODS
	}
}
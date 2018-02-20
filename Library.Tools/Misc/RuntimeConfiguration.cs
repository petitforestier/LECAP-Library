namespace Library.Tools.Misc
{
	using Library.Tools.Enums;

	public static class RuntimeConfiguration
	{
		#region Public METHODS

		/// <summary>
		/// Retourne la configuration en cours d'exécution, debug ou release
		/// </summary>
		public static RuntimeConfEnum GetRuntimeConfiguration()
		{
			bool isDebug = false;
#if DEBUG
			isDebug = true;
#endif
			return (isDebug) ? RuntimeConfEnum.Debug : RuntimeConfEnum.Release;
		}

		#endregion Public METHODS
	}
}
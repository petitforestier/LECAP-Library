namespace Library.Tools.Debug
{
	using System;

	using Library.Tools.Tasks;

	public class MyTimer : IDisposable
	{
		#region Public PROPERTIES

		/// <summary>
		/// Retourne le temps de l'opération
		/// </summary>
		public TimeSpan OperationTime
		{
			get
			{
				return DateTime.Now - Start;
			}
		}

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		/// <summary>
		/// Définit un compteur permettant de connaitre le temps d'exécution une partie de code. A utiliser avec un using
		/// </summary>
		public MyTimer(Boolean iIsPrintOnDispose = false, string iName = "", int iRemainingCount = 0)
		{
			Name = iName;
			RemainingCount = iRemainingCount;
			Start = DateTime.Now;
			IsPrintOnDispose = iIsPrintOnDispose;
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Retourne le temps restant d'après le nombre d'itérations restantes en argument
		/// </summary>
		public TimeSpan GetRemainingTime(int iRemainingCount)
		{
			return TimeSpan.FromTicks(iRemainingCount * OperationTime.Ticks);
		}

		#endregion Public METHODS

		#region Protected METHODS

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (RemainingCount == 0)
				{
					if (IsPrintOnDispose)
					{
						MyDebug.PrintInformation(string.Format("Durée : {0} => {1}", Name, OperationTime));
					}
				}
				else
				{
					if (IsPrintOnDispose)
					{
						MyDebug.PrintInformation(string.Format("Durée opération : {0} => {1} // temps restant : {2}", Name, OperationTime, RemainingCount * OperationTime.Ticks));
					}
				}
			}
		}

		#endregion Protected METHODS

		#region Private FIELDS

		private Boolean IsPrintOnDispose;
		private string Name;
		private int RemainingCount;
		private DateTime Start;

		#endregion Private FIELDS
	}
}
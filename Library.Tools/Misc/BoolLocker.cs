using System;

namespace Library.Tools.Misc
{
	public class BoolLocker : IDisposable
	{
		#region Public CONSTRUCTORS

		/// <summary>
		/// Permet de passer le bool à l'opposer de sa valeur par défaut, et remettre la valeur par défaut au dispose. Protège dans le cas d'une lever d'exception
		/// </summary>
		public BoolLocker(ref BoolLock iBoolLock)
		{
			TheBoolLock = iBoolLock;
			TheBoolLock.Value = !TheBoolLock.DefaultValue;
		}

		#endregion Public CONSTRUCTORS

		#region Public METHODS

		public void Dispose()
		{
			TheBoolLock.Value = TheBoolLock.DefaultValue;
		}

		#endregion Public METHODS

		#region Private FIELDS

		private BoolLock TheBoolLock;

		#endregion Private FIELDS
	}
}
namespace Library.Tools.Extensions
{
	using System.Collections.Generic;
	using System.Windows.Forms;

	public static class MyControlCollection
	{
		#region Public METHODS

		/// <summary>
		/// Retourne la liste de tous les controls
		/// </summary>
		public static List<Control> ToFullList(this Control.ControlCollection iCollection)
		{
			if (iCollection == null) return null;

			var result = new List<Control>();
			foreach (var controlItem in iCollection)
			{
				result.Add((Control)controlItem);
			}
			return result;
		}

		/// <summary>
		/// Retourne la liste des controls seulement de type Tresult
		/// </summary>
		public static List<TResult> ToSelectList<TResult>(this Control.ControlCollection iCollection)
		{
			if (iCollection == null) return null;

			var result = new List<TResult>();
			foreach (var controlItem in iCollection)
			{
				if (controlItem is TResult)
					result.Add((TResult)controlItem);
			}
			return result;
		}

		#endregion Public METHODS
	}
}
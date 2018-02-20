
namespace Library.Tools.Misc
{
	public class BoolLock
	{
		#region Public PROPERTIES

		public bool Value { get; set; }
		public bool DefaultValue { get; private set; }

		#endregion Public PROPERTIES

		#region Public CONSTRUCTORS

		/// <summary>
		/// Définie une bool de verrouillage pour être utiliser avec boolLocker. La valeur par défault permet de définir une valeur par défaut à true si nécessaire
		/// </summary>
		public BoolLock(bool iDefaultValue = false)
		{
			DefaultValue = iDefaultValue;
			Value = iDefaultValue;
		}

		#endregion Public CONSTRUCTORS
	}
}
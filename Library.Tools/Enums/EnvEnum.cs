namespace Library.Tools.Enums
{
	using Library.Tools.Attributes;

	public enum EnvEnum
	{
		[Name("FR", "Développement")]
		DevEnv = 1,

		[Name("FR", "Production")]
		PrdEnv = 2,

		[Name("FR", "Qualité")]
		QasEnv = 3,
	}
}
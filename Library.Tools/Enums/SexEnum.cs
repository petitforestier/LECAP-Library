using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace Library.Tools.Enums
{
	public enum SexEnum
	{
		[Name("FR", "Mâle")]
		Male = 1,

		[Name("FR", "Femelle")]
		Female = 2,
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Web
{
	internal class RedirectException : Exception
	{
		#region Public CONSTRUCTORS

		public RedirectException()
			: base()
		{
		}

		#endregion Public CONSTRUCTORS
	}
}
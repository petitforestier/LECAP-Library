using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Enums;

namespace Library.Tools.Session
{
	public class Session
	{
		public long AccountId { get; set; }
		public int SiteId { get; set; }
		//public int AuthorizationLevel { get; set; }
		public string TokenId { get; set; }
		public bool IsTestMode { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HeaderMessage
{
	public class ContextHeaderMessage
	{
		public ContextMessage Context { get; set; }
		public SecurityContext Security { get; set; }
	}

	public class ContextMessage
    {
        public long AccountId {get;set;}
		public int SiteID{get;set;}
    }

	public class SecurityContext
	{
		public string TokenId { get; set; }
	}
}
}

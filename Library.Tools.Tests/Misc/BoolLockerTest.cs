using System;
using Library.Tools.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tools.Tests.Misc
{
	[TestClass]
	public class BoolLockerTest
	{
		[TestMethod]
		public void BoolLocker()
		{
			//false par défaut
			BoolLock theBoolLock = new BoolLock();

			using(new BoolLocker(ref theBoolLock))
			{
				if (theBoolLock.Value == false) throw new Exception();
			}

			if (theBoolLock.Value == true) throw new Exception();

			//true par défaut
			BoolLock theBoolLock2 = new BoolLock(true);

			using (new BoolLocker(ref theBoolLock2))
			{
				if (theBoolLock2.Value == true) throw new Exception();
			}

			if (theBoolLock2.Value == false) throw new Exception();

			//cas d'une exception

			BoolLock theBoolLock3 = new BoolLock();
			try
			{
				using (new BoolLocker(ref theBoolLock3))
				{
					if (theBoolLock3.Value == false) throw new Exception();
					throw new Exception();
				}
			}
			catch { }

			if (theBoolLock3.Value == true) throw new Exception();
		}
	}
}

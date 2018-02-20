using Library.Tools.Comparator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Library.Tools.Tests.Comparator
{
    [TestClass]
    public class ListComparatorTest
    {
        #region Public METHODS

        [TestMethod]
        public void ComparatorTest()
        {
            //class
            var carList = new List<car>();
            carList.Add(new car() { id = "1", power = 2 });
            carList.Add(new car() { id = "3", power = 2 });

            var carList2 = new List<car2>();
            carList2.Add(new car2() { id = "1", power = 2 });
            carList2.Add(new car2() { id = "4", power = 2 });

            var carComparator = new ListComparator<car, car2>(carList, x => x.id, carList2, x => x.id);
            if (carComparator.CommonList.Count != 1) throw new Exception();
            if (carComparator.NewList.Count != 1) throw new Exception();
            if (carComparator.RemovedList.Count != 1) throw new Exception();
            if (carComparator.IsOnlyCommon()) throw new Exception();

            //class avec doublons
            var carList5 = new List<car>();
            carList5.Add(new car() { id = "1", power = 2 });
            carList5.Add(new car() { id = "1", power = 2 });
            carList5.Add(new car() { id = "3", power = 2 });

            var carList6 = new List<car2>();
            carList6.Add(new car2() { id = "1", power = 2 });
            carList6.Add(new car2() { id = "4", power = 2 });

            bool errorRaised = false;
            try
            {
                var carComparator7 = new ListComparator<car, car2>(carList5, x => x.id, carList6, x => x.id);
            }
            catch (Exception)
            {
                errorRaised = true;
            }
            if (errorRaised == false) throw new Exception();

            //enum
            var carList3 = new List<carEnum?>();
            carList3.Add(carEnum.peugeot);
            carList3.Add(carEnum.renault);

            var carList4 = new List<carEnum?>();
            carList4.Add(carEnum.peugeot);
            carList4.Add(carEnum.tata);

            var carComparator2 = new ListComparator<carEnum?, carEnum?>(carList3, x => x, carList4, x => x);
            if (carComparator2.CommonList.Count != 1) throw new Exception();
            if (carComparator2.NewList.Count != 1) throw new Exception();
            if (carComparator2.RemovedList.Count != 1) throw new Exception();
            if (carComparator2.IsOnlyCommon()) throw new Exception();
        }

        #endregion
    }

    internal enum carEnum
    {
        peugeot = 1,
        renault = 2,
        tata = 3,
    }

    internal class car
    {
        #region Public PROPERTIES

        public string id { get; set; }
        public int power { get; set; }

        #endregion
    }

    internal class car2
    {
        #region Public PROPERTIES

        public string id { get; set; }
        public int power { get; set; }
        public int power2 { get; set; }

        #endregion
    }
}
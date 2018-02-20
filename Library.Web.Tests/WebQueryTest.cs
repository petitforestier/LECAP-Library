using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Tasks;
using Library.Web;
using System.Threading.Tasks;
using Library.Tools.Extensions;

namespace Library.Data.Web.Tests
{
    [TestClass]
    public class WebQueryTest
    {

        [TestMethod]
        public async Task GetHtmlSource()
        {
            try
            {
                var urlList = new List<string>();
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-bosch-12-volts-n32648");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-106-avec-collier-n313569");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-bobine-d-allumage-12-volts-beru-zs-172-n315436");
                urlList.Add("http://www.serial-kombi.com/fr-FR/combi-split-2/1950-7/1967-moteur-/-allumage-allumeur-bosch-avec-capsule-de-depression-qualite-d-origine-12-volts-n312584");

                var notifier = new ProgressCancelNotifier();
                var webQuery = new WebQuery2(notifier, false);

               var result = await webQuery.AllCarsInParallelNonBlockingAsync();

               if (result.IsNullOrEmpty()) throw new Exception();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}

using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Web.Tests
{
    [TestClass]
    public class WebQueryTests
    {
        #region Public METHODS

        [TestMethod]
        public async Task GetHtmlSource()
        {
            var url = new Uri(@"https://www.google.fr");
            var webQuery = new WebQuery(new ProgressCancelNotifier(), false, false);
            string sourcehtml = await webQuery.GetSourceAsync(url);
            string sourcehtml2 = webQuery.GetSource(url);
            if (sourcehtml.IsNullOrEmpty()) throw new Exception();
        }

        [TestMethod]
        public async Task GetImage()
        {
            var url = new Uri(@"http://www.performance-parts.fr/424-thickbox_default/bac-a-casques-omp-noir.jpg");
            var webQuery = new WebQuery(new ProgressCancelNotifier(), false, false);
            Image image = await webQuery.GetImageAsync(url);
            Image image2 = webQuery.GetImage(url);
            if (image == null) throw new Exception();
        }

        [TestMethod]
        public void GetImage2()
        {
            var url = new Uri(@"http://www.performance-parts.fr/2402-thickbox_default/adapt-peltor-fmt200.jpg");
            var webQuery = new WebQuery(new ProgressCancelNotifier(), false, false);
            try
            {
                Image image = webQuery.GetImage(url);
                Image image2 = webQuery.GetImage(url);
                if (image == null) throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CleanHtmlExpression()
        {
            string strg1 = "efzefze";
            string strg2 = "zefzefzefez";

            string strg3 = "ATTENTION. LIVRE SANS LA CARTOUCHE<br />il n\'y a plus que l\'embout flexible dans la boite.<br />l\'embout rigide a été supprimé.<br />Attention de le noter car la photo du cata 2013 ne correspond plus";
            string strg3result = "ATTENTION. LIVRE SANS LA CARTOUCHE" + Environment.NewLine + "il n\'y a plus que l\'embout flexible dans la boite." + Environment.NewLine + "l\'embout rigide a été supprimé." + Environment.NewLine + "Attention de le noter car la photo du cata 2013 ne correspond plus";

            string exprWithBreak = strg1 + "<br>" + strg2;
            string exprWithoutBreak = strg1 + strg2;

            if (HtmlTools.CleanHtmlExpression(exprWithBreak, true) == exprWithBreak) throw new Exception();
            if (HtmlTools.CleanHtmlExpression(exprWithBreak, false) != exprWithoutBreak) throw new Exception();
            if (HtmlTools.CleanHtmlExpression(strg3, true) != strg3result) throw new Exception();

            var string4 = "Catalogue Comp&eacute;tition 2015 Fran&ccedil;ais / Espagnol / Anglais";
            var string4Result = "Catalogue Compétition 2015 Français / Espagnol / Anglais";
            if (HtmlTools.CleanHtmlExpression(string4, false) != string4Result) throw new Exception();
        }

        [TestMethod]
        public async Task GetImage3()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(@"http://www.performance-parts.fr/1514-thickbox_default/protection-micro-contre-le-vent-peltor.jpg");
                    var stream = await client.GetStreamAsync(url);
                    var image = Image.FromStream(stream);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public async Task GetImageHideIp()
        {
            var url = new Uri(@"http://www.performance-parts.fr/424-thickbox_default/bac-a-casques-omp-noir.jpg");
            var webQuery = new WebQuery(new ProgressCancelNotifier(), false, false, true);
            Image image = await webQuery.GetImageAsync(url);
            if (image == null) throw new Exception();
        }

        [TestMethod]
        public async Task GetHtmlSourceHideIp()
        {
            var url = new Uri(@"https://www.google.fr");
            var webQuery = new WebQuery(new ProgressCancelNotifier(), false, false, true);
            string sourcehtml = await webQuery.GetSourceAsync(url);
            string sourcehtml2 = webQuery.GetSource(url);
            if (sourcehtml.IsNullOrEmpty()) throw new Exception();
        }

        [TestMethod]
        public async Task GetMyIp()
        {
            var webQuery = new WebQuery(new ProgressCancelNotifier(), false, false);
            var test = await webQuery.GetMyIpAsync();
            Assert.IsFalse(test.StartsWith("192.168"));
        }

        #endregion
    }
}
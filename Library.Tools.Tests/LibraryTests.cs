using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.Drawing;

using System.Web;

using Library;
using Library.Tools.Misc;
using Library. File.MSExcel;
using Library.Extensions;
using Library.Web;

using Library.Debug;


namespace Library.Tests
{
	[TestClass]
	public class LibraryTests
	{
		public int refreshIpFrequency = 50;
		public string proxyAdress = "127.0.0.1";
		public int proxyPort = 8118;
		public string socksAdress = "127.0.0.1";
		public int socksPort = 9051;
		public string socksPassword = "szzfzemapoksbcuozv";
		public WebQueryWithProxy proxyQuery;

		public class Sell
		{
			public string ProductEbayId { get; set; }
			public string Title { get; set; }
			public string Date { get; set; }
			public decimal Price { get; set; }
			public decimal TotalPrice
			{
				get
				{
					return Quantity * Price;
				}
			}
			public decimal RealTotalPrice
			{
				get
				{
					return RealQuantity * Price;
				}
			}
			public string Device { get; set; }
			public int Quantity { get; set; }
			public int RealQuantity { get; set; }
			public string Url { get; set; }
			public string SellerName { get; set; }
		}

		[TestMethod]
		public void WebQuery2()
		{
			//string[] filePaths = Directory.GetFiles(@"D:\Documents\Contact chéreau");

			//Library.Misc.MyFile txtFile = new Misc.MyFile();

			//string txt = string.Empty;
			//foreach(string item in filePaths)
			//{
			//	 txt += txtFile.GetStringFromTextFile(item);
			//}

			//System.IO.File.WriteAllText(@"D:\Téléchargements\contact.txt", txt);
			

		}

		[TestMethod]
		public void GetExcelList()
		{
			AttributeView item1 = new AttributeView() { Name = "re", Value = "ef" };

			List<AttributeView> list = new List<AttributeView>();
			list.Add(item1);
			list.Add(item1);


			var test = list.GetDuplicates(x => x.Name, true);

		}

		[TestMethod]
		public void stringRemoveSub()
		{
			string s1 = "hello";
			string s2 = "hello";

			int test = string.Compare(s1, s2, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace);
			
			
			Dictionary<string,string> dic = new Dictionary<string,string>();



			dic.Add("(avec flexible)","ok");

			//string test = "(avec fléxible) Tube intermediaire";

			//test = test.Replace(dic, StringComparison.CurrentCultu);
		}

		[TestMethod]
		public void stringCommonPrefix()
		{
			//string test = "";

			//MyDebug.PrintInformation(test.GetCommonPrefix(test));

		}

		[TestMethod]
		public void stringRemoveFirstOccurance()
		{
			string  test = "O-IC-801A.J.37";
			string result = test.RemoveFirstOccurence("-");
		}

		private void HttpPostRequest(string url, Dictionary<string, string> postParameters)
		{

			CookieContainer cookieContainer = new CookieContainer();

			string postData = "";

			foreach (string key in postParameters.Keys)
			{
				postData += HttpUtility.UrlEncode(key) + "="
					  + HttpUtility.UrlEncode(postParameters[key]) + "&";
			}

			HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
			myHttpWebRequest.Method = "POST";

			byte[] data = Encoding.ASCII.GetBytes(postData);

			myHttpWebRequest.CookieContainer = cookieContainer;
			myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
			myHttpWebRequest.ContentLength = data.Length;

			Stream requestStream = myHttpWebRequest.GetRequestStream();
			requestStream.Write(data, 0, data.Length);
			requestStream.Close();

			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

			Stream responseStream = myHttpWebResponse.GetResponseStream();

			StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

			string pageContent = myStreamReader.ReadToEnd();

			myStreamReader.Close();
			responseStream.Close();

			myHttpWebResponse.Close();

			string urlAddress = "http://www.serial-kombi.com/fr-FR/accessoire-entretien-et-cadeau-accessoire-interieur-lit-de-cabine-pour-enfant-pliant-t5-4/2003-n314639";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
			request.KeepAlive = true;
			request.CookieContainer = cookieContainer;
			HttpWebResponse response2 = (HttpWebResponse)request.GetResponse();
			response2.Close();

		}

		//[TestMethod]
		//public void currencyTest()
		//{
		//	string test = "ef";

		//	MyDebug.PrintInformation("efe");



		//}

		[TestMethod]
		public void picture()
		{
			string url = @"http://localhost/D:\Drive\Projets\eSaphir\Données\2-Fournisseur\Performance Parts\Photos inox car\ABARTH\AF500 Abarth 500 (print).jpg";

			Image theImage = Library.File.Picture.PictureTools.GetImageFromUrl(url);

			//MyDebug.PrintInformation(Library.Culture.LangEnum.FR.Object.LangId);
		}

		[TestMethod]
		public void keepString()
		{
			//MyDebug.PrintInformation(Library.Culture.LangEnum.FR.Object.LangId);


		}
		
		public static IsVisibleAttribute[] Read<T>(Expression<Func<T>> func)
		{
			var member = func.Body as MemberExpression;
			if (member == null) throw new ArgumentException("Lambda must resolve to a member");
			return (IsVisibleAttribute[])Attribute.GetCustomAttributes( member.Member, typeof(IsVisibleAttribute), false);
		}

		[TestMethod]
		public void redirectTest()
		{
			//string url = @"http://www.gt2i.com/fr/omp/48826-cable-omp-ja-800-ja-836-=-ja-849-ja-850.html";

			Uri url = new Uri("http://www.akrapovic.com/api/en-US/products/motorcycle?$inlinecount=allpages&BrandId=44&top=100 ");

			var web = new Library.Web.WebQuerySimple();

			string test = web.GetHtmlSource(url);


			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			  webRequest.AllowAutoRedirect = false;  // IMPORTANT
			  webRequest.Timeout = 10000;           // timeout 10s

			  // Get the response ...
			  using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
			  {   
				 // Now look to see if it's a redirect
				 if ((int)webResponse.StatusCode >= 300 && (int)webResponse.StatusCode <= 399)
				 {
				   string uriString = webResponse.Headers["Location"];
				   Console.WriteLine("Redirect to " + uriString ?? "NULL");
				   // webResponse.Close(); // don't forget to close it - or bad things happen!
										   // The using() clause will ensure this happens
		
				 }
			   }
		}
	}

	public class IsVisibleAttribute : Attribute
	{
		public IsVisibleAttribute(bool value)
		{
			this.IsVisible = value;
		}

		public bool IsVisible { get; protected set; }
	}

	public class AttributeView
	{
		[IsVisible(true)]
		public string Name { get; set; }

		[IsVisible(true)]
		public string Value { get; set; }
	}


}

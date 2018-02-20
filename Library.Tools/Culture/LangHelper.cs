using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Extensions;

namespace Library.Tools.Culture
{
    public static class LangHelper
    {
        public static string RemoveUselessWords(string iText,string iLang)
        {
            if (iText.IsNullOrEmpty()) return iText;
            if (iLang.IsNullOrEmpty()) throw new ArgumentNullException();

            if(iLang.ToLower() == "fr")
            {
                var wordlist = new List<string>();
                //Mots décollés
                wordlist.Add("le");
                wordlist.Add("la");
                wordlist.Add("les");
                wordlist.Add("un");
                wordlist.Add("une");
                wordlist.Add("des");
                wordlist.Add("au");
                wordlist.Add("à la");
                wordlist.Add("à");
                wordlist.Add("aux");
                wordlist.Add("du");
                wordlist.Add("de");
                wordlist.Add("avec");
                wordlist.Add("en");

                foreach (var item in wordlist)
                    iText = iText.ReplaceWord(item, "", " ",true,false, true);

                //Mots collés
                var startWordlist = new List<string>();
                startWordlist.Add("l'");
                startWordlist.Add("s'");
                startWordlist.Add("j'");
                startWordlist.Add("m'");
                startWordlist.Add("n'");
                startWordlist.Add("d'");

                foreach (var item in startWordlist)
                    iText = iText.ReplaceWord(item, "", " ", false, false, true);
            }
            else
            {
                throw new NotSupportedException();
            }

            return iText.CleanSpaces();
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Tools.Tests;

namespace Library.Tools.Tests.Lang
{
    [TestClass]
    public class LangHelperTests
    {
        [TestMethod]
        public void RemoveUselessWords()
        {
            string text = "Bonnet à côtes l'doublé en polaire l'Serial-Kombi couleur Bordeaux avec broderie logo SK";
            string result = "Bonnet côtes doublé polaire Serial-Kombi couleur Bordeaux broderie logo SK";

            if (Library.Tools.Culture.LangHelper.RemoveUselessWords(text, "FR") != result) throw new Exception();
            

        }
    }
}

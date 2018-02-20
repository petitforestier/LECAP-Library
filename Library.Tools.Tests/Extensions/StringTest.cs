using Library.Tools.Debug;
using Library.Tools.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests
{
    [TestClass]
    public class StringTest
    {
        #region Public METHODS

        [TestMethod]
        public void Reduce()
        {
            if ("tesp".Reduce(2) != "te")
                throw new Exception();
        }

        [TestMethod]
        public void AddKeyWords()
        {
            //normal
            string text = "Robe Légère décolletée noire";

            var keyList = new List<string>();
            keyList.Add("sexy noire");
            keyList.Add("robe");

            text = text.AddKeyWords(keyList, Tools.Enums.PositionEnum.End);

            if (text != "Robe Légère décolletée noire sexy") throw new Exception();

            //liste vide
            string text2 = "text";
            var keyList2 = new List<string>();
            text2 = text2.AddKeyWords(keyList2, Tools.Enums.PositionEnum.End);
            if (text2 != "text") throw new Exception();

            //longueur maximale
            string text3 = "Robe Légère";
            var keyList3 = new List<string>() { "cc", "bbbbbbbbbbbbbbbbbbbb", "aa" };
            text3 = text3.AddKeyWords(keyList3, Tools.Enums.PositionEnum.End, 20);

            if (text3 != "Robe Légère cc aa") throw new Exception();
        }

        [TestMethod]
        public void ReplaceStart()
        {
            if ("Le chat".ReplaceStart("le ", "", false, false) != "chat") throw new Exception();
            if ("lé chat".ReplaceStart("le ", "", false, false) != "chat") throw new Exception();
            if ("LE chat".ReplaceStart("le ", "", false, false) != "chat") throw new Exception();
            if ("Lechat".ReplaceStart("le ", "", false, false) != "Lechat") throw new Exception();
            if ("chat le".ReplaceStart("le ", "", false, false) != "chat le") throw new Exception();
        }

        [TestMethod]
        public void ReplaceEnd()
        {
            if ("Le chat".ReplaceEnd("le ", "", false, false) != "Le chat") throw new Exception();
            if ("le chat".ReplaceEnd("le ", "", false, false) != "le chat") throw new Exception();
            if ("LE chat".ReplaceEnd("le ", "", false, false) != "LE chat") throw new Exception();
            if ("Lechat".ReplaceEnd("le ", "", false, false) != "Lechat") throw new Exception();

            if ("le chat".ReplaceEnd(" chat", "", false, false) != "le") throw new Exception();
            if ("le CHAT".ReplaceEnd(" chat", "", false, false) != "le") throw new Exception();
            if ("LE chät".ReplaceEnd(" chat", "", false, false) != "LE") throw new Exception();
            if ("Lechat".ReplaceEnd(" chat", "", false, false) != "Lechat") throw new Exception();

            if ("Lechat".ReplaceEnd(" chat", "", false, false) != "Lechat") throw new Exception();
            if ("Lechat chat chat chat".ReplaceEnd(" chat", "", false, false) != "Lechat chat chat") throw new Exception();
        }

        [TestMethod]
        public void ReplaceWord()
        {
            if ("Le gros chat noir".ReplaceWord("noir", "", " ", true, false, false) != "Le gros chat") throw new Exception();
            if ("Le gros chat noir".ReplaceWord("le", "", " ", true, false, false) != "gros chat noir") throw new Exception();
            if ("Le gros chät noir".ReplaceWord("CHAT", "", " ", true, false, false) != "Le gros noir") throw new Exception();
            if ("L'gros chät noir".ReplaceWord("l'", "", " ", true, false, false) != "L'gros chät noir") throw new Exception();

            if ("L'chat noir".ReplaceWord("l'", "", " ", false, false, false) != "chat noir") throw new Exception();
            if ("Le gros chat l'noir".ReplaceWord("l'", "", " ", false, false, false) != "Le gros chat noir") throw new Exception();
        }

        [TestMethod]
        public void SplitByOpenCloseChar()
        {
            string text = "zadazd{\"attributes_values\":{\"1\":Gris\"}},{act\":\"0.00\",minimal_quantit}efaefad";
            var list = text.SplitByOpenCloseChar('{', '}', false);
            if (list[0] != "\"attributes_values\":{\"1\":Gris\"}") throw new Exception();
            if (list[1] != "act\":\"0.00\",minimal_quantit") throw new Exception();

            string text2 = "za}dazd{\"attributes_values\":{\"1\":Gris\"}},{act\":\"0.00\",minimal_quantit}efaefad";
            try
            {
                var list2 = text2.SplitByOpenCloseChar('{', '}', false);
                throw new Exception();
            }
            catch
            {
            }

            string text3 = "zadazd{\"attributes_values\":{\"1\":Gris\"}},var combinations={act\":\"0.00\",minimal_quantit}efaefad";
            var list3 = text3.SplitByOpenCloseChar('{', '}', false, ",var combinations=");
            if (list3.Single() != "act\":\"0.00\",minimal_quantit") throw new Exception();

            string text4 = "zadazd{\"attributes_values\":{\"1\":Gris\"}},var combinations={act\":\"0.00\",minimal_quantit}efaefad";
            var list4 = text4.SplitByOpenCloseChar('{', '}', true, "zadazd{");
            if (list4.Single() != "\"attributes_values\":{\"1\":Gris\"}") throw new Exception();
        }

        [TestMethod]
        public void SplitWithEchappementChar2()
        {
            string text = "zadazd{\"attributes_values,,\":{\"1\":Gris\"}},dezd{act\":\"0.00\",minimal_quantit},efaefad";
            var list = text.SplitWithEchappementChar('{', '}',',');

            if (list[0] != "zadazd{\"attributes_values,,\":{\"1\":Gris\"}}") throw new Exception();
            if (list[1] != "dezd{act\":\"0.00\",minimal_quantit}") throw new Exception();
            if (list[2] != "efaefad") throw new Exception();

        }

        [TestMethod]
        public void GetSubStringList()
        {
            var text1 = "(cezd)(efzezezf)";

            var result = text1.GetSubStringList("(", ")");
            if (result[0] != "cezd") throw new Exception();
            if (result[1] != "efzezezf") throw new Exception();

            var result2 = text1.GetSubStringList("(", "]");
        }


        [TestMethod]
        public void SplitWithEchappementChar()
        {
            string text = "\"Taille,\":4,\"Couleur\":14";
            var list = text.SplitWithEchappementChar('\"', ',');

            if (list[0] != "\"Taille,\":4") throw new Exception();
            if (list[1] != "\"Couleur\":14") throw new Exception();
           
        }

        [TestMethod]
        public void EmptyIfNull()
        {
            string test = null;
            Assert.IsTrue(test.EmptyIfNull() == string.Empty);
        }
        

        #endregion
    }
}
namespace Library.Tools.Extensions
{
    using Library.Tools.Enums;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class MyString
    {
        #region Public METHODS

        /// <summary>
        /// Ajout le message au string avec un retour à la ligne avant si besoin. Fonctionne même si le string est vide
        /// </summary>
        public static string AddLine(this string iString, string iMessage)
        {
            if (iMessage.IsNullOrEmpty())
                return iString;

            if (iString == null)
                iString = string.Empty;

            if (iString != string.Empty)
                iString += Environment.NewLine;

            iString += iMessage;
            return iString;
        }

        /// <summary>
        /// Ajout un saut de ligne vide
        /// </summary>
        public static string AddEmptyLine(this string iString)
        {
            if (iString == null)
                iString = string.Empty;

            iString += Environment.NewLine;

            return iString;
        }

        /// <summary>
        /// Supprime les sauts de ligne et les tabulations
        /// </summary>
        public static string CleanBreaks(this string iText)
        {
            return iText.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        /// <summary>
        /// Ne conserve que les espaces seuls, supprime les espaces aux extrémités, supprime les sauts de lignes
        /// </summary>
        public static string CleanSpaces(this string iText)
        {
            return Regex.Replace(iText, @"\s+", " ").Trim();
        }

        /// <summary>
        /// Ne conserve que les espaces seuls, supprime les espaces aux extrémités, supprime les espaces après un retour à la ligne, peut ou non supprimer les lignes vides
        /// </summary>
        public static string CleanSpaces(this string iText, bool iKeepEmptyLines)
        {
            string result = string.Empty;

            //espace multiple
            string firstPass = iText;
            while (firstPass.Contains("  ")) firstPass = firstPass.Replace("  ", " ");

            if (firstPass.Contains(Environment.NewLine))
            {
                //bouclage sur les lignes
                foreach (var lineItem in firstPass.Split(Environment.NewLine))
                {
                    if (iKeepEmptyLines == false && lineItem.Trim().IsNullOrEmpty())
                        continue;

                    if (result.IsNotNullAndNotEmpty())
                        result += Environment.NewLine;

                    result += lineItem.Trim();
                }
            }
            else
            {
                result = firstPass.Trim();
            }
            return result;
        }

        /// <summary>
        /// Retourne si le texte contient la valeur sans tenir compte des majuscules et des accents
        /// </summary>
        public static bool ContainsIgnoringAccentsCapital(this string iText, string iValeur)
        {
            string theText = iText.RemoveDiacritics().ToLower();
            string theValue = iValeur.RemoveDiacritics().ToLower();

            if (theText.Contains(theValue))
                return true;

            return false;
        }

        /// <summary>
        /// Retourne si le texte contient la valeur sans tenir compte des majuscules, des espaces et des accents
        /// </summary>
        public static bool ContainsIgnoringSpacesAccentsCapital(this string iText, string iValeur)
        {
            string theText = iText.RemoveDiacritics().ToLower().Replace(" ", "");
            string theValue = iValeur.RemoveDiacritics().ToLower().Replace(" ", "");

            if (theText.Contains(theValue))
                return true;

            return false;
        }

        /// <summary>
        /// Retourne une valeur qui indique si un des mots de la phrase est présent dans le text sans tenir compte des majuscules et des accents
        /// </summary>
        public static bool ContainsWords(this string iText, string iSentence)
        {
            string theText = iText.RemoveDiacritics().ToLower();
            string flatSentence = iSentence.RemoveDiacritics().ToLower();

            if (theText.Contains(flatSentence))
                return true;

            foreach (string item in iText.Split(" "))
            {
                if (flatSentence.Contains(item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Détermine l'égalité de deux chaines en supprimant les espaces double et sans tenir compte des accents et des majuscules
        /// </summary>
        public static bool Equal(this string iText, string iCompareText)
        {
            if (iText == null && iCompareText == null)
            {
                return true;
            }
            else if ((iText != null && iCompareText == null) ||
                (iText == null && iCompareText != null))
            {
                return false;
            }
            else
            {
                iText = iText.CleanSpaces();
                iCompareText = iCompareText.CleanSpaces();

                if (iText.ToLower().RemoveDiacritics() == iCompareText.ToLower().RemoveDiacritics())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Equivalent à string.format, plus rapide à écrire
        /// </summary>
        public static string FormatString(this string iString, params object[] args)
        {
            return string.Format(iString, args);
        }

        /// <summary>
        /// Retourne le début commun entre deux string
        /// </summary>
        public static string GetCommonPrefix(this string iPrimaryText, string iSecondaryText, bool iIsCaseSensitive = false)
        {
            string shortestString;
            string otherString;
            if (iPrimaryText.Length < iSecondaryText.Length)
            {
                shortestString = iPrimaryText;
                otherString = iSecondaryText;
            }
            else
            {
                shortestString = iSecondaryText;
                otherString = iPrimaryText;
            }

            int count = 0;
            foreach (char c in shortestString)
            {
                string first = (iIsCaseSensitive) ? otherString.Substring(count, 1) : otherString.Substring(count, 1).ToUpper();
                string second = (iIsCaseSensitive) ? c.ToString() : c.ToString().ToUpper();

                if (first != second)
                {
                    if (count == 0) { return null; };
                    return shortestString.Substring(0, count - 1);
                }
                count++;
            }
            return shortestString;
        }

        /// <summary>
        /// Retourne la valeur de l'enum depuis la valeur string
        /// </summary>
        public static T GetEnum<T>(this string iString) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), iString);
        }

        /// <summary>
        /// Retourne une partie d'une chaine de caractère extraite d'un string, en spécifiant le string juste avant et le string juste après
        /// </summary>
        public static List<string> GetSubStringList(this string iText, string iStartString, string iEndString)
        {
            List<string> resultList = new List<string>();
            string text = iText;
            while (true == true)
            {
                int start = text.IndexOf(iStartString);
                if (start != -1)
                {
                    start = start + +iStartString.Length;
                    int lenght = text.Substring(start, text.Length - start).IndexOf(iEndString);
                    if (lenght != -1)
                    {
                        resultList.Add(text.Substring(start, lenght));
                        text = text.Substring(start + lenght + iEndString.Length, text.Length - start - lenght - iEndString.Length);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return resultList;
        }

        /// <summary>
        /// Retourne un string avec la séquence des valeurs du dictionnaire si la clé est présentes dans le text en paramètre. Ne tient pas compte des majuscule et des accents
        /// </summary>
        public static string GetTags(this string iText, Dictionary<string, string> iTagMatchDic)
        {
            string result = "";
            foreach (KeyValuePair<string, string> item in iTagMatchDic)
            {
                if (iText.ContainsIgnoringAccentsCapital(item.Key))
                {
                    result += item.Value + " ";
                }
            }
            return result.Trim();
        }

        /// <summary>
        /// Retourne sur le string n'est pas null et pas vide
        /// </summary>
        public static bool IsNotNullAndNotEmpty(this string iText)
        {
            if (iText != null && iText != string.Empty)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retourne si le string est null ou vide
        /// </summary>
        public static bool IsNullOrEmpty(this string iText)
        {
            return !iText.IsNotNullAndNotEmpty();
        }

        /// <summary>
        /// Retourne sur le string est un nombre
        /// </summary>
        public static bool IsNumeric(this string iString)
        {
            float output;
            return float.TryParse(iString, out output);
        }

        /// <summary>
        /// Retourne sur le string n'est pas un nombre
        /// </summary>
        public static bool IsNotNumeric(this string iString)
        {
            return !IsNumeric(iString);
        }

        /// <summary>
        /// Retourne sur le string est un entier
        /// </summary>
        public static bool IsInteger(this string iString)
        {
            Int64 output;
            return Int64.TryParse(iString, out output);
        }

        /// <summary>
        /// Retourne sur le string n'est pas un entier
        /// </summary>
        public static bool IsNotInteger(this string iString)
        {
            return !IsInteger(iString);
        }

        /// <summary>
        /// Retourne la chaine de caractéres identique entre deux chaines de caractères
        /// </summary>
        public static string KeepIdentiqueParts(this string iFirstText, string iOriginalText, string iSeparator)
        {
            if (iOriginalText == iSeparator) throw new ArgumentException("Le texte original et le séparateur ne peuvent être identiques");

            if (iFirstText == null || iFirstText == string.Empty) return iFirstText;

            if (iOriginalText == null || iOriginalText == string.Empty) return iFirstText;

            string result = string.Empty;
            string restOriginalText = iOriginalText;
            foreach (string stringItem in iFirstText.Split(iSeparator).Enum())
            {
                if (iOriginalText.Contains(stringItem))
                    result += iSeparator + stringItem;
            }
            return result;
        }

        /// <summary>
        /// Détermine la non égalité de deux chaines en supprimant les espaces double et sans tenir compte des accents et des majuscules
        /// </summary>
        public static bool NotEqual(this string iText, string iCompareText)
        {
            return !iText.Equal(iCompareText);
        }

        /// <summary>
        /// Limite un string à un nombre de caractères, attention peut couper un mot en plein milieu
        /// </summary>
        public static string Reduce(this string iText, int iMaxLenght)
        {
            if (iMaxLenght < iText.Length)
            {
                return iText.Substring(0, iMaxLenght);
            }
            return iText;
        }

        /// <summary>
        /// Limite un string à un nombre de caractères sans couper un mot en plein milieu. Début et fin d'un mot défini par le séparateur
        /// </summary>
        public static string Reduce(this string iText, int iMaxLenght, string iSeparator)
        {
            while (iText.Length > iMaxLenght)
            {
                iText = iText.RemoveLastOccurence(iSeparator);
            }
            return iText;
        }

        /// <summary>
        /// Limite un string à un nombre de caractères sans couper un mot en plein milieu. Début et fin d'un mot défini par le séparateur et ajout un string à la fin en tenant compte de la longueur de celui ci
        /// </summary>
        public static string Reduce(this string iText, int iMaxLenght, string iSeparator, string iAddEndString)
        {
            while (iText.Length + iAddEndString.Length > iMaxLenght)
            {
                iText = iText.RemoveLastOccurence(iSeparator) + iAddEndString;
            }
            return iText + iAddEndString;
        }

        /// <summary>
        /// Retourne l'expression d'entré sans les accents
        /// </summary>
        public static string RemoveDiacritics(this string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Retourne la chaine de caractères sans le premier string situé avant le premiere separateur
        /// </summary>
        public static string RemoveFirstOccurence(this string iText, string iSeparator)
        {
            if (iText.IsNullOrEmpty() || iSeparator == null || iText.Contains(iSeparator) == false) return iText;

            string sequenceToRemoved = iText.Split(iSeparator).First() + iSeparator;

            return iText.Substring(sequenceToRemoved.Length);
        }

        /// <summary>
        /// Suppression de la première occurence d'un extrait
        /// </summary>
        public static string RemoveFirstSubString(this string iText, string iStartString, string iEndString, bool iIsKeepStartEndString)
        {
            List<string> subList = iText.GetSubStringList(iStartString, iEndString);

            if (subList.IsNotNullAndNotEmpty())
            {
                string subString = (iIsKeepStartEndString == true) ? subList[0] : iStartString + subList[0] + iEndString;

                int index = iText.IndexOf(subString);
                string cleanPath = (index < 0)
                    ? iText
                    : iText.Remove(index, subString.Length);
                return cleanPath;
            }
            return iText;
        }

        /// <summary>
        /// Retourne un string sans la séquence situé après le string en argument
        /// </summary>
        public static string RemoveFrom(this string iText, string iStartString)
        {
            int searchIndex = iText.IndexOf(iStartString);
            return (searchIndex == -1) ? iText : iText.Substring(0, searchIndex);
        }

        /// <summary>
        /// Retourne la chaine de caractères sans le dernier string et dernier séparateur
        /// </summary>
        public static string RemoveLastOccurence(this string iText, string iSeparator)
        {
            if (iSeparator.IsNullOrEmpty() || !iText.Contains(iSeparator)) return iText;

            if (iText.Substring(iText.Length - iSeparator.Length) == iSeparator)
            {
                iText = iText.Remove(iText.Length - iSeparator.Length);
            }
            string sequenceToRemoved = iText.Split(iSeparator).Last();

            if (sequenceToRemoved == null || sequenceToRemoved == string.Empty)
            {
                return iText.Remove(iText.Length - iSeparator.Length);
            }
            else
            {
                return iText.Remove(iText.Length - (sequenceToRemoved.Length + iSeparator.Length));
            }
        }

        /// <summary>
        /// Supprime tous les extraits contenue dans une chaine de caractères
        /// </summary>
        public static string RemoveSubString(this string iText, string iStartString, string iEndString, bool iIsKeepStartEndString, int iMaxLenght = 0)
        {
            while (true == true)
            {
                string cleanedExpression = iText.RemoveFirstSubString(iStartString, iEndString, iIsKeepStartEndString);
                if (cleanedExpression != iText)
                {
                    iText = cleanedExpression;
                }
                else
                {
                    break;
                }

                if (iMaxLenght != 0)
                {
                    if (iText.Length <= iMaxLenght)
                    {
                        break;
                    }
                }
            }
            return iText;
        }

        /// <summary>
        /// Retourne un string avec les clé du dictionnaire remplacé par les valeurs du dictionnaire
        /// </summary>
        public static string Replace(this string iText, Dictionary<string, string> iReplaceRuleList, bool iCaseSensible, bool iDiacriticSensible)
        {
            foreach (KeyValuePair<string, string> pairItem in iReplaceRuleList.Enum())
                iText = iText.Replace(pairItem.Key, pairItem.Value, iCaseSensible, iDiacriticSensible);

            return iText;
        }

        /// <summary>
        /// Replacement d'un string avec le choix des sensibilités
        /// </summary>
        public static string Replace(this string iText, string iOldString, string iNewString, bool iCaseSensible, bool iDiacriticSensible)
        {
            var sb = new StringBuilder();

            int previousIndex = 0;

            string compareText1 = iText;
            if (!iCaseSensible)
                compareText1 = compareText1.ToLower();

            if (!iDiacriticSensible)
                compareText1 = compareText1.RemoveDiacritics();

            string compareOldString = iOldString;

            if (!iCaseSensible)
                compareOldString = compareOldString.ToLower();

            if (!iDiacriticSensible)
                compareOldString = compareOldString.RemoveDiacritics();

            int index = compareText1.IndexOf(compareOldString);
            while (index != -1)
            {
                sb.Append(iText.Substring(previousIndex, index - previousIndex));
                sb.Append(iNewString);
                index += iOldString.Length;

                previousIndex = index;

                string compareText2 = iText;
                if (!iCaseSensible)
                    compareText2 = compareText2.ToLower();

                if (!iDiacriticSensible)
                    compareText2 = compareText2.RemoveDiacritics();

                index = compareText2.IndexOf(compareOldString, index);
            }
            sb.Append(iText.Substring(previousIndex));
            iText = sb.ToString();

            return iText;
        }

        /// <summary>
        /// Retourne un string avec remplacement d'un string sans tenir compte du début spécifié par iSartString
        /// </summary>
        public static string ReplaceFrom(this string iText, string iStartString, string iReplaceString)
        {
            if (iText.Contains(iStartString))
            {
                return iText.RemoveFrom(iStartString) + iReplaceString;
            }
            return iText;
        }

        /// <summary>
        /// Fonction split avec des strings au lieu des caractères
        /// </summary>
        public static string[] Split(this string iText, string iSeparator)
        {
            return iText.Split(new string[] { iSeparator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Retourne une chaine vide si le string est null. A appeler pour éviter toute action sur un string null
        /// </summary>
        public static string String(this string iString)
        {
            return iString ?? string.Empty;
        }

        /// <summary>
        /// Retourne le int32 depuis un string
        /// </summary>
        public static Int32 ToInt32(this string iText)
        {
            return Convert.ToInt32(iText);
        }

        /// <summary>
        /// Retourne le int64 depuis un string
        /// </summary>
        public static Int64 ToInt64(this string iText)
        {
            return Convert.ToInt64(iText);
        }

        /// <summary>
        /// Retourne le decimal depuis un string
        /// </summary>
        public static decimal ToDecimal(this string iText)
        {
            return Convert.ToDecimal(iText);
        }

        /// <summary>
        /// Retourne un string avec la première lettre en majuscule et le reste en miniscule
        /// </summary>
        public static string ToUpperFirst(this string iText)
        {
            if (string.IsNullOrEmpty(iText))
            {
                return string.Empty;
            }
            return char.ToUpper(iText[0]) + iText.Substring(1);
        }

        /// <summary>
        /// Remplace les \n (généré par enter dans une texte box) par la vrai constante
        /// </summary>
        public static string BreakFormat(this string iText)
        {
            return iText.Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Ajoute les mots clé soit au début ou à la fin du text, seulement s'il n'est pas déjà présent dans le texte. La longueur max du texte peut être précisé, 0 n'impose pas de longueur max.
        /// </summary>
        public static string AddKeyWords(this string iText, List<string> iKeyWordList, PositionEnum iPosition, int iMaxLenght = 0)
        {
            //recompose une nouvelle liste si un mot clé comporte un espace;
            var keyWordList = new List<string>();
            foreach (var stringItem in iKeyWordList.Enum())
                keyWordList.AddRange(stringItem.Split(" ").ToList());

            //Bouclage sur la liste de mot clé complète
            foreach (var keywordItem in keyWordList.Enum())
            {
                if (iText.ContainsIgnoringAccentsCapital(keywordItem) == false)
                {
                    //passe au prochain mot si longueur totale trop longue
                    if (iMaxLenght != 0)
                        if (iText.Length + 1 + keywordItem.Length > iMaxLenght)
                            continue;

                    switch (iPosition)
                    {
                        case PositionEnum.Start:
                            iText = keywordItem + " " + iText;
                            break;

                        case PositionEnum.End:
                            iText = iText + " " + keywordItem;
                            break;

                        default:
                            throw new NotSupportedException(iPosition.ToStringWithEnumName());
                    }
                }
            }
            return iText;
        }

        /// <summary>
        /// Retourne le hashcode d'un string
        /// </summary>
        public static string GetHashSHA1(this string iString)
        {
            if (string.IsNullOrEmpty(iString))
                return null;

            using (var sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(iString);

                byte[] hash = sha1.ComputeHash(textData);

                return BitConverter.ToString(hash).Replace("-", string.Empty).String().Reduce(28);
            }
        }

        /// <summary>
        /// Remplace le début d'un text si la chaine est présente
        /// </summary>
        public static string ReplaceStart(this string iString, string iOldString, string iNewString, bool iCaseSensible, bool iDiacriticSensible)
        {
            string compareString = iString;
            if (!iCaseSensible) compareString = compareString.ToLower();
            if (!iDiacriticSensible) compareString = compareString.RemoveDiacritics();

            string compareOldString = iOldString;
            if (!iCaseSensible) compareOldString = compareOldString.ToLower();
            if (!iDiacriticSensible) compareOldString = compareOldString.RemoveDiacritics();

            if (compareString.StartsWith(compareOldString))
                iString = iString.Substring(iOldString.Length);
            return iNewString + iString;
        }

        /// <summary>
        /// Remplace la fin d'un text si la chaine est présente
        /// </summary>
        public static string ReplaceEnd(this string iString, string iOldString, string iNewString, bool iCaseSensible, bool iDiacriticSensible)
        {
            string compareString = iString;
            if (!iCaseSensible) compareString = compareString.ToLower();
            if (!iDiacriticSensible) compareString = compareString.RemoveDiacritics();

            string compareOldString = iOldString;
            if (!iCaseSensible) compareOldString = compareOldString.ToLower();
            if (!iDiacriticSensible) compareOldString = compareOldString.RemoveDiacritics();

            if (compareString.EndsWith(compareOldString.ToLower()))
                iString = iString.Substring(0, iString.Length - iOldString.Length);
            return iString + iNewString;
        }

        /// <summary>
        /// Remplace un mot dans un text. Que le mot se situe en milieux de phrase ou au début ou à la fin
        /// </summary>
        public static string ReplaceWord(this string iString, string iOldString, string iNewString, string iSeparator, bool iCompleteWord, bool iCaseSensible, bool iDiacriticSensible)
        {
            //Au milieu d'abord
            if (iCompleteWord)
                iString = iString.Replace(iSeparator + iOldString + iSeparator, iSeparator, iCaseSensible, iDiacriticSensible);
            else
                iString = iString.Replace(iSeparator + iOldString, iSeparator, iCaseSensible, iDiacriticSensible);

            //debut
            if (iCompleteWord)
                iString = iString.ReplaceStart(iOldString + iSeparator, iNewString, iCaseSensible, iDiacriticSensible);
            else
                iString = iString.ReplaceStart(iOldString, iNewString, iCaseSensible, iDiacriticSensible);

            //fin
            if (iCompleteWord)
                iString = iString.ReplaceEnd(iSeparator + iOldString, iNewString, iCaseSensible, iDiacriticSensible);

            return iString;
        }

        /// <summary>
        /// Retourne une liste de string, séparé de caractères ouvrant et fermant.
        /// </summary>
        public static List<string> SplitByOpenCloseChar(this string iText, char iOpenChar, char iCloseChar, bool iStartWithOneOpenChar, string iTargetString = null)
        {
            if (iText.IsNullOrEmpty()) throw new ArgumentNullException();

            var resultList = new List<string>();
            string text = iText;

            if (iTargetString != null)
                text = text.RemoveFirstOccurence(iTargetString);

            //Bouclage sur les charactères du string
            string theString = string.Empty;
            int charCounter = 0;

            if (iStartWithOneOpenChar)
                charCounter = 1;

            foreach (var charItem in text)
            {
                if (charItem == iOpenChar)
                {
                    charCounter += 1;
                    if (charCounter == 1)
                        continue;
                }
                else if (charItem == iCloseChar)
                {
                    if (charCounter == 0)
                        break;
                    //throw new Exception("Un charactère fermant arrive avant un charactère ouvrant");

                    if (charCounter == 1)
                    {
                        resultList.Add(theString);
                        theString = string.Empty;
                        if (iStartWithOneOpenChar)
                            break;
                    }

                    charCounter -= 1;
                }

                if (charCounter != 0)
                    theString += charItem;
            }

            return resultList;
        }

        /// <summary>
        /// Retourne une liste de string, séparer du caractère séparant non contenu entre caractère d'échappement
        /// </summary>
        /// <param name="iText"></param>
        /// <param name="iEchappement"></param>
        /// <param name="iSplit"></param>
        /// <returns></returns>
        public static List<string> SplitWithEchappementChar(this string iText, char iEchappement, char iSplit)
        {
            if (iText.IsNullOrEmpty()) throw new ArgumentNullException();

            var resultList = new List<string>();
            string text = iText;

            //Bouclage sur les charactères du string
            string theString = string.Empty;
            bool InsideEchappement = false;

            foreach (var charItem in text)
            {
                if (charItem == iEchappement)
                    InsideEchappement = !InsideEchappement;

                if (charItem == iSplit && InsideEchappement == false)
                {
                    resultList.Add(theString);
                    theString = string.Empty;
                }
                else
                {
                    theString += charItem;
                }
            }

            if (theString.IsNotNullAndNotEmpty())
                resultList.Add(theString);

            return resultList;
        }

        /// <summary>
        /// Retourne une liste de string, séparer du caractère séparant non contenu entre caractère d'échappement
        /// </summary>
        /// <param name="iText"></param>
        /// <param name="iEchappement"></param>
        /// <param name="iSplit"></param>
        /// <returns></returns>
        public static List<string> SplitWithEchappementChar(this string iText, char iOpenEchappement, char iCloseEchappement, char iSplit)
        {
            if (iText.IsNullOrEmpty()) throw new ArgumentNullException();

            var resultList = new List<string>();
            string text = iText;

            //Bouclage sur les charactères du string
            string theString = string.Empty;
            int deepEchappement = 0;

            foreach (var charItem in text)
            {
                if (charItem == iOpenEchappement)
                    deepEchappement++;

                if (charItem == iCloseEchappement)
                {
                    if (deepEchappement == 0) throw new Exception("Echappement fermant avant échappement ouvrant");
                    deepEchappement--;
                }

                if (charItem == iSplit && deepEchappement == 0)
                {
                    resultList.Add(theString);
                    theString = string.Empty;
                }
                else
                {
                    theString += charItem;
                }
            }

            if (theString.IsNotNullAndNotEmpty())
                resultList.Add(theString);

            return resultList;
        }

        /// <summary>
        /// Remplace les caractères de saut de ligne par la constant de la platforme adéquate
        /// </summary>
        /// <param name="iText"></param>
        /// <returns></returns>
        public static string BreaksFormat(this string iText)
        {
            if (iText.IsNullOrEmpty()) return iText;
            return iText.Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Retourne un string null si vide sinon effectue un trim pour supprimer les espaces aux extrémités
        /// </summary>
        /// <param name="iText"></param>
        /// <returns></returns>
        public static string NullIfEmptyElseTrim(this string iText)
        {
            return (iText.IsNotNullAndNotEmpty()) ? iText.Trim() : null;
        }

        /// <summary>
        /// Retourne un string empty si le text argument est null
        /// </summary>
        public static string EmptyIfNull(this string iText)
        {
            return iText ?? string.Empty;
        }

		/// <summary>
		/// Retourne true si l'argument est un GUID
		/// </summary>
		/// <param name="iGuid"></param>
		/// <returns></returns>
		public static bool IsGuid(this string iGuid)
		{
			Guid output = Guid.Empty;
			return Guid.TryParse(iGuid, out output);
		}


		#endregion
	}
}
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Library.Text
{
	public class TextTools
	{
		#region Public METHODS

		public string GetStringFromTextFile(string iPath)
		{
			using (System.IO.StreamReader myFile = new System.IO.StreamReader(iPath))
			{
				return myFile.ReadToEnd();
			}
		}

        public List<List<string>> GetListFromCSVFile(string iPath)
        {
            using (var parser = new TextFieldParser(iPath))
            {
                var result = new List<List<string>>();
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    var rowList = new List<string>();
                    //Process row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        var rowSplit = field.Split(';');

                        foreach (var cellItem in rowSplit)
                            rowList.Add(cellItem);
                        result.Add(rowList);
                    }
                }
                return result;
            }
        }

        #endregion Public METHODS
    }
}
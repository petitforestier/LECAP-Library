using Library.Excel.Object;
using Library.Tools.Attributes;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Library.Tools.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Library.Excel
{
    public partial class ExcelTools : Cancellable
    {
        #region Public CONSTRUCTORS

        public ExcelTools(CancellationTokenSource iCancellationTokenSource)
            : base(iCancellationTokenSource)
        {
        }

        #endregion

        #region Public METHODS

        /// <summary>
        /// Recupère les données d'un fichier excel, sans excel, utilisable sur serveur
        /// </summary>
        /// <param name="iFilePath"></param>
        /// <param name="iColumnIndexList">Liste des numéros de colonne, commence à 1</param>
        /// <param name="iSheetIndex">Index de la page à récupérer, commence à 1</param>
        /// <param name="iAllowIncompleteRow">Autorise une ligne avec des colonnes obligatoires vides</param>
        /// <param name="iMasterColumn">Numéro de la colonne qui ne peut pas être vide, sinon c'est que nous sommes la fin du fichier</param>
        /// <returns></returns>
        public List<List<string>> GetListFromExcelFile(string iFilePath, List<int> iColumnIndexList, int iSheetIndex, bool iAllowIncompleteRow, int iFirstRow)
        {
            if (iFilePath == null) throw new ArgumentNullException("Chemin non renseigné");
            if (System.IO.File.Exists(iFilePath) == false) throw new ArgumentNullException("Le chemin {0] n'existe pas".FormatString(iFilePath));

            if (iColumnIndexList.IsNotNullAndNotEmpty() &&
                iSheetIndex != 0)
            {
                var rowList = new List<List<string>>();

                IWorkbook workbook;
                using (FileStream file = new FileStream(iFilePath, FileMode.Open, FileAccess.Read,FileShare.ReadWrite))
                    workbook = WorkbookFactory.Create(file);

                var evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();

                var sheet = workbook.GetSheetAt(iSheetIndex - 1);
                int lastUsedRow = sheet.LastRowNum;

                //bouclage sur les lignes
                int rowCounter = 1;
                int rowCount = lastUsedRow + 1 - iFirstRow + 1;
                for (int rowIndex = iFirstRow - 1; rowIndex <= lastUsedRow; rowIndex++)
                {
                    var isCompletRow = true;
                    var columnList = new List<string>();

                    //bouclage sur les colonnes
                    foreach (var columnIndex in iColumnIndexList.Enum())
                    {
                        CancellationToken.ThrowIfCancellationRequested();

                        var cellRange = sheet.GetRow(rowIndex).GetCell(columnIndex - 1);
                        if (cellRange != null)
                        {
                            columnList.Add(GetStringVisibleValue(cellRange, evaluator).Replace("\n", Environment.NewLine));
                        }
                        else
                        {
                            isCompletRow = iAllowIncompleteRow;
                            columnList.Add(string.Empty);

                            if (isCompletRow == false)
                                break;
                        }
                    }

                    if (isCompletRow)
                    {
                        rowList.Add(columnList);
                        MyDebug.PrintInformation("Ligne {0} complète importée".FormatString(rowIndex), rowCount, rowCounter);
                    }
                    else
                        MyDebug.PrintInformation("Ligne {0} imcomplète".FormatString(rowIndex), rowCount, rowCounter);

                    rowCounter++;
                }
                MyDebug.PrintInformation("Fin de l'importation excel '{0}'".FormatString(iFilePath));
                return rowList;
            }
            return null;
        }

        /// <summary>
        /// Permet exporter vers un nouveau fichier excel une liste d'objet ou de type primitif. Sauvegarder par default dans temp sauf si chemin spécifié;
        /// </summary>
        /// <returns>Retourne le chemin du fichier créé</returns>
        public string SendListToNewExcelFile<T>(List<ExcelSheet<T>> iSheetList, string iSaveFullPath = null)
        {
            if (iSheetList.IsNullOrEmpty()) throw new ArgumentException("Les données à exporter sont vides");

            var workbook = new HSSFWorkbook();
            IDataFormat dataFormatCustom = workbook.CreateDataFormat();

            ICellStyle doubleCellStyle = workbook.CreateCellStyle();
            doubleCellStyle.DataFormat = dataFormatCustom.GetFormat("#,##0.00");

            ICellStyle dateTimeCellStyle = workbook.CreateCellStyle();
            dateTimeCellStyle.DataFormat = dataFormatCustom.GetFormat("dd/MM/yyyy");

            ISheet workSheet;
            int sheetIndex = 0;
            foreach (var sheetItem in iSheetList)
            {
                sheetIndex++;
                if (sheetItem.SheetName.IsNotNullAndNotEmpty())
                    workSheet = workbook.CreateSheet(sheetItem.SheetName.Reduce(30));
                else
                    workSheet = workbook.CreateSheet();

                var rowCounter = 0;
                var columnCounter = 0;

                //Ecriture du message
                foreach (var item in sheetItem.iMessageList.Enum())
                {
                    var cell = workSheet.CreateRow(rowCounter).CreateCell(1);
                    cell.SetCellValue(item);
                    rowCounter++;
                }

                //variable sans propriété (ex string, int etc...)
                var tType = sheetItem.DataList.First().GetType();
                if (tType.IsPrimitive || tType.IsValueType || (tType == typeof(string)))
                {
                    //Bouclage sur les item de la liste (ligne excel)
                    var itemCounter = 1;
                    foreach (var item in sheetItem.DataList)
                    {
                        CancellationToken.ThrowIfCancellationRequested();
                        if (item != null)
                        {
                            var cell = workSheet.CreateRow(rowCounter).CreateCell(0);
                            cell.SetCellValue(item.ToString());
                        }
                        MyDebug.PrintInformation("Ligne exportée", itemCounter, sheetItem.DataList.Enum().Count());
                        rowCounter += 1;
                        itemCounter++;
                    }
                }

                //Object avec des propriétés (pour une classe)
                else
                {
                    var properties = tType.GetProperties();
                    var theHeaderRow = workSheet.CreateRow(rowCounter);
                    foreach (var property in properties)
                    {
                        var theCellHeader = theHeaderRow.CreateCell(columnCounter);
                        if (sheetItem.Lang.IsNotNullAndNotEmpty())
                        {
                            var name = tType.GetName(property.Name, sheetItem.Lang);
                            if (name != null)
                                theCellHeader.SetCellValue(name);
                            else
                                theCellHeader.SetCellValue(property.Name);
                        }
                        else
                        {
                            theCellHeader.SetCellValue(property.Name);
                        }
                        columnCounter++;
                    }
                    rowCounter++;
                    columnCounter = 0;

                    //Bouclage sur les item de la liste (ligne excel)
                    var itemCounter = 1;
                    foreach (var item in sheetItem.DataList)
                    {
                        var theDataRow = workSheet.CreateRow(rowCounter);
                        //Bouclage sur les propriétés de l'item (colonne excel)
                        foreach (var propertyItem in properties)
                        {
                            CancellationToken.ThrowIfCancellationRequested();

                            var theCell = theDataRow.CreateCell(columnCounter);
                            var thePropertyItem = propertyItem.GetValue(item);

                            if (thePropertyItem != null)
                            {
                                if (propertyItem.PropertyType == typeof(DateTime) || propertyItem.PropertyType == typeof(DateTime?))
                                {
                                    theCell.SetCellValue(((DateTime)thePropertyItem));
                                    theCell.CellStyle = dateTimeCellStyle;
                                }
                                else if (propertyItem.PropertyType == typeof(decimal) || propertyItem.PropertyType == typeof(decimal?)
                                    || propertyItem.PropertyType == typeof(double) || propertyItem.PropertyType == typeof(double?)
                                    || propertyItem.PropertyType == typeof(int) || propertyItem.PropertyType == typeof(int?)
                                    || propertyItem.PropertyType == typeof(Int16) || propertyItem.PropertyType == typeof(Int16?)
                                    || propertyItem.PropertyType == typeof(Int64) || propertyItem.PropertyType == typeof(Int64?))
                                {
                                    theCell.SetCellValue(Convert.ToDouble(thePropertyItem));
                                    theCell.CellStyle = doubleCellStyle;
                                }
                                else
                                    theCell.SetCellValue(thePropertyItem.ToString());
                            }
                            columnCounter++;
                        }

                        MyDebug.PrintInformation("Ligne exportée", itemCounter, sheetItem.DataList.Enum().Count());
                        rowCounter++;
                        columnCounter = 0;
                        itemCounter++;
                    }
                }
            }

            var filePath = (iSaveFullPath.IsNotNullAndNotEmpty()) ? iSaveFullPath : Path.GetTempPath() + Guid.NewGuid().ToString() + ".xls";
            FileStream xfile = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write);
            workbook.Write(xfile);
            xfile.Close();
            MyDebug.PrintInformation("Exportation dans le fichier '{0}'".FormatString(filePath));
            return filePath;
        }

        /// <summary>
        /// Retourne la valeur de la cellule ou le résultat dans le cas d'une formule
        /// </summary>
        /// <param name="iCell"></param>
        /// <param name="iEvalutator"></param>
        /// <returns></returns>
        public string GetStringVisibleValue(ICell iCell, IFormulaEvaluator iEvalutator)
        {
            var formulaType = iEvalutator.EvaluateFormulaCell(iCell);

            if (formulaType == CellType.Numeric)
                return iCell.NumericCellValue.ToString();
            else if (formulaType == CellType.String)
                return iCell.StringCellValue;
            else if (formulaType == CellType.Blank)
                return string.Empty;
            else if (formulaType == CellType.Boolean)
                return iCell.BooleanCellValue.ToString();
            else if (formulaType == CellType.Unknown)
                return iCell.ToString();
            else if (formulaType == CellType.Error)
                throw new Exception("Erreur dans la cellule ligne {0}, colonne {1}".FormatString(iCell.RowIndex + 1, iCell.ColumnIndex + 1));
            else
                throw new NotSupportedException(formulaType.ToStringWithEnumName());
        }

        /// <summary>
        /// Exportation en fichier CSV
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iList"></param>
        /// <param name="iSaveFullPath"></param>
        /// <returns></returns>
        public string SendListToNewCSVFile<T>(List<T> iList, string iSaveFullPath = null)
        {
            var csv = new StringBuilder();

            //Entête des colonnes
            string headerString = string.Empty;
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string)
                    || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?)
                    || property.PropertyType == typeof(double) || property.PropertyType == typeof(double?)
                    || property.PropertyType == typeof(int) || property.PropertyType == typeof(int?)
                    || property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Int16?)
                    || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Int64?)
                    || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?)
                    )
                {
                    if (headerString.IsNotNullAndNotEmpty())
                        headerString += ";";
                    headerString += property.Name;
                }
            }
            headerString += Environment.NewLine;
            csv.Append(headerString);

            //Bouclage sur les lignes
            foreach (var item in iList)
            {
                string rowString = string.Empty;

                //var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(string)
                        || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?)
                        || property.PropertyType == typeof(double) || property.PropertyType == typeof(double?)
                        || property.PropertyType == typeof(int) || property.PropertyType == typeof(int?)
                        || property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Int16?)
                        || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Int64?)
                        || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?)
                        )
                    {
                        if (rowString.IsNotNullAndNotEmpty())
                            rowString += ";";
                        var theValue = property.GetValue(item);
                        if (theValue != null)
                            rowString += theValue.ToString();
                        else
                            rowString += "";
                    }
                }

                rowString += Environment.NewLine;
                csv.Append(rowString);
            }

            var filePath = (iSaveFullPath.IsNotNullAndNotEmpty()) ? iSaveFullPath : Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";

            File.WriteAllText(filePath, csv.ToString());
            return filePath;
        }

        #endregion
    }
}
using Library.Tools.Debug;
using System;

namespace Library.Tools.Tasks
{
    public class ReporterProgress
    {
        #region Public PROPERTIES

        public string Message{get;set;}
        public int ProgressCounter { get; set; }
        public MyTimer Timer { get; set; }

        #endregion
    }
}
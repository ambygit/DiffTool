using System;
using System.Collections.Generic;
using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ReportBuilder
{
    public interface IDiffResultReportBuilder<TSource,TResult> where TResult : IDiffResult
    {
        void Append(EDiffStatus diffStatus, int recordPosition, TSource record, bool isSourceRecord);
        List<TResult> GetRecords();
        void Print(Action<TSource> printingAction);
    }
}

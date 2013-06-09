using System;
using System.Collections.Generic;
using Hsbc.Task.Tools.DiffLibrary.Exceptions;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;

namespace Hsbc.Task.Tools.DiffLibrary.ReportBuilder
{
    internal class DiffResultBuilderFactory
    {
        public static readonly DiffResultBuilderFactory _instance;

        static DiffResultBuilderFactory()
        {
            _instance = new DiffResultBuilderFactory();
        }

        public static DiffResultBuilderFactory Instance
        {
            get { return _instance; }
        }

        private static readonly Dictionary<Type, Type> Result2BuilderMap = new Dictionary<Type, Type>
                                                                      {
                                                                         {typeof (PerLineDiffResult),typeof (PerLineDiffResultReportBuilder)},
                                                                         {typeof (LinesRangeDiffResult),typeof (LinesRangeDiffResultReportBuilder)},
                                                                      };

        public IDiffResultReportBuilder<TSource, IDiffResult> GetBuilder<TSource>(Type resultType, IDiffResultOption<IDiffResult> diffResultOption)
        {
            Type builderType;
            if(!Result2BuilderMap.TryGetValue(resultType,out builderType))
            {
                throw new SetupException("No builder mapping found for result type: {0}", resultType);
            }
            return (IDiffResultReportBuilder<TSource, IDiffResult>)Activator.CreateInstance(builderType, new object[] { diffResultOption });
        }
    }


}
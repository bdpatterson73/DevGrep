using System.Collections.Generic;
using SmartSearch.Support;

namespace SmartSearch.Analysis
{
	
	/// <summary> This analyzer is used to facilitate scenarios where different
	/// fields require different analysis techniques.  Use <see cref="AddAnalyzer" />
	/// to add a non-default analyzer on a field name basis.
	/// 
	/// <p/>Example usage:
	/// 
	/// <code>
	/// PerFieldAnalyzerWrapper aWrapper =
	/// new PerFieldAnalyzerWrapper(new StandardAnalyzer());
	/// aWrapper.addAnalyzer("firstname", new KeywordAnalyzer());
	/// aWrapper.addAnalyzer("lastname", new KeywordAnalyzer());
	/// </code>
	/// 
	/// <p/>In this example, StandardAnalyzer will be used for all fields except "firstname"
	/// and "lastname", for which KeywordAnalyzer will be used.
	/// 
	/// <p/>A PerFieldAnalyzerWrapper can be used like any other analyzer, for both indexing
	/// and query parsing.
	/// </summary>
	public class PerFieldAnalyzerWrapper:Analyzer
	{
		private readonly Analyzer defaultAnalyzer;
		private readonly IDictionary<string, Analyzer> analyzerMap = new HashMap<string, Analyzer>();
		
		
		/// <summary> Constructs with default analyzer.
		/// 
		/// </summary>
		/// <param name="defaultAnalyzer">Any fields not specifically
		/// defined to use a different analyzer will use the one provided here.
		/// </param>
		public PerFieldAnalyzerWrapper(Analyzer defaultAnalyzer)
            : this(defaultAnalyzer, null)
		{
		}
		
		/// <summary> Constructs with default analyzer and a map of analyzers to use for 
		/// specific fields.
		/// 
		/// </summary>
		/// <param name="defaultAnalyzer">Any fields not specifically
		/// defined to use a different analyzer will use the one provided here.
		/// </param>
		/// <param name="fieldAnalyzers">a Map (String field name to the Analyzer) to be 
		/// used for those fields 
		/// </param>
        public PerFieldAnalyzerWrapper(Analyzer defaultAnalyzer, IEnumerable<KeyValuePair<string, Analyzer>> fieldAnalyzers)
		{
			this.defaultAnalyzer = defaultAnalyzer;
			if (fieldAnalyzers != null)
			{
				foreach(var entry in fieldAnalyzers)
					analyzerMap[entry.Key] = entry.Value;
			}
            SetOverridesTokenStreamMethod<PerFieldAnalyzerWrapper>();
		}
		
		
		/// <summary> Defines an analyzer to use for the specified field.
		/// 
		/// </summary>
		/// <param name="fieldName">field name requiring a non-default analyzer
		/// </param>
		/// <param name="analyzer">non-default analyzer to use for field
		/// </param>
		public virtual void  AddAnalyzer(System.String fieldName, Analyzer analyzer)
		{
			analyzerMap[fieldName] = analyzer;
		}
		
		public override TokenStream TokenStream(System.String fieldName, System.IO.TextReader reader)
		{
			var analyzer = analyzerMap[fieldName] ?? defaultAnalyzer;

			return analyzer.TokenStream(fieldName, reader);
		}
		
		public override TokenStream ReusableTokenStream(string fieldName, System.IO.TextReader reader)
		{
			if (overridesTokenStreamMethod)
			{
				
				return TokenStream(fieldName, reader);
			}
			var analyzer = analyzerMap[fieldName] ?? defaultAnalyzer;

			return analyzer.ReusableTokenStream(fieldName, reader);
		}
		
		/// <summary>Return the positionIncrementGap from the analyzer assigned to fieldName </summary>
		public override int GetPositionIncrementGap(string fieldName)
		{
			var analyzer = analyzerMap[fieldName] ?? defaultAnalyzer;
		    return analyzer.GetPositionIncrementGap(fieldName);
		}

        /// <summary> Return the offsetGap from the analyzer assigned to field </summary>
        public override int GetOffsetGap(Documents.IFieldable field)
        {
            Analyzer analyzer = analyzerMap[field.Name] ?? defaultAnalyzer;
            return analyzer.GetOffsetGap(field);
        }
		
		public override System.String ToString()
		{
			// {{Aroush-2.9}} will 'analyzerMap.ToString()' work in the same way as Java's java.util.HashMap.toString()? 
			return "PerFieldAnalyzerWrapper(" + analyzerMap + ", default=" + defaultAnalyzer + ")";
		}
	}
}
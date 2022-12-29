namespace SmartSearch.Analysis
{
	
	/// <summary> "Tokenizes" the entire stream as a single token. This is useful
	/// for data like zip codes, ids, and some product names.
	/// </summary>
	public class KeywordAnalyzer:Analyzer
	{
		public KeywordAnalyzer()
		{
            SetOverridesTokenStreamMethod<KeywordAnalyzer>();
		}
		public override TokenStream TokenStream(System.String fieldName, System.IO.TextReader reader)
		{
			return new KeywordTokenizer(reader);
		}
		public override TokenStream ReusableTokenStream(System.String fieldName, System.IO.TextReader reader)
		{
			if (overridesTokenStreamMethod)
			{
				
				return TokenStream(fieldName, reader);
			}
			var tokenizer = (Tokenizer) PreviousTokenStream;
			if (tokenizer == null)
			{
				tokenizer = new KeywordTokenizer(reader);
				PreviousTokenStream = tokenizer;
			}
			else
				tokenizer.Reset(reader);
			return tokenizer;
		}
	}
}
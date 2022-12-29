using System;
using SmartSearch.Analysis.Tokenattributes;
using SmartSearch.Util;
using CharReader = SmartSearch.Analysis.CharReader;
using Token = SmartSearch.Analysis.Token;
using Tokenizer = SmartSearch.Analysis.Tokenizer;
using AttributeSource = SmartSearch.Util.AttributeSource;
using Version = SmartSearch.Util.Version;

namespace SmartSearch.Analysis.Standard
{
	
	/// <summary>A grammar-based tokenizer constructed with JFlex
	/// 
	/// <p/> This should be a good tokenizer for most European-language documents:
	/// 
	/// <list type="bullet">
	/// <item>Splits words at punctuation characters, removing punctuation. However, a 
	/// dot that's not followed by whitespace is considered part of a token.</item>
	/// <item>Splits words at hyphens, unless there's a number in the token, in which case
	/// the whole token is interpreted as a product number and is not split.</item>
	/// <item>Recognizes email addresses and internet hostnames as one token.</item>
	/// </list>
	/// 
	/// <p/>Many applications have specific tokenizer needs.  If this tokenizer does
	/// not suit your application, please consider copying this source code
	/// directory to your project and maintaining your own grammar-based tokenizer.
	/// 
	/// <a name="version"/>
	/// <p/>
	/// You must specify the required <see cref="Version" /> compatibility when creating
	/// StandardAnalyzer:
	/// <list type="bullet">
	/// <item>As of 2.4, Tokens incorrectly identified as acronyms are corrected (see
	/// </item>
	/// </list>
	/// </summary>
	
	public sealed class StandardTokenizer:Tokenizer
	{
		private void  InitBlock()
		{
			maxTokenLength = StandardAnalyzer.DEFAULT_MAX_TOKEN_LENGTH;
		}
		/// <summary>A private instance of the JFlex-constructed scanner </summary>
		private StandardTokenizerImpl scanner;
		
		public const int ALPHANUM   = 0;
		public const int APOSTROPHE = 1;
		public const int ACRONYM    = 2;
		public const int COMPANY    = 3;
		public const int EMAIL      = 4;
		public const int HOST       = 5;
		public const int NUM        = 6;
		public const int CJ         = 7;
		
		/// <deprecated> this solves a bug where HOSTs that end with '.' are identified
		/// as ACRONYMs.
		/// </deprecated>
        [Obsolete("this solves a bug where HOSTs that end with '.' are identified as ACRONYMs.")]
		public const int ACRONYM_DEP = 8;
		
		/// <summary>String token types that correspond to token type int constants </summary>
		public static readonly System.String[] TOKEN_TYPES = new System.String[]{"<ALPHANUM>", "<APOSTROPHE>", "<ACRONYM>", "<COMPANY>", "<EMAIL>", "<HOST>", "<NUM>", "<CJ>", "<ACRONYM_DEP>"};
		
		private bool replaceInvalidAcronym;
		
		private int maxTokenLength;

	    /// <summary>Set the max allowed token length.  Any token longer
	    /// than this is skipped. 
	    /// </summary>
	    public int MaxTokenLength
	    {
	        get { return maxTokenLength; }
	        set { this.maxTokenLength = value; }
	    }

	    /// <summary> Creates a new instance of the
	    /// <see cref="SmartSearch.Analysis.Standard.StandardTokenizer" />. Attaches
	    /// the <c>input</c> to the newly created JFlex scanner.
	    /// 
	    /// </summary>
	    /// <param name="matchVersion"></param>
	    /// <param name="input">The input reader
	    /// 
	    /// </param>
	    public StandardTokenizer(Version matchVersion, System.IO.TextReader input):base()
		{
			InitBlock();
			this.scanner = new StandardTokenizerImpl(input);
			Init(input, matchVersion);
		}

		/// <summary> Creates a new StandardTokenizer with a given <see cref="AttributeSource" />.</summary>
		public StandardTokenizer(Version matchVersion, AttributeSource source, System.IO.TextReader input):base(source)
		{
			InitBlock();
			this.scanner = new StandardTokenizerImpl(input);
			Init(input, matchVersion);
		}
		
		/// <summary> Creates a new StandardTokenizer with a given
		/// <see cref="SmartSearch.Util.AttributeSource.AttributeFactory" />
		/// </summary>
		public StandardTokenizer(Version matchVersion, AttributeFactory factory, System.IO.TextReader input):base(factory)
		{
			InitBlock();
			this.scanner = new StandardTokenizerImpl(input);
			Init(input, matchVersion);
		}
		
		private void  Init(System.IO.TextReader input, Version matchVersion)
		{
			if (matchVersion.OnOrAfter(Version.SmartSearch_24))
			{
			    replaceInvalidAcronym = true;
			}
			else
			{
			    replaceInvalidAcronym = false;
			}
		    this.input = input;
		    termAtt = AddAttribute<ITermAttribute>();
		    offsetAtt = AddAttribute<IOffsetAttribute>();
		    posIncrAtt = AddAttribute<IPositionIncrementAttribute>();
		    typeAtt = AddAttribute<ITypeAttribute>();
		}
		
		// this tokenizer generates three attributes:
		// offset, positionIncrement and type
		private ITermAttribute termAtt;
		private IOffsetAttribute offsetAtt;
		private IPositionIncrementAttribute posIncrAtt;
		private ITypeAttribute typeAtt;
		
		///<summary>
		/// (non-Javadoc)
		/// <see cref="SmartSearch.Analysis.TokenStream.IncrementToken()" />
        ///</summary>
		public override bool IncrementToken()
		{
			ClearAttributes();
			int posIncr = 1;
			
			while (true)
			{
				int tokenType = scanner.GetNextToken();
				
				if (tokenType == StandardTokenizerImpl.YYEOF)
				{
					return false;
				}
				
				if (scanner.Yylength() <= maxTokenLength)
				{
					posIncrAtt.PositionIncrement = posIncr;
					scanner.GetText(termAtt);
					int start = scanner.Yychar();
					offsetAtt.SetOffset(CorrectOffset(start), CorrectOffset(start + termAtt.TermLength()));
					// This 'if' should be removed in the next release. For now, it converts
					// invalid acronyms to HOST. When removed, only the 'else' part should
					// remain.
					if (tokenType == StandardTokenizerImpl.ACRONYM_DEP)
					{
						if (replaceInvalidAcronym)
						{
							typeAtt.Type = StandardTokenizerImpl.TOKEN_TYPES[StandardTokenizerImpl.HOST];
							termAtt.SetTermLength(termAtt.TermLength() - 1); // remove extra '.'
						}
						else
						{
							typeAtt.Type = StandardTokenizerImpl.TOKEN_TYPES[StandardTokenizerImpl.ACRONYM];
						}
					}
					else
					{
						typeAtt.Type = StandardTokenizerImpl.TOKEN_TYPES[tokenType];
					}
					return true;
				}
				// When we skip a too-long term, we still increment the
				// position increment
				else
					posIncr++;
			}
		}
		
		public override void  End()
		{
			// set final offset
			int finalOffset = CorrectOffset(scanner.Yychar() + scanner.Yylength());
			offsetAtt.SetOffset(finalOffset, finalOffset);
		}
		
		public override void  Reset(System.IO.TextReader reader)
		{
			base.Reset(reader);
			scanner.Reset(reader);
		}
		
		/// <summary>
		/// Remove in 3.X and make true the only valid value
        /// </summary>
        /// <param name="replaceInvalidAcronym">Set to true to replace mischaracterized acronyms as HOST.
        /// </param>
        [Obsolete("Remove in 3.X and make true the only valid value.")]
		public void  SetReplaceInvalidAcronym(bool replaceInvalidAcronym)
		{
			this.replaceInvalidAcronym = replaceInvalidAcronym;
		}
	}
}
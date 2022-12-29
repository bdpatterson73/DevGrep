using AttributeSource = SmartSearch.Util.AttributeSource;

namespace SmartSearch.Analysis
{
	
	/// <summary> A Tokenizer is a TokenStream whose input is a Reader.
	/// <p/>
	/// This is an abstract class; subclasses must override <see cref="TokenStream.IncrementToken()" />
	/// <p/>
    /// NOTE: Subclasses overriding <see cref="TokenStream.IncrementToken()" /> must call
	/// <see cref="AttributeSource.ClearAttributes()" /> before setting attributes.
	/// </summary>
	
	public abstract class Tokenizer:TokenStream
	{
		/// <summary>The text source for this Tokenizer. </summary>
		protected internal System.IO.TextReader input;

	    private bool isDisposed;
		
		/// <summary>Construct a tokenizer with null input. </summary>
		protected internal Tokenizer()
		{
		}
		
		/// <summary>Construct a token stream processing the given input. </summary>
		protected internal Tokenizer(System.IO.TextReader input)
		{
			this.input = CharReader.Get(input);
		}
		
		/// <summary>Construct a tokenizer with null input using the given AttributeFactory. </summary>
		protected internal Tokenizer(AttributeFactory factory):base(factory)
		{
		}
		
		/// <summary>Construct a token stream processing the given input using the given AttributeFactory. </summary>
		protected internal Tokenizer(AttributeFactory factory, System.IO.TextReader input):base(factory)
		{
			this.input = CharReader.Get(input);
		}
		
		/// <summary>Construct a token stream processing the given input using the given AttributeSource. </summary>
		protected internal Tokenizer(AttributeSource source):base(source)
		{
		}
		
		/// <summary>Construct a token stream processing the given input using the given AttributeSource. </summary>
		protected internal Tokenizer(AttributeSource source, System.IO.TextReader input):base(source)
		{
			this.input = CharReader.Get(input);
		}
		
        protected override void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                if (input != null)
                {
                    input.Close();
                }
            }

            input = null;
            isDisposed = true;
        }
  
		/// <summary>Return the corrected offset. If <see cref="input" /> is a <see cref="CharStream" /> subclass
		/// this method calls <see cref="CharStream.CorrectOffset" />, else returns <c>currentOff</c>.
		/// </summary>
		/// <param name="currentOff">offset as seen in the output
		/// </param>
		/// <returns> corrected offset based on the input
		/// </returns>
		/// <seealso cref="CharStream.CorrectOffset">
		/// </seealso>
		protected internal int CorrectOffset(int currentOff)
		{
			return (input is CharStream)?((CharStream) input).CorrectOffset(currentOff):currentOff;
		}
		
		/// <summary>Expert: Reset the tokenizer to a new reader.  Typically, an
		/// analyzer (in its reusableTokenStream method) will use
		/// this to re-use a previously created tokenizer. 
		/// </summary>
		public virtual void  Reset(System.IO.TextReader input)
		{
			this.input = input;
		}
	}
}
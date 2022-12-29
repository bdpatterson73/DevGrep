// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Brian Patterson" email="briandavidpatterson@gmail.com"/>
//     <version>$Revision: 1966 $</version>
// </file>

namespace BLS.TextEditor.Src.Document
{
	/// <summary>
	/// This interface is used to describe a span inside a text sequence
	/// </summary>
	public interface ISegment
	{
		/// <value>
		/// The offset where the span begins
		/// </value>
		int Offset {
			get;
			set;
		}
		
		/// <value>
		/// The length of the span
		/// </value>
		int Length {
			get;
			set;
		}
	}
	
}

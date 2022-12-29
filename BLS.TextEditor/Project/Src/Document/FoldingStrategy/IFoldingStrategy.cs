// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Brian Patterson" email="briandavidpatterson@gmail.com"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System.Collections.Generic;

namespace BLS.TextEditor.Src.Document.FoldingStrategy
{
	/// <summary>
	/// This interface is used for the folding capabilities
	/// of the textarea.
	/// </summary>
	public interface IFoldingStrategy
	{
		/// <remarks>
		/// Calculates the fold level of a specific line.
		/// </remarks>
		List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation);
	}
}

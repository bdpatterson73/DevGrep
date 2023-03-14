// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Brian Patterson" email="bdpatterson73@gmail.com"/>
//     <version>$Revision: 1301 $</version>
// </file>

using System.Collections.Generic;
using System.Xml;

namespace BLS.TextEditor.Src.Document.HighlightingStrategy.SyntaxModes
{
	public interface ISyntaxModeFileProvider
	{
		ICollection<SyntaxMode> SyntaxModes {
			get;
		}
		
		XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode);
		void UpdateSyntaxModeList();
	}
}

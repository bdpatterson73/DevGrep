// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Brian Patterson" email="briandavidpatterson@gmail.com"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace BLS.TextEditor.Src.Document.HighlightingStrategy.SyntaxModes
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		List<SyntaxMode> syntaxModes = null;
		
		public ICollection<SyntaxMode> SyntaxModes {
			get {
				return syntaxModes;
			}
		}
		
		public ResourceSyntaxModeProvider()
		{
			Assembly assembly = typeof(SyntaxMode).Assembly;
			Stream syntaxModeStream = assembly.GetManifestResourceStream("BLS.TextEditor.Resources.SyntaxModes.xml");
			if (syntaxModeStream != null) {
				syntaxModes = SyntaxMode.GetSyntaxModes(syntaxModeStream);
			} else {
				syntaxModes = new List<SyntaxMode>();
			}
		}
		
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			Assembly assembly = typeof(SyntaxMode).Assembly;
			return new XmlTextReader(assembly.GetManifestResourceStream("BLS.TextEditor.Resources." + syntaxMode.FileName));
		}
		
		public void UpdateSyntaxModeList()
		{
			// resources don't change during runtime
		}
	}
}

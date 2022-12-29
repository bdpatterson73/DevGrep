// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <author name="Brian Patterson"/>
//     <version>$Revision: 2719 $</version>
// </file>

using System;

namespace BLS.TextEditor.Src.Util
{
	/// <summary>
	/// Central location for logging calls in the text editor.
	/// </summary>
	static class LoggingService
	{
		public static void Debug(string text)
		{
			#if DEBUG
			Console.WriteLine(text);
			#endif
		}
	}
}

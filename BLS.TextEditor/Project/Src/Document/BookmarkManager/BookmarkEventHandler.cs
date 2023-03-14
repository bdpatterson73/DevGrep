// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Brian Patterson" email="bdpatterson73@gmail.com"/>
//     <version>$Revision: 915 $</version>
// </file>

using System;

namespace BLS.TextEditor.Src.Document.BookmarkManager
{
	public delegate void BookmarkEventHandler(object sender, BookmarkEventArgs e);
	
	/// <summary>
	/// Description of BookmarkEventHandler.
	/// </summary>
	public class BookmarkEventArgs : EventArgs
	{
		Bookmark bookmark;
		
		public Bookmark Bookmark {
			get {
				return bookmark;
			}
		}
		
		public BookmarkEventArgs(Bookmark bookmark)
		{
			this.bookmark = bookmark;
		}
	}
}

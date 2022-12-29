// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Brian Patterson" email="briandavidpatterson@gmail.com"/>
//     <version>$Revision: 1040 $</version>
// </file>


using System.Reflection;
using BLS.TextEditor;

[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: AssemblyCompany("ic#code")]
[assembly: AssemblyProduct("SharpDevelop")]
[assembly: AssemblyCopyright("2000-2008 AlphaSierraPapa")]
[assembly: AssemblyVersion(RevisionClass.FullVersion)]

namespace BLS.TextEditor
{
    internal static class RevisionClass
    {
        public const string Major = "3";
        public const string Minor = "0";
        public const string Build = "0";
        public const string Revision = "3437";
	
        public const string MainVersion = Major + "." + Minor;
        public const string FullVersion = Major + "." + Minor + "." + Build + "." + Revision;
    }
}

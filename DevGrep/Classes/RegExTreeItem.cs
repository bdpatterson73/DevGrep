using System;
using System.Web.Security;
using System.Windows.Forms;

namespace DevGrep.Classes
{
	/// <summary>
	/// Summary description for RegExTreeItem.
	/// </summary>
	public class RegExTreeItem:TreeNode
	{
		public RegExTreeItem()
		{
			//
			// TODO: Add constructor logic here
			//
		}

	    private string _entryType="";
	    private string _entryName="";
	    private string _entryText="";
	    private string _hashCode="";
	    private string _parentHash="";

	    public string ParentHash
	    {
	        get
	        {
	            return _parentHash;
	        }
	        set
	        {
	            _parentHash = value;
	        }
	    }

	    #region HashCode Property
        /// <summary>
        /// HashCode Property
        /// </summary>
        /// <remarks>Generates a hash based on the EntryType and EntryName.</remarks>
	    public string HashCode
	    {
	        get
	        {
	            _hashCode=FormsAuthentication.HashPasswordForStoringInConfigFile(_entryType+_entryName,"MD5"); 
	            return _hashCode;
	        }
	    }
	    #endregion

	    public string EntryText
	    {
	        get
	        {
	            return _entryText;
	        }
	        set
	        {
	            _entryText = value;
	        }
	    }

	    public string EntryName
	    {
	        get
	        {
	            return _entryName;
	        }
	        set
	        {
	            _entryName = value;
                this.Text = _entryName;
	        }
	    }

	    public string EntryType
	    {
	        get
	        {
	            return _entryType;
	        }
	        set
	        {
	            _entryType = value;
                if (_entryType.ToUpper()=="FOLDER")
                {
                    this.ImageIndex = 0;
                    this.SelectedImageIndex =1;
                }
                if (_entryType.ToUpper()=="REGEX")
                {
                    this.ImageIndex = 2;

                }
                
	        }
	    }

        public override string ToString()
        {
            return this._entryName;
        }

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep.Forms
{
    public partial class ImageButton : Button
    {
        private ButtonType _buttonFunction = ButtonType.Other;
        public enum ButtonType
        {
            Other,
            Add,
            Edit,
            Delete
        }
        public ImageButton()
        {
            InitializeComponent();
            SetImage();
        }

        private void SetImage()
        {
            if (this.Image == null || this.Image != DevGrep.Properties.Resources.ImageButtonAdd
                || this.Image != DevGrep.Properties.Resources.ImageButtonDelete
                || this.Image != DevGrep.Properties.Resources.ImageButtonEdit
                || this.Image != DevGrep.Properties.Resources.ImageButtonOther)
            {
                switch (_buttonFunction)
                {
                    case ButtonType.Add:
                        this.Image = DevGrep.Properties.Resources.ImageButtonAdd;
                        break;
                    case ButtonType.Delete:
                        this.Image = DevGrep.Properties.Resources.ImageButtonDelete;
                        break;
                    case ButtonType.Edit:
                        this.Image = DevGrep.Properties.Resources.ImageButtonEdit;
                        break;
                    default:
                        this.Image = DevGrep.Properties.Resources.ImageButtonOther;
                        break;
                }
            }
        }

        public ButtonType ButtonFunction
        {
            get { return _buttonFunction; }
            set
            {
                _buttonFunction = value;
                SetImage();
            }
        }

        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get { return ""; }
            set { base.Text = ""; }
        }
    }
}

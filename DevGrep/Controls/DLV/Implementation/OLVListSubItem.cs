using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// A ListViewSubItem that knows which image should be drawn against it.
    /// </summary>
    [Browsable(false)]
    public class OLVListSubItem : ListViewItem.ListViewSubItem {
        #region Constructors

        /// <summary>
        /// Create a OLVListSubItem
        /// </summary>
        public OLVListSubItem() {
        }

        /// <summary>
        /// Create a OLVListSubItem that shows the given string and image
        /// </summary>
        public OLVListSubItem(object modelValue, string text, Object image) {
            this.ModelValue = modelValue;
            this.Text = text;
            this.ImageSelector = image;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the model value is being displayed by this subitem.
        /// </summary>
        public object ModelValue
        {
            get { return modelValue; }
            private set { modelValue = value; }
        }
        private object modelValue;

        /// <summary>
        /// Gets if this subitem has any decorations set for it.
        /// </summary>
        public bool HasDecoration {
            get {
                return this.decorations != null && this.decorations.Count > 0;
            }
        }

        /// <summary>
        /// Gets or sets the decoration that will be drawn over this item
        /// </summary>
        /// <remarks>Setting this replaces all other decorations</remarks>
        public IDecoration Decoration {
            get {
                if (this.HasDecoration)
                    return this.Decorations[0];
                else
                    return null;
            }
            set {
                this.Decorations.Clear();
                if (value != null)
                    this.Decorations.Add(value);
            }
        }

        /// <summary>
        /// Gets the collection of decorations that will be drawn over this item
        /// </summary>
        public IList<IDecoration> Decorations {
            get {
                if (this.decorations == null)
                    this.decorations = new List<IDecoration>();
                return this.decorations;
            }
        }
        private IList<IDecoration> decorations;

        /// <summary>
        /// Get or set the image that should be shown against this item
        /// </summary>
        /// <remarks><para>This can be an Image, a string or an int. A string or an int will
        /// be used as an index into the small image list.</para></remarks>
        public Object ImageSelector {
            get { return imageSelector; }
            set { imageSelector = value; }
        }
        private Object imageSelector;

        /// <summary>
        /// Gets or sets the url that should be invoked when this subitem is clicked
        /// </summary>
        public string Url {
            get { return this.url; }
            set { this.url = value; }
        }
        private string url;

        #endregion

        #region Implementation Properties

        /// <summary>
        /// Return the state of the animatation of the image on this subitem.
        /// Null means there is either no image, or it is not an animation
        /// </summary>
        internal ImageRenderer.AnimationState AnimationState;

        #endregion
    }

}

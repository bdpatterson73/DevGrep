

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// Instances of this class put a CheckedListBox into a tool strip menu item.
    /// </summary>
    public class ToolStripCheckedListBox : ToolStripControlHost {

        /// <summary>
        /// Create a ToolStripCheckedListBox
        /// </summary>
        public ToolStripCheckedListBox()
            : base(new CheckedListBox()) {
            this.CheckedListBoxControl.MaximumSize = new Size(400, 700);
            this.CheckedListBoxControl.ThreeDCheckBoxes = true;
            this.CheckedListBoxControl.CheckOnClick = true;
            this.CheckedListBoxControl.SelectionMode = SelectionMode.One;
        }

        /// <summary>
        /// Gets the control embedded in the menu
        /// </summary>
        public CheckedListBox CheckedListBoxControl {
            get {
                return Control as CheckedListBox;
            }
        }

        /// <summary>
        /// Gets the items shown in the checkedlistbox
        /// </summary>
        public CheckedListBox.ObjectCollection Items {
            get {
                return this.CheckedListBoxControl.Items;
            }
        }

        /// <summary>
        /// Gets or sets whether an item should be checked when it is clicked
        /// </summary>
        public bool CheckedOnClick {
            get {
                return this.CheckedListBoxControl.CheckOnClick;
            }
            set {
                this.CheckedListBoxControl.CheckOnClick = value;
            }
        }

        /// <summary>
        /// Gets a collection of the checked items
        /// </summary>
        public CheckedListBox.CheckedItemCollection CheckedItems {
            get {
                return this.CheckedListBoxControl.CheckedItems;
            }
        }

        /// <summary>
        /// Add a possibly checked item to the control
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isChecked"></param>
        public void AddItem(object item, bool isChecked) {
            this.Items.Add(item);
            if (isChecked)
                this.CheckedListBoxControl.SetItemChecked(this.Items.Count - 1, true);
        }

        /// <summary>
        /// Add an item with the given state to the control
        /// </summary>
        /// <param name="item"></param>
        /// <param name="state"></param>
        public void AddItem(object item, CheckState state) {
            this.Items.Add(item);
            this.CheckedListBoxControl.SetItemCheckState(this.Items.Count - 1, state);
        }

        /// <summary>
        /// Gets the checkedness of the i'th item
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public CheckState GetItemCheckState(int i) {
            return this.CheckedListBoxControl.GetItemCheckState(i);
        }

        /// <summary>
        /// Set the checkedness of the i'th item
        /// </summary>
        /// <param name="i"></param>
        /// <param name="checkState"></param>
        public void SetItemState(int i, CheckState checkState) {
            if (i >= 0 && i < this.Items.Count)
                this.CheckedListBoxControl.SetItemCheckState(i, checkState);
        }

        /// <summary>
        /// Check all the items in the control
        /// </summary>
        public void CheckAll() {
            for (int i = 0; i < this.Items.Count; i++)
                this.CheckedListBoxControl.SetItemChecked(i, true);
        }

        /// <summary>
        /// Unchecked all the items in the control
        /// </summary>
        public void UncheckAll() {
            for (int i = 0; i < this.Items.Count; i++)
                this.CheckedListBoxControl.SetItemChecked(i, false);
        }

        #region Events

        /// <summary>
        /// Listen for events on the underlying control
        /// </summary>
        /// <param name="c"></param>
        protected override void OnSubscribeControlEvents(Control c) {
            base.OnSubscribeControlEvents(c);

            CheckedListBox control = (CheckedListBox)c;
            control.ItemCheck += new ItemCheckEventHandler(OnItemCheck);
        }

        /// <summary>
        /// Stop listening for events on the underlying control
        /// </summary>
        /// <param name="c"></param>
        protected override void OnUnsubscribeControlEvents(Control c) {
            base.OnUnsubscribeControlEvents(c);

            CheckedListBox control = (CheckedListBox)c;
            control.ItemCheck -= new ItemCheckEventHandler(OnItemCheck);
        }

        /// <summary>
        /// Tell the world that an item was checked
        /// </summary>
        public event ItemCheckEventHandler ItemCheck;

        /// <summary>
        /// Trigger the ItemCheck event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCheck(object sender, ItemCheckEventArgs e) {
            if (ItemCheck != null) {
                ItemCheck(this, e);
            }
        }

        #endregion
    }
}

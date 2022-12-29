using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DevGrep.Controls.DLV
{

    #region Delegate declarations

    /// <summary>
    /// These delegates are used to extract an aspect from a row object
    /// </summary>
    public delegate Object AspectGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to put a changed value back into a model object
    /// </summary>
    public delegate void AspectPutterDelegate(Object rowObject, Object newValue);

    /// <summary>
    /// These delegates can be used to convert an aspect value to a display string,
    /// instead of using the default ToString()
    /// </summary>
    public delegate string AspectToStringConverterDelegate(Object value);

    /// <summary>
    /// These delegates are used to get the tooltip for a cell
    /// </summary>
    public delegate String CellToolTipGetterDelegate(OLVColumn column, Object modelObject);

    /// <summary>
    /// These delegates are used to the state of the checkbox for a row object.
    /// </summary>
    /// <remarks><para>
    /// For reasons known only to someone in Microsoft, we can only set
    /// a boolean on the ListViewItem to indicate it's "checked-ness", but when
    /// we receive update events, we have to use a tristate CheckState. So we can
    /// be told about an indeterminate state, but we can't set it ourselves.
    /// </para>
    /// <para>As of version 2.0, we can now return indeterminate state.</para>
    /// </remarks>
    public delegate CheckState CheckStateGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to get the state of the checkbox for a row object.
    /// </summary>
    /// <param name="rowObject"></param>
    /// <returns></returns>
    public delegate bool BooleanCheckStateGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to put a changed check state back into a model object
    /// </summary>
    public delegate CheckState CheckStatePutterDelegate(Object rowObject, CheckState newValue);

    /// <summary>
    /// These delegates are used to put a changed check state back into a model object
    /// </summary>
    /// <param name="rowObject"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    public delegate bool BooleanCheckStatePutterDelegate(Object rowObject, bool newValue);

    /// <summary>
    /// The callbacks for RightColumnClick events
    /// </summary>
    public delegate void ColumnRightClickEventHandler(object sender, ColumnClickEventArgs e);

    /// <summary>
    /// This delegate will be used to own draw header column.
    /// </summary>
    public delegate bool HeaderDrawingDelegate(Graphics g, Rectangle r, int columnIndex, OLVColumn column, bool isPressed, HeaderStateStyle stateStyle);

    /// <summary>
    /// This delegate is called when a group has been created but not yet made
    /// into a real ListViewGroup. The user can take this opportunity to fill
    /// in lots of other details about the group.
    /// </summary>
    public delegate void GroupFormatterDelegate(OLVGroup group, GroupingParameters parms);

    /// <summary>
    /// These delegates are used to retrieve the object that is the key of the group to which the given row belongs.
    /// </summary>
    public delegate Object GroupKeyGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to convert a group key into a title for the group
    /// </summary>
    public delegate string GroupKeyToTitleConverterDelegate(Object groupKey);

    /// <summary>
    /// These delegates are used to get the tooltip for a column header
    /// </summary>
    public delegate String HeaderToolTipGetterDelegate(OLVColumn column);

    /// <summary>
    /// These delegates are used to fetch the image selector that should be used
    /// to choose an image for this column.
    /// </summary>
    public delegate Object ImageGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to draw a cell
    /// </summary>
    public delegate bool RenderDelegate(EventArgs e, Graphics g, Rectangle r, Object rowObject);

    /// <summary>
    /// These delegates are used to fetch a row object for virtual lists
    /// </summary>
    public delegate Object RowGetterDelegate(int rowIndex);

    /// <summary>
    /// These delegates are used to format a listviewitem before it is added to the control.
    /// </summary>
    public delegate void RowFormatterDelegate(OLVListItem olvItem);

    /// <summary>
    /// These delegates are used to sort the listview in some custom fashion
    /// </summary>
    public delegate void SortDelegate(OLVColumn column, SortOrder sortOrder);

    #endregion
}

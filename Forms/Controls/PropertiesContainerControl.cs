using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Scene;

namespace SceneEditor.Forms.Controls
{
  partial class PropertiesContainerControl : UserControl
  {
    #region Constructors
    
    public PropertiesContainerControl()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public PropertiesContainer PropertiesContainer
    {
      set
      {
        this.Controls.Clear();
        if(value != null)
        {
          int x = 0;
          x = AddEditControls(MARGIN, value.Properties);
          x = AddEditControls(x + OFFSET_X, value.UserProperties);
        }
        
        Size size = new Size();
        foreach(Control child in this.Controls)
        {
          int x = child.Location.X + child.Width;
          int y = child.Location.Y + child.Height;
          size.Width = Math.Max(x, size.Width);
          size.Height = Math.Max(y, size.Height);
        }
        
        this.Size = size;
      }
    }
    
    #endregion
    
    #region Private methods
    
    private int AddEditControls(int x, Dictionary<string, IProperty> properties)
    {
      int y = MARGIN;
      int maxX = x;
      if(properties != null)
      {
        foreach(KeyValuePair<string, IProperty> kvp in properties)
        {
          Control editControl = kvp.Value.CreateEditControl(kvp.Key);
          this.Controls.Add(editControl);
          editControl.Location = new Point(x, y);
          y = editControl.Location.Y + editControl.Height + OFFSET_Y;
          maxX = Math.Max(maxX, editControl.Location.X + editControl.Width);
        }
      }
      
      return maxX;
    }
    
    #endregion
    
    #region Private constants
    
    private static int MARGIN   = 10;
    private static int OFFSET_X = 10;
    private static int OFFSET_Y = 0;
    
    #endregion
  }
}

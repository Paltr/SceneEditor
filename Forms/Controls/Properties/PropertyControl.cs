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
  partial class PropertyControl : UserControl
  {
    #region Constructors
    
    public PropertyControl()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Protected methods
    
    protected IProperty Property
    {
      get { return m_Property; }
      set
      {
        if(this.Property != value)
        {
          if(this.Property != null)
          {
            this.Property.ValueChanged -= this.OnPropertyValueChanged;
          }
          
          m_Property = value;
          if(this.Property != null)
          {
            this.Property.ValueChanged += this.OnPropertyValueChanged;
          }
        }
      }
    }
    
    #endregion
    
    #region Protected virtual methods
    
    protected virtual void UpdatePropertyControl()
    {
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnPropertyValueChanged(IProperty sender)
    {
      UpdatePropertyControl();
    }
    
    #endregion
    
    #region Private data
    
    private IProperty m_Property;
    
    #endregion
  }
}

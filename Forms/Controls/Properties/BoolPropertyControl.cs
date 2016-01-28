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
  partial class BoolPropertyControl : PropertyControl
  {
    #region Constructors
    
    public BoolPropertyControl()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public void SetProperty(string label, BoolProperty property)
    {
      m_Property = property;
      this.Property = property;
      m_CheckBox.Text = label;
      UpdatePropertyControl();
    }
    
    #endregion
    
    #region Protected overridden methods
    
    protected override void UpdatePropertyControl()
    {
      if(this.Property != null)
      {
        m_CheckBox.Checked = m_Property.Value;
      }
    }
    
    #endregion
    
    #region Private data
    
    private BoolProperty m_Property;
    
    #endregion
  }
}

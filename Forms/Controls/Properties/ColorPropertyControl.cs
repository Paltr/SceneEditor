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
  partial class ColorPropertyControl : PropertyControl
  {
    public ColorPropertyControl()
    {
      InitializeComponent();
      m_ColorInputBox.CustomValueChecker = delegate(string str)
      {
        if(m_Property != null)
        {
          return m_Property.TrySetValue(str);
        }
        
        return null;
      };
    }

    #region Public methods
    
    public void SetProperty(string label, ColorProperty property)
    {
      m_Property = property;
      this.Property = property;
      m_Label.Text = label + ":";
      UpdatePropertyControl();
    }
    
    #endregion

    #region Protected overridden methods
    
    protected override void UpdatePropertyControl()
    {
      if(this.Property != null)
      {
        m_ColorButton.BackColor = m_Property.Value;
        m_ColorInputBox.Text = m_Property.ToString();
      }
    }
    
    #endregion

    #region Private event handlers
    
    private void OnColorClicked(object sender, EventArgs e)
    {
      if(this.Property != null)
      {
        ColorDialog form = new ColorDialog();
        form.Color = m_Property.Value;
        if(form.ShowDialog() == DialogResult.OK)
        {
          m_Property.Value = form.Color;
          UpdatePropertyControl();
        }
      }
    }

    #endregion

    #region Private data
    
    private ColorProperty m_Property;
    
    #endregion
  }
}

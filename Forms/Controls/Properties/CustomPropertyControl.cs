using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls.Controls;
using SceneEditor.Scene;

namespace SceneEditor.Forms.Controls
{
  partial class CustomPropertyControl : PropertyControl
  {
    #region Constructors
    
    public CustomPropertyControl()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public void SetProperty(string label, StringProperty property)
    {
      m_InputBox.InputCheckerType = InputChecker.VOID;
      SetPropertyBase(label, property);
    }
    
    public void SetProperty(string label, IntProperty property)
    {
      m_InputBox.InputCheckerType = InputChecker.INT;
      SetPropertyBase(label, property);
    }
    
    public void SetProperty(string label, FloatProperty property)
    {
      m_InputBox.InputCheckerType = InputChecker.FLOAT;
      SetPropertyBase(label, property);
    }
    
    public void SetProperty(string label, Vector2Property property)
    {
      m_InputBox.InputCheckerType = InputChecker.VECTOR2;
      SetPropertyBase(label, property);
    }
    
    #endregion
    
    #region Protected overridden methods
    
    protected override void UpdatePropertyControl()
    {
      if(this.Property != null)
      {
        m_InputBox.Text = this.Property.ToString();
      }
    }
    
    #endregion
    
    #region Private methods
    
    private void SetPropertyBase(string label, IProperty property)
    {
      this.Property = property;
      m_Label.Text = label + ":";
      UpdatePropertyControl();
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnValueSubmitted(InputBox sender, string oldValue)
    {
      if(this.Property != null)
      {
        string errorStr = this.Property.TrySetValue(m_InputBox.Text);
        if(errorStr != null)
        {
          m_InputBox.Text = oldValue;
          MessageBox.Show("Value Check Error: " + errorStr, "Invalid value",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
    
    #endregion
  }
}

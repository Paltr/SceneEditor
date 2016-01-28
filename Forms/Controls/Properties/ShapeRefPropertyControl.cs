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
using SceneEditor.Forms.Interfaces;

namespace SceneEditor.Forms.Controls
{
  partial class ShapeRefPropertyControl : PropertyControl
  {
    #region Constructors
    
    public ShapeRefPropertyControl()
    {
      InitializeComponent();
      m_RefPathInputBox.CustomValueChecker = delegate(string str)
      {
        if(m_Property != null)
        {
          return m_Property.TrySetValue(str);
        }
        
        return null;
      };
    }
    
    #endregion
    
    #region Public methods
    
    public void SetProperty(string label, ShapeRefProperty property)
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
        m_RefPathInputBox.Text = this.Property.ToString();
      }
      else
      {
        m_RefPathInputBox.Text = "";
      }
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnTargetBtnClicked(object sender, EventArgs e)
    {
      Solution.Instance.Editor.StartShapeSelection(this.OnShapeSelected);
    }
    
    private void OnShapeSelected(IEditor sender, Shape shape)
    {
      m_Property.Value = shape;
    }
    
    private void OnRefPathSubmitted(InputBox sender, string oldValue)
    {
      m_Property.TrySetValue(sender.Text);
    }
    
    #endregion
    
    #region Private data
    
    private ShapeRefProperty m_Property;
    
    #endregion
  }
}

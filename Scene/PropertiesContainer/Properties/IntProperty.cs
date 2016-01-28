using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Scene
{
  class IntProperty : IProperty
  {
    #region Constructors
    
    public IntProperty()
    {
    }
    
    public IntProperty(int value)
    {
      m_Value = value;
    }
    
    #endregion
    
    #region Public overridden methods

    public override string ToString()
    {
      return m_Value.ToString();
    }
    
    #endregion
    
    #region IProperty interface implementation
    
    public void Prepare()
    {
    }
    
    public IProperty Clone()
    {
      return new IntProperty(m_Value);
    }
    
    public Control CreateEditControl(string label)
    {
      CustomPropertyControl editControl = new CustomPropertyControl();
      editControl.SetProperty(label, this);
      return editControl;
    }
    
    public string TrySetValue(string value)
    {
      if(int.TryParse(value, out m_Value))
      {
        if(this.ValueChanged != null)
        {
          this.ValueChanged(this);
        }
        
        return null;
      }
      else
      {
        return value + " isn't a int value";
      }
    }
    
    public event IPropertyValueChangedHandler ValueChanged;
    
    #endregion
    
    #region Private data
    
    private int m_Value;
    
    #endregion
  }
}

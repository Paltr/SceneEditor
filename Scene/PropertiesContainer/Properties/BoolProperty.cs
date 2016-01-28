using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Scene
{
  class BoolProperty : IProperty
  {
    #region Constructors
    
    public BoolProperty()
    {
    }
    
    public BoolProperty(bool value)
    {
      m_Value = value;
    }
    
    #endregion
    
    #region Public methods
    
    public bool Value
    {
      get { return m_Value; }
      set { TrySetValue(value.ToString()); }
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
      return new BoolProperty(m_Value);
    }
    
    public Control CreateEditControl(string label)
    {
      BoolPropertyControl editControl = new BoolPropertyControl();
      editControl.SetProperty(label, this);
      return editControl;
    }
    
    public string TrySetValue(string value)
    {
      if(bool.TryParse(value, out m_Value))
      {
        if(this.ValueChanged != null)
        {
          this.ValueChanged(this);
        }
        
        return null;
      }
      else
      {
        return value + " isn't a bool value";
      }
    }
    
    public event IPropertyValueChangedHandler ValueChanged;
    
    #endregion
    
    #region Private data
    
    private bool m_Value;
    
    #endregion
  }
}

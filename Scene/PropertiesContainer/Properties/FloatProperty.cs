using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Scene
{
  class FloatProperty : IProperty
  {
    #region Constructors
    
    public FloatProperty()
    {
    }
    
    public FloatProperty(float value)
    {
      m_Value = value;
    }
    
    #endregion
    
    #region Public methods
    
    public float Value
    {
      get { return m_Value; }
      set
      {
        if(this.Value != value)
        {
          m_Value = value;
          if(this.ValueChanged != null)
          {
            this.ValueChanged(this);
          }
        }
      }
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
      return new FloatProperty(m_Value);
    }
    
    public Control CreateEditControl(string label)
    {
      CustomPropertyControl editControl = new CustomPropertyControl();
      editControl.SetProperty(label, this);
      return editControl;
    }
    
    public virtual string TrySetValue(string value)
    {
      float temp;
      if(float.TryParse(value, out temp))
      {
        this.Value = temp;
        return null;
      }
      else
      {
        return value + " isn't a float value";
      }
    }
    
    public event IPropertyValueChangedHandler ValueChanged;
    
    #endregion
    
    #region Private data
    
    private float m_Value;
    
    #endregion
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Math;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Scene
{
  class Vector2Property : IProperty
  {
    #region Constructors
    
    public Vector2Property()
    {
    }
    
    public Vector2Property(Vector2f value)
    {
      m_Value = value;
    }
    
    #endregion
    
    #region Public methods
    
    public Vector2f Value
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
      return new Vector2Property(m_Value);
    }
    
    public Control CreateEditControl(string label)
    {
      CustomPropertyControl editControl = new CustomPropertyControl();
      editControl.SetProperty(label, this);
      return editControl;
    }
    
    public virtual string TrySetValue(string value)
    {
      Vector2f temp;
      if(Vector2f.TryParse(value, out temp))
      {
        this.Value = temp;
        return null;
      }
      else
      {
        return value + " isn't a Vector2f value";
      }
    }
    
    public event IPropertyValueChangedHandler ValueChanged;
    
    #endregion
    
    #region Private data
    
    private Vector2f m_Value;
    
    #endregion
  }
}

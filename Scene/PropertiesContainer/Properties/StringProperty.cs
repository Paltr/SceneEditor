using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Scene
{
  class StringProperty : IProperty
  {
    #region Constructors
    
    public StringProperty()
      : this("")
    {
    }
    
    public StringProperty(string value)
    {
      m_Value = value;
    }
    
    #endregion
    
    #region Public methods
    
    public string Value
    {
      get { return m_Value; }
      set
      {
        if(value == null)
        {
          throw new ArgumentNullException();
        }
        
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
      return new StringProperty(m_Value);
    }
    
    public Control CreateEditControl(string label)
    {
      CustomPropertyControl editControl = new CustomPropertyControl();
      editControl.SetProperty(label, this);
      return editControl;
    }
    
    public virtual string TrySetValue(string value)
    {
      if(value == null)
      {
        return "StringProperty cannot have null value";
      }
      
      this.Value = value;
      return null;
    }
    
    public event IPropertyValueChangedHandler ValueChanged;
    
    #endregion
    
    #region Private data
    
    private string m_Value;
    
    #endregion
  }
}

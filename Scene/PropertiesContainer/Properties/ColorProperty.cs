using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SceneEditor.Forms.Controls;
using Util.Extensions;

namespace SceneEditor.Scene
{
  class ColorProperty : IProperty
  {
    #region Constructors
    
    public ColorProperty()
    {
    }

    public ColorProperty(Color value)
    {
      m_Value = value;
    }

    #endregion

    #region Public methods
    
    public virtual Color Value
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

    #region Public overriden methods

    public override string ToString()
    {
      return ColorExt.Encode(m_Value);
    }
    
    #endregion

    #region IProperty interface implementation

    public void Prepare()
    {
    }

    public IProperty Clone()
    {
      return new ColorProperty(m_Value);
    }

    public Control CreateEditControl(string label)
    {
      ColorPropertyControl editControl = new ColorPropertyControl();
      editControl.SetProperty(label, this);
      return editControl;
    }

    public virtual string TrySetValue(string value)
    {
      Color temp;
      if(ColorExt.TryDecode(value, out temp))
      {
        m_Value = temp;
        return null;
      }
      else
      {
        return "Invalid color format";
      }
    }
    
    public event IPropertyValueChangedHandler ValueChanged;

    #endregion

    #region Private data
    
    private Color m_Value;

    #endregion
  }
}

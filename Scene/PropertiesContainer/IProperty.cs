using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SceneEditor.Scene
{
  delegate void IPropertyValueChangedHandler(IProperty sender);
  
  interface IProperty
  {
    void Prepare();
    IProperty Clone();
    Control CreateEditControl(string label);
    string TrySetValue(string value);
    
    event IPropertyValueChangedHandler ValueChanged;
  }
}

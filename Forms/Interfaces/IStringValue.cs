using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Forms.Interfaces
{
  interface IStringValue
  {
    string Value { get; set; }
    string CheckValue(string value);
  }
}

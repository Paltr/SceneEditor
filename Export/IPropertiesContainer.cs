using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Export
{
  public interface IPropertiesContainer
  {
    string Name { get; }
    IDictionary<string, string> Properties { get; }
  }
}

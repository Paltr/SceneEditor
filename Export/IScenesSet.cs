using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Export
{
  public interface IScenesSet
  {
    List<IScene> Scenes { get; }
  }
}

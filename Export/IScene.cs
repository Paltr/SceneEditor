using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;

namespace SceneEditor.Export
{
  public interface IScene : IPropertiesContainer
  {
    Vector2f Size { get; }
    List<IShape> Shapes { get; }
  }
}

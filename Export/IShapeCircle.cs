using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;

namespace SceneEditor.Export
{
  public interface IShapeCircle
  {
    Vector2f Position { get; }
    float Angle { get; }
  }
}

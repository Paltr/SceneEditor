using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;

namespace SceneEditor.Export
{
  public interface IShape : IPropertiesContainer
  {
    IScene Scene { get; }
    string TemplateName { get; }
    Vector2f Position { get; }
    float Angle { get; }
    List<IShapeCircle> Circles { get; }
  }
}

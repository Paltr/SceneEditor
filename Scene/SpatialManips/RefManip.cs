using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Math;
using Util.Spatial;
using Util.Extensions;
using GLRenderer;
using GLRenderer.Controls;

namespace SceneEditor.Scene
{
  class RefManip : PivotManip
  {
    #region Contructors
    
    public RefManip(ShapeCircle refCircle, ISceneView sceneView)
      : base(refCircle, sceneView)
    {
    }
    
    #endregion
    
    #region Public overridden methods

    public override string Name
    {
      get
      {
        if(this.ShapeCircle.Shape == null)
        {
          return "Unnamed reference";
        }
        else
        {
          return this.ShapeCircle.Shape.Name + " reference";
        }
      }
    }
    
    #endregion
    
    #region Protected overridden methods
    
    protected override void TrySetPosition(Vector2f position)
    {
      List<Vector2f> childPositions = new List<Vector2f>();
      foreach(ShapeCircle child in this.ShapeCircle.Children)
      {
        childPositions.Add(child.Position);
      }
      
      this.ShapeCircle.Position = position;
      int index = 0;
      foreach(ShapeCircle child in this.ShapeCircle.Children)
      {
        child.Position = childPositions[index];
        ++index;
      }
    }
    
    #endregion
  }
}

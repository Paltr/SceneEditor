using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Util.Math;
using Util.Spatial;
using Util.Extensions;
using GLRenderer;

namespace SceneEditor.Scene
{
  sealed class CircleTemplate : ShapeTemplate
  {
    #region Constructors

    public CircleTemplate(string name, string propertiesFilepath)
      : base(name, propertiesFilepath)
    {
      m_RootCircle = new ShapeCircle(this, new Transform(), (ISceneView)null);
      
      Transform radiusVectorTransform = new Transform();
      radiusVectorTransform.Position = new Vector2f(-100.0f, 0.0f);
      new ShapeCircle(m_RootCircle, radiusVectorTransform);
      
      m_ShapeCircleSettingsList = new List<ShapeCircleSettings>();
      m_ShapeCircleSettingsList.Resize(m_RootCircle.AllCircles.Count);
    }
    
    public CircleTemplate(CircleTemplate circleTemplate)
      : base(circleTemplate)
    {
      m_RootCircle = circleTemplate.CloneRootCircle(this);
      this.Solid = circleTemplate.Solid;
      m_ShapeCircleSettingsList = new List<ShapeCircleSettings>(circleTemplate.m_ShapeCircleSettingsList);
    }

    #endregion

    #region Public methods

    public bool Solid { get; set; }
    
    #endregion

    #region Public overridden methods

    public override ShapeCircle RootCircle
    {
      get { return m_RootCircle; }
    }
    
    public override ShapeTemplate Clone()
    {
      return new CircleTemplate(this);
    }
    
    public override bool TryTouch(Shape shape, Vector2f position, bool selected)
    {
      Circle circle = GetShapeCircle(shape);
      if(circle.CheckPointInside(position))
      {
        return true;
      }
      
      if(selected)
      {
        return TryTouchManip(shape, position, selected);
      }
      
      return false;
    }
    
    public override IList<ShapeCircleSettings> PerCircleSettings
    {
      get { return m_ShapeCircleSettingsList; }
    }
    
    public override List<SpatialManip> GetManips(Shape shape)
    {
      List<SpatialManip> manips = new List<SpatialManip>();
      ShapeCircle root = shape.RootCircle;
      ShapeCircle radiusVectorCircle = GetRadiusVectorCircle(root);
      Dictionary<ShapeCircle, ShapeCircleSettings> shapeSettings = GetCirclesSettingsMap(shape.RootCircle);
      RequestAddManip(manips, shapeSettings, shape, radiusVectorCircle);
      return manips;
    }
    
    public override void Render(Shape shape, Renderer renderer, bool selected)
    {
      Circle circle = GetShapeCircle(shape);
      renderer.PushPen();
      Pen pen = new Pen(shape.Color);
      renderer.Pen = pen;
      if(this.Solid)
      {
        renderer.FillCircle(circle);
      }
      else
      {
        pen.Width = SceneConstants.PenWidth;
        renderer.DrawCircle(circle);
      }

      renderer.PopPen();
    }
    
    public override Rect2f EditRect
    {
      get
      {
        Shape shape = CreateEditShape();
        return Rect2f.CreateBoundingRect(GetShapeCircle(shape));
      }
    }

    #endregion

    #region Private methods

    private Circle GetShapeCircle(Shape shape)
    {
      ShapeCircle root = shape.RootCircle;
      ShapeCircle radiusVectorCircle = GetRadiusVectorCircle(root);
      Vector2f center = root.Position;
      return new Circle(center, Vector2f.Distance(center, radiusVectorCircle.Position));
    }

    private ShapeCircle GetRadiusVectorCircle(ShapeCircle root)
    {
      return root.Children[0];
    }
    
    #endregion

    #region Private data

    private readonly ShapeCircle m_RootCircle;
    private readonly List<ShapeCircleSettings> m_ShapeCircleSettingsList;

    #endregion
  }
}

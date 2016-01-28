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
  class RectTemplate : ShapeTemplate
  {
    #region Constructors
    
    public RectTemplate(string name, string propertiesFilepath)
      : base(name, propertiesFilepath)
    {
      m_RootCircle = new ShapeCircle(this, new Transform(), (ISceneView)null);
      
      Transform leftBottomTransform = new Transform();
      leftBottomTransform.Position = new Vector2f(-100.0f, -100.0f);
      new ShapeCircle(m_RootCircle, leftBottomTransform);
      
      Transform rightTopTransform = new Transform();
      rightTopTransform.Position = new Vector2f(100.0f, 100.0f);
      new ShapeCircle(m_RootCircle, rightTopTransform);
      
      m_ShapeCircleSettingsList = new List<ShapeCircleSettings>();
      m_ShapeCircleSettingsList.Resize(m_RootCircle.AllCircles.Count);
    }
    
    public RectTemplate(RectTemplate rectTemplate)
      : base(rectTemplate)
    {
      m_RootCircle = rectTemplate.CloneRootCircle(this);
      m_ShapeCircleSettingsList = new List<ShapeCircleSettings>(rectTemplate.m_ShapeCircleSettingsList);
      this.Normalized = rectTemplate.Normalized;
    }
    
    #endregion
    
    #region Public methods
    
    public bool Normalized
    {
      get { return m_Normalized; }
      set
      {
        if(this.Normalized != value)
        {
          m_Normalized = value;
          if(this.Normalized)
          {
            Normalize();
          }
        }
      }
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override ShapeCircle RootCircle
    {
      get { return m_RootCircle; }
    }
    
    public override ShapeTemplate Clone()
    {
      return new RectTemplate(this);
    }
    
    public override bool TryTouch(Shape shape, Vector2f position, bool selected)
    {
      Quad2f shapeQuad = GetShapeQuad(shape);
      if(Helpers.CheckPointInside(position, shapeQuad.Vertices))
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
      ShapeCircle leftBottomCircle = GetLeftBottomCircle(root);
      ShapeCircle rightTopCircle = GetRightTopCircle(root);
      Dictionary<ShapeCircle, ShapeCircleSettings> shapeSettings = GetCirclesSettingsMap(shape.RootCircle);
      RequestAddManip(manips, shapeSettings, shape, leftBottomCircle);
      RequestAddManip(manips, shapeSettings, shape, rightTopCircle);
      return manips;
    }
    
    public override void Render(Shape shape, Renderer renderer, bool selected)
    {
      renderer.PushPen();
      Pen pen = new Pen(shape.Color);
      pen.Width = SceneConstants.PenWidth;
      renderer.Pen = pen;
      Quad2f quad = GetShapeQuad(shape);
      renderer.DrawLine(quad.Vertices, true);
      renderer.PopPen();
    }
    
    public override Rect2f EditRect
    {
      get
      {
        Shape shape = CreateEditShape();
        return GetShapeQuad(shape).BoundingRect;
      }
    }
    
    #endregion
    
    #region Protected methods
    
    protected ShapeCircle GetLeftBottomCircle(ShapeCircle root)
    {
      return root.Children[0];
    }
    
    protected ShapeCircle GetRightTopCircle(ShapeCircle root)
    {
      return root.Children[1];
    }
    
    protected Vector2f Size
    {
      get
      {
        ShapeCircle root = this.RootCircle;
        ShapeCircle leftBottomCircle = GetLeftBottomCircle(root);
        ShapeCircle rightTopCircle = GetRightTopCircle(root);
        Vector2f leftBottomOffset = (leftBottomCircle.Position - root.Position).Rotate(-root.Angle);
        Vector2f rightTopOffset = (rightTopCircle.Position - root.Position).Rotate(-root.Angle);
        return (rightTopOffset - leftBottomOffset);
      }
      
      set
      {
        Vector2f halfSize = value / 2.0f;
        ShapeCircle root = this.RootCircle;
        ShapeCircle leftBottomCircle = GetLeftBottomCircle(root);
        ShapeCircle rightTopCircle = GetRightTopCircle(root);
        leftBottomCircle.Transform.Position = -halfSize;
        rightTopCircle.Transform.Position = halfSize;
      }
    }
    
    protected Quad2f GetShapeQuad(Shape shape)
    {
      ShapeCircle root = shape.RootCircle;
      ShapeCircle leftBottomCircle = GetLeftBottomCircle(root);
      ShapeCircle rightTopCircle = GetRightTopCircle(root);
      Vector2f leftBottomOffset = (leftBottomCircle.Position - root.Position).Rotate(-shape.Angle);
      Vector2f rightTopOffset = (rightTopCircle.Position - root.Position).Rotate(-shape.Angle);
      Vector2f rectLeftBottom = root.Position + leftBottomOffset;
      Vector2f rightTopBottom = root.Position + rightTopOffset;
      Rect2f rect = new Rect2f(rectLeftBottom, rightTopBottom - rectLeftBottom).Normalize();
      return rect.Rotate(root.Position, shape.Angle);
    }
    
    #endregion
    
    #region Protected overridden methods
    
    protected override void ApplyPosition(ShapeCircle shapeCircle, Vector2f position)
    {
      if(this.Normalized)
      {
        ShapeCircle root = shapeCircle.Root;
        ShapeCircle leftBottomCircle = GetLeftBottomCircle(root);
        ShapeCircle rightTopCircle = GetRightTopCircle(root);
        shapeCircle.Position = position;
        if(shapeCircle == leftBottomCircle || shapeCircle == rightTopCircle)
        {
          Vector2f leftBottomPosition = leftBottomCircle.Position;
          Vector2f rightTopPosition = rightTopCircle.Position;
          
          root.Position = (leftBottomPosition + rightTopPosition) / 2.0f;
          leftBottomCircle.Position = leftBottomPosition;
          rightTopCircle.Position = rightTopPosition;
        }
      }
      else
      {
        base.ApplyPosition(shapeCircle, position);
      }
    }
    
    #endregion
    
    #region Private methods
    
    private void Normalize()
    {
    }
    
    #endregion
    
    #region Private data
    
    private readonly ShapeCircle m_RootCircle;
    private readonly List<ShapeCircleSettings> m_ShapeCircleSettingsList;
    private bool m_Normalized;
    
    #endregion
  }
}

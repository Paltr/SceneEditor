using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Util.Math;
using GLRenderer;

namespace SceneEditor.Scene
{
  abstract class ShapeTemplate : ShapeCircle.IOwner
  {
    #region Constructors
    
    public ShapeTemplate(string name, string propertiesFilepath)
    {
      m_Name = name;
      m_PropertiesFilepath = propertiesFilepath;
      this.Color = Color.Black;
    }
    
    public ShapeTemplate(ShapeTemplate shapeTemplate)
      : this(shapeTemplate.Name, shapeTemplate.PropertiesFilepath)
    {
      this.Backgroud = shapeTemplate.Backgroud;
      this.Color = shapeTemplate.Color;
      this.EditableColor = shapeTemplate.EditableColor;
    }
    
    #endregion
    
    #region Public types
    
    public class ShapeCircleSettings
    {
      public ShapeCircleSettings()
      {
        this.EnableOffset = true;
        this.EnableRotate = true;
      }
      
      public bool EnableOffset
      {
        get { return m_EnableOffset; }
        set { m_EnableOffset = value; }
      }
      
      public bool EnableRotate
      {
        get { return m_EnableRotate; }
        set { m_EnableRotate = value; }
      }
      
      private bool m_EnableOffset;
      private bool m_EnableRotate;
    }
    
    #endregion
    
    #region Public methods
    
    public string Name
    {
      get { return m_Name; }
      set { m_Name = value; }
    }
    
    public string PropertiesFilepath
    {
      get { return m_PropertiesFilepath; }
      set { m_PropertiesFilepath = value; }
    }
    
    public bool Backgroud
    {
      get { return m_Backgroud; }
      set { m_Backgroud = value; }
    }
    
    public Color Color
    {
      get { return m_Color; }
      set { m_Color = value; }
    }
    
    public bool EditableColor { get; set; }

    public Vector2f Anchor
    {
      get { return this.RootCircle.Position; }
      set { this.RootCircle.Position = value; }
    }
    
    public Dictionary<ShapeCircle, ShapeCircleSettings> GetCirclesSettingsMap(ShapeCircle circle)
    {
      Dictionary<ShapeCircle, ShapeCircleSettings> result =
        new Dictionary<ShapeCircle, ShapeCircleSettings>();
      List<ShapeCircle> allCircles = circle.AllCircles;
      IList<ShapeCircleSettings> perCircleSettings = this.PerCircleSettings;
      for(int index = 0; index < perCircleSettings.Count; ++index)
      {
        result.Add(allCircles[index], perCircleSettings[index]);
      }
      
      return result;
    }
    
    public ShapeCircleSettings GetCircleSettings(ShapeCircle shapeCircle)
    {
      Dictionary<ShapeCircle, ShapeCircleSettings> settings = GetCirclesSettingsMap(shapeCircle);
      return settings[shapeCircle];
    }
    
    public Shape CreateEditShape()
    {
      return CreateEditShape(null);
    }
    
    public Shape CreateEditShape(ISceneView sceneView)
    {
      Shape shape = new Shape(sceneView);
      shape.EditTemplateMode = true;
      shape.Template = this;
      return shape;
    }
    
    public void Render(Renderer renderer, Vector2f position)
    {
      Shape shape = new Shape(null);
      shape.Template = this;
      shape.Position = position;
      Render(shape, renderer, false);
    }
    
    #endregion
    
    #region ShapeCircle.IOwner interface implementation
    
    public void TryTranslate(ShapeCircle shapeCircle, Vector2f position)
    {
      EnableFreeform(shapeCircle.Owner.RootCircle, true);
      ApplyPosition(shapeCircle, position);
      EnableFreeform(shapeCircle.Owner.RootCircle, false);
      History.Change();
    }
    
    public void TryResize(ShapeCircle shapeCircle, float radius)
    {
      EnableFreeform(shapeCircle.Owner.RootCircle, true);
      ApplySize(shapeCircle, radius);
      EnableFreeform(shapeCircle.Owner.RootCircle, false);
      History.Change();
    }
    
    public void TryRotate(ShapeCircle shapeCircle, float angle)
    {
      EnableFreeform(shapeCircle.Owner.RootCircle, true);
      ApplyRotation(shapeCircle, angle);
      EnableFreeform(shapeCircle.Owner.RootCircle, false);
      History.Change();
    }
    
    #endregion
    
    #region Public abstract methods
    
    public abstract ShapeCircle RootCircle { get; }
    public abstract ShapeTemplate Clone();
    public abstract bool TryTouch(Shape shape, Vector2f position, bool selected);
    public abstract IList<ShapeCircleSettings> PerCircleSettings { get; }
    public abstract List<SpatialManip> GetManips(Shape shape);
    public abstract void Render(Shape shape, Renderer renderer, bool selected);
    
    #endregion
    
    #region Public virtual methods
    
    public virtual Rect2f EditRect
    {
      get
      {
        Vector2f leftBottom = new Vector2f(float.PositiveInfinity, float.PositiveInfinity);
        Vector2f rightTop = new Vector2f(float.NegativeInfinity, float.NegativeInfinity);
        foreach(ShapeCircle circle in this.RootCircle.AllCircles)
        {
          float radius = Math.Max(SceneConstants.ManipRadius, circle.Radius);
          Vector2f currLeftBottom = circle.Position - new Vector2f(radius, radius);
          Vector2f currRightTop = circle.Position + new Vector2f(radius, radius);
          if(leftBottom.X > currLeftBottom.X)
          {
            leftBottom.X = currLeftBottom.X;
          }
          
          if(leftBottom.Y > currLeftBottom.Y)
          {
            leftBottom.Y = currLeftBottom.Y;
          }
          
          if(rightTop.X < currRightTop.X)
          {
            rightTop.X = currRightTop.X;
          }
          
          if(rightTop.Y < currRightTop.Y)
          {
            rightTop.Y = currRightTop.Y;
          }
        }
      
        return new Rect2f(leftBottom, rightTop - leftBottom);
      }
    }

    public virtual SpatialManip GetSelectionManip(Shape shape)
    {
      if(shape.EditTemplateMode)
      {
        return new RefManip(shape.RootCircle, shape.SceneView);
      }
      else
      {
        PivotManip manip = new PivotManip(shape.RootCircle, shape.SceneView);
        ShapeCircleSettings shapeCircleSettings = GetCircleSettings(shape.RootCircle);
        manip.EnableRotate = shapeCircleSettings.EnableRotate;
        return manip;
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void ChangedHandler(ShapeTemplate sender);
    
    public event ChangedHandler Changed;
    
    #endregion
    
    #region Protected methods
    
    protected ShapeCircle CloneRootCircle(ShapeTemplate owner)
    {
      return CloneRootCircle(owner, this.RootCircle.SceneView);
    }
    
    protected ShapeCircle CloneRootCircle(ShapeTemplate owner, ISceneView sceneView)
    {
      ShapeCircle clone = new ShapeCircle(owner, this.RootCircle.Transform.Clone(), sceneView);
      foreach(ShapeCircle circle in this.RootCircle.Children)
      {
        CloneCircle(circle, clone);
      }
      
      return clone;
    }
    
    private void CloneCircle(ShapeCircle circle, ShapeCircle parent)
    {
      new ShapeCircle(parent, circle.Transform.Clone());
    }

    protected bool TryTouchManip(Shape shape, Vector2f position, bool selected)
    {
      List<SpatialManip> manips = GetManips(shape);
      manips.Add(GetSelectionManip(shape));
      foreach(SpatialManip manip in manips)
      {
        if(manip.TryTouch(position, selected))
        {
          return true;
        }
      }

      return false;
    }
    
    protected void NotifyChanged()
    {
      if(this.Changed != null)
      {
        this.Changed(this);
      }
    }
    
    #endregion
    
    #region Protected virtual methods
    
    protected virtual void ApplyPosition(ShapeCircle shapeCircle, Vector2f position)
    {
      shapeCircle.Position = position;
    }
    
    protected virtual void ApplySize(ShapeCircle shapeCircle, float radius)
    {
      shapeCircle.Radius = radius;
    }
    
    protected virtual void ApplyRotation(ShapeCircle shapeCircle, float angle)
    {
      shapeCircle.Angle = angle;
    }

    protected void RequestAddManip(List<SpatialManip> manips,
      Dictionary<ShapeCircle, ShapeCircleSettings> shapeSettings, Shape shape, ShapeCircle circle)
    {
      ShapeCircleSettings circleSettings = shapeSettings[circle];
      if(shape.EditTemplateMode || circleSettings.EnableOffset || circleSettings.EnableRotate)
      {
        PivotManip manip = new PivotManip(circle, shape.SceneView);
        manips.Add(manip);
        if(!shape.EditTemplateMode)
        {
          manip.EnableOffset = circleSettings.EnableOffset;
          manip.EnableRotate = circleSettings.EnableRotate;
        }
      }
    }
    
    #endregion
    
    #region Private methods
    
    private void EnableFreeform(ShapeCircle rootCircle, bool freeform)
    {
      foreach(ShapeCircle circle in rootCircle.AllCircles)
      {
        circle.Freeform = freeform;
      }
    }
    
    #endregion
    
    #region Private data
    
    private string m_Name;
    private string m_PropertiesFilepath;
    private bool m_Backgroud;
    private Color m_Color;

    #endregion
  }
}

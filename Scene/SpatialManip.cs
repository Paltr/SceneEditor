using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;
using GLRenderer;
using GLRenderer.Controls;

namespace SceneEditor.Scene
{
  abstract class SpatialManip
  {
    #region Constructors
    
    public SpatialManip(ISceneView sceneView)
    {
      m_SceneView = sceneView;
    }
    
    #endregion
    
    #region Public methods
    
    public bool PositionUsed
    {
      get { return !this.Position.CheckInvalid(); }
    }
    
    public Vector2f Position
    {
      get { return GetPosition(); }
      set
      {
        if(this.Position != value && !this.Position.CheckInvalid())
        {
          Vector2f oldValue = this.Position;
          TrySetPosition(value);
          if(this.Position != oldValue)
          {
            NotifyPositionChanged(oldValue);
          }
        }
      }
    }
    
    public bool AngleUsed
    {
      get { return !float.IsNaN(this.Angle); }
    }
    
    public float Angle
    {
      get { return GetAngle(); }
      set
      {
        if(this.Angle != value && !float.IsNaN(this.Angle))
        {
          float oldValue = this.Angle;
          TrySetAngle(value);
          if(this.Angle != oldValue)
          {
            NotifyAngleChanged(oldValue);
          }
        }
      }
    }
    
    #endregion
    
    #region Public abstract methods
    
    public abstract string Name { get; }
    public abstract bool TryTouch(Vector2f position, bool selected);
    public abstract void Prepare();
    
    public abstract void Render(Renderer renderer, bool selected);
    public abstract void HandleMouseDown(GLMouseEvent e);
    public abstract void HandleMouseMove(GLMouseEvent e);
    
    public abstract List<ShapeCircle> ManipShapeCircles { get; }
    
    #endregion
    
    #region Public events
    
    public delegate void PositionChangedHandler(SpatialManip sender, Vector2f oldValue);
    public delegate void AngleChangedHandler(SpatialManip sender, float oldValue);
    
    public event PositionChangedHandler PositionChanged;
    public event AngleChangedHandler AngleChanged;
    
    #endregion
    
    #region Protected virtual methods
    
    protected virtual void TrySetPosition(Vector2f position)
    {
      foreach(ShapeCircle shapeCircle in this.ManipShapeCircles)
      {
        shapeCircle.Position = position;
      }
    }
    
    protected virtual Vector2f GetPosition()
    {
      List<ShapeCircle> shapeCircles = this.ManipShapeCircles;
      if(shapeCircles.Count != 0)
      {
        return shapeCircles[0].Position;
      }
      else
      {
        return Vector2f.Invalid;
      }
    }
    
    protected virtual void TrySetAngle(float angle)
    {
      foreach(ShapeCircle shapeCircle in this.ManipShapeCircles)
      {
        shapeCircle.Angle = angle;
      }
    }
    
    protected virtual float GetAngle()
    {
      List<ShapeCircle> shapeCircles = this.ManipShapeCircles;
      if(shapeCircles.Count != 0)
      {
        return shapeCircles[0].Angle;
      }
      else
      {
        return float.NaN;
      }
    }
    
    #endregion
    
    #region Protected methods
    
    protected void InvalidateView()
    {
      if(m_SceneView != null)
      {
        m_SceneView.Invalidate();
      }
    }
    
    protected Circle GetPositionManipCircle(Vector2f posManipCenter)
    {
      return new Circle(posManipCenter, SceneConstants.ManipRadius);
    }
    
    protected Circle GetPositionManipCircle(ShapeCircle shapeCircle)
    {
      return GetPositionManipCircle(shapeCircle.Position);
    }
    
    protected Circle GetAngleManipCircle(Vector2f posManipCenter, float angle)
    {
      Vector2f center = posManipCenter + new Vector2f(SceneConstants.AngleManipLength, 0.0f).Rotate(angle);
      return new Circle(center, SceneConstants.ManipRadius);
    }
    
    protected Circle GetAngleManipCircle(Circle posManipCircle, float angle)
    {
      return GetAngleManipCircle(posManipCircle.Center, angle);
    }
    
    protected Circle GetAngleManipCircle(ShapeCircle shapeCircle)
    {
      return GetAngleManipCircle(shapeCircle.Position, shapeCircle.Angle);
    }
    
    #endregion
    
    #region Private methods
    
    private void NotifyPositionChanged(Vector2f oldValue)
    {
      if(this.PositionChanged != null)
      {
        this.PositionChanged(this, oldValue);
      }
    }
    
    private void NotifyAngleChanged(float oldValue)
    {
      if(this.AngleChanged != null)
      {
        this.AngleChanged(this, oldValue);
      }
    }
    
    #endregion
    
    #region Private data
    
    private readonly ISceneView m_SceneView;
    
    #endregion
  }
}

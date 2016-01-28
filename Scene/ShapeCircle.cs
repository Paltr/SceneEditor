using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;
using Util.Spatial;
using SceneEditor.Export;

namespace SceneEditor.Scene
{
  class ShapeCircle : IShapeCircle
  {
    #region Public IOwner interface
    
    public interface IOwner
    {
      ShapeCircle RootCircle { get; }
      void TryTranslate(ShapeCircle shapeCircle, Vector2f position);
      void TryResize(ShapeCircle shapeCircle, float radius);
      void TryRotate(ShapeCircle shapeCircle, float angle);
    }
    
    #endregion
    
    #region Constructors
    
    public ShapeCircle(IOwner owner, ITransform transform, ISceneView sceneView)
    {
      m_Owner = owner;
      m_SceneView = sceneView;
      m_Transform = new TransformWrapper(transform, this.SceneView);
      m_Children = new List<ShapeCircle>();
    }
    
    public ShapeCircle(ShapeCircle parent, ITransform transform)
    {
      if(parent != null)
      {
        m_SceneView = parent.SceneView;
        parent.AddChild(this);
      }
      
      m_Owner = parent.Owner;
      m_Parent = parent;
      m_Transform = new TransformWrapper(transform, this.SceneView);
      m_Children = new List<ShapeCircle>();
    }
    
    #endregion
    
    #region Public methods
    
    public Scene Scene
    {
      get { return Solution.Instance.Scenes.FindScene(this); }
    }
    
    public Shape Shape
    {
      get { return Solution.Instance.Scenes.FindShape(this); }
    }
    
    public IOwner Owner
    {
      get { return m_Owner; }
    }
    
    public ISceneView SceneView
    {
      get { return m_SceneView; }
    }
    
    public ShapeCircle Parent
    {
      get { return m_Parent; }
    }
    
    public ITransform Transform
    {
      get { return m_Transform; }
    }
    
    public IList<ShapeCircle> Children
    {
      get { return new List<ShapeCircle>(m_Children); }
    }
    
    public bool Freeform
    {
      get { return m_Freeform; }
      set { m_Freeform = value; }
    }
    
    public Vector2f Position
    {
      get { return TransformMethods.GetPosition(GetTransformIter()); }
      set
      {
        if(this.Freeform)
        {
          TransformMethods.SetPosition(GetTransformIter(), value);
          if(this.PositionChanged != null)
          {
            this.PositionChanged(this);
          }
          
          InvalidateView();
        }
        else
        {
          m_Owner.TryTranslate(this, value);
        }
      }
    }
    
    public float Radius
    {
      get { return m_Radius; }
      set
      {
        if(this.Freeform)
        {
          m_Radius = value;
          InvalidateView();
        }
        else
        {
          m_Owner.TryResize(this, value);
        }
      }
    }
    
    public float Angle
    {
      get { return TransformMethods.GetAngle(GetTransformIter()); }
      set
      {
        if(this.Freeform)
        {
          TransformMethods.SetAngle(GetTransformIter(), value);
          if(this.AngleChanged != null)
          {
            this.AngleChanged(this);
          }
          
          InvalidateView();
        }
        else
        {
          m_Owner.TryRotate(this, value);
        }
      }
    }
    
    public ShapeCircle Root
    {
      get
      {
        ShapeCircle root = this;
        while(root.Parent != null)
        {
          root = root.Parent;
        }
        
        return root;
      }
    }
    
    public List<ShapeCircle> AllCircles
    {
      get
      {
        ShapeCircle root = this.Root;
        List<ShapeCircle> circles = new List<ShapeCircle>();
        circles.Add(root);
        AccumChildCircles(circles, root);
        return circles;
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void PositionChangedHandler(ShapeCircle sender);
    public delegate void AngleChangedHandler(ShapeCircle sender);
    
    public event PositionChangedHandler PositionChanged;
    public event AngleChangedHandler AngleChanged;
    
    #endregion
    
    #region Private types
    
    #region TransformIter
    
    private class TransformIter : ITransformIter
    {
      public TransformIter(ShapeCircle shapeCircle)
      {
        m_ShapeCircle = shapeCircle;
      }
      
      public ITransform Transform
      {
        get { return (m_ShapeCircle != null ? m_ShapeCircle.Transform : null); }
      }
      
      public bool MoveParentTransform()
      {
        if(m_ShapeCircle != null)
        {
          m_ShapeCircle = m_ShapeCircle.Parent;
        }
        
        return m_ShapeCircle != null;
      }
      
      private ShapeCircle m_ShapeCircle;
    }
    
    #endregion
    
    #endregion
    
    #region Private methods
    
    private void AddChild(ShapeCircle child)
    {
      m_Children.Add(child);
    }
     
    private ITransformIter GetTransformIter()
    {
      return new TransformIter(this);
    }
    
    private void AccumChildCircles(List<ShapeCircle> accum, ShapeCircle circle)
    {
      foreach(ShapeCircle childCircle in circle.Children)
      {
        accum.Add(childCircle);
      }
      
      foreach(ShapeCircle childCircle in circle.Children)
      {
        AccumChildCircles(accum, childCircle);
      }
    }
    
    private void InvalidateView()
    {
      if(m_SceneView != null)
      {
        m_SceneView.Invalidate();
      }
    }
    
    #endregion
    
    #region Private data
    
    private readonly IOwner m_Owner;
    private readonly ISceneView m_SceneView;
    private readonly ShapeCircle m_Parent;
    private readonly ITransform m_Transform;
    private readonly List<ShapeCircle> m_Children;
    
    private bool m_Freeform;
    private float m_Radius;
    
    #endregion
  }
}

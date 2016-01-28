using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;
using Util.Spatial;

namespace SceneEditor.Scene
{
  class TransformWrapper : ITransform
  {
    #region Constructors
    
    public TransformWrapper(ITransform transform, ISceneView sceneView)
    {
      m_Transform = transform;
      m_SceneView = sceneView;
    }
    
    #endregion
    
    #region ITransform interface implementation
    
    public ITransform Clone()
    {
      return new TransformWrapper(m_Transform.Clone(), m_SceneView);
    }
    
    public Vector2f Position
    {
      get { return m_Transform.Position; }
      set
      {
        m_Transform.Position = value;
        InvalidateView();
      }
    }
    
    public float Angle
    {
      get { return m_Transform.Angle; }
      set
      {
        m_Transform.Angle = value;
        InvalidateView();
      }
    }
    
    public Vector2f Scale
    {
      get { return m_Transform.Scale; }
      set
      {
        m_Transform.Scale = value;
        InvalidateView();
      }
    }
    
    #endregion
    
    #region Private methods
    
    private void InvalidateView()
    {
      if(m_SceneView != null)
      {
        m_SceneView.Invalidate();
      }
    }
    
    #endregion
    
    #region Private data
    
    private readonly ITransform m_Transform;
    private readonly ISceneView m_SceneView;
    
    #endregion
  }
}

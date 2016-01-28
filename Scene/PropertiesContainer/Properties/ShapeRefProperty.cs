using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Scene
{
  class ShapeRefProperty : IProperty
  {
    #region Constructors
    
    public ShapeRefProperty()
    {
      UpdateShapeRefPath();
    }
    
    public ShapeRefProperty(string value)
    {
      m_ShapeRefPath = value;
    }
    
    #endregion
    
    #region Public methods
    
    public void LinkShape()
    {
      if(!string.IsNullOrEmpty(m_ShapeRefPath))
      {
        string[] pathParts = m_ShapeRefPath.Split(':');
        string sceneName = pathParts[0];
        string shapeName = pathParts[1];
        Scene scene = Solution.Instance.Scenes.FindScene(sceneName);
        if(scene == null)
        {
          throw new NullReferenceException("Scene " + sceneName + " not found");
        }
        
        Shape shape = scene.FindShape(shapeName);
        if(shape == null)
        {
          throw new NullReferenceException("Shape " + shapeName + " not found");
        }
        
        this.Value = shape;
      }
      else
      {
        this.Value = null;
      }
    }
    
    public Shape Value
    {
      get { return m_Shape; }
      set
      {
        if(this.Value != value)
        {
          UnregisterSceneHandlers();
          m_Shape = value;
          RegisterSceneHandlers();
          UpdateShapeRefPath();
          if(this.ValueChanged != null)
          {
            this.ValueChanged(this);
          }
        }
      }
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override string ToString()
    {
      return m_ShapeRefPath;
    }
    
    #endregion
    
    #region IProperty interface implementation

    public void Prepare()
    {
      if(!m_Prepared)
      {
        m_Prepared = true;
        LinkShape();
      }
    }
    
    public IProperty Clone()
    {
      ShapeRefProperty clone = new ShapeRefProperty(m_ShapeRefPath);
      if(m_Prepared)
      {
        clone.Prepare();
      }
      
      return clone;
    }

    public Control CreateEditControl(string label)
    {
      ShapeRefPropertyControl control = new ShapeRefPropertyControl();
      control.SetProperty(label, this);
      return control;
    }

    public string TrySetValue(string value)
    {
      string prevValue = m_ShapeRefPath;
      m_ShapeRefPath = value;
      try
      {
        if(m_Prepared)
        {
          LinkShape();
        }
      }
      catch(NullReferenceException e)
      {
        m_ShapeRefPath = prevValue;
        return e.Message;
      }
      
      return null;
    }

    public event IPropertyValueChangedHandler ValueChanged;

    #endregion
    
    #region Private methods
    
    private void RegisterSceneHandlers()
    {
      if(this.Value != null)
      {
        this.Value.Scene.NameChanged += this.OnSceneNameChanged;
      }
    }

    private void UnregisterSceneHandlers()
    {
      if(this.Value != null)
      {
        this.Value.Scene.NameChanged -= this.OnSceneNameChanged;
      }
    }

    private void UpdateShapeRefPath()
    {
      if(this.Value != null)
      {
        string sceneName = this.Value.Scene.Name;
        string shapeName = this.Value.Name;
        m_ShapeRefPath = sceneName + ":" + shapeName;
      }
      else
      {
        m_ShapeRefPath = "";
      }
    }
    
    #endregion

    #region Private event handlers

    private void OnSceneNameChanged(PropertiesContainer sender, string previous)
    {
      UpdateShapeRefPath();
    }

    #endregion
    
    #region Private data
    
    private bool m_Prepared;
    private string m_ShapeRefPath;
    private Shape m_Shape;
    
    #endregion
  }
}

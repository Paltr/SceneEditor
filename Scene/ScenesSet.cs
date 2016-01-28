using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneEditor.Export;

namespace SceneEditor.Scene
{
  class ScenesSet : IEnumerable<Scene>, IScenesSet
  {
    #region Contructors
    
    public ScenesSet(ShapeTemplatesSet shapeTemplatesSet, ISceneView sceneView)
    {
      m_ShapeTemplatesSet = shapeTemplatesSet;
      m_SceneView = sceneView;
      m_Scenes = new List<Scene>();
    }
    
    #endregion
    
    #region Public methods
    
    public void Prepare()
    {
      foreach(Scene scene in this)
      {
        scene.Prepare();
      }
    }
    
    public ShapeTemplatesSet ShapeTemplatesSet
    {
      get { return m_ShapeTemplatesSet; }
    }
    
    public Scene CreateScene(string name)
    {
      History.Change();
      if(FindScene(name) != null)
      {
        throw new ArgumentException();
      }
      
      Scene scene = new Scene(this, this.ShapeTemplatesSet, m_SceneView);
      scene.Name = name;
      m_Scenes.Add(scene);
      if(this.SceneAdded != null)
      {
        this.SceneAdded(this, scene);
      }
      
      return scene;
    }

    public Scene CloneScene(Scene scene, string cloneName)
    {
      History.Change();
      if(FindScene(cloneName) != null)
      {
        throw new ArgumentException();
      }

      Scene clone = new Scene(this, this.ShapeTemplatesSet, m_SceneView);
      clone.Name = cloneName;
      clone.UserPropertiesFilepath = scene.UserPropertiesFilepath;
      clone.Size = scene.Size;
      foreach(Shape shape in scene.Shapes)
      {
        clone.CreateShapeClone(shape);
      }

      m_Scenes.Add(clone);
      if(this.SceneAdded != null)
      {
        this.SceneAdded(this, clone);
      }

      return clone;
    }

    public void SwapScenePositions(Scene scene0, Scene scene1)
    {
      if(scene0 == null || scene1 == null)
      {
        throw new ArgumentNullException();
      }

      History.Change();
      int index0 = m_Scenes.IndexOf(scene0);
      int index1 = m_Scenes.IndexOf(scene1);
      if(index0 == -1 || index1 == -1)
      {
        throw new KeyNotFoundException();
      }

      m_Scenes[index0] = scene1;
      m_Scenes[index1] = scene0;
    }
    
    public Scene FindScene(string name)
    {
      foreach(Scene scene in m_Scenes)
      {
        if(scene.Name == name)
        {
          return scene;
        }
      }
      
      return null;
    }
    
    public Scene FindScene(Shape shape)
    {
      foreach(Scene scene in m_Scenes)
      {
        if(scene.CheckContains(shape))
        {
          return scene;
        }
      }
      
      return null;
    }
    
    public Scene FindScene(ShapeCircle shapeCircle)
    {
      foreach(Scene scene in m_Scenes)
      {
        if(scene.FindShape(shapeCircle) != null)
        {
          return scene;
        }
      }
      
      return null;
    }
    
    public Shape FindShape(ShapeCircle shapeCircle)
    {
      foreach(Scene scene in m_Scenes)
      {
        Shape shape = scene.FindShape(shapeCircle);
        if(shape != null)
        {
          return shape;
        }
      }
      
      return null;
    }
    
    public void RemoveScene(Scene scene)
    {
      History.Change();
      if(!m_Scenes.Remove(scene))
      {
        throw new ArgumentException();
      }
      
      if(this.SceneRemoved != null)
      {
        this.SceneRemoved(this, scene);
      }
    }
    
    public List<Shape> AllShapes
    {
      get
      {
        List<Shape> shapes = new List<Shape>();
        foreach(Scene scene in m_Scenes)
        {
          shapes.AddRange(scene.Shapes);
        }
        
        return shapes;
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void SceneAddedHandler(ScenesSet sender, Scene scene);
    public delegate void SceneRemovedHandler(ScenesSet sender, Scene scene);
    
    public event SceneAddedHandler SceneAdded;
    public event SceneRemovedHandler SceneRemoved;
    
    #endregion
    
    #region IEnumerable<ShapeTemplate> interface implementation
    
    IEnumerator<Scene> IEnumerable<Scene>.GetEnumerator()
    {
      return m_Scenes.GetEnumerator();
    }
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return m_Scenes.GetEnumerator();
    }
    
    #endregion
    
    #region IScenesSet interface implementation
    
    public List<IScene> Scenes
    {
      get { return m_Scenes.ConvertAll(scene => (IScene)scene); }
    }
    
    #endregion
    
    #region Private data
    
    private readonly ShapeTemplatesSet m_ShapeTemplatesSet;
    private readonly ISceneView m_SceneView;
    private readonly List<Scene> m_Scenes;
    
    #endregion
  }
}

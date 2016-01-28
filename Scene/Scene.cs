using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;
using Util.Math;
using GLRenderer;
using SceneEditor.Forms.Interfaces;
using SceneEditor.Export;

namespace SceneEditor.Scene
{
  class Scene : PropertiesContainer, IScene
  {
    #region Constructors
    
    public Scene(ScenesSet scenesSet, ShapeTemplatesSet shapeTemplatesSet, ISceneView sceneView)
    {
      m_ScenesSet = scenesSet;
      m_ShapeTemplatesSet = shapeTemplatesSet;
      m_SceneView = sceneView;
      m_Size = Settings.DefaultSceneSize;
      m_Shapes = new List<Shape>();
      
      this.UserPropertiesFilepath = "SceneProperties.xml";
    }
    
    #endregion
    
    #region Public methods
    
    public Vector2f Size
    {
      get { return m_Size; }
      set
      {
        if(this.Size != value)
        {
          History.Change();
          Vector2f previous = this.Size;
          m_Size = value;
          if(this.SizeChanged != null)
          {
            this.SizeChanged(this, previous);
          }
        }
      }
    }
    
    public ShapeTemplatesSet ShapeTemplatesSet
    {
      get { return m_ShapeTemplatesSet; }
    }
    
    public List<Shape> Shapes
    {
      get { return new List<Shape>(m_Shapes); }
      set { m_Shapes = new List<Shape>(value); }
    }
    
    public IList<Shape> OrderedShapes
    {
      get
      {
        List<Shape> result = new List<Shape>();
        foreach(Shape shape in m_Shapes)
        {
          int index = 0;
          for(; index < result.Count; ++index)
          {
            Shape other = result[index];
            if(other.ZOrder > shape.ZOrder)
            {
              result.Insert(index, shape);
              break;
            }
          }
          
          if(index == result.Count)
          {
            result.Add(shape);
          }
        }
        
        return result;
      }
    }
    
    public Shape CreateShape(string name)
    {
      History.Change();
      Shape result = new Shape(m_SceneView);
      if(FindShape(name) != null)
      {
        name = NameGenerator.GenerateName(name, true, CreateObjectNameChecker());
      }

      result.Name = name;
      AddShape(result);
      return result;
    }

    public Shape CreateShapeClone(Shape shape)
    {
      History.Change();
      Shape result = shape.Clone(this);
      if(FindShape(result.Name) != null)
      {
        result.Name = NameGenerator.GenerateName(result.Name, true, CreateObjectNameChecker());
      }

      return result;
    }
    
    public bool CheckContains(Shape shape)
    {
      return m_Shapes.Contains(shape);
    }
    
    public Shape FindShape(string name)
    {
      foreach(Shape shape in m_Shapes)
      {
        if(shape.Name == name)
        {
          return shape;
        }
      }
      
      return null;
    }
    
    public Shape FindShape(ShapeCircle circle)
    {
      foreach(Shape shape in m_Shapes)
      {
        if(shape.Circles.Contains(circle))
        {
          return shape;
        }
      }
      
      return null;
    }
    
    public void RemoveShape(Shape shape)
    {
      History.Change();
      if(!m_Shapes.Remove(shape))
      {
        throw new ArgumentException();
      }
      
      if(this.ShapeRemoved != null)
      {
        this.ShapeRemoved(this, shape);
      }
      
      m_SceneView.Invalidate();
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override void Prepare()
    {
      base.Prepare();
      foreach(Shape shape in m_Shapes)
      {
        shape.Prepare();
      }
    }
    
    public override Dictionary<string, IProperty> Properties
    {
      get
      {
        Dictionary<string, IProperty> properties = new Dictionary<string, IProperty>();
        properties["Name"] = new NameProperty(this);
        properties["Size"] = new SizeProperty(this);
        return properties;
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void SizeChangedHandler(Scene sender, Vector2f previous);
    public delegate void ShapeAddedHandler(Scene sender, Shape shape);
    public delegate void ShapeRemovedHandler(Scene sender, Shape shape);
    
    public event SizeChangedHandler SizeChanged;
    public event ShapeAddedHandler ShapeAdded;
    public event ShapeRemovedHandler ShapeRemoved;
    
    #endregion
    
    #region Export.IScene interface implementation
    
    List<IShape> IScene.Shapes
    {
      get { return this.Shapes.Cast<IShape>().ToList(); }
    }
    
    #endregion
    
    #region Private types
    
    #region NameProperty
    
    private class NameProperty : StringProperty
    {
      #region Constructors
      
      public NameProperty(Scene owner)
      {
        m_Owner = owner;
        m_Owner.NameChanged += this.OnOwnerRenamed;
      }
      
      #endregion
      
      #region Public overridden methods
      
      public override string ToString()
      {
        return m_Owner.Name;
      }

      public override string TrySetValue(string value)
      {
        CheckValueCorrectnessDelegate checker =
          Solution.Instance.CreateSceneNameChecker(m_Owner.Name);
        string errorStr = checker(value);
        if(errorStr == null)
        {
          m_Owner.Name = value;
          base.TrySetValue(value);
        }
        
        return errorStr;
      }
      
      #endregion
      
      #region Private event handlers
      
      private void OnOwnerRenamed(PropertiesContainer sender, string previous)
      {
        base.TrySetValue(sender.Name);
      }
      
      #endregion
      
      #region Private data
      
      private readonly Scene m_Owner;
      
      #endregion
    }
    
    #endregion
    
    #region SizeProperty
    
    private class SizeProperty : Vector2Property
    {
      #region Constructors
      
      public SizeProperty(Scene owner)
      {
        m_Owner = owner;
        m_Owner.SizeChanged += this.OnOwnerSizeChanged;
      }
      
      #endregion
      
      #region Public overridden methods
      
      public override string ToString()
      {
        return m_Owner.Size.ToString();
      }
      
      public override string TrySetValue(string value)
      {
        m_Owner.Size = Vector2f.Parse(value);
        return null;
      }
      
      #endregion
      
      #region Private event handlers
      
      private void OnOwnerSizeChanged(PropertiesContainer sender, Vector2f previous)
      {
        base.TrySetValue(m_Owner.Size.ToString());
      }
      
      #endregion
      
      #region Private data
      
      private readonly Scene m_Owner;
      
      #endregion
    }
    
    #endregion
    
    #endregion

    #region Private methods

    private CheckValueCorrectnessDelegate CreateObjectNameChecker()
    {
      return delegate(string name)
      {
        Shape shape = this.FindShape(name);
        if(shape != null)
        {
          return "Shape with name " + name + " is already used";
        }
        else
        {
          return null;
        }
      };
    }

    private void AddShape(Shape shape)
    {
      m_Shapes.Add(shape);
      if(this.ShapeAdded != null)
      {
        this.ShapeAdded(this, shape);
      }
      
      m_SceneView.Invalidate();
    }

    #endregion
    
    #region Private data
    
    private readonly ScenesSet m_ScenesSet;
    private readonly ShapeTemplatesSet m_ShapeTemplatesSet;
    private readonly ISceneView m_SceneView;
    
    private Vector2f m_Size;
    private List<Shape> m_Shapes;
    
    #endregion
  }
}

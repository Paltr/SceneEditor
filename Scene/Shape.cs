using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Util;
using Util.Math;
using Util.Spatial;
using GLRenderer;
using SceneEditor.Forms.Interfaces;
using SceneEditor.Export;

namespace SceneEditor.Scene
{
  class Shape : PropertiesContainer,
                ShapeCircle.IOwner,
                IShape
  {
    #region Constructors
    
    public Shape(ISceneView sceneView)
      : this()
    {
      m_SceneView = sceneView;
    }
    
    private Shape()
    {
      m_Manips = new List<SpatialManip>();
    }
    
    #endregion
    
    #region Public methods
    
    public Shape Clone()
    {
      return Clone(this.Scene);
    }

    public Shape Clone(Scene parentScene)
    {
      Shape clone = parentScene.CreateShape(this.Name);
      clone.Template = this.Template;
      clone.EditTemplateMode = this.EditTemplateMode;
      IList<ShapeCircle> circles = this.Circles;
      IList<ShapeCircle> cloneCircles = clone.Circles;
      for(int index = 0; index < circles.Count; ++index)
      {
        ShapeCircle circle = circles[index];
        ShapeCircle cloneCircle = cloneCircles[index];
        cloneCircle.Position = circle.Position;
        cloneCircle.Radius = circle.Radius;
        cloneCircle.Angle = circle.Angle;
      }

      Dictionary<string, IProperty> cloneProperties = clone.UserProperties;
      foreach(var kvp in this.UserProperties)
      {
        IProperty property = cloneProperties[kvp.Key];
        property.TrySetValue(kvp.Value.ToString());
      }

      clone.ZOrder = this.ZOrder;
      return clone;
    }

    public Scene Scene
    {
      get
      {
        ScenesSet scenes = Solution.Instance.Scenes;
        if(scenes != null)
        {
          return scenes.FindScene(this);
        }
        
        return null;
      }
    }
    
    public ShapeTemplatesSet ShapeTemplatesSet
    {
      get { return Solution.Instance.Scenes.ShapeTemplatesSet; }
    }
    
    public ShapeTemplate Template
    {
      get { return m_ShapeTemplate; }
      set
      {
        if(this.Template != value)
        {
          m_ShapeTemplate = value;
          OnTemplateChanged();
        }
      }
    }
    
    public bool EditTemplateMode
    {
      get { return m_EditTemplateMode; }
      set
      {
        if(this.EditTemplateMode != value)
        {
          OnTemplateChanged();
          m_EditTemplateMode = value;
        }
      }
    }

    public bool EditableColor
    {
      get
      {
        if(this.Template != null)
        {
          return this.Template.EditableColor;
        }
        else
        {
          return false;
        }
      }
    }

    public Color Color
    {
      get
      {
        if(this.Template != null)
        {
          if(this.Template.EditableColor)
          {
            return m_Color;
          }
          else
          {
            return this.Template.Color;
          }
        }

        return Color.Black;
      }

      set
      {
        if(this.Color != value)
        {
          if(this.Template == null || !this.Template.EditableColor)
          {
            throw new InvalidOperationException();
          }

          History.Change();
          m_Color = value;
          InvalidateView();
        }
      }
    }
    
    public Vector2f Position
    {
      get
      {
        if(m_RootCircle != null)
        {
          return m_RootCircle.Position;
        }
        else
        {
          return new Vector2f();
        }
      }
      
      set
      {
        if(m_RootCircle != null)
        {
          m_RootCircle.Position = value;
        }
      }
    }
    
    public float Angle
    {
      get
      {
        if(m_RootCircle != null)
        {
          return m_RootCircle.Angle;
        }
        else
        {
          return 0.0f;
        }
      }
      
      set
      {
        if(m_RootCircle != null)
        {
          m_RootCircle.Angle = value;
        }
      }
    }
    
    public float ZOrder
    {
      get { return m_ZOrder; }
      set
      {
        if(this.ZOrder != value)
        {
          History.Change();
          m_ZOrder = value;
          InvalidateView();
          if(this.ZOrderChanged != null)
          {
            this.ZOrderChanged(this);
          }
        }
      }
    }
    
    public List<ShapeCircle> Circles
    {
      get { return m_RootCircle.AllCircles; }
    }
    
    public bool TryTouch(Vector2f position, bool selected)
    {
      if(this.Template != null)
      {
        return this.Template.TryTouch(this, position, selected);
      }
      else
      {
        return false;
      }
    }
    
    public SpatialManip SelectionManip
    {
      get { return m_SelectionManip; }
    }
    
    public List<SpatialManip> Manips
    {
      get { return new List<SpatialManip>(m_Manips); }
    }
    
    public ISceneView SceneView
    {
      get { return m_SceneView; }
    }
    
    public void Render(Renderer renderer, bool selected)
    {
      if(this.Template != null)
      {
        this.Template.Render(this, renderer, selected);
      }
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override Dictionary<string, IProperty> Properties
    {
      get
      {
        Dictionary<string, IProperty> properties = new Dictionary<string, IProperty>();
        properties["Name"] = new NameProperty(this);
        properties["Position"] = new PositionProperty(this);
        properties["Angle"] = new AngleProperty(this);
        properties["ZOrder"] = new ZOrderProperty(this);
        if(this.EditableColor)
        {
          properties["Color"] = new ShapeColorProperty(this);
        }

        return properties;
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void PositionChangedHandler(Shape sender);
    public delegate void AngleChangedHandler(Shape sender);
    public delegate void ZOrderChangedHandler(Shape sender);
    
    public event PositionChangedHandler PositionChanged;
    public event AngleChangedHandler AngleChanged;
    public event ZOrderChangedHandler ZOrderChanged;
    
    #endregion
    
    #region ShapeCircle.IOwner interface implementation
    
    public ShapeCircle RootCircle
    {
      get { return m_RootCircle; }
    }
    
    public void TryTranslate(ShapeCircle circle, Vector2f position)
    {
      if(this.Template != null)
      {
        this.Template.TryTranslate(circle, position);
      }
    }
    
    public void TryResize(ShapeCircle circle, float radius)
    {
      if(this.Template != null)
      {
        this.Template.TryResize(circle, radius);
      }
    }
    
    public void TryRotate(ShapeCircle circle, float angle)
    {
      if(this.Template != null)
      {
        this.Template.TryRotate(circle, angle);
      }
    }
    
    #endregion
    
    #region Export.IShape interface implementation
    
    IScene IShape.Scene
    {
      get { return this.Scene; }
    }
    
    string IShape.TemplateName
    {
      get { return this.Template.Name; }
    }
    
    List<IShapeCircle> IShape.Circles
    {
      get
      {
        List<ShapeCircle> circles = this.Circles;
        return circles.Cast<IShapeCircle>().ToList();
      }
    }
    
    #endregion
    
    #region Private types
    
    #region NameProperty
    
    private class NameProperty : StringProperty
    {
      #region Constructors
      
      public NameProperty(Shape owner)
      {
        m_Owner = owner;
        m_Owner.NameChanged += this.OnOwnerRenamed;
        this.Value = owner.Name;
      }
      
      #endregion
      
      #region Public overridden methods
      
      public override string TrySetValue(string value)
      {
        CheckValueCorrectnessDelegate checker =
          Solution.Instance.CreateShapeNameChecker(m_Owner.Scene, m_Owner.Name);
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
      
      private readonly Shape m_Owner;
      
      #endregion
    }
    
    #endregion
    
    #region Position property
    
    private class PositionProperty : Vector2Property
    {
      #region Constructors
      
      public PositionProperty(Shape owner)
      {
        m_Owner = owner;
        this.Value = m_Owner.Position;
        m_Owner.PositionChanged += this.HandlePositionChanged;
      }
      
      #endregion
      
      #region Public overridden methods
      
      public override string TrySetValue(string value)
      {
        string errorStr = base.TrySetValue(value);
        if(errorStr == null)
        {
          m_Owner.Position = this.Value;
        }
        
        return errorStr;
      }
      
      #endregion
      
      #region Private event handlers
      
      private void HandlePositionChanged(Shape sender)
      {
        this.Value = m_Owner.Position;
      }
      
      #endregion
      
      #region Private data
      
      private readonly Shape m_Owner;
      
      #endregion
    }
    
    #endregion
    
    #region Angle property
    
    private class AngleProperty : FloatProperty
    {
      #region Constructors
      
      public AngleProperty(Shape owner)
      {
        m_Owner = owner;
        this.Value = this.Degrees;
        m_Owner.AngleChanged += this.HandleAngleChanged;
      }
      
      #endregion
      
      #region Public methods
      
      public float Degrees
      {
        get { return Helpers.Rad2Deg(m_Owner.Angle);}
      }
      
      #endregion
      
      #region Public overridden methods
      
      public override string TrySetValue(string value)
      {
        string errorStr = base.TrySetValue(value);
        if(errorStr == null)
        {
          m_Owner.Angle = Helpers.Deg2Rad(this.Value);
        }
        
        return errorStr;
      }
      
      #endregion
      
      #region Private event handlers
      
      private void HandleAngleChanged(Shape sender)
      {
        this.Value = this.Degrees;
      }
      
      #endregion
      
      #region Private data
      
      private readonly Shape m_Owner;
      
      #endregion
    }
    
    #endregion
    
    #region Z order property
    
    private class ZOrderProperty : FloatProperty
    {
      #region Constructors
      
      public ZOrderProperty(Shape owner)
      {
        m_Owner = owner;
        this.Value = m_Owner.ZOrder;
      }
      
      #endregion
      
      #region Public overridden methods
      
      public override string TrySetValue(string value)
      {
        string errorStr = base.TrySetValue(value);
        if(errorStr == null)
        {
          m_Owner.ZOrder = this.Value;
        }
        
        return errorStr;
      }
      
      #endregion
      
      #region Private data
      
      private readonly Shape m_Owner;
      
      #endregion
    }
    
    #endregion

    #region Shape color property
    
    private class ShapeColorProperty : ColorProperty
    {
      #region Constructors
      
      public ShapeColorProperty(Shape owner)
      {
        m_Owner = owner;
        this.Value = m_Owner.Color;
      }
      
      #endregion
      
      #region Public overridden methods

      public override Color Value
      {
        set
        {
          base.Value = value;
          m_Owner.Color = this.Value;
        }
      }

      public override string TrySetValue(string value)
      {
        string errorStr = base.TrySetValue(value);
        if(errorStr == null)
        {
          m_Owner.Color = this.Value;
        }
        
        return errorStr;
      }
      
      #endregion
      
      #region Private data
      
      private readonly Shape m_Owner;
      
      #endregion
    }

    #endregion
    
    #endregion
    
    #region Private methods
    
    private void OnTemplateChanged()
    {
      Vector2f position = this.Position;
      float angle = this.Angle;
      if(m_RootCircle != null)
      {
        m_RootCircle.PositionChanged -= this.HandleRootPositionChanged;
        m_RootCircle.AngleChanged -= this.HandleRootAngleChanged;
      }
      
      if(this.Template != null)
      {
        ShapeCircle templateRootCircle = this.Template.RootCircle;
        CompositeTransform compositeTransform = new CompositeTransform();
        if(this.EditTemplateMode)
        {
          compositeTransform.InducedTransforms.Add(templateRootCircle.Transform);
        }
        else
        {
          compositeTransform.InducedTransforms.Add(templateRootCircle.Transform);
          compositeTransform.InducedTransforms.Add(new Transform());
        }
        
        m_RootCircle = new ShapeCircle(this, compositeTransform, this.SceneView);
        m_Color = this.Template.Color;
        foreach(ShapeCircle templateChildCircle in templateRootCircle.Children)
        {
          CreateCircleFromTemplate(templateChildCircle, m_RootCircle);
        }
        
        m_SelectionManip = this.Template.GetSelectionManip(this);
        m_Manips = this.Template.GetManips(this);
        if(m_Manips == null)
        {
          m_Manips = new List<SpatialManip>();
        }
        
        this.UserPropertiesFilepath = this.Template.PropertiesFilepath;
      }
      else
      {
        m_RootCircle = new ShapeCircle(this, new Transform(), this.SceneView);
        m_Color = Color.Black;
        m_Manips = new List<SpatialManip>();
      }
      
      if(!this.EditTemplateMode)
      {
        this.Position = position;
        this.Angle = angle;
      }
      
      m_RootCircle.PositionChanged += this.HandleRootPositionChanged;
      m_RootCircle.AngleChanged += this.HandleRootAngleChanged;
      InvalidateView();
    }
    
    private void CreateCircleFromTemplate(ShapeCircle templateCircle, ShapeCircle parent)
    {
      ITransform transform = templateCircle.Transform;
      if(!this.EditTemplateMode)
      {
        CompositeTransform compositeTransform = new CompositeTransform();
        compositeTransform.InducedTransforms.Add(templateCircle.Transform);
        compositeTransform.InducedTransforms.Add(new Transform());
        transform = compositeTransform;
      }
      
      ShapeCircle circle = null;
      if(parent != null)
      {
        circle = new ShapeCircle(parent, transform);
      }
      else
      {
        circle = new ShapeCircle(this, transform, this.SceneView);
      }
      
      foreach(ShapeCircle templateChildCircle in templateCircle.Children)
      {
        CreateCircleFromTemplate(templateChildCircle, circle);
      }
    }
    
    private void InvalidateView()
    {
      if(this.SceneView != null)
      {
        this.SceneView.Invalidate();
      }
    }
    
    #endregion
    
    #region Private event handlers
    
    private void HandleTemplateChanged(ShapeTemplate sender)
    {
      InvalidateView();
    }
    
    private void HandleRootPositionChanged(ShapeCircle sender)
    {
      if(this.PositionChanged != null)
      {
        this.PositionChanged(this);
      }
    }
    
    private void HandleRootAngleChanged(ShapeCircle sender)
    {
      if(this.AngleChanged != null)
      {
        this.AngleChanged(this);
      }
    }
    
    #endregion
    
    #region Private data
    
    private readonly ISceneView m_SceneView;
    
    private ShapeTemplate m_ShapeTemplate;
    private bool m_EditTemplateMode;
    private ShapeCircle m_RootCircle;
    private float m_ZOrder;
    private Color m_Color;
    
    private SpatialManip m_SelectionManip;
    private List<SpatialManip> m_Manips;
    
    #endregion
  }
}

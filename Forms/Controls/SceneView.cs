using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Util;
using Util.Math;
using Util.Extensions;
using GLRenderer;
using GLRenderer.Controls;
using CustomControls.Controls;
using CustomControls.Helpers;
using SceneEditor.Scene;
using SceneEditor.Forms.Interfaces;

namespace SceneEditor.Forms.Controls
{
  partial class SceneView : ShapesView, ISceneView
  {
    #region Contructors
    
    public SceneView()
    {
      InitializeComponent();
      this.GridDx = Settings.SceneGridDx;
      this.GridDy = Settings.SceneGridDy;
    }
    
    #endregion
    
    #region Public methods
    
    public float GridDx
    {
      get { return m_GridDx; }
      set
      {
        if(this.GridDx != value)
        {
          m_GridDx = value;
          Invalidate();
        }
      }
    }

    public float GridDy
    {
      get { return m_GridDy; }
      set
      {
        if(this.GridDy != value)
        {
          m_GridDy = value;
          Invalidate();
        }
      }
    }

    public IEditor Editor
    {
      get { return m_Editor; }
      set
      {
        if(this.Editor != null)
        {
          this.Editor.SelectedSceneChanged -= this.OnSelectedSceneChanged;
          this.Editor.SelectedShapeChanged -= this.OnSelectedShapeChanged;
          this.Editor.ActiveTemplateChanged -= this.OnActiveTemplateChanged;
        }
        
        m_Editor = value;
        if(this.Editor != null)
        {
          this.Editor.SelectedSceneChanged += this.OnSelectedSceneChanged;
          this.Editor.SelectedShapeChanged += this.OnSelectedShapeChanged;
          this.Editor.ActiveTemplateChanged += this.OnActiveTemplateChanged;
        }
      }
    }
    
    public ScalingHelper ScalingHelper
    {
      get { return m_ScalingHelper; }
      set
      {
        if(this.ScalingHelper != value)
        {
          m_ScalingHelper = value;
          UpdateSceneSize();
        }
      }
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override ICollection<Shape> Shapes
    {
      get
      {
        if(this.SelectedScene != null)
        {
          return this.SelectedScene.OrderedShapes;
        }
        else
        {
          return new List<Shape>();
        }
      }
    }
    
    #endregion
    
    #region Protected overriden methods
    
    protected override void NotifySelectedShapeChanged(Shape previous)
    {
      if(this.Editor != null)
      {
        this.Editor.SelectedShape = this.SelectedShape;
      }
    }
    
    protected override void OnMouseDown(object sender, GLMouseEvent e)
    {
      if(this.ActiveTemplate == null)
      {
        base.OnMouseDown(sender, e);
      }
    }
    
    protected override void OnMouseMove(object sender, GLMouseEvent e)
    {
      base.OnMouseMove(sender, e);
      this.TemplatePreviewPos = e.Location;
    }
    
    protected override void OnMouseUp(object sender, GLMouseEvent e)
    {
      base.OnMouseUp(sender, e);
      if(e.Buttons == MouseButtons.Left)
      {
        if(this.SelectedScene != null)
        {
          if(this.ActiveTemplate != null)
          {
            Shape shape = this.SelectedScene.CreateShape(this.ActiveTemplate.Name);
            shape.Template = this.ActiveTemplate;
            shape.Position = e.Location;
          }
        }
      }
      else if(e.Buttons == MouseButtons.Right)
      {
        this.ActiveTemplate = null;
        this.SelectedShape = null;
      }
    }
    
    protected override void OnPaint(Renderer renderer)
    {
      base.OnPaint(renderer);
      if(this.SelectedScene != null)
      {
        RenderGrid(renderer);
        if(this.TemplateVisible)
        {
          Color color = Color.FromArgb(127, Color.White);
          renderer.PushPen();
          renderer.Pen = new Pen(color);
          this.ActiveTemplate.Render(renderer, m_TemplatePreviewPos);
          renderer.PopPen();
        }
      }
    }
    
    #endregion
    
    #region Private types
    
    private enum Mode
    {
      NONE,
      MOVE,
      ROTATE,
      APPEAR
    }
    
    #endregion
    
    #region Private methods
    
    private Scene.Scene SelectedScene
    {
      get
      {
        if(this.Editor != null)
        {
          return this.Editor.SelectedScene;
        }
        else
        {
          return null;
        }
      }
    }
    
    private ShapeTemplate ActiveTemplate
    {
      get
      {
        if(this.Editor != null)
        {
          return this.Editor.ActiveTemplate;
        }
        else
        {
          return null;
        }
      }
      
      set
      {
        if(this.Editor != null)
        {
          this.Editor.ActiveTemplate = value;
        }
      }
    }
    
    private bool TemplateVisible
    {
      get
      {
        return (this.ActiveTemplate != null && !m_TemplatePreviewPos.CheckInvalid());
      }
    }
    
    private Vector2f TemplatePreviewPos
    {
      get { return m_TemplatePreviewPos; }
      set
      {
        if(m_TemplatePreviewPos.CheckInvalid() != value.CheckInvalid() || m_TemplatePreviewPos != value)
        {
          m_TemplatePreviewPos = value;
          if(this.ActiveTemplate != null)
          {
            Invalidate();
          }
        }
      }
    }

    private void RenderGrid(Renderer renderer)
    {
      Vector2f visibleArea = this.ScalingHelper.VisibleArea;
      if(this.GridDx != 0.0f)
      {
        Vector2f p0 = new Vector2f(0.0f, 0.0f);
        Vector2f p1 = new Vector2f(0.0f, visibleArea.Y);
        uint linesAmount = (uint)(visibleArea.X / this.GridDx);
        RenderGridLines(renderer, p0, p1, new Vector2f(this.GridDx, 0.0f), linesAmount);
      }

      if(this.GridDy != 0.0f)
      {
        Vector2f p0 = new Vector2f(0.0f, 0.0f);
        Vector2f p1 = new Vector2f(visibleArea.X, 0.0f);
        uint linesAmount = (uint)(visibleArea.Y / this.GridDy);
        RenderGridLines(renderer, p0, p1, new Vector2f(0.0f, this.GridDy), linesAmount);
      }
    }

    private void RenderGridLines(Renderer renderer, Vector2f p0, Vector2f p1, Vector2f dp, uint amount)
    {
      renderer.PushPen();
      renderer.Pen = SceneConstants.GridPen;
      for(uint index = 0; index < amount; ++index)
      {
        renderer.DrawLine(p0, p1);
        p0 += dp;
        p1 += dp;
      }

      renderer.PopPen();
    }
    
    private void UpdateSceneSize()
    {
      Vector2f sceneSize = new Vector2f(100.0f, 100.0f);
      if(this.SelectedScene != null)
      {
        sceneSize = this.SelectedScene.Size;
      }
      
      if(this.ScalingHelper != null)
      {
        this.ScalingHelper.VisibleArea = sceneSize;
      }
      else
      {
        this.Size = sceneSize.ToSystemSize();
      }
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnSelectedSceneChanged(IEditor sender, Scene.Scene previous)
    {
      if(previous != null)
      {
        previous.SizeChanged -= this.OnSceneSizeChanged;
      }
      
      if(this.SelectedScene != null)
      {
        this.SelectedScene.SizeChanged += this.OnSceneSizeChanged;
      }
      
      UpdateSceneSize();
      Invalidate();
    }
    
    private void OnSelectedShapeChanged(IEditor sender, Shape previous)
    {
      Invalidate();
      this.SelectedShape = sender.SelectedShape;
      if(this.SelectedShape != null)
      {
        this.ActiveTemplate = null;
      }
    }
    
    private void OnActiveTemplateChanged(IEditor sender, ShapeTemplate previous)
    {
      Invalidate();
      if(this.ActiveTemplate != null)
      {
        this.SelectedShape = null;
      }
    }
    
    private void OnSceneSizeChanged(Scene.Scene sender, Vector2f previous)
    {
      UpdateSceneSize();
    }
    
    private void OnMouseEnter(object sender, GLMouseEvent e)
    {
      this.TemplatePreviewPos = e.Location;
    }
    
    private void OnMouseLeave(object sender, GLMouseEvent e)
    {
      this.TemplatePreviewPos = e.Location;
    }
    
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Delete)
      {
        if(this.SelectedShape != null)
        {
          this.SelectedScene.RemoveShape(this.SelectedShape);
          this.SelectedShape = null;
        }
      }
    }
    
    #endregion
    
    #region Private data
    
    private float m_GridDx;
    private float m_GridDy;
    private IEditor m_Editor;
    private ScalingHelper m_ScalingHelper;
    private Vector2f m_TemplatePreviewPos;
    
    #endregion
  }
}

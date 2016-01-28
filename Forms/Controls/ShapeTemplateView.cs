using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Math;
using Util.Spatial;
using CustomControls.Controls;
using GLRenderer;
using GLRenderer.Controls;
using SceneEditor.Scene;

namespace SceneEditor.Forms.Controls
{
  partial class ShapeTemplateView : ShapesView, ISceneView
  {
    #region Contructors
    
    public ShapeTemplateView()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public ShapeTemplate Template
    {
      get { return m_Template; }
      set
      {
        if(this.Template != value)
        {
          m_Template = value;
          this.SelectedShape = null;
          AutoSize();
          if(this.Template != null)
          {
            this.SelectedShape = this.Template.CreateEditShape(this);
          }
        }
      }
    }
    
    public Rect2f OutRect
    {
      get
      {
        Vector2f leftBottom = -this.ReferencePointPx;
        Vector2f size = new Vector2f(this.Size.Width, this.Size.Height);
        return new Rect2f(leftBottom, size);
      }
      
      set
      {
        if(this.OutRect != value)
        {
          Rect2f oldValue = this.OutRect;
          this.ReferencePointPx = -value.LeftBottom;
          this.Size = value.Size.ToSystemSize();
          if(this.OutRectChanged != null)
          {
            this.OutRectChanged(this, oldValue);
          }
        }
      }
    }
    
    public new void AutoSize()
    {
      if(this.Template != null)
      {
        Rect2f boundingRect = this.Template.EditRect;
        boundingRect.LeftBottom -= new Vector2f(60.0f, 60.0f);
        boundingRect.Size += new Vector2f(120.0f, 120.0f);
        this.OutRect = boundingRect;
      }
      else
      {
        this.OutRect = new Rect2f(100, 100);
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void OutRectChangedHandler(ShapeTemplateView sender, Rect2f oldValue);
    
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public event OutRectChangedHandler OutRectChanged;
    
    #endregion
    
    #region Public overridden methods
    
    public override ICollection<Shape> Shapes
    {
      get
      {
        List<Shape> shapes = new List<Shape>();
        if(this.SelectedShape != null)
        {
          shapes.Add(this.SelectedShape);
        }
        
        return shapes;
      }
    }
    
    #endregion
    
    #region Private data
    
    private ShapeTemplate m_Template;
    
    #endregion
  }
}

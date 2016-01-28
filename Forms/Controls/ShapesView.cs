using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Math;
using CustomControls.Controls;
using GLRenderer;
using GLRenderer.Controls;
using SceneEditor.Scene;

namespace SceneEditor.Forms.Controls
{
  abstract partial class ShapesView : GLControl
  {
    #region Constructors
    
    public ShapesView()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public Shape SelectedShape
    {
      get { return m_SelectedShape; }
      protected set
      {
        if(this.SelectedShape != value)
        {
          Shape previousShape = this.SelectedShape;
          m_SelectedShape = value;
          if(this.SelectedShape != null)
          {
            this.SelectedManip = this.SelectedShape.SelectionManip;
          }
          else
          {
            this.SelectedManip = null;
          }
          
          NotifySelectedShapeChanged(previousShape);
          Invalidate();
        }
      }
    }
    
    public SpatialManip SelectedManip
    {
      get { return m_SelectedManip; }
      protected set
      {
        if(this.SelectedManip != value)
        {
          SpatialManip oldValue = this.SelectedManip;
          m_SelectedManip = value;
          if(this.SelectedManip != null)
          {
            this.SelectedManip.Prepare();
          }
          
          Invalidate();
          if(this.SelectedManipChanged != null)
          {
            this.SelectedManipChanged(this, oldValue);
          }
        }
      }
    }
    
    public void StartShapeSelection(ShapeSelectedHandler handler)
    {
      m_ShapeSelectedHandler = handler;
    }

    public void StopShapeSelection()
    {
      m_ShapeSelectedHandler = null;
    }
    
    #endregion
    
    #region Public abstract methods
    
    public abstract ICollection<Shape> Shapes { get; }
    
    #endregion
    
    #region Public events
    
    public delegate void SelectedManipChangedHandler(ShapesView sender, SpatialManip oldValue);
    public delegate void ShapeSelectedHandler(ShapesView sender, Shape shape);
    
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public event SelectedManipChangedHandler SelectedManipChanged;
    
    #endregion
    
    #region Protected virtual methods
    
    protected virtual void NotifySelectedShapeChanged(Shape previous)
    {
    }
    
    #endregion
    
    #region Protected virtual event handlers
    
    protected virtual void OnMouseDown(object sender, GLMouseEvent e)
    {
      if(e.Buttons == MouseButtons.Left)
      {
        if(m_ShapeSelectedHandler != null)
        {
          bool delayed;
          Shape shape = FindShape(e.Location, e.Shift, out delayed);
          if(shape != null || delayed)
          {
            m_ShapeSelectedHandler(this, shape);
            m_ShapeSelectedHandler = null;
          }
        }
        else
        {
          Shape shape = FindShape(e.Location, e.Control);
          if(this.SelectedShape == shape && shape != null && e.Shift)
          {
            Shape shapeClone = shape.Clone();
            this.SelectedShape = shapeClone;
            SpatialManip manip = SelectSpatialManip(e.Location);
            if(manip != null)
            {
              this.SelectedManip = manip;
            }
            
            if(this.SelectedManip != null)
            {
              this.SelectedManip.HandleMouseDown(e);
            }
          }
          else if(this.SelectedShape == shape)
          {
            SpatialManip manip = SelectSpatialManip(e.Location);
            if(manip != null)
            {
              this.SelectedManip = manip;
            }
            
            if(this.SelectedManip != null)
            {
              this.SelectedManip.HandleMouseDown(e);
            }
          }
          else if(shape != null)
          {
            this.SelectedShape = shape;
          }
        }
      }
    }
    
    protected virtual void OnMouseMove(object sender, GLMouseEvent e)
    {
      if(this.SelectedManip != null)
      {
        this.SelectedManip.HandleMouseMove(e);
      }
    }
    
    protected virtual void OnMouseUp(object sender, GLMouseEvent e)
    {
    }
    
    #endregion
    
    #region Protected overridden methods

    protected override void OnPaint(Renderer renderer)
    {
      base.OnPaint(renderer);
      foreach(Shape shape in this.Shapes)
      {
        shape.Render(renderer, this.SelectedShape == shape);
      }
      
      if(this.SelectedShape != null)
      {
        SpatialManip selectionManip = this.SelectedShape.SelectionManip;
        selectionManip.Render(renderer, this.SelectedManip == selectionManip);
        foreach(SpatialManip manip in this.SelectedShape.Manips)
        {
          manip.Render(renderer, this.SelectedManip == manip);
        }
      }
    }
    
    #endregion
    
    #region Private methods
    
    private Shape FindShape(Vector2f position, bool backgroudCollect)
    {
      bool temp;
      return FindShape(position, backgroudCollect, out temp);
    }
    
    private Shape FindShape(Vector2f position, bool backgroudCollect, out bool delayed)
    {
      delayed = false;
      List<Shape> candidates = new List<Shape>();
      foreach(Shape shape in this.Shapes)
      {
        bool selected = (this.SelectedShape == shape);
        if(shape.TryTouch(position, selected))
        {
          if(selected)
          {
            return this.SelectedShape;
          }
          
          candidates.Add(shape);
        }
      }
      
      if(candidates.Count != 0)
      {
        if(candidates.Count == 1)
        {
          return candidates[0];
        }
        else
        {
          List<Shape> filteredCandidates = new List<Shape>();
          if(backgroudCollect)
          {
            filteredCandidates.AddRange(candidates);
          }
          else
          {
            foreach(Shape shape in candidates)
            {
              if(!shape.Template.Backgroud)
              {
                filteredCandidates.Add(shape);
              }
            }
          }
          
          if(filteredCandidates.Count == 0)
          {
            return null;
          }
          else if(filteredCandidates.Count == 1)
          {
            return filteredCandidates[0];
          }
          else
          {
            LightContextMenu contextMenu = new LightContextMenu();
            for(int index = 0; index < filteredCandidates.Count; ++index)
            {
              Shape shape = filteredCandidates[index];
              if(!shape.Template.Backgroud || backgroudCollect)
              {
                ItemClickHandler activateKey = delegate()
                {
                  this.SelectedShape = shape;
                };
                contextMenu.AddItem(shape.Name, activateKey);
                delayed = true;
              }
            }
            
            contextMenu.Show(this, GLToControlPosition(position));
            return null;
          }
        }
      }
      
      return null;
    }
    
    private SpatialManip SelectSpatialManip(Vector2f position)
    {
      if(this.SelectedShape != null)
      {
        List<SpatialManip> candidates = new List<SpatialManip>();
        foreach(SpatialManip manip in this.SelectedShape.Manips)
        {
          if(manip.TryTouch(position, this.SelectedManip == manip))
          {
            if(this.SelectedManip == manip)
            {
              return this.SelectedManip;
            }
            
            candidates.Add(manip);
          }
        }
        
        if(candidates.Count != 0)
        {
          if(candidates.Count == 1)
          {
            return candidates[0];
          }
          else
          {
            LightContextMenu contextMenu = new LightContextMenu();
            for(int index = 0; index < candidates.Count; ++index)
            {
              SpatialManip manip = candidates[index];
              ItemClickHandler activateKey = delegate()
              {
                this.SelectedManip = manip;
              };
              contextMenu.AddItem(manip.Name, activateKey);
            }
            
            contextMenu.Show(this, GLToControlPosition(position));
            return null;
          }
        }
        else
        {
          return this.SelectedShape.SelectionManip;
        }
      }
      
      return null;
    }
    
    #endregion
    
    #region Private data
    
    private Shape m_SelectedShape;
    private SpatialManip m_SelectedManip;
    private ShapeSelectedHandler m_ShapeSelectedHandler;
    
    #endregion
  }
}

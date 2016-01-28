using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Math;
using GLRenderer;
using GLRenderer.Controls;

namespace SceneEditor.Scene
{
  class PivotManip : SpatialManip
  {
    #region Constructors
    
    public PivotManip(ShapeCircle circle, ISceneView sceneView)
      : base(sceneView)
    {
      if(circle == null)
      {
        throw new NullReferenceException();
      }
      
      m_ShapeCircle = circle;
      this.EnableOffset = true;
      this.EnableRotate = true;
    }
    
    #endregion
    
    #region Public methods
    
    public bool EnableOffset
    {
      get { return m_EnableOffset; }
      set { m_EnableOffset = value; }
    }
    
    public bool EnableRotate
    {
      get { return m_EnableRotate; }
      set { m_EnableRotate = value; }
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override string Name
    {
      get
      {
        if(this.ShapeCircle.Shape == null)
        {
          return "Unnamed pivot";
        }
        else
        {
          return this.ShapeCircle.Shape.Name + " pivot";
        }
      }
    }
    
    public override bool TryTouch(Vector2f position, bool selected)
    {
      return this.PositionCircle.CheckPointInside(position)
        || (selected && this.EnableRotate && this.AngleCircle.CheckPointInside(position));
    }
    
    public override void Prepare()
    {
      m_Mode = Mode.UNDEFINED;
    }
    
    public override void Render(Renderer renderer, bool selected)
    {
      renderer.PushPen();
      renderer.Pen = SceneConstants.ManipPen;
      renderer.FillCircle(this.PositionCircle);
      if(selected && this.EnableRotate)
      {
        renderer.DrawLine(this.Position, this.AngleCircle.Center);
        renderer.FillCircle(this.AngleCircle);
      }
      
      renderer.PopPen();
    }
    
    public override void HandleMouseDown(GLMouseEvent e)
    {
      if((e.Buttons & MouseButtons.Left) != 0)
      {
        m_Mode = Mode.TRANSLATE;
        if(this.AngleCircle.CheckPointInside(e.Location))
        {
          m_Mode = Mode.ROTATE;
        }
      }
    }
    
    public override void HandleMouseMove(GLMouseEvent e)
    {
      if((e.Buttons & MouseButtons.Left) != 0)
      {
        if(this.CurrentMode == Mode.TRANSLATE)
        {
          this.Position += e.Offset;
        }
        else if(this.CurrentMode == Mode.ROTATE)
        {
          Vector2f offset = e.Location - this.Position;
          this.Angle = offset.Angle;
        }
      }
    }
    
    public override List<ShapeCircle> ManipShapeCircles
    {
      get { return new List<ShapeCircle>{ this.ShapeCircle }; }
    }
    
    #endregion
    
    #region Protected types
    
    protected enum Mode
    {
      UNDEFINED,
      TRANSLATE,
      ROTATE
    }
    
    #endregion
    
    #region Protected methods
    
    protected ShapeCircle ShapeCircle
    {
      get { return m_ShapeCircle; }
    }
    
    protected Mode CurrentMode
    {
      get { return m_Mode; }
    }
    
    protected Circle PositionCircle
    {
      get { return GetPositionManipCircle(this.ShapeCircle); }
    }
    
    protected Circle AngleCircle
    {
      get { return GetAngleManipCircle(this.ShapeCircle); }
    }
    
    #endregion
    
    #region Private data
    
    private readonly ShapeCircle m_ShapeCircle;
    private Mode m_Mode;
    
    private bool m_EnableOffset;
    private bool m_EnableRotate;
    
    #endregion
  }
}

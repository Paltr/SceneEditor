using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;
using Util.Spatial;
using GLRenderer;

namespace SceneEditor.Scene
{
  sealed class ImageTemplate : RectTemplate
  {
    #region Constructors
    
    public ImageTemplate(string name, string propertiesFilepath)
      : base(name, propertiesFilepath)
    {
      DisableUnusedTransforms();
    }
    
    public ImageTemplate(ImageTemplate imageTemplate)
      : base(imageTemplate)
    {
      m_DiffuseFilepath = imageTemplate.DiffuseFilepath;
      UpdateMaterial();
      DisableUnusedTransforms();
    }
    
    private void DisableUnusedTransforms()
    {
      for(int index = 1; index < this.PerCircleSettings.Count; ++index)
      {
        ShapeCircleSettings settings = this.PerCircleSettings[index];
        settings.EnableOffset = false;
        settings.EnableRotate = false;
      }
    }
    
    #endregion
    
    #region Public methods
    
    public string DiffuseFilepath
    {
      get { return m_DiffuseFilepath; }
      set
      {
        if(m_DiffuseFilepath != value)
        {
          NotifyChanged();
          m_DiffuseFilepath = value;
          if(UpdateMaterial())
          {
            this.Size = new Vector2f(m_Material.Width, m_Material.Height);
          }
        }
      }
    }
    
    public Material Material
    {
      get { return m_Material; }
    }
    
    #endregion
    
    #region Public overridden methods
    
    public override ShapeTemplate Clone()
    {
      return new ImageTemplate(this);
    }
    
    public override void Render(Shape shape, Renderer renderer, bool selected)
    {
      if(this.Material != null)
      {
        Quad2f imageQuad = GetShapeQuad(shape);
        renderer.DrawImage(this.Material, imageQuad);
        if(selected)
        {
          renderer.PushPen();
          renderer.Pen = SceneConstants.HighlightPen;
          renderer.DrawLine(imageQuad.Vertices, true);
          renderer.PopPen();
        }
      }
    }
    
    #endregion
    
    #region Private methods
    
    public bool UpdateMaterial()
    {
      if(string.IsNullOrEmpty(this.DiffuseFilepath))
      {
        m_Material = null;
      }
      else
      {
        m_Material = Program.MaterialFactory.CreateMaterial(this.DiffuseFilepath);
      }
      
      return m_Material != null;
    }
    
    #endregion
    
    #region Private data
    
    private string m_DiffuseFilepath;
    private Material m_Material;
    
    #endregion
  }
}

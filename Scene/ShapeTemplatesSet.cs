using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Generic;

namespace SceneEditor.Scene
{
  class ShapeTemplatesSet : IEnumerable<ShapeTemplate>
  {
    #region Constructors
    
    public ShapeTemplatesSet()
    {
      m_Templates = new DoubledList<string, ShapeTemplate>();
    }
    
    #endregion
    
    #region Public methods
    
    public ICollection<ShapeTemplate> Templates
    {
      get { return m_Templates.Values; }
    }
    
    public CircleTemplate CreateCircleTemplate(string name, string propertiesFilepath)
    {
      CircleTemplate circleTemplate = new CircleTemplate(name, propertiesFilepath);
      RegisterShapeTemplate(circleTemplate);
      return circleTemplate;
    }

    public ImageTemplate CreateImageTemplate(string name, string propertiesFilepath)
    {
      ImageTemplate imageTemplate = new ImageTemplate(name, propertiesFilepath);
      RegisterShapeTemplate(imageTemplate);
      return imageTemplate;
    }
    
    public RectTemplate CreateRectTemplate(string name, string propertiesFilepath)
    {
      RectTemplate rectTemplate = new RectTemplate(name, propertiesFilepath);
      RegisterShapeTemplate(rectTemplate);
      return rectTemplate;
    }
    
    public void RegisterShapeTemplate(ShapeTemplate shapeTemplate)
    {
      if(FindTemplate(shapeTemplate.Name) != null)
      {
        throw new ArgumentException("Shape template " + shapeTemplate.Name + " already exists");
      }
      
      History.Change();
      m_Templates[shapeTemplate.Name] = shapeTemplate;
      if(this.TemplateAdded != null)
      {
        this.TemplateAdded(this, shapeTemplate);
      }
    }
    
    public ShapeTemplate FindTemplate(string name)
    {
      ShapeTemplate template = null;
      m_Templates.TryGetValue(name, out template);
      return template;
    }
    
    public void RemoveTemplate(ShapeTemplate shapeTemplate)
    {
      History.Change();
      if(!m_Templates.Remove(shapeTemplate.Name))
      {
        throw new ArgumentException();
      }
      
      if(this.TemplateRemoved != null)
      {
        this.TemplateRemoved(this, shapeTemplate);
      }
    }
    
    public void RemoveAllTemplates()
    {
      List<ShapeTemplate> allTemplates = new List<ShapeTemplate>(m_Templates.Values);
      foreach(ShapeTemplate shapeTemplate in allTemplates)
      {
        RemoveTemplate(shapeTemplate);
      }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void AddedHandler(ShapeTemplatesSet sender, ShapeTemplate template);
    public delegate void RemovedHandler(ShapeTemplatesSet sender, ShapeTemplate template);
    
    public event AddedHandler TemplateAdded;
    public event RemovedHandler TemplateRemoved;
    
    #endregion
    
    #region IEnumerable<ShapeTemplate> interface implementation
    
    IEnumerator<ShapeTemplate> IEnumerable<ShapeTemplate>.GetEnumerator()
    {
      return m_Templates.Values.GetEnumerator();
    }
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return m_Templates.Values.GetEnumerator();
    }
    
    #endregion
    
    #region Private data
    
    private readonly DoubledList<string, ShapeTemplate> m_Templates;
    
    #endregion
  }
}

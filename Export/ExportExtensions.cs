using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Math;
using Util.Extensions;

namespace SceneEditor.Export
{
  public static class ExportExtensions
  {
    #region IScenesSet extension methods
    
    public static IScene FindScene(this IScenesSet scenesSet, string name)
    {
      return scenesSet.Scenes.Find(scene => scene.Name == name);
    }
    
    public static IShape FindShapeByRefPath(this IScenesSet scenesSet, string shapeRefPath)
    {
      string[] pathParts = shapeRefPath.Split(':');
      string sceneName = pathParts[0];
      string shapeName = pathParts[1];
      return FindShape(scenesSet, sceneName, shapeName);
    }
    
    public static IShape GetShapeByRefPath(this IScenesSet scenesSet, string shapeRefPath)
    {
      IShape shape = FindShapeByRefPath(scenesSet, shapeRefPath);
      if(shape == null)
      {
        throw new ArgumentException("Shape with ref path " + shapeRefPath + " not found");
      }
      
      return shape;
    }
    
    public static IShape FindShape(this IScenesSet scenesSet, string sceneName, string shapeName)
    {
      IScene scene = FindScene(scenesSet, sceneName);
      if(scene != null)
      {
        return scene.FindShapeByName(shapeName);
      }
      
      return null;
    }
    
    #endregion
    
    #region IScene extension methods
    
    public static List<IShape> CollectShapesByTemplate(this IScene scene, string templateName)
    {
      return scene.Shapes.FindAll(shape => shape.TemplateName == templateName).ToList();
    }
    
    public static IShape FindShapeByTemplate(this IScene scene, string templateName)
    {
      return scene.Shapes.Find(shape => shape.TemplateName == templateName);
    }
    
    public static IShape GetShapeByTemplate(this IScene scene, string templateName)
    {
      IShape shape = FindShapeByTemplate(scene, templateName);
      if(shape == null)
      {
        throw new ArgumentException("Shape with template name " + templateName + " not found");
      }
      
      return shape;
    }
    
    public static IShape FindShapeByName(this IScene scene, string name)
    {
      return scene.Shapes.Find(shape => shape.Name == name);
    }
    
    public static IShape GetShapeByName(this IScene scene, string name)
    {
      IShape shape = FindShapeByTemplate(scene, name);
      if(shape == null)
      {
        throw new ArgumentException("Shape with name " + name + " not found");
      }
      
      return shape;
    }
    
    #endregion
    
    #region IShape extension methods
    
    public static Rect2f GetBoundingRect(this IShape shape)
    {
      ICollection<Vector2f> points = shape.Circles.ConvertAll(shapeCircle => shapeCircle.Position);
      return Rect2f.CreateBoundingRect(points);
    }
    
    #endregion
  }
}

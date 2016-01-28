using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Util.Xml;
using Util.Math;
using SceneEditor.Forms.Interfaces;

namespace SceneEditor.Scene
{
  class PropertiesBuilder
  {
    #region Contructors
    
    public PropertiesBuilder(string absFilepath)
    {
      m_NameToPropertyMap = new Dictionary<string, IProperty>();
      absFilepath = Program.FindFilepath(absFilepath);
      Document document = Document.CreateFromFile(absFilepath);
      if(document == null)
      {
        throw new FileNotFoundException("No properties file found", absFilepath);
      }
      
      DataElement argsContainer = document.RootElement;
      foreach(DataElement propertyEl in argsContainer.CollectChildren("property"))
      {
        AddProperty(propertyEl);
      }
    }
    
    #endregion
    
    #region Public methods
    
    public Dictionary<string, IProperty> BuildProperties()
    {
      Dictionary<string, IProperty> result = new Dictionary<string, IProperty>();
      foreach(KeyValuePair<string, IProperty> kvp in m_NameToPropertyMap)
      {
        IProperty property = kvp.Value.Clone();
        result[kvp.Key] = property;
      }
      
      return result;
    }
    
    #endregion
    
    #region Private methods
    
    private void AddProperty(DataElement propertyEl)
    {
      string name = propertyEl.GetAttribValue("name");
      string typeStr = propertyEl.GetAttribValue("type");
      
      IProperty property;
      switch(typeStr)
      {
        case "string":
        {
          property = CreateStringProperty(propertyEl);
          break;
        }
        
        case "bool":
        {
          property = CreateBoolProperty(propertyEl);
          break;
        }
        
        case "int":
        {
          property = CreateIntProperty(propertyEl);
          break;
        }
        
        case "float":
        {
          property = CreateFloatProperty(propertyEl);
          break;
        }
        
        case "vector2":
        {
          property = CreateVector2Property(propertyEl);
          break;
        }
        
        case "shape_ref":
        {
          property = CreateShapeRefProperty(propertyEl);
          break;
        }
        
        default:
        {
          throw new ArgumentException();
        }
      }
      
      m_NameToPropertyMap[name] = property;
      string defaultValueStr = propertyEl.GetAttribValue("default");
      if(defaultValueStr != null)
      {
        string errorStr = property.TrySetValue(defaultValueStr);
        if(errorStr != null)
        {
          throw new InvalidDataException();
        }
      }
    }
    
    private IProperty CreateBoolProperty(DataElement propertyEl)
    {
      return new BoolProperty();
    }
    
    private IProperty CreateStringProperty(DataElement propertyEl)
    {
      return new StringProperty();
    }
    
    private IProperty CreateIntProperty(DataElement propertyEl)
    {
      return new IntProperty();
    }
    
    private IProperty CreateFloatProperty(DataElement propertyEl)
    {
      return new FloatProperty();
    }
    
    private IProperty CreateVector2Property(DataElement propertyEl)
    {
      return new Vector2Property();
    }
    
    private IProperty CreateShapeRefProperty(DataElement propertyEl)
    {
      return new ShapeRefProperty();
    }
    
    #endregion
    
    #region Private data
    
    private readonly Dictionary<string, IProperty> m_NameToPropertyMap;
    
    #endregion
  }
}

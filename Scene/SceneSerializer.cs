using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Util.Common;
using Util.Math;
using Util.Xml;
using SceneEditor.Forms.Interfaces;

namespace SceneEditor.Scene
{
  static class SceneSerializer
  {
    #region Public static methods
    
    public static void SaveShapeTemplates(ShapeTemplatesSet templates, string filepath)
    {
      string folder = Directory.GetParent(filepath).FullName;
      Document document = new Document("root");
      DataElement templatesEl = document.RootElement.CreateChild("templates");
      foreach(ShapeTemplate template in templates)
      {
        if(template is CircleTemplate)
        {
          SaveTemplate(templatesEl.CreateChild("circle"), folder, (CircleTemplate)template);
        }
        else if(template is ImageTemplate)
        {
          SaveTemplate(templatesEl.CreateChild("image"), folder, (ImageTemplate)template);
        }
        else if(template is RectTemplate)
        {
          SaveTemplate(templatesEl.CreateChild("rect"), folder, (RectTemplate)template);
        }
        else
        {
          throw new ArgumentException();
        }
      }
      
      document.Save(filepath);
    }
    
    public static void OpenTemplates(ShapeTemplatesSet templates, string filepath)
    {
      string folder = Directory.GetParent(filepath).FullName;
      templates.RemoveAllTemplates();
      Document document = Document.CreateFromFile(filepath);
      DataElement templatesEl = document.RootElement.GetChild("templates");
      foreach(DataElement templateEl in templatesEl.Children)
      {
        switch(templateEl.Name)
        {
          case "circle": CreateCircleTemplate(templateEl, folder, templates); break;
          case "image": CreateImageTemplate(templateEl, folder, templates); break;
          case "rect": CreateRectTemplate(templateEl, folder, templates); break;
          default: throw new ArgumentException();
        }
      }
    }
    
    public static void SaveScenes(ScenesSet scenes,
      string filepath, string templatesFilepath)
    {
      Document document = new Document("root");
      DataElement templatesEl = document.RootElement.CreateChild("templates");
      templatesEl.CreateAttribute("filepath", templatesFilepath);
      DataElement scenesContainer = document.RootElement.CreateChild("scenes");
      foreach(Scene scene in scenes)
      {
        DataElement sceneElement = scenesContainer.CreateChild("scene");
        sceneElement.CreateAttribute("name", scene.Name);
        SaveScene(sceneElement, scene);
      }
      
      document.Save(filepath);
    }
    
    public static ScenesSet OpenScenes(ISceneView sceneView,
      string filepath, out string templatesFilepath)
    {
      Document document = Document.CreateFromFile(filepath);
      DataElement templatesEl = document.RootElement.GetChild("templates");
      templatesFilepath = templatesEl.GetAttribValue("filepath");
      while(!File.Exists(templatesFilepath))
      {
        DialogResult dialogResult = MessageBox.Show("Would you like to select correct one?",
          "No template descriptor", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        if(dialogResult == DialogResult.Yes)
        {
          OpenFileDialog openFileDialog = new OpenFileDialog();
          openFileDialog.DefaultExt = "template";
          openFileDialog.Filter = "Template (*.Template)|*.Template";
          openFileDialog.RestoreDirectory = true;
          if(openFileDialog.ShowDialog() == DialogResult.OK)
          {
            templatesFilepath = openFileDialog.FileName;
          }
        }
        else
        {
          return null; /* canceled by user */
        }
      }
      
      ShapeTemplatesSet templates = new ShapeTemplatesSet();
      OpenTemplates(templates, templatesFilepath);
      ScenesSet scenes = new ScenesSet(templates, sceneView);
      DataElement scenesContainer = document.RootElement.GetChild("scenes");
      foreach(DataElement sceneElement in scenesContainer.CollectChildren("scene"))
      {
        string name = sceneElement.GetAttribValue("name");
        Scene scene = scenes.CreateScene(name);
        LoadScene(sceneElement, scene, templates);
      }
      
      return scenes;
    }
    
    #endregion
    
    #region Private types
    
    private struct TemplateBaseInfo
    {
      public string name;
      public string propertiesFilepath;
    }
    
    #endregion
    
    #region Private template related methods
    
    private static void SaveTemplate(DataElement node, string folder, CircleTemplate template)
    {
      SaveBaseTemplate(node, folder, template);
      node.CreateAttribute("solid", template.Solid.ToString());
    }

    private static void SaveTemplate(DataElement node, string folder, ImageTemplate template)
    {
      SaveRectTemplate(node, folder, template);
      string relDiffuseFilepath = DirMethods.EvaluateRelativePath(folder, template.DiffuseFilepath);
      node.CreateAttribute("rel_diffuse_filepath", relDiffuseFilepath);
    }
    
    private static void SaveTemplate(DataElement node, string folder, RectTemplate template)
    {
      SaveRectTemplate(node, folder, template);
    }
    
    private static void SaveBaseTemplate(DataElement node, string folder, ShapeTemplate template)
    {
      node.CreateAttribute("name", template.Name);
      node.CreateAttribute("color", template.Color.ToArgb().ToString());
      node.CreateAttribute("editable_color", template.EditableColor.ToString());
      node.CreateAttribute("background", template.Backgroud.ToString());
      DataElement circlesEl = node.CreateChild("circles");
      foreach(ShapeCircle circle in template.RootCircle.AllCircles)
      {
        DataElement circleEl = circlesEl.CreateChild("circle");
        circleEl.CreateAttribute("position", circle.Position.ToString());
        circleEl.CreateAttribute("radius", circle.Radius.ToString());
        circleEl.CreateAttribute("angle", circle.Angle.ToString());
      }
      
      DataElement circlesSettingsEl = node.CreateChild("circles_settings");
      foreach(ShapeTemplate.ShapeCircleSettings settings in template.PerCircleSettings)
      {
        DataElement circleSettingsEl = circlesSettingsEl.CreateChild("settings");
        circleSettingsEl.CreateAttribute("enable_offset", settings.EnableOffset.ToString());
        circleSettingsEl.CreateAttribute("enable_rotate", settings.EnableRotate.ToString());
      }
      
      if(!string.IsNullOrEmpty(template.PropertiesFilepath))
      {
        string relPropertiesFilepath = DirMethods.EvaluateRelativePath(folder, template.PropertiesFilepath);
        node.CreateAttribute("rel_properties_filepath", relPropertiesFilepath);
      }
    }
    
    private static void SaveRectTemplate(DataElement node, string folder, RectTemplate template)
    {
      SaveBaseTemplate(node, folder, template);
      node.CreateAttribute("normalized", template.Normalized.ToString());
    }
    
    private static void CreateCircleTemplate(DataElement node, string folder, ShapeTemplatesSet templates)
    {
      TemplateBaseInfo info = LoadTemplateBaseInfo(node, folder);
      CircleTemplate template = templates.CreateCircleTemplate(info.name, info.propertiesFilepath);
      LoadCircleTemplate(node, template);
    }

    private static void CreateImageTemplate(DataElement node, string folder, ShapeTemplatesSet templates)
    {
      TemplateBaseInfo info = LoadTemplateBaseInfo(node, folder);
      ImageTemplate template = templates.CreateImageTemplate(info.name, info.propertiesFilepath);
      string relDiffuseFilepath = node.GetAttribValue("rel_diffuse_filepath");
      template.DiffuseFilepath = DirMethods.EvaluateAbsolutePath(folder, relDiffuseFilepath);
      LoadRectTemplate(node, template);
    }
    
    private static void CreateRectTemplate(DataElement node, string folder, ShapeTemplatesSet templates)
    {
      TemplateBaseInfo info = LoadTemplateBaseInfo(node, folder);
      RectTemplate template = templates.CreateRectTemplate(info.name, info.propertiesFilepath);
      LoadRectTemplate(node, template);
    }
    
    private static TemplateBaseInfo LoadTemplateBaseInfo(DataElement node, string folder)
    {
      TemplateBaseInfo info = new TemplateBaseInfo();
      info.name = node.GetAttribValue("name");
      string relPropertiesFilepath = node.GetAttribValue("rel_properties_filepath");
      if(!string.IsNullOrEmpty(relPropertiesFilepath))
      {
        info.propertiesFilepath = DirMethods.EvaluateAbsolutePath(folder, relPropertiesFilepath);
      }
      
      return info;
    }
    
    private static void LoadBaseTemplate(DataElement node, ShapeTemplate template)
    {
      string colorStr = node.GetAttribValue("color");
      string backgroudStr = node.GetAttribValue("background");
      template.Color = Color.FromArgb(int.Parse(colorStr));
      template.Backgroud = bool.Parse(backgroudStr);

      string editableColorStr = node.GetAttribValue("editable_color");
      if(editableColorStr != null)
      {
        template.EditableColor = bool.Parse(editableColorStr);
      }
      
      DataElement circlesEl = node.GetChild("circles");
      List<ShapeCircle> allCircles = template.RootCircle.AllCircles;
      int index = 0;
      foreach(DataElement circleEl in circlesEl.CollectChildren("circle"))
      {
        string positionStr = circleEl.GetAttribValue("position");
        string radiusStr = circleEl.GetAttribValue("radius");
        string angleStr = circleEl.GetAttribValue("angle");
        ShapeCircle circle = allCircles[index];
        circle.Position = Vector2f.Parse(positionStr);
        circle.Radius = float.Parse(radiusStr);
        circle.Angle = float.Parse(angleStr);
        ++index;
      }
      
      DataElement circlesSettingsEl = node.GetChild("circles_settings");
      index = 0;
      foreach(DataElement circleSettingsEl in circlesSettingsEl.CollectChildren("settings"))
      {
        ShapeTemplate.ShapeCircleSettings settings = template.PerCircleSettings[index];
        string enableOffsetStr = circleSettingsEl.GetAttribValue("enable_offset");
        string enableRotateStr = circleSettingsEl.GetAttribValue("enable_rotate");
        settings.EnableOffset = bool.Parse(enableOffsetStr);
        settings.EnableRotate = bool.Parse(enableRotateStr);
        ++index;
      }
    }

    private static void LoadCircleTemplate(DataElement node, CircleTemplate template)
    {
      LoadBaseTemplate(node, template);
      string solidStr = node.GetAttribValue("solid");
      template.Solid = bool.Parse(solidStr);
    }
    
    private static void LoadRectTemplate(DataElement node, RectTemplate template)
    {
      LoadBaseTemplate(node, template);
      string normalizedStr = node.GetAttribValue("normalized");
      template.Normalized = bool.Parse(normalizedStr);
    }
    
    #endregion
    
    #region Private scene related methods
    
    private static void SaveScene(DataElement sceneElement, Scene scene)
    {
      sceneElement.CreateAttribute("size", scene.Size.ToString());
      DataElement sceneObjectContainer = sceneElement.CreateChild("shapes");
      foreach(Shape shape in scene.Shapes)
      {
        DataElement shapeEl = sceneObjectContainer.CreateChild("shape");
        shapeEl.CreateAttribute("name", shape.Name);
        SaveShape(shapeEl, shape);
      }
      
      DataElement propertiesContainer = sceneElement.CreateChild("properties");
      SaveUserProperties(propertiesContainer, scene);
    }
    
    private static void LoadScene(DataElement sceneElement, Scene scene, ShapeTemplatesSet templates)
    {
      scene.Size = Vector2f.Parse(sceneElement.GetAttribValue("size"));
      DataElement shapesEl = sceneElement.GetChild("shapes");
      foreach(DataElement shapeEl in shapesEl.CollectChildren("shape"))
      {
        string name = shapeEl.GetAttribValue("name");
        Shape shape = scene.CreateShape(name);
        LoadShape(shapeEl, shape, templates);
      }
      
      DataElement propertiesContainer = sceneElement.GetChild("properties");
      if(propertiesContainer != null)
      {
        LoadUserProperties(propertiesContainer, scene);
      }
    }
    
    private static void SaveShape(DataElement node, Shape shape)
    {
      node.CreateAttribute("template_name", shape.Template.Name);
      node.CreateAttribute("z_order", shape.ZOrder.ToString());
      if(shape.EditableColor)
      {
        node.CreateAttribute("color", shape.Color.ToArgb().ToString());
      }

      DataElement circlesEl = node.CreateChild("circles");
      foreach(ShapeCircle circle in shape.Circles)
      {
        SaveShapeCircle(circlesEl.CreateChild("circle"), circle);
      }
      
      DataElement propertiesEl = node.CreateChild("user_properties");
      SaveUserProperties(propertiesEl, shape);
    }
    
    private static void LoadShape(DataElement node, Shape shape, ShapeTemplatesSet templates)
    {
      string templateName = node.GetAttribValue("template_name");
      ShapeTemplate template = templates.FindTemplate(templateName);
      shape.Template = template;
      shape.ZOrder = float.Parse(node.GetAttribValue("z_order"));

      string colorStr = node.GetAttribValue("color");
      if(colorStr != null)
      {
        shape.Color = Color.FromArgb(int.Parse(colorStr));
      }

      if(shape.EditableColor)
      {
        node.CreateAttribute("color", shape.Color.ToArgb().ToString());
      }

      DataElement circlesEl = node.GetChild("circles");
      IList<DataElement> circleElList = circlesEl.CollectChildren("circle");
      IList<ShapeCircle> circles = shape.Circles;
      for(int index = 0; index < circleElList.Count; ++index)
      {
        DataElement circleEl = circleElList[index];
        ShapeCircle circle = circles[index];
        LoadShapeCircle(circleEl, circle);
      }
      
      DataElement userPropertiesEl = node.GetChild("user_properties");
      LoadUserProperties(userPropertiesEl, shape);
    }
    
    private static void SaveShapeCircle(DataElement node, ShapeCircle shapeCircle)
    {
      node.CreateAttribute("position", shapeCircle.Position.ToString());
      node.CreateAttribute("radius", shapeCircle.Radius.ToString());
      node.CreateAttribute("angle", shapeCircle.Angle.ToString());
    }
    
    private static void LoadShapeCircle(DataElement node, ShapeCircle shapeCircle)
    {
      shapeCircle.Position = Vector2f.Parse(node.GetAttribValue("position"));
      shapeCircle.Radius = float.Parse(node.GetAttribValue("radius"));
      shapeCircle.Angle = float.Parse(node.GetAttribValue("angle"));
    }
    
    private static void SaveUserProperties(DataElement container, PropertiesContainer properties)
    {
      foreach(KeyValuePair<string, IProperty> kvp in properties.UserProperties)
      {
        DataElement element = container.CreateChild("property");
        element.CreateAttribute("name", kvp.Key);
        element.CreateAttribute("value", kvp.Value.ToString());
      }
    }
    
    private static void LoadUserProperties(DataElement container, PropertiesContainer properties)
    {
      IDictionary<string, IProperty> propertiesMap = properties.UserProperties;
      foreach(DataElement element in container.CollectChildren("property"))
      {
        string name = element.GetAttribValue("name");
        IProperty property;
        if(propertiesMap.TryGetValue(name, out property))
        {
          string valueStr = element.GetAttribValue("value");
          string errorStr = property.TrySetValue(valueStr);
          if(errorStr != null)
          {
            throw new InvalidDataException();
          }
        }
      }
    }
    
    #endregion
  }
}

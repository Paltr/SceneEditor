using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Util.Common;
using Util.Xml;
using Util.Math;
using SceneEditor.Export;

namespace SceneEditor
{
  static class Settings
  {
    #region Contructors
    
    static Settings()
    {
      Document document = Program.FindDocument(Program.SettingsFilepath);
      if(document != null)
      {
        DataElement sceneElement = document.RootElement.GetChild("scene");
        string translatorDLLRelFilepath = sceneElement.GetAttribValue("translator_dll_rel_filepath");
        
        string mainDirPath = Directory.GetParent(Program.SettingsFilepath).FullName;
        if(!string.IsNullOrEmpty(translatorDLLRelFilepath))
        {
          string translatorDLLFilepath =
            DirMethods.EvaluateAbsolutePath(mainDirPath, translatorDLLRelFilepath);
          try
          {
            Assembly assembly = Assembly.LoadFile(translatorDLLFilepath);
            string translatorClassStr = sceneElement.GetAttribValue("translator_class");
            m_SceneTranslator = (ISceneTranslator)assembly.CreateInstance(translatorClassStr);
          }
          catch(FileNotFoundException)
          {
            MessageBox.Show(translatorDLLFilepath + " not found", "Dll not found",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
        
        string sceneSizeDefaultStr = sceneElement.GetAttribValue("scene_size_default");
        string sceneGridDxStr = sceneElement.GetAttribValue("scene_grid_dx");
        string sceneGridDyStr = sceneElement.GetAttribValue("scene_grid_dy");
        string scalePercentsDefaultStr = sceneElement.GetAttribValue("scale_percents_default");
        string scalePercentsMinStr = sceneElement.GetAttribValue("scale_percents_min");
        string scalePercentsMaxStr = sceneElement.GetAttribValue("scale_percents_max");
        string templateIconSizeStr = sceneElement.GetAttribValue("template_icon_size");
        
        m_DefaultSceneSize = Vector2f.Parse(sceneSizeDefaultStr);
        if(!string.IsNullOrEmpty(sceneGridDxStr))
        {
          m_SceneGridDx = int.Parse(sceneGridDxStr);
        }

        if(!string.IsNullOrEmpty(sceneGridDyStr))
        {
          m_SceneGridDy = int.Parse(sceneGridDyStr);
        }

        if(!string.IsNullOrEmpty(scalePercentsDefaultStr))
        {
          m_ScalePercentsDefault = int.Parse(scalePercentsDefaultStr);
        }
        else
        {
          m_ScalePercentsDefault = 100;
        }
        
        if(!string.IsNullOrEmpty(scalePercentsMinStr))
        {
          m_ScalePercentsMin = int.Parse(scalePercentsMinStr);
        }
        
        if(!string.IsNullOrEmpty(scalePercentsMaxStr))
        {
          m_ScalePercentsMax = int.Parse(scalePercentsMaxStr);
        }
        else
        {
          m_ScalePercentsMax = int.MaxValue;
        }

        if(!string.IsNullOrEmpty(templateIconSizeStr))
        {
          Vector2f templateIconSize = Vector2f.Parse(templateIconSizeStr);
          m_TemplateIconSize.Width = (int)templateIconSize.X;
          m_TemplateIconSize.Height = (int)templateIconSize.Y;
        }
      }
      else
      {
        m_DefaultSceneSize = new Vector2f(1000, 1000);
      }
    }
    
    #endregion
    
    #region Public static methods
    
    public static ISceneTranslator SceneTranslator
    {
      get { return m_SceneTranslator; }
    }
    
    public static Vector2f DefaultSceneSize
    {
      get { return m_DefaultSceneSize; }
    }

    public static int SceneGridDx
    {
      get { return m_SceneGridDx; }
    }

    public static int SceneGridDy
    {
      get { return m_SceneGridDy; }
    }
    
    public static int ScalePercentsDefault
    {
      get { return m_ScalePercentsDefault; }
    }
    
    public static int ScalePercentsMin
    {
      get { return m_ScalePercentsMin; }
    }
    
    public static int ScalePercentsMax
    {
      get { return m_ScalePercentsMax; }
    }

    public static Size TemplateIconSize
    {
      get { return m_TemplateIconSize; }
    }
    
    #endregion
    
    #region Private data
    
    private static readonly ISceneTranslator m_SceneTranslator;
    private static readonly Vector2f m_DefaultSceneSize;
    private static readonly int m_SceneGridDx;
    private static readonly int m_SceneGridDy;
    private static readonly int m_ScalePercentsDefault;
    private static readonly int m_ScalePercentsMin;
    private static readonly int m_ScalePercentsMax;
    private static readonly Size m_TemplateIconSize = new Size(16, 16);
    
    #endregion
  }
}

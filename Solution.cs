using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Util;
using SceneEditor.Scene;
using SceneEditor.Forms.Interfaces;

namespace SceneEditor
{
  class Solution
  {
    #region Private data
    
    public static Solution Instance
    {
      get { return m_Project; }
      private set { m_Project = value; }
    }
    
    #endregion
    
    #region Contructors
    
    public Solution(IEditor editor)
    {
      Solution.Instance = this;
      m_Editor = editor;
    }
    
    #endregion
    
    #region Public methods
    
    public IEditor Editor
    {
      get { return m_Editor; }
    }
    
    public ScenesSet Scenes
    {
      get { return m_Scenes; }
      private set { m_Scenes = value; }
    }
    
    public string ScenesFilepath
    {
      get { return m_ScenesFilepath; }
      private set
      {
        if(this.ScenesFilepath != value)
        {
          string previous = m_ScenesFilepath;
          m_ScenesFilepath = value;
          if(this.ProjectNameChanged != null)
          {
            this.ProjectNameChanged(this, previous);
          }
        }
      }
    }
    
    public string ProjectName
    {
      get
      {
        if(string.IsNullOrEmpty(m_ScenesFilepath))
        {
          return "Untitled";
        }
        else
        {
          return Path.GetFileNameWithoutExtension(m_ScenesFilepath);
        }
      }
    }
    
    public void CreateScenes(ISceneView sceneView)
    {
      if(DoBeforeCloseActions())
      {
        ScenesSet previous = this.Scenes;
        this.ScenesFilepath = null;
        m_TemplatesFilepath = null;
        ShapeTemplatesSet templatesSet = new ShapeTemplatesSet();
        this.Scenes = new ScenesSet(templatesSet, sceneView);
        NotifyNewScenes(previous);
        History.ResetChanges();
      }
    }
    
    public bool SaveScenesAs()
    {
      string prevScenesFilepath = m_ScenesFilepath;
      this.ScenesFilepath = null;
      if(!SaveScenes())
      {
        this.ScenesFilepath = prevScenesFilepath;
        return false;
      }
      
      return true;
    }
    
    public bool SaveScenes()
    {
      if(this.ScenesFilepath == null)
      {
        if(this.SaveScenesDialog.ShowDialog() == DialogResult.OK)
        {
          this.ScenesFilepath = this.SaveScenesDialog.FileName;
        }
      }
      
      if(m_ScenesFilepath != null)
      {
        return SaveScenes(m_ScenesFilepath);
      }
      
      return false;
    }
    
    public bool SaveScenes(string filepath)
    {
      if(SaveTemplates())
      {
        SceneSerializer.SaveScenes(this.Scenes, filepath, m_TemplatesFilepath);
        History.ResetChanges();
        return true;
      }
      
      return false;
    }
    
    public void OpenScenes(ISceneView sceneView)
    {
      if(DoBeforeCloseActions())
      {
        if(this.OpenScenesDialog.ShowDialog() == DialogResult.OK)
        {
          OpenScenes(this.OpenScenesDialog.FileName, sceneView);
        }
      }
    }
    
    public void OpenScenes(string filepath, ISceneView sceneView)
    {
      ScenesSet previous = this.Scenes;
      this.ScenesFilepath = filepath;
      this.Scenes = SceneSerializer.OpenScenes(sceneView, filepath, out m_TemplatesFilepath);
      this.Scenes.Prepare();
      NotifyNewScenes(previous);
      History.ResetChanges();
    }
    
    public bool CloseScenes()
    {
      return DoBeforeCloseActions();
    }
    
    public void NewTemplates()
    {
      m_TemplatesFilepath = null;
      this.Scenes.ShapeTemplatesSet.RemoveAllTemplates();
    }
    
    public bool SaveTemplates()
    {
      if(m_TemplatesFilepath == null)
      {
        if(this.SaveTemplatesDialog.ShowDialog() == DialogResult.OK)
        {
          m_TemplatesFilepath = this.SaveTemplatesDialog.FileName;
        }
      }
      
      if(m_TemplatesFilepath != null)
      {
        SaveTemplates(m_TemplatesFilepath);
        return true;
      }
      
      return false;
    }
    
    public void SaveTemplates(string filepath)
    {
      SceneSerializer.SaveShapeTemplates(this.Scenes.ShapeTemplatesSet, filepath);
    }
    
    public void OpenTemplates()
    {
      if(this.OpenTemplatesDialog.ShowDialog() == DialogResult.OK)
      {
        OpenTemplates(this.OpenTemplatesDialog.FileName);
      }
    }
    
    public void OpenTemplates(string filepath)
    {
      m_TemplatesFilepath = filepath;
      SceneSerializer.OpenTemplates(this.Scenes.ShapeTemplatesSet, filepath);
    }
    
    public CheckValueCorrectnessDelegate CreateSceneNameChecker()
    {
      return CreateSceneNameChecker(null);
    }
    
    public CheckValueCorrectnessDelegate CreateSceneNameChecker(string except)
    {
      CheckValueCorrectnessDelegate checker = delegate(string str)
      {
        if(this.Scenes.FindScene(str) != null && except != str)
        {
          return "Scene name " + str + " is already used";
        }
        else
        {
          return null;
        }
      };
      return checker;
    }
    
    public CheckValueCorrectnessDelegate CreateShapeNameChecker(SceneEditor.Scene.Scene scene)
    {
      return CreateShapeNameChecker(scene, null);
    }
    
    public CheckValueCorrectnessDelegate CreateShapeNameChecker(SceneEditor.Scene.Scene scene, string except)
    {
      CheckValueCorrectnessDelegate checker = delegate(string str)
      {
        if(scene.FindShape(str) != null && except != str)
        {
          return "Scene shape name " + str + " is already used";
        }
        else
        {
          return null;
        }
      };
      return checker;
    }
    
    #endregion
    
    #region Public events
    
    public delegate void ClosedHandler(Solution sender);
    public delegate void NewScenesHandler(Solution sender, ScenesSet previous);
    public delegate void ProjectNameChangedHandler(Solution sender, string previous);
    
    public event ClosedHandler Closed;
    public event NewScenesHandler NewScenes;
    public event ProjectNameChangedHandler ProjectNameChanged;
    
    #endregion
    
    #region Private methods
    
    private void NotifyNewScenes(ScenesSet previous)
    {
      if(this.NewScenes != null)
      {
        this.NewScenes(this, previous);
      }
    }
    
    private bool DoBeforeCloseActions()
    {
      if(History.ProjectChanged)
      {
        bool result = false;
        if(this.Scenes != null)
        {
          DialogResult dialogRes = MessageBox.Show("Save changes?", "Save changes?",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
          if(dialogRes == DialogResult.Yes)
          {
            result = SaveScenes();
          }
          else if(dialogRes == DialogResult.No)
          {
            result = true;
          }
        }
        
        if(this.Closed != null)
        {
          this.Closed(this);
        }
        
        return result;
      }
      
      return true;
    }
    
    private SaveFileDialog SaveScenesDialog
    {
      get
      {
        if(m_SaveScenesDialog == null)
        {
          m_SaveScenesDialog = new SaveFileDialog();
          m_SaveScenesDialog.DefaultExt = "scenes";
          m_SaveScenesDialog.Filter = "Scenes (*.Scenes)|*.Scenes";
          m_SaveScenesDialog.RestoreDirectory = true;
        }
        
        return m_SaveScenesDialog;
      }
    }
    
    private OpenFileDialog OpenScenesDialog
    {
      get
      {
        if(m_OpenScenesDialog == null)
        {
          m_OpenScenesDialog = new OpenFileDialog();
          m_OpenScenesDialog.DefaultExt = "scenes";
          m_OpenScenesDialog.Filter = "Scenes (*.Scenes)|*.Scenes";
          m_OpenScenesDialog.RestoreDirectory = true;
        }
        
        return m_OpenScenesDialog;
      }
    }
    
    private SaveFileDialog SaveTemplatesDialog
    {
      get
      {
        if(m_SaveTemplatesDialog == null)
        {
          m_SaveTemplatesDialog = new SaveFileDialog();
          m_SaveTemplatesDialog.DefaultExt = "template";
          m_SaveTemplatesDialog.Filter = "Template (*.Template)|*.Template";
          m_SaveTemplatesDialog.RestoreDirectory = true;
        }
        
        return m_SaveTemplatesDialog;
      }
    }
    
    private OpenFileDialog OpenTemplatesDialog
    {
      get
      {
        if(m_OpenTemplatesDialog == null)
        {
          m_OpenTemplatesDialog = new OpenFileDialog();
          m_OpenTemplatesDialog.DefaultExt = "template";
          m_OpenTemplatesDialog.Filter = "Template (*.Template)|*.Template";
          m_OpenTemplatesDialog.RestoreDirectory = true;
        }
        
        return m_OpenTemplatesDialog;
      }
    }
    
    #endregion
    
    #region Private static data
    
    private static Solution m_Project;
    
    #endregion
    
    #region Private data
    
    private readonly IEditor m_Editor;
    
    private ScenesSet m_Scenes;
    private string m_ScenesFilepath;
    private string m_TemplatesFilepath;
    
    private SaveFileDialog m_SaveScenesDialog;
    private OpenFileDialog m_OpenScenesDialog;
    private SaveFileDialog m_SaveTemplatesDialog;
    private OpenFileDialog m_OpenTemplatesDialog;
    
    #endregion
  }
}

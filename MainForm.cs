using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls.Helpers;
using GLRenderer.Controls;
using SceneEditor.Forms;
using SceneEditor.Forms.Interfaces;
using SceneEditor.Forms.Controls;
using SceneEditor.Scene;

namespace SceneEditor
{
  partial class MainForm : Form, IEditor
  {
    #region Contructors
    
    public MainForm()
    {
      InitializeComponent();
      CustomControls.Controls.Helper.InsertAutoScrollLosingWorkaround(m_SplitContainer.Panel1, m_SceneView);
      m_ProjectExplorer.Editor = this;
      m_TemplatesExplorer.Editor = this;
      m_SceneView.Editor = this;
      
      ScalingHelper scalingHelper = new GLScalingHelper(m_SceneView, m_GLBackground,
        m_MinusBtn, m_PercentsTextBox, m_PlusBtn);
      m_SceneView.ScalingHelper = scalingHelper;
      scalingHelper.ScaleChanged += this.OnScaleChanged;
      scalingHelper.ScalePercents = Settings.ScalePercentsDefault;
      scalingHelper.MinScalePercents = Settings.ScalePercentsMin;
      scalingHelper.MaxScalePercents = Settings.ScalePercentsMax;
      
      Solution solution = new Solution(this);
      solution.NewScenes += this.OnNewScenes;
      solution.ProjectNameChanged += this.OnProjectNameChanged;
      
      CreateScenes();
      OnProjectNameChanged(solution, null);
    }
    
    #endregion
    
    #region Editor interface implementation
    
    public Scene.Scene SelectedScene
    {
      get { return m_SelectedScene; }
      set
      {
        if(this.SelectedScene != value)
        {
          Scene.Scene previous = this.SelectedScene;
          m_SelectedScene = value;
          if(this.SelectedSceneChanged != null)
          {
            this.SelectedSceneChanged(this, previous);
          }
          
          this.SelectedShape = null;
        }
        
        m_PropertiesContainer.PropertiesContainer = value;
      }
    }
    
    public Shape SelectedShape
    {
      get { return m_SelectedShape; }
      set
      {
        if(this.SelectedShape != value)
        {
          if(value != null)
          {
            Scene.Scene shapeScene = this.Scenes.FindScene(value);
            this.SelectedScene = value.Scene;
          }
          
          Shape previous = this.SelectedShape;
          m_SelectedShape = value;
          if(this.SelectedShapeChanged != null)
          {
            this.SelectedShapeChanged(this, previous);
          }
        }
        
        m_PropertiesContainer.PropertiesContainer = value;
      }
    }
    
    public ShapeTemplate ActiveTemplate
    {
      get { return m_ActiveTemplate; }
      set
      {
        if(this.ActiveTemplate != value)
        {
          ShapeTemplate previous = this.ActiveTemplate;
          m_ActiveTemplate = value;
          if(this.ActiveTemplateChanged != null)
          {
            this.ActiveTemplateChanged(this, previous);
          }
        }
      }
    }
    
    public void StartShapeSelection(EditorShapeSelectionModeHandler handler)
    {
      m_SceneView.StartShapeSelection((sender, shape) =>
      {
        handler(this, shape);
        m_ProjectExplorer.StopShapeSelection();
      });
      m_ProjectExplorer.StartShapeSelection((sender, shape) =>
      {
        handler(this, shape);
        m_SceneView.StopShapeSelection();
      });
    }
    
    public event EditorSelectedSceneChangedHandler SelectedSceneChanged;
    public event EditorSelectedShapeChangedHandler SelectedShapeChanged;
    public event EditorActiveTemplateChangedHandler ActiveTemplateChanged;
    
    #endregion
    
    #region Private methods
    
    private ScenesSet Scenes
    {
      get { return Solution.Instance.Scenes; }
    }
    
    private void CreateScenes()
    {
      Solution.Instance.CreateScenes(m_SceneView);
    }
    
    private bool SaveScenes()
    {
      return Solution.Instance.SaveScenes();
    }
    
    private void OpenScenes()
    {
      Solution.Instance.OpenScenes(m_SceneView);
    }
    
    private void OpenScenes(string filepath)
    {
      Solution.Instance.OpenScenes(filepath, m_SceneView);
    }
    
    private bool CloseScenes()
    {
      return Solution.Instance.CloseScenes();
    }
    
    private void NewTemplates()
    {
      Solution.Instance.NewTemplates();
    }
    
    private bool SaveTemplates()
    {
      return Solution.Instance.SaveTemplates();
    }
    
    private void SaveTemplates(string filepath)
    {
      Solution.Instance.SaveTemplates(filepath);
    }
    
    private void OpenTemplates()
    {
      m_TemplatesExplorer.SelectTemplateOnCreate = false;
      Solution.Instance.OpenTemplates();
      m_TemplatesExplorer.SelectTemplateOnCreate = true;
    }
    
    private void OpenTemplates(string filepath)
    {
      Solution.Instance.OpenTemplates(filepath);
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnScaleChanged(ScalingHelper sender, float previous)
    {
      SceneConstants.SceneScale = sender.Scale;
    }
    
    private void OnNewScenesSplitBtnClick(object sender, EventArgs e)
    {
      CreateScenes();
    }
    
    private void OnSaveScenesSplitBtnClick(object sender, EventArgs e)
    {
      SaveScenes();
    }
    
    private void OnSaveAsScenesSplitBtnClick(object sender, EventArgs e)
    {
      Solution.Instance.SaveScenesAs();
    }
    
    private void OnOpenScenesSplitBtnClick(object sender, EventArgs e)
    {
      OpenScenes();
    }
    
    private void OnNewTemplateSplitBtnClick(object sender, EventArgs e)
    {
      NewTemplates();
    }
    
    private void OnSaveTemplateSplitBtnClick(object sender, EventArgs e)
    {
      SaveTemplates();
    }
    
    private void OnSaveAsTemplateSplitBtnClick(object sender, EventArgs e)
    {
      string prevTemplatesFilepath = m_TemplatesFilepath;
      m_TemplatesFilepath = null;
      if(!SaveTemplates())
      {
        m_TemplatesFilepath = prevTemplatesFilepath;
      }
    }
    
    private void OnOpenTemplateSplitBtnClick(object sender, EventArgs e)
    {
      OpenTemplates();
    }
    
    private void OnExitBtnClick(object sender, EventArgs e)
    {
      this.Close();
    }
    
    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      if(!CloseScenes())
      {
        e.Cancel = true;
      }
    }
    
    private void OnNewScenes(Solution sender, ScenesSet previous)
    {
      if(previous != null)
      {
        previous.SceneRemoved -= this.OnSceneRemoved;
      }
      
      m_ProjectExplorer.Scenes = sender.Scenes;
      m_TemplatesExplorer.Scenes = sender.Scenes;
      if(sender.Scenes != null)
      {
        sender.Scenes.SceneRemoved += this.OnSceneRemoved;
      }
    }
    
    private void OnSceneRemoved(ScenesSet sender, Scene.Scene scene)
    {
      if(this.SelectedScene == scene)
      {
        this.SelectedScene = null;
      }
    }
    
    private void OnProjectNameChanged(Solution sender, string previous)
    {
      string title = "SceneEditor";
      if(!string.IsNullOrEmpty(sender.ProjectName))
      {
        title += " - " + sender.ProjectName;
      }
      
      this.Text = title;
    }
    
    #endregion
    
    #region Private data
    
    private string m_TemplatesFilepath;
    
    private Scene.Scene m_SelectedScene;
    private Shape m_SelectedShape;
    private ShapeTemplate m_ActiveTemplate;
    
    #endregion
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using CustomControls.Forms;
using CustomControls.Controls;
using SceneEditor.Scene;
using SceneEditor.Forms.Interfaces;

namespace SceneEditor.Forms.Controls
{
  partial class ProjectExplorerControl : UserControl
  {
    #region Public constructors
    
    public ProjectExplorerControl()
    {
      InitializeComponent();
      m_ProjectTreeView.Nodes.Add("Scenes");
    }
    
    #endregion

    #region Public types

    public delegate void ShapeSelectedHandler(ProjectExplorerControl sender, Shape shape);

    #endregion
    
    #region Public methods
    
    public IEditor Editor
    {
      get { return m_Editor; }
      set
      {
        if(this.Editor != null)
        {
          this.Editor.SelectedSceneChanged -= this.OnSelectedSceneChanged;
          this.Editor.SelectedShapeChanged -= this.OnSelectedShapeChanged;
        }
        
        m_Editor = value;
        if(this.Editor != null)
        {
          this.Editor.SelectedSceneChanged += this.OnSelectedSceneChanged;
          this.Editor.SelectedShapeChanged += this.OnSelectedShapeChanged;
        }
      }
    }
    
    public ScenesSet Scenes
    {
      get { return m_Scenes; }
      set
      {
        if(this.Scenes != value)
        {
          if(this.Scenes != null)
          {
            this.Scenes.SceneAdded -= this.OnSceneAdded;
            this.Scenes.SceneRemoved -= this.OnSceneRemoved;
          }
          
          m_Scenes = value;
          RebuildScenes();
          if(this.Scenes != null)
          {
            this.Scenes.SceneAdded += this.OnSceneAdded;
            this.Scenes.SceneRemoved += this.OnSceneRemoved;
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
    
    #region Private types
    
    private enum NodeLevel
    {
      PROJECT,
      SCENE,
      SHAPE
    }
    
    #endregion
    
    #region Private methods
    
    private TreeNodeEx ProjectNode
    {
      get { return m_ProjectTreeView.Nodes[0]; }
    }
    
    private Scene.Scene SelectedScene
    {
      get
      {
        if(this.Editor != null)
        {
          return this.Editor.SelectedScene;
        }
        
        return null;
      }
      
      set
      {
        if(this.Editor != null)
        {
          this.Editor.SelectedScene = value;
        }
      }
    }
    
    private Shape SelectedShape
    {
      set
      {
        if(this.Editor != null)
        {
          this.Editor.SelectedShape = value;
        }
      }
    }
    
    private void RebuildScenes()
    {
      TreeNodeEx rootNode = this.ProjectNode;
      foreach(TreeNodeEx node in rootNode.Nodes)
      {
        Scene.Scene scene = (Scene.Scene)node.Tag;
        RemoveScene(scene);
      }
      
      rootNode.Nodes.Clear();
      if(this.Scenes != null)
      {
        foreach(Scene.Scene scene in this.Scenes)
        {
          TreeNodeEx node = rootNode.Nodes.Add(scene.Name, scene);
          AddScene(scene);
          RebuildSceneNode(node);
        }
      }
      
      rootNode.Expand();
    }
    
    private void RebuildSceneNode(TreeNodeEx node)
    {
      Scene.Scene scene = (Scene.Scene)node.Tag;
      foreach(Shape shape in scene.Shapes)
      {
        node.Nodes.Add(shape.Name, shape);
      }
    }
    
    private void AddScene(Scene.Scene scene)
    {
      scene.ShapeAdded += this.OnShapeAdded;
      scene.ShapeRemoved += this.OnShapeRemoved;
    }
    
    private void RemoveScene(Scene.Scene scene)
    {
      scene.ShapeAdded -= this.OnShapeAdded;
      scene.ShapeRemoved -= this.OnShapeRemoved;
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnSceneAdded(ScenesSet sender, Scene.Scene scene)
    {
      TreeNodeEx node = this.ProjectNode.Nodes.Add(scene.Name, scene);
      RebuildSceneNode(node);
      AddScene(scene);
      node.Select();
    }
    
    private void OnSceneRemoved(ScenesSet sender, Scene.Scene scene)
    {
      RemoveScene(scene);
      TreeNodeEx node = this.ProjectNode.Nodes.FindFirstByText(scene.Name);
      node.Remove();
    }
    
    private void OnShapeAdded(Scene.Scene sender, Shape shape)
    {
      TreeNodeEx sceneNode = this.ProjectNode.Nodes.FindFirstByTag(sender);
      sceneNode.Nodes.Add(shape.Name, shape);
      sceneNode.Expand();
    }
    
    private void OnShapeRemoved(Scene.Scene sender, Shape shape)
    {
      TreeNodeEx sceneNode = this.ProjectNode.Nodes.FindFirstByTag(sender);
      TreeNodeEx shapeNode = sceneNode.Nodes.FindFirstByTag(shape);
      shapeNode.Remove();
    }
    
    private void OnSelectedSceneChanged(IEditor sender, Scene.Scene previous)
    {
      if(previous != null)
      {
        TreeNodeEx node = this.ProjectNode.Nodes.FindFirstByTag(previous);
        if(node != null)
        {
          node.BackColor = node.BackDefaultColor;
        }
      }
      
      if(sender.SelectedScene != null)
      {
        TreeNodeEx node = this.ProjectNode.Nodes.FindFirstByTag(sender.SelectedScene);
        node.Select();
        node.BackColor = Color.Yellow;
      }
    }
    
    private void OnSelectedShapeChanged(IEditor sender, Shape previous)
    {
      if(previous != null)
      {
        TreeNodeEx node = this.ProjectNode.Nodes.FindFirstRecursivelyByTag(previous);
        if(node != null)
        {
          node.BackColor = node.BackDefaultColor;
        }
      }
      
      if(sender.SelectedShape != null)
      {
        TreeNodeEx node = this.ProjectNode.Nodes.FindFirstRecursivelyByTag(sender.SelectedShape);
        node.Select();
        node.BackColor = Color.Yellow;
      }
    }
    
    private void OnNodeClick(TreeViewEx sender, MouseClickArgs args)
    {
      switch((NodeLevel)args.Node.Level)
      {
        case NodeLevel.PROJECT:
        {
          if(args.Button == MouseButtons.Right)
          {
            LightContextMenu contextMenu = new LightContextMenu();
            ItemClickHandler addSceneHandler = delegate()
            {
              CheckValueCorrectnessDelegate checker = Solution.Instance.CreateSceneNameChecker();
              string sceneName = NameGenerator.GenerateName("Scene", checker);
              Scene.Scene scene = m_Scenes.CreateScene(sceneName);
              this.SelectedScene = scene;
            };
            contextMenu.AddItem("Add scene", addSceneHandler);
            contextMenu.Show(this, args.Location);
          }
          
          break;
        }
        
        case NodeLevel.SCENE:
        {
          Scene.Scene scene = (Scene.Scene)args.Node.Tag;
          this.SelectedScene = scene;
          if(args.Button == MouseButtons.Right)
          {
            LightContextMenu contextMenu = new LightContextMenu();
            if(Settings.SceneTranslator != null)
            {
              ItemClickHandler codeToClipboardHandler = delegate()
              {
                try
                {
                  string code = Settings.SceneTranslator.Translate(m_Scenes, scene);
                  System.Windows.Forms.Clipboard.SetText(code);
                }
                catch(Exception e)
                {
                  MessageBox.Show("Translation error: " + e.Message, "Translation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              };
              contextMenu.AddItem("Copy code to clipboard", codeToClipboardHandler);
            }
            
            ItemClickHandler cloneSceneHandler = delegate()
            {
              CheckValueCorrectnessDelegate checker = Solution.Instance.CreateSceneNameChecker();
              string cloneName = NameGenerator.GenerateName(scene.Name, checker);
              Scene.Scene clone = m_Scenes.CloneScene(scene, cloneName);
              this.SelectedScene = clone;
            };
            ItemClickHandler removeSceneHandler = delegate()
            {
              m_Scenes.RemoveScene(scene);
              this.SelectedScene = null;
            };
            contextMenu.AddItem("Clone scene", cloneSceneHandler);
            contextMenu.AddItem("Remove scene", removeSceneHandler);
            contextMenu.Show(this, args.Location);
          }
          
          break;
        }
        
        case NodeLevel.SHAPE:
        {
          Shape shape = (Shape)args.Node.Tag;
          if(shape != null && m_ShapeSelectedHandler != null)
          {
            m_ShapeSelectedHandler(this, shape);
            m_ShapeSelectedHandler = null;
          }
          else
          {
            this.SelectedShape = shape;
            if(args.Button == MouseButtons.Right)
            {
              LightContextMenu contextMenu = new LightContextMenu();
              ItemClickHandler removeObjectHandler = delegate()
              {
                this.SelectedScene.RemoveShape(shape);
                this.SelectedShape = null;
              };
              contextMenu.AddItem("Remove object", removeObjectHandler);
              contextMenu.Show(this, args.Location);
            }
          }
          
          break;
        }
      }
    }
    
    private void OnBeforeLabelEdit(TreeViewEx sender, LabelEditArgs args)
    {
      args.CancelEdit = (args.Node.Level == 0);
    }
    
    private void OnAfterLabelEdit(TreeViewEx sender, LabelEditArgs args)
    {
      switch((NodeLevel)args.Node.Level)
      {
        case NodeLevel.SCENE:
        {
          Scene.Scene scene = (Scene.Scene)args.Node.Tag;
          if(args.Label != scene.Name)
          {
            CheckValueCorrectnessDelegate checker = Solution.Instance.CreateSceneNameChecker(scene.Name);
            if(ValueCheckerForm.CheckCorrectness(args.Label, checker))
            {
              scene.Name = args.Label;
            }
            else
            {
              args.CancelEdit = true;
            }
          }
          
          break;
        }
        
        case NodeLevel.SHAPE:
        {
          Shape shape = (Shape)args.Node.Tag;
          if(args.Label != shape.Name)
          {
            CheckValueCorrectnessDelegate checker =
              Solution.Instance.CreateShapeNameChecker(this.SelectedScene, shape.Name);
            if(ValueCheckerForm.CheckCorrectness(args.Label, checker))
            {
              shape.Name = args.Label;
            }
            else
            {
              args.CancelEdit = true;
            }
          }
          
          break;
        }
      }
    }
    
    private void OnTreeViewNodeAdded(TreeViewEx sender, TreeNodeEx node)
    {
      PropertiesContainer named = (PropertiesContainer)node.Tag;
      if(named != null)
      {
        named.NameChanged += this.OnObjectRenamed;
      }
    }
    
    private void OnTreeViewNodeRemoved(TreeViewEx sender, TreeNodeEx node)
    {
      PropertiesContainer named = (PropertiesContainer)node.Tag;
      if(named != null)
      {
        named.NameChanged -= this.OnObjectRenamed;
      }
    }
    
    private void OnObjectRenamed(PropertiesContainer sender, string previous)
    {
      foreach(TreeNodeEx node in this.ProjectNode.AllNodes)
      {
        if(node.Tag == sender)
        {
          node.Text = sender.Name;
        }
      }
    }

    private void OnItemDrap(TreeViewEx sender, ItemDragArgs args)
    {
      args.Enabled = (args.node.Level == (int)NodeLevel.SCENE);
    }

    private void OnItemDrapDrop(TreeViewEx sender, ItemDrapDropArgs args)
    {
      Scene.Scene scene0 = (Scene.Scene)args.SrcNode.Tag;
      Scene.Scene scene1 = (Scene.Scene)args.DstNode.Tag;
      m_Scenes.SwapScenePositions(scene0, scene1);
    }
    
    #endregion
    
    #region Private data
    
    private IEditor m_Editor;
    private ScenesSet m_Scenes;
    private ShapeSelectedHandler m_ShapeSelectedHandler;
    
    #endregion
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls.Controls;
using SceneEditor.Scene;
using SceneEditor.Forms.Interfaces;

namespace SceneEditor.Forms.Controls
{
  partial class TemplatesExplorerControl : UserControl
  {
    #region Constructors
    
    public TemplatesExplorerControl()
    {
      InitializeComponent();
      m_TemplatesTreeView.ItemHeight = Settings.TemplateIconSize.Height;
      m_TemplatesTreeView.ImageList.ImageSize = Settings.TemplateIconSize;
      m_TemplatesTreeView.Nodes.Add("Templates");
      this.SelectTemplateOnCreate = true;
    }
    
    #endregion
    
    #region Public methods
    
    public IEditor Editor
    {
      get { return m_Editor; }
      set
      {
        if(this.Editor != null)
        {
          this.Editor.ActiveTemplateChanged -= this.OnActiveTemplateChanged;
        }
        
        m_Editor = value;
        if(this.Editor != null)
        {
          this.Editor.ActiveTemplateChanged += this.OnActiveTemplateChanged;
        }
      }
    }
    
    public ScenesSet Scenes
    {
      get { return m_ScenesSet; }
      set
      {
        if(this.Scenes != value)
        {
          m_ScenesSet = value;
          if(this.Scenes != null)
          {
            this.Templates = this.Scenes.ShapeTemplatesSet;
          }
          else
          {
            this.Templates = null;
          }
        }
      }
    }
    
    public ShapeTemplatesSet Templates
    {
      get { return m_Templates; }
      private set
      {
        if(this.Templates != value)
        {
          if(this.Templates != null)
          {
            this.Templates.TemplateAdded -= this.OnTemplateAdded;
            this.Templates.TemplateRemoved -= this.OnTemplateRemoved;
          }
          
          m_Templates = value;
          RebuildTemplates();
          if(this.Templates != null)
          {
            this.Templates.TemplateAdded += this.OnTemplateAdded;
            this.Templates.TemplateRemoved += this.OnTemplateRemoved;
          }
        }
      }
    }
    
    public bool SelectTemplateOnCreate
    {
      get { return m_SelectTemplateOnCreate; }
      set { m_SelectTemplateOnCreate = value; }
    }
    
    #endregion
    
    #region Private methods
    
    private ShapeTemplate ActiveTemplate
    {
      get
      {
        if(this.Editor != null)
        {
          return this.Editor.ActiveTemplate;
        }
        
        return null;
      }
      
      set
      {
        if(this.Editor != null)
        {
          this.Editor.ActiveTemplate = value;
        }
      }
    }
    
    private TreeNodeEx TemplatesNode
    {
      get { return m_TemplatesTreeView.Nodes[0]; }
    }
    
    private void RebuildTemplates()
    {
      TreeNodeEx rootNode = this.TemplatesNode;
      rootNode.SetUsedImages(Properties.Resources.Template);
      rootNode.Nodes.Clear();
      if(this.Templates != null)
      {
        foreach(ShapeTemplate template in this.Templates)
        {
          TreeNodeEx node = rootNode.Nodes.Add(template.Name, template);
          if(template is ImageTemplate)
          {
            ImageTemplate imageTemplate = (ImageTemplate)template;
            node.SetUsedImagesPath(imageTemplate.DiffuseFilepath);
          }
          else if(template is RectTemplate)
          {
            node.SetUsedImages(Properties.Resources.Rect);
          }
          else if(template is CircleTemplate)
          {
            node.SetUsedImages(Properties.Resources.Circle);
          }
        }
      }
      
      rootNode.Expand();
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnTemplateAdded(ShapeTemplatesSet sender, ShapeTemplate template)
    {
      this.TemplatesNode.Nodes.Add(template.Name, template);
      if(this.SelectTemplateOnCreate)
      {
        this.ActiveTemplate = template;
      }
    }
    
    private void OnTemplateRemoved(ShapeTemplatesSet sender, ShapeTemplate template)
    {
      TreeNodeEx node = this.TemplatesNode.Nodes.FindFirstByText(template.Name);
      node.Remove();
    }
    
    private void OnActiveTemplateChanged(IEditor sender, ShapeTemplate previous)
    {
      if(previous != null)
      {
        TreeNodeEx node = this.TemplatesNode.Nodes.FindFirstByText(previous.Name);
        node.BackColor = node.BackDefaultColor;
      }
      
      if(this.ActiveTemplate != null)
      {
        TreeNodeEx node = this.TemplatesNode.Nodes.FindFirstByText(this.ActiveTemplate.Name);
        node.Select();
        node.BackColor = Color.Yellow;
      }
      else
      {
        m_TemplatesTreeView.SelectedNode = null;
      }
    }
    
    private void OnNodeClick(TreeViewEx sender, MouseClickArgs args)
    {
      if(args.Node.Level == 0)
      {
        if(args.Button == MouseButtons.Right)
        {
          LightContextMenu contextMenu = new LightContextMenu();
          contextMenu.AddItem("Edit templates...", this.OnEditTemplatesClick);
          contextMenu.Show(this, args.Location);
          this.ActiveTemplate = null;
        }
      }
      else if(args.Node.Level == 1)
      {
        if(args.Button == MouseButtons.Left)
        {
          this.ActiveTemplate = (ShapeTemplate)args.Node.Tag;
        }
      }
    }
    
    private void OnEditTemplatesClick()
    {
      EditShapeTemplateForm form = new EditShapeTemplateForm();
      form.Templates = m_Templates.Templates;
      form.Scenes = m_ScenesSet;
      if(form.ShowDialog(this) == DialogResult.OK)
      {
        this.ActiveTemplate = null;
      }
    }
    
    #endregion
    
    #region Private data
    
    private IEditor m_Editor;
    private ScenesSet m_ScenesSet;
    private ShapeTemplatesSet m_Templates;
    private bool m_SelectTemplateOnCreate;
    
    #endregion
  }
}

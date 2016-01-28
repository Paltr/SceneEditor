namespace SceneEditor.Forms.Controls
{
  partial class ProjectExplorerControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.m_ProjectTreeView = new CustomControls.Controls.TreeViewEx();
      this.SuspendLayout();
      // 
      // m_ProjectTreeView
      // 
      this.m_ProjectTreeView.AllowDrop = true;
      this.m_ProjectTreeView.CheckBoxes = false;
      this.m_ProjectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_ProjectTreeView.EnableCheck = true;
      this.m_ProjectTreeView.ItemHeight = 16;
      this.m_ProjectTreeView.LabelEdit = true;
      this.m_ProjectTreeView.Location = new System.Drawing.Point(0, 0);
      this.m_ProjectTreeView.MultiSelect = false;
      this.m_ProjectTreeView.Name = "m_ProjectTreeView";
      this.m_ProjectTreeView.NodeCrossLevelEnabled = false;
      this.m_ProjectTreeView.Scrollable = true;
      this.m_ProjectTreeView.SelectedNode = null;
      this.m_ProjectTreeView.ShowImages = false;
      this.m_ProjectTreeView.ShowPlusMinus = true;
      this.m_ProjectTreeView.ShowRootLines = true;
      this.m_ProjectTreeView.Size = new System.Drawing.Size(298, 420);
      this.m_ProjectTreeView.TabIndex = 0;
      this.m_ProjectTreeView.TreeViewNodeAdded += new CustomControls.Controls.TreeViewNodeAddedHandler(this.OnTreeViewNodeAdded);
      this.m_ProjectTreeView.TreeViewNodeRemoved += new CustomControls.Controls.TreeViewNodeRemovedHandler(this.OnTreeViewNodeRemoved);
      this.m_ProjectTreeView.BeforeLabelEdit += new CustomControls.Controls.TreeViewBeforeLabelEditHandler(this.OnBeforeLabelEdit);
      this.m_ProjectTreeView.AfterLabelEdit += new CustomControls.Controls.TreeViewAfterLabelEditHandler(this.OnAfterLabelEdit);
      this.m_ProjectTreeView.NodeMouseClick += new CustomControls.Controls.TreeViewMouseClickHandler(this.OnNodeClick);
      this.m_ProjectTreeView.ItemDrap += new CustomControls.Controls.TreeViewItemDrapHandler(this.OnItemDrap);
      this.m_ProjectTreeView.ItemDrapDrop += new CustomControls.Controls.TreeViewItemDrapDropHandler(this.OnItemDrapDrop);
      // 
      // ProjectExplorerControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_ProjectTreeView);
      this.Name = "ProjectExplorerControl";
      this.Size = new System.Drawing.Size(298, 420);
      this.ResumeLayout(false);

    }

    #endregion

    private CustomControls.Controls.TreeViewEx m_ProjectTreeView;
  }
}

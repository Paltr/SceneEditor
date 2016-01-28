namespace SceneEditor
{
  partial class MainForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
      this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.m_PlusBtn = new System.Windows.Forms.ToolStripButton();
      this.m_PercentsTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.m_MinusBtn = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.m_ProjectExplorer = new SceneEditor.Forms.Controls.ProjectExplorerControl();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.m_SplitContainer = new System.Windows.Forms.SplitContainer();
      this.m_SceneView = new SceneEditor.Forms.Controls.SceneView();
      this.m_PropertiesPanel = new System.Windows.Forms.Panel();
      this.m_PropertiesContainer = new SceneEditor.Forms.Controls.PropertiesContainerControl();
      this.m_TemplatesExplorer = new SceneEditor.Forms.Controls.TemplatesExplorerControl();
      this.m_SaveScenesDialog = new System.Windows.Forms.SaveFileDialog();
      this.m_OpenScenesDialog = new System.Windows.Forms.OpenFileDialog();
      this.m_SaveTemplatesDialog = new System.Windows.Forms.SaveFileDialog();
      this.m_OpenTemplatesDialog = new System.Windows.Forms.OpenFileDialog();
      this.m_GLBackground = new System.Windows.Forms.Panel();
      this.toolStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.m_SplitContainer.Panel1.SuspendLayout();
      this.m_SplitContainer.Panel2.SuspendLayout();
      this.m_SplitContainer.SuspendLayout();
      this.m_PropertiesPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripSeparator4,
            this.m_PlusBtn,
            this.m_PercentsTextBox,
            this.m_MinusBtn});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(743, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.ShowDropDownArrow = false;
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
      this.toolStripDropDownButton1.Text = "File";
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.newToolStripMenuItem.Text = "New";
      this.newToolStripMenuItem.Click += new System.EventHandler(this.OnNewScenesSplitBtnClick);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.saveToolStripMenuItem.Text = "Save";
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveScenesSplitBtnClick);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.saveAsToolStripMenuItem.Text = "Save as...";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAsScenesSplitBtnClick);
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.openToolStripMenuItem.Text = "Open...";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenScenesSplitBtnClick);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitBtnClick);
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1,
            this.loadToolStripMenuItem});
      this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
      this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.ShowDropDownArrow = false;
      this.toolStripDropDownButton2.Size = new System.Drawing.Size(66, 22);
      this.toolStripDropDownButton2.Text = "Templates";
      // 
      // newToolStripMenuItem1
      // 
      this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
      this.newToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
      this.newToolStripMenuItem1.Text = "New";
      this.newToolStripMenuItem1.Click += new System.EventHandler(this.OnNewTemplateSplitBtnClick);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(118, 6);
      // 
      // saveToolStripMenuItem1
      // 
      this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
      this.saveToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
      this.saveToolStripMenuItem1.Text = "Save";
      this.saveToolStripMenuItem1.Click += new System.EventHandler(this.OnSaveTemplateSplitBtnClick);
      // 
      // saveAsToolStripMenuItem1
      // 
      this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
      this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
      this.saveAsToolStripMenuItem1.Text = "Save as...";
      this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.OnSaveAsTemplateSplitBtnClick);
      // 
      // loadToolStripMenuItem
      // 
      this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
      this.loadToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.loadToolStripMenuItem.Text = "Open...";
      this.loadToolStripMenuItem.Click += new System.EventHandler(this.OnOpenTemplateSplitBtnClick);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // m_PlusBtn
      // 
      this.m_PlusBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_PlusBtn.Image = global::SceneEditor.Properties.Resources.Plus;
      this.m_PlusBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_PlusBtn.ImageTransparentColor = System.Drawing.Color.Fuchsia;
      this.m_PlusBtn.Name = "m_PlusBtn";
      this.m_PlusBtn.Size = new System.Drawing.Size(23, 22);
      this.m_PlusBtn.Text = "toolStripButton1";
      // 
      // m_PercentsTextBox
      // 
      this.m_PercentsTextBox.Name = "m_PercentsTextBox";
      this.m_PercentsTextBox.Size = new System.Drawing.Size(40, 25);
      // 
      // m_MinusBtn
      // 
      this.m_MinusBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_MinusBtn.Image = global::SceneEditor.Properties.Resources.Minus;
      this.m_MinusBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_MinusBtn.ImageTransparentColor = System.Drawing.Color.Fuchsia;
      this.m_MinusBtn.Name = "m_MinusBtn";
      this.m_MinusBtn.Size = new System.Drawing.Size(23, 22);
      this.m_MinusBtn.Text = "toolStripButton2";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 25);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.m_ProjectExplorer);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(743, 558);
      this.splitContainer1.SplitterDistance = 184;
      this.splitContainer1.TabIndex = 1;
      // 
      // m_ProjectExplorer
      // 
      this.m_ProjectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_ProjectExplorer.Editor = null;
      this.m_ProjectExplorer.Location = new System.Drawing.Point(0, 0);
      this.m_ProjectExplorer.Name = "m_ProjectExplorer";
      this.m_ProjectExplorer.Scenes = null;
      this.m_ProjectExplorer.Size = new System.Drawing.Size(184, 558);
      this.m_ProjectExplorer.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.m_SplitContainer);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.m_TemplatesExplorer);
      this.splitContainer2.Size = new System.Drawing.Size(555, 558);
      this.splitContainer2.SplitterDistance = 398;
      this.splitContainer2.TabIndex = 0;
      // 
      // m_SplitContainer
      // 
      this.m_SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_SplitContainer.Location = new System.Drawing.Point(0, 0);
      this.m_SplitContainer.Name = "m_SplitContainer";
      this.m_SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // m_SplitContainer.Panel1
      // 
      this.m_SplitContainer.Panel1.AutoScroll = true;
      this.m_SplitContainer.Panel1.Controls.Add(this.m_SceneView);
      this.m_SplitContainer.Panel1.Controls.Add(this.m_GLBackground);
      // 
      // m_SplitContainer.Panel2
      // 
      this.m_SplitContainer.Panel2.Controls.Add(this.m_PropertiesPanel);
      this.m_SplitContainer.Size = new System.Drawing.Size(398, 558);
      this.m_SplitContainer.SplitterDistance = 393;
      this.m_SplitContainer.TabIndex = 0;
      // 
      // m_SceneView
      // 
      this.m_SceneView.AutoSize = true;
      this.m_SceneView.BackColor = System.Drawing.Color.Black;
      this.m_SceneView.Editor = null;
      this.m_SceneView.Location = new System.Drawing.Point(0, 0);
      this.m_SceneView.Name = "m_SceneView";
      this.m_SceneView.ScalingHelper = null;
      this.m_SceneView.Size = new System.Drawing.Size(150, 150);
      this.m_SceneView.TabIndex = 0;
      this.m_SceneView.VSync = false;
      // 
      // m_PropertiesPanel
      // 
      this.m_PropertiesPanel.AutoScroll = true;
      this.m_PropertiesPanel.Controls.Add(this.m_PropertiesContainer);
      this.m_PropertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_PropertiesPanel.Location = new System.Drawing.Point(0, 0);
      this.m_PropertiesPanel.Name = "m_PropertiesPanel";
      this.m_PropertiesPanel.Size = new System.Drawing.Size(398, 161);
      this.m_PropertiesPanel.TabIndex = 0;
      // 
      // m_PropertiesContainer
      // 
      this.m_PropertiesContainer.Location = new System.Drawing.Point(3, 3);
      this.m_PropertiesContainer.Name = "m_PropertiesContainer";
      this.m_PropertiesContainer.Size = new System.Drawing.Size(376, 146);
      this.m_PropertiesContainer.TabIndex = 0;
      // 
      // m_TemplatesExplorer
      // 
      this.m_TemplatesExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_TemplatesExplorer.Editor = null;
      this.m_TemplatesExplorer.Location = new System.Drawing.Point(0, 0);
      this.m_TemplatesExplorer.Name = "m_TemplatesExplorer";
      this.m_TemplatesExplorer.Scenes = null;
      this.m_TemplatesExplorer.SelectTemplateOnCreate = true;
      this.m_TemplatesExplorer.Size = new System.Drawing.Size(153, 558);
      this.m_TemplatesExplorer.TabIndex = 0;
      // 
      // m_SaveScenesDialog
      // 
      this.m_SaveScenesDialog.DefaultExt = "scenes";
      this.m_SaveScenesDialog.Filter = "Scenes (*.Scenes)|*.Scenes";
      this.m_SaveScenesDialog.RestoreDirectory = true;
      // 
      // m_OpenScenesDialog
      // 
      this.m_OpenScenesDialog.DefaultExt = "scenes";
      this.m_OpenScenesDialog.Filter = "Scenes (*.Scenes)|*.Scenes";
      this.m_OpenScenesDialog.RestoreDirectory = true;
      // 
      // m_SaveTemplatesDialog
      // 
      this.m_SaveTemplatesDialog.DefaultExt = "template";
      this.m_SaveTemplatesDialog.Filter = "Template (*.Template)|*.Template";
      this.m_SaveTemplatesDialog.RestoreDirectory = true;
      // 
      // m_OpenTemplatesDialog
      // 
      this.m_OpenTemplatesDialog.DefaultExt = "template";
      this.m_OpenTemplatesDialog.Filter = "Template (*.Template)|*.Template";
      this.m_OpenTemplatesDialog.RestoreDirectory = true;
      // 
      // m_GLBackground
      // 
      this.m_GLBackground.Location = new System.Drawing.Point(0, 0);
      this.m_GLBackground.Name = "m_GLBackground";
      this.m_GLBackground.Size = new System.Drawing.Size(171, 174);
      this.m_GLBackground.TabIndex = 1;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(743, 583);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.toolStrip1);
      this.Name = "MainForm";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.m_SplitContainer.Panel1.ResumeLayout(false);
      this.m_SplitContainer.Panel1.PerformLayout();
      this.m_SplitContainer.Panel2.ResumeLayout(false);
      this.m_SplitContainer.ResumeLayout(false);
      this.m_PropertiesPanel.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.SplitContainer m_SplitContainer;
    private SceneEditor.Forms.Controls.TemplatesExplorerControl m_TemplatesExplorer;
    private SceneEditor.Forms.Controls.SceneView m_SceneView;
    private SceneEditor.Forms.Controls.ProjectExplorerControl m_ProjectExplorer;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.SaveFileDialog m_SaveScenesDialog;
    private System.Windows.Forms.OpenFileDialog m_OpenScenesDialog;
    private System.Windows.Forms.SaveFileDialog m_SaveTemplatesDialog;
    private System.Windows.Forms.OpenFileDialog m_OpenTemplatesDialog;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton m_PlusBtn;
    private System.Windows.Forms.ToolStripTextBox m_PercentsTextBox;
    private System.Windows.Forms.ToolStripButton m_MinusBtn;
    private System.Windows.Forms.Panel m_PropertiesPanel;
    private SceneEditor.Forms.Controls.PropertiesContainerControl m_PropertiesContainer;
    private System.Windows.Forms.Panel m_GLBackground;


  }
}


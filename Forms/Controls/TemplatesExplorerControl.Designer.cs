namespace SceneEditor.Forms.Controls
{
  partial class TemplatesExplorerControl
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
      this.m_TemplatesTreeView = new CustomControls.Controls.TreeViewEx();
      this.SuspendLayout();
      // 
      // m_TemplatesTreeView
      // 
      this.m_TemplatesTreeView.CheckBoxes = false;
      this.m_TemplatesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_TemplatesTreeView.EnableCheck = true;
      this.m_TemplatesTreeView.ItemHeight = 16;
      this.m_TemplatesTreeView.LabelEdit = false;
      this.m_TemplatesTreeView.Location = new System.Drawing.Point(0, 0);
      this.m_TemplatesTreeView.MultiSelect = false;
      this.m_TemplatesTreeView.Name = "m_TemplatesTreeView";
      this.m_TemplatesTreeView.NodeCrossLevelEnabled = false;
      this.m_TemplatesTreeView.Scrollable = true;
      this.m_TemplatesTreeView.SelectedNode = null;
      this.m_TemplatesTreeView.ShowImages = true;
      this.m_TemplatesTreeView.ShowPlusMinus = true;
      this.m_TemplatesTreeView.ShowRootLines = true;
      this.m_TemplatesTreeView.Size = new System.Drawing.Size(320, 455);
      this.m_TemplatesTreeView.TabIndex = 0;
      this.m_TemplatesTreeView.NodeMouseClick += new CustomControls.Controls.TreeViewMouseClickHandler(this.OnNodeClick);
      // 
      // TemplatesExplorerControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_TemplatesTreeView);
      this.Name = "TemplatesExplorerControl";
      this.Size = new System.Drawing.Size(320, 455);
      this.ResumeLayout(false);

    }

    #endregion

    private CustomControls.Controls.TreeViewEx m_TemplatesTreeView;
  }
}

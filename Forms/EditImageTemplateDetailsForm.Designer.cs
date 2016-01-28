namespace SceneEditor.Forms
{
  partial class EditImageTemplateDetailsForm
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
      this.m_DiffuseFilepathTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.m_BrowseDiffusePathBtn = new System.Windows.Forms.Button();
      this.m_CancelBtn = new System.Windows.Forms.Button();
      this.m_AcceptBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_DiffuseFilepathTextBox
      // 
      this.m_DiffuseFilepathTextBox.Location = new System.Drawing.Point(61, 12);
      this.m_DiffuseFilepathTextBox.Name = "m_DiffuseFilepathTextBox";
      this.m_DiffuseFilepathTextBox.Size = new System.Drawing.Size(204, 20);
      this.m_DiffuseFilepathTextBox.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(43, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Diffuse:";
      // 
      // m_BrowseDiffusePathBtn
      // 
      this.m_BrowseDiffusePathBtn.Location = new System.Drawing.Point(271, 11);
      this.m_BrowseDiffusePathBtn.Name = "m_BrowseDiffusePathBtn";
      this.m_BrowseDiffusePathBtn.Size = new System.Drawing.Size(49, 20);
      this.m_BrowseDiffusePathBtn.TabIndex = 3;
      this.m_BrowseDiffusePathBtn.Text = "...";
      this.m_BrowseDiffusePathBtn.UseVisualStyleBackColor = true;
      this.m_BrowseDiffusePathBtn.Click += new System.EventHandler(this.OnBrowseDiffusePathBtnClick);
      // 
      // m_CancelBtn
      // 
      this.m_CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_CancelBtn.Location = new System.Drawing.Point(245, 56);
      this.m_CancelBtn.Name = "m_CancelBtn";
      this.m_CancelBtn.Size = new System.Drawing.Size(75, 23);
      this.m_CancelBtn.TabIndex = 8;
      this.m_CancelBtn.Text = "Cancel";
      this.m_CancelBtn.UseVisualStyleBackColor = true;
      this.m_CancelBtn.Click += new System.EventHandler(this.OnCancelBtnClick);
      // 
      // m_AcceptBtn
      // 
      this.m_AcceptBtn.Location = new System.Drawing.Point(164, 56);
      this.m_AcceptBtn.Name = "m_AcceptBtn";
      this.m_AcceptBtn.Size = new System.Drawing.Size(75, 23);
      this.m_AcceptBtn.TabIndex = 7;
      this.m_AcceptBtn.Text = "Ok";
      this.m_AcceptBtn.UseVisualStyleBackColor = true;
      this.m_AcceptBtn.Click += new System.EventHandler(this.OnAcceptBtnClick);
      // 
      // EditImageTemplateDetailsForm
      // 
      this.AcceptButton = this.m_AcceptBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_CancelBtn;
      this.ClientSize = new System.Drawing.Size(336, 89);
      this.Controls.Add(this.m_CancelBtn);
      this.Controls.Add(this.m_AcceptBtn);
      this.Controls.Add(this.m_BrowseDiffusePathBtn);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.m_DiffuseFilepathTextBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditImageTemplateDetailsForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Edit image template details";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox m_DiffuseFilepathTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button m_BrowseDiffusePathBtn;
    private System.Windows.Forms.Button m_CancelBtn;
    private System.Windows.Forms.Button m_AcceptBtn;
  }
}
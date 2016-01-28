namespace SceneEditor.Forms
{
  partial class EditRectTemplateDetailsForm
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
      this.m_CancelBtn = new System.Windows.Forms.Button();
      this.m_AcceptBtn = new System.Windows.Forms.Button();
      this.m_NormalizedCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // m_CancelBtn
      // 
      this.m_CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_CancelBtn.Location = new System.Drawing.Point(94, 35);
      this.m_CancelBtn.Name = "m_CancelBtn";
      this.m_CancelBtn.Size = new System.Drawing.Size(75, 23);
      this.m_CancelBtn.TabIndex = 10;
      this.m_CancelBtn.Text = "Cancel";
      this.m_CancelBtn.UseVisualStyleBackColor = true;
      this.m_CancelBtn.Click += new System.EventHandler(this.OnCancelBtnClick);
      // 
      // m_AcceptBtn
      // 
      this.m_AcceptBtn.Location = new System.Drawing.Point(13, 35);
      this.m_AcceptBtn.Name = "m_AcceptBtn";
      this.m_AcceptBtn.Size = new System.Drawing.Size(75, 23);
      this.m_AcceptBtn.TabIndex = 9;
      this.m_AcceptBtn.Text = "Ok";
      this.m_AcceptBtn.UseVisualStyleBackColor = true;
      this.m_AcceptBtn.Click += new System.EventHandler(this.OnAcceptBtnClick);
      // 
      // m_NormalizedCheckBox
      // 
      this.m_NormalizedCheckBox.AutoSize = true;
      this.m_NormalizedCheckBox.Location = new System.Drawing.Point(16, 12);
      this.m_NormalizedCheckBox.Name = "m_NormalizedCheckBox";
      this.m_NormalizedCheckBox.Size = new System.Drawing.Size(72, 17);
      this.m_NormalizedCheckBox.TabIndex = 11;
      this.m_NormalizedCheckBox.Text = "Normalize";
      this.m_NormalizedCheckBox.UseVisualStyleBackColor = true;
      // 
      // EditRectTemplateDetailsForm
      // 
      this.AcceptButton = this.m_AcceptBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_CancelBtn;
      this.ClientSize = new System.Drawing.Size(176, 66);
      this.Controls.Add(this.m_NormalizedCheckBox);
      this.Controls.Add(this.m_CancelBtn);
      this.Controls.Add(this.m_AcceptBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditRectTemplateDetailsForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Edit rect template details";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button m_CancelBtn;
    private System.Windows.Forms.Button m_AcceptBtn;
    private System.Windows.Forms.CheckBox m_NormalizedCheckBox;
  }
}
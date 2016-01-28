namespace SceneEditor.Forms
{
  partial class EditCircleTemplateDetailsForm
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
      this.m_SolidCheckBox = new System.Windows.Forms.CheckBox();
      this.m_CancelBtn = new System.Windows.Forms.Button();
      this.m_AcceptBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_SolidCheckBox
      // 
      this.m_SolidCheckBox.AutoSize = true;
      this.m_SolidCheckBox.Location = new System.Drawing.Point(13, 10);
      this.m_SolidCheckBox.Name = "m_SolidCheckBox";
      this.m_SolidCheckBox.Size = new System.Drawing.Size(49, 17);
      this.m_SolidCheckBox.TabIndex = 14;
      this.m_SolidCheckBox.Text = "Solid";
      this.m_SolidCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_CancelBtn
      // 
      this.m_CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_CancelBtn.Location = new System.Drawing.Point(91, 33);
      this.m_CancelBtn.Name = "m_CancelBtn";
      this.m_CancelBtn.Size = new System.Drawing.Size(75, 23);
      this.m_CancelBtn.TabIndex = 13;
      this.m_CancelBtn.Text = "Cancel";
      this.m_CancelBtn.UseVisualStyleBackColor = true;
      this.m_CancelBtn.Click += new System.EventHandler(this.OnCancelBtnClick);
      // 
      // m_AcceptBtn
      // 
      this.m_AcceptBtn.Location = new System.Drawing.Point(10, 33);
      this.m_AcceptBtn.Name = "m_AcceptBtn";
      this.m_AcceptBtn.Size = new System.Drawing.Size(75, 23);
      this.m_AcceptBtn.TabIndex = 12;
      this.m_AcceptBtn.Text = "Ok";
      this.m_AcceptBtn.UseVisualStyleBackColor = true;
      this.m_AcceptBtn.Click += new System.EventHandler(this.OnAcceptBtnClick);
      // 
      // EditCircleTemplateDetailsForm
      // 
      this.AcceptButton = this.m_AcceptBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_CancelBtn;
      this.ClientSize = new System.Drawing.Size(176, 66);
      this.Controls.Add(this.m_SolidCheckBox);
      this.Controls.Add(this.m_CancelBtn);
      this.Controls.Add(this.m_AcceptBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditCircleTemplateDetailsForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Edit circle template";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox m_SolidCheckBox;
    private System.Windows.Forms.Button m_CancelBtn;
    private System.Windows.Forms.Button m_AcceptBtn;
  }
}
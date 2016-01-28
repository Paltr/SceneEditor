namespace SceneEditor.Forms.Controls
{
  partial class BoolPropertyControl
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
      this.m_CheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // m_CheckBox
      // 
      this.m_CheckBox.AutoSize = true;
      this.m_CheckBox.Location = new System.Drawing.Point(3, 3);
      this.m_CheckBox.Name = "m_CheckBox";
      this.m_CheckBox.Size = new System.Drawing.Size(54, 17);
      this.m_CheckBox.TabIndex = 0;
      this.m_CheckBox.Text = "Name";
      this.m_CheckBox.UseVisualStyleBackColor = true;
      // 
      // BoolPropertyControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_CheckBox);
      this.Name = "BoolPropertyControl";
      this.Size = new System.Drawing.Size(190, 30);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox m_CheckBox;
  }
}

namespace SceneEditor.Forms.Controls
{
  partial class CustomPropertyControl
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
      this.m_InputBox = new CustomControls.Controls.InputBox();
      this.m_Label = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // m_InputBox
      // 
      this.m_InputBox.ApplyControl = this.m_InputBox;
      this.m_InputBox.CancelControl = this.m_InputBox;
      this.m_InputBox.CustomValueChecker = null;
      this.m_InputBox.InputCheckerType = CustomControls.Controls.InputChecker.FLOAT;
      this.m_InputBox.Location = new System.Drawing.Point(124, 3);
      this.m_InputBox.Name = "m_InputBox";
      this.m_InputBox.Size = new System.Drawing.Size(173, 20);
      this.m_InputBox.TabIndex = 0;
      this.m_InputBox.Text = "0";
      this.m_InputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnValueSubmitted);
      // 
      // m_Label
      // 
      this.m_Label.AutoSize = true;
      this.m_Label.Location = new System.Drawing.Point(3, 6);
      this.m_Label.Name = "m_Label";
      this.m_Label.Size = new System.Drawing.Size(38, 13);
      this.m_Label.TabIndex = 1;
      this.m_Label.Text = "Name:";
      // 
      // CustomPropertyControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_Label);
      this.Controls.Add(this.m_InputBox);
      this.Name = "CustomPropertyControl";
      this.Size = new System.Drawing.Size(300, 30);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private CustomControls.Controls.InputBox m_InputBox;
    private System.Windows.Forms.Label m_Label;
  }
}

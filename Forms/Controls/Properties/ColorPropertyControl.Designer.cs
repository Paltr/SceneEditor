namespace SceneEditor.Forms.Controls
{
  partial class ColorPropertyControl
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
      this.m_ColorInputBox = new CustomControls.Controls.InputBox();
      this.m_Label = new System.Windows.Forms.Label();
      this.m_ColorButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_ColorInputBox
      // 
      this.m_ColorInputBox.ApplyControl = this.m_ColorInputBox;
      this.m_ColorInputBox.CancelControl = this.m_ColorInputBox;
      this.m_ColorInputBox.CustomValueChecker = null;
      this.m_ColorInputBox.InputCheckerType = CustomControls.Controls.InputChecker.VOID;
      this.m_ColorInputBox.Location = new System.Drawing.Point(125, 5);
      this.m_ColorInputBox.Name = "m_ColorInputBox";
      this.m_ColorInputBox.Size = new System.Drawing.Size(148, 20);
      this.m_ColorInputBox.TabIndex = 7;
      // 
      // m_Label
      // 
      this.m_Label.AutoSize = true;
      this.m_Label.Location = new System.Drawing.Point(4, 8);
      this.m_Label.Name = "m_Label";
      this.m_Label.Size = new System.Drawing.Size(34, 13);
      this.m_Label.TabIndex = 5;
      this.m_Label.Text = "Color:";
      // 
      // m_ColorButton
      // 
      this.m_ColorButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.m_ColorButton.Location = new System.Drawing.Point(277, 5);
      this.m_ColorButton.Name = "m_ColorButton";
      this.m_ColorButton.Size = new System.Drawing.Size(20, 20);
      this.m_ColorButton.TabIndex = 20;
      this.m_ColorButton.UseVisualStyleBackColor = false;
      this.m_ColorButton.Click += new System.EventHandler(this.OnColorClicked);
      // 
      // ColorPropertyControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_ColorButton);
      this.Controls.Add(this.m_ColorInputBox);
      this.Controls.Add(this.m_Label);
      this.Name = "ColorPropertyControl";
      this.Size = new System.Drawing.Size(300, 30);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private CustomControls.Controls.InputBox m_ColorInputBox;
    private System.Windows.Forms.Label m_Label;
    private System.Windows.Forms.Button m_ColorButton;
  }
}

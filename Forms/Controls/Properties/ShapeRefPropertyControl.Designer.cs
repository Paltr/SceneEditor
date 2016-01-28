namespace SceneEditor.Forms.Controls
{
  partial class ShapeRefPropertyControl
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
      this.button1 = new System.Windows.Forms.Button();
      this.m_RefPathInputBox = new CustomControls.Controls.InputBox();
      this.m_Label = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Image = global::SceneEditor.Properties.Resources.Target;
      this.button1.Location = new System.Drawing.Point(276, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(20, 20);
      this.button1.TabIndex = 3;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.OnTargetBtnClicked);
      // 
      // m_RefPathInputBox
      // 
      this.m_RefPathInputBox.ApplyControl = this.m_RefPathInputBox;
      this.m_RefPathInputBox.CancelControl = this.m_RefPathInputBox;
      this.m_RefPathInputBox.CustomValueChecker = null;
      this.m_RefPathInputBox.InputCheckerType = CustomControls.Controls.InputChecker.VOID;
      this.m_RefPathInputBox.Location = new System.Drawing.Point(124, 3);
      this.m_RefPathInputBox.Name = "m_RefPathInputBox";
      this.m_RefPathInputBox.Size = new System.Drawing.Size(148, 20);
      this.m_RefPathInputBox.TabIndex = 4;
      this.m_RefPathInputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnRefPathSubmitted);
      // 
      // m_Label
      // 
      this.m_Label.AutoSize = true;
      this.m_Label.Location = new System.Drawing.Point(3, 6);
      this.m_Label.Name = "m_Label";
      this.m_Label.Size = new System.Drawing.Size(38, 13);
      this.m_Label.TabIndex = 2;
      this.m_Label.Text = "Name:";
      // 
      // ShapeRefPropertyControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_RefPathInputBox);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.m_Label);
      this.Name = "ShapeRefPropertyControl";
      this.Size = new System.Drawing.Size(300, 30);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private CustomControls.Controls.InputBox m_RefPathInputBox;
    private System.Windows.Forms.Label m_Label;


  }
}

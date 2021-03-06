﻿namespace SceneEditor.Forms.Controls
{
  partial class SceneView
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
      this.SuspendLayout();
      // 
      // SceneView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "SceneView";
      this.MouseLeave += new GLRenderer.Controls.GLMouseEventHandler(this.OnMouseLeave);
      this.MouseEnter += new GLRenderer.Controls.GLMouseEventHandler(this.OnMouseEnter);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
      this.ResumeLayout(false);

    }

    #endregion
  }
}

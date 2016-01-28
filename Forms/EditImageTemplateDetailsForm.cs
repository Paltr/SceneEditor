using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls.Forms;
using GLRenderer;
using SceneEditor.Scene;
using Util.Math;

namespace SceneEditor.Forms
{
  partial class EditImageTemplateDetailsForm : Form
  {
    #region Constructors
    
    public EditImageTemplateDetailsForm()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public ImageTemplate Template
    {
      get { return m_Template; }
      set
      {
        if(this.Template != value)
        {
          m_Template = value;
          m_DiffuseFilepathTextBox.Enabled = (this.Template != null);
          if(this.Template != null)
          {
            this.DiffuseFilepath = this.Template.DiffuseFilepath;
          }
          else
          {
            this.DiffuseFilepath = "";
          }
        }
      }
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnBrowseDiffusePathBtnClick(object sender, EventArgs e)
    {
      ImageOpenForm imageOpenDlg = new ImageOpenForm();
      if(imageOpenDlg.ShowDialog(this) == DialogResult.OK)
      {
        this.DiffuseFilepath = imageOpenDlg.MSDialog.FileName;
      }
    }
    
    private void OnAcceptBtnClick(object sender, EventArgs e)
    {
      bool correct = true;
      if(this.Template != null)
      {
        if(this.Template.DiffuseFilepath != this.DiffuseFilepath)
        {
          if(!ApplyDiffuseChanges())
          {
      	    MessageBox.Show("Invalid Diffuse Image", "Invalid image",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            correct = false;
          }
        }
      }
      
      if(correct)
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }
    
    private void OnCancelBtnClick(object sender, EventArgs e)
    {
      this.Close();
    }
    
    #endregion
    
    #region Private methods
    
    private string DiffuseFilepath
    {
      get { return m_DiffuseFilepath; }
      set
      {
        m_DiffuseFilepath = value;
        m_DiffuseFilepathTextBox.Text = value;
      }
    }
    
    private bool ApplyDiffuseChanges()
    {
      if(!string.IsNullOrEmpty(this.DiffuseFilepath))
      {
        try
        {
          if(!string.IsNullOrEmpty(this.DiffuseFilepath))
          {
            Material material = Program.MaterialFactory.CreateMaterial(this.DiffuseFilepath);
            m_Template.Anchor = new Vector2f(material.Width / 2.0f, material.Height / 2.0f);
            m_Template.DiffuseFilepath = this.DiffuseFilepath;
            return true;
          }
          else
          {
            m_Template.DiffuseFilepath = this.DiffuseFilepath;
          }
        }
        catch(Exception)
        {
          return false;
        }
      }
      
      return true;
    }
    
    #endregion
    
    #region Private data
    
    private ImageTemplate m_Template;
    private string m_DiffuseFilepath;
    
    #endregion
  }
}

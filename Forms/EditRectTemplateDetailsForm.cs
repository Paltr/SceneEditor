using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SceneEditor.Scene;

namespace SceneEditor.Forms
{
  partial class EditRectTemplateDetailsForm : Form
  {
    #region Constructors
    
    public EditRectTemplateDetailsForm()
    {
      InitializeComponent();
    }
    
    #endregion
    
    #region Public methods
    
    public RectTemplate Template
    {
      get { return m_Template; }
      set
      {
        if(this.Template != value)
        {
          m_Template = value;
          if(this.Template != null)
          {
            m_NormalizedCheckBox.Checked = this.Template.Normalized;
          }
        }
      }
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnAcceptBtnClick(object sender, EventArgs e)
    {
      m_Template.Normalized = m_NormalizedCheckBox.Checked;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    
    private void OnCancelBtnClick(object sender, EventArgs e)
    {
      this.Close();
    }
    
    #endregion
    
    #region Private methods
    
    private RectTemplate m_Template;
    
    #endregion
  }
}

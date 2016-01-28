using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Util;
using Util.Math;
using Util.Extensions;
using CustomControls.Controls;
using SceneEditor.Scene;
using SceneEditor.Forms.Controls;

namespace SceneEditor.Forms
{
  partial class EditShapeTemplateForm : Form
  {
    #region Constructors
    
    public EditShapeTemplateForm()
    {
      InitializeComponent();
      m_TemplateTypeNames = new Dictionary<Type, string>()
      {
        { typeof(CircleTemplate), "Circle" },
        { typeof(ImageTemplate), "Image" },
        { typeof(RectTemplate), "Rect" }
      };
      foreach(KeyValuePair<Type, string> kvp in m_TemplateTypeNames)
      {
        m_TypeComboBox.Items.Add(kvp.Value);
      }
      
      m_OldNewTemplateMap = new Dictionary<ShapeTemplate, ShapeTemplate>();
    }
    
    #endregion
    
    #region Public methods
    
    public ICollection<ShapeTemplate> Templates
    {
      get
      {
        List<ShapeTemplate> result = new List<ShapeTemplate>();
        foreach(ListItem item in m_TemplatesListBox.Items)
        {
          ShapeTemplate template = (ShapeTemplate)item.Tag;
          result.Add(template);
        }
        
        return result;
      }
      
      set
      {
        m_TemplatesListBox.Items.Clear();
        m_OldNewTemplateMap.Clear();
        if(value != null)
        {
          foreach(ShapeTemplate template in value)
          {
            ShapeTemplate clone = template.Clone();
            m_OldNewTemplateMap[template] = clone;
            AddTemplate(clone);
          }
        }
        
        if(m_TemplatesListBox.Items.Count != 0)
        {
          m_TemplatesListBox.SelectedIndex = 0;
        }
        
        UpdateTemplateInfo();
      }
    }
    
    public ScenesSet Scenes
    {
      get { return m_ScenesSet; }
      set { m_ScenesSet = value; }
    }
    
    #endregion
    
    #region Private event handlers
    
    private void OnCreateBtnClick(object sender, EventArgs e)
    {
      string templateName = NameGenerator.GenerateName("Template", CreateTemplateNameChecker(""));
      ShapeTemplate template = new CircleTemplate(templateName, string.Empty);
      AddTemplate(template);
    }
    
    private void OnRemoveTemplateBtnClick(object sender, EventArgs e)
    {
      ListItem selected = m_TemplatesListBox.SelectedItem;
      if(selected != null)
      {
        m_TemplatesListBox.Items.Remove(selected);
      }
    }
    
    private void OnOutRectLeftBottomSubmitted(InputBox sender, string oldValue)
    {
      Rect2f rect = m_ShapeTemplateView.OutRect;
      rect.LeftBottom = sender.InterpretValueAsVector2();
      m_ShapeTemplateView.OutRect = rect;
    }
    
    private void OnOutRectSizeSubmitted(InputBox sender, string oldValue)
    {
      Rect2f rect = m_ShapeTemplateView.OutRect;
      rect.Size = sender.InterpretValueAsVector2();
      m_ShapeTemplateView.OutRect = rect;
    }
    
    private void OnAutoBtnClicked(object sender, EventArgs e)
    {
      m_ShapeTemplateView.AutoSize();
    }
    
    private void OnTemplateNameSubmitted(InputBox sender, string oldValue)
    {
      if(this.Template != null)
      {
        RenameShapeTemplate(this.Template, sender.Text);
      }
    }
    
    private void OnColorClicked(object sender, EventArgs e)
    {
      if(this.Template != null)
      {
        ColorDialog form = new ColorDialog();
        form.Color = this.Template.Color;
        if(form.ShowDialog() == DialogResult.OK)
        {
          this.Template.Color = form.Color;
          UpdateTemplateInfo();
          m_ShapeTemplateView.Invalidate();
        }
      }
    }

    private void OnEditableColorCheckedChanged(object sender, EventArgs e)
    {
      if(this.Template != null)
      {
        this.Template.EditableColor = m_EditableColorCheckbox.Checked;
      }
    }
    
    private void OnPlaceOnBackgroudCheckedChanged(object sender, EventArgs e)
    {
      if(this.Template != null)
      {
        this.Template.Backgroud = m_PlaceOnBackgroudCheckbox.Checked;
      }
    }

    private void OnTemplateTypeChanged(object sender, EventArgs e)
    {
      Type templateType = null;
      foreach(KeyValuePair<Type, string> kvp in m_TemplateTypeNames)
      {
        if(kvp.Value == m_TypeComboBox.Text)
        {
          templateType = kvp.Key;
        }
      }
      
      if(templateType == null)
      {
        throw new InvalidOperationException();
      }
      
      ShapeTemplate template = null;
      if(!templateType.Equals(this.Template.GetType()))
      {
        if(templateType.Equals(typeof(CircleTemplate)))
        {
          template = CreateDefaultCircleTemplate();
        }
        else if(templateType.Equals(typeof(ImageTemplate)))
        {
          template = CreateDefaultImageTemplate();
        }
        else if(templateType.Equals(typeof(RectTemplate)))
        {
          template = CreateDefaultRectTemplate();
        }
        else
        {
          throw new ArgumentException();
        }
        
        ReplaceTemplate(this.Template, template);
        UpdateTemplateInfo();
      }
    }
    
    private void OnTemplateDetailsBtnClick(object sender, EventArgs e)
    {
      if(this.Template != null)
      {
        Form form = null;
        if(this.Template is CircleTemplate)
        {
          EditCircleTemplateDetailsForm editForm = new EditCircleTemplateDetailsForm();
          editForm.Template = (CircleTemplate)this.Template;
          form = editForm;
        }
        else if(this.Template is ImageTemplate)
        {
          EditImageTemplateDetailsForm editForm = new EditImageTemplateDetailsForm();
          editForm.Template = (ImageTemplate)this.Template;
          form = editForm;
        }
        else if(this.Template is RectTemplate)
        {
          EditRectTemplateDetailsForm editForm = new EditRectTemplateDetailsForm();
          editForm.Template = (RectTemplate)this.Template;
          form = editForm;
        }
        
        if(form != null && form.ShowDialog() == DialogResult.OK)
        {
          m_ShapeTemplateView.AutoSize();
          m_ShapeTemplateView.Invalidate();
        }
      }
    }
    
    private void OnBrowsePropertiesPathBtnClick(object sender, EventArgs e)
    {
      if(m_OpenPropertiesDialog.ShowDialog() == DialogResult.OK)
      {
        m_PropertiesFilepathInputBox.Text = m_OpenPropertiesDialog.FileName;
        this.Template.PropertiesFilepath = m_PropertiesFilepathInputBox.Text;
      }
    }
    
    private void OnPropertiesFilepathSubmitted(InputBox sender, string oldValue)
    {
      this.Template.PropertiesFilepath = m_PropertiesFilepathInputBox.Text;
    }
    
    private void OnSelectedPositionSubmitted(InputBox sender, string oldValue)
    {
      if(this.SelectedManip != null)
      {
        this.SelectedManip.Position = sender.InterpretValueAsVector2();
      }
    }
    
    private void OnSelectedAngleSubmitted(InputBox sender, string oldValue)
    {
      if(this.SelectedManip != null)
      {
        float degree = sender.InterpretValueAsFloat();
        this.SelectedManip.Angle = Helpers.Deg2Rad(degree);
      }
    }
    
    private void OnSelectedTemplateChanged(ReorderableListBox sender, int prevSelectedIndex)
    {
      UpdateTemplateInfo();
    }
    
    private void OnAfterTemplateRenamed(ReorderableListBox sender, ItemLabelEditEventArgs args)
    {
      ShapeTemplate shapeTemplate = (ShapeTemplate)args.Item.Tag;
      args.CancelEdit = !RenameShapeTemplate(shapeTemplate, args.Label);
    }
    
    private void OnOutRectChanged(ShapeTemplateView sender, Rect2f oldValue)
    {
      UpdateOutRect();
    }
    
    private void OnShapeManipChanged(ShapesView sender, SpatialManip oldValue)
    {
      if(oldValue != null)
      {
        oldValue.PositionChanged -= this.OnPositionChanged;
        oldValue.AngleChanged -= this.OnAngleChanged;
      }
      
      if(sender.SelectedManip != null)
      {
        sender.SelectedManip.PositionChanged += this.OnPositionChanged;
        sender.SelectedManip.AngleChanged += this.OnAngleChanged;
      }
      
      UpdateSelectedSpatial();
    }
    
    private void OnPositionChanged(SpatialManip sender, Vector2f oldValue)
    {
      UpdateSelectedSpatial();
    }
    
    private void OnAngleChanged(SpatialManip sender, float oldValue)
    {
      UpdateSelectedSpatial();
    }
    
    private void OnPositionSwitchCheckedChanged(object sender, EventArgs e)
    {
      if(this.SelectedCircleSettings != null)
      {
        this.SelectedCircleSettings.EnableOffset = m_PositionCheckBox.Checked;
      }
    }
    
    private void OnAngleSwitchCheckedChanged(object sender, EventArgs e)
    {
      if(this.SelectedCircleSettings != null)
      {
        this.SelectedCircleSettings.EnableRotate = m_AngleCheckBox.Checked;
      }
    }
    
    private void OnAcceptBtnClick(object sender, EventArgs e)
    {
      if(this.Scenes != null)
      {
        ShapeTemplatesSet templates = this.Scenes.ShapeTemplatesSet;
        templates.RemoveAllTemplates();
        foreach(ShapeTemplate template in this.Templates)
        {
          templates.RegisterShapeTemplate(template);
        }
        
        List<Shape> shapes = this.Scenes.AllShapes;
        foreach(Shape shape in shapes)
        {
          if(m_OldNewTemplateMap.ContainsKey(shape.Template))
          {
            shape.Template = m_OldNewTemplateMap[shape.Template];
          }
        }
      }
      
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void OnCancelBtnClick(object sender, EventArgs e)
    {
      this.Close();
    }
    
    #endregion
    
    #region Private methods
    
    private CheckValueCorrectnessDelegate CreateTemplateNameChecker(string ignoredName)
    {
      CheckValueCorrectnessDelegate nameChecker = delegate(string name)
      {
        if(ignoredName == name || m_TemplatesListBox.FindStringExact(name) == -1)
        {
          return null;
        }
        else
        {
          return "Template name " + name + " is already exists";
        }
      };
      return nameChecker;
    }
    
    private ShapeTemplate Template
    {
      get { return (ShapeTemplate)m_TemplatesListBox.SelectedObject; }
      set
      {
        if(this.Template != value)
        {
          m_TemplatesListBox.SelectedObject = value;
          UpdateTemplateInfo();
        }
      }
    }
    
    private SpatialManip SelectedManip
    {
      get { return m_ShapeTemplateView.SelectedManip; }
    }
    
    private ShapeCircle SelectedCircle
    {
      get
      {
        if(this.SelectedManip != null)
        {
          List<ShapeCircle> circles = this.SelectedManip.ManipShapeCircles;
          return circles.GetFirst();
        }
        
        return null;
      }
    }
    
    private ShapeTemplate.ShapeCircleSettings SelectedCircleSettings
    {
      get
      {
        if(this.Template != null && this.SelectedCircle != null)
        {
          return this.Template.GetCircleSettings(this.SelectedCircle);
        }
        
        return null;
      }
    }
    
    private void UpdateTemplateInfo()
    {
      if(this.Template != null)
      {
        m_NameTextBox.CustomValueChecker = null;
        m_NameTextBox.Text = this.Template.Name;
        m_NameTextBox.CustomValueChecker = CreateTemplateNameChecker(this.Template.Name);
        m_ColorButton.BackColor = this.Template.Color;
        m_EditableColorCheckbox.Checked = this.Template.EditableColor;
        m_PlaceOnBackgroudCheckbox.Checked = this.Template.Backgroud;
        string name = m_TemplateTypeNames[this.Template.GetType()];
        m_TypeComboBox.SelectedIndex = m_TypeComboBox.FindStringExact(name);
        m_TemplatesListBox.SelectedObject = this.Template;
        m_TemplatesListBox.SelectedItem.Text = this.Template.Name;
        m_PropertiesFilepathInputBox.Text = this.Template.PropertiesFilepath;
        m_ShapeTemplateView.Template = this.Template;
        m_PropertiesFilepathInputBox.Text = this.Template.PropertiesFilepath;
      }
      else
      {
        m_NameTextBox.Text = "";
        m_PlaceOnBackgroudCheckbox.Checked = false;
        m_ColorButton.BackColor = Color.Black;
        m_PropertiesFilepathInputBox.Text = "";
        m_ShapeTemplateView.Template = null;
        m_PropertiesFilepathInputBox.Text = "";
      }
      
      bool controlsEnabled = (this.Template != null);
      m_RemoveTemplateBtn.Enabled = controlsEnabled;
      m_AutoOutRectButton.Enabled = controlsEnabled;
      m_NameTextBox.Enabled = controlsEnabled;
      m_TypeComboBox.Enabled = controlsEnabled;
      m_TemplateDetailsButton.Enabled = controlsEnabled;
      m_PropertiesFilepathInputBox.Enabled = controlsEnabled;
      m_BrowsePropertiesPathButton.Enabled = controlsEnabled;
      m_ShapeTemplateView.Enabled = controlsEnabled;
      m_PropertiesFilepathInputBox.Enabled = controlsEnabled;
      UpdateOutRect();
    }
    
    private string GenerateTemplateName(string baseName)
    {
      return NameGenerator.GenerateName(baseName, CreateTemplateNameChecker(""));
    }
    
    private bool RenameShapeTemplate(ShapeTemplate shapeTemplate, string name)
    {
      CheckValueCorrectnessDelegate checker = CreateTemplateNameChecker(shapeTemplate.Name);
      if(checker(name) == null)
      {
        shapeTemplate.Name = name;
        if(this.Template == shapeTemplate)
        {
          m_NameTextBox.Text = name;
          m_TemplatesListBox.SelectedItem.Text = name;
        }
        
        return true;
      }
      
      return false;
    }
    
    private CircleTemplate CreateDefaultCircleTemplate()
    {
      string templateName = GenerateTemplateName("Circle");
      return new CircleTemplate(templateName, string.Empty);
    }

    private ImageTemplate CreateDefaultImageTemplate()
    {
      string templateName = GenerateTemplateName("Image");
      return new ImageTemplate(templateName, string.Empty);
    }
    
    private RectTemplate CreateDefaultRectTemplate()
    {
      string templateName = GenerateTemplateName("Rect");
      return new RectTemplate(templateName, string.Empty);
    }
    
    private void AddTemplate(ShapeTemplate template)
    {
      m_TemplatesListBox.Items.Add(template.Name, template);
      this.Template = template;
    }
    
    private void ReplaceTemplate(ShapeTemplate template, ShapeTemplate newTemplate)
    {
      m_TemplatesListBox.Objects.Replace(template, newTemplate);
      if(this.Templates.Contains(template))
      {
        m_OldNewTemplateMap[template] = newTemplate;
      }
      else
      {
        foreach(KeyValuePair<ShapeTemplate, ShapeTemplate> kvp in m_OldNewTemplateMap)
        {
          if(kvp.Value == template)
          {
            m_OldNewTemplateMap[kvp.Key] = newTemplate;
            break;
          }
        }
      }
    }
    
    private void RemoveTemplate(ShapeTemplate template)
    {
      ReplaceTemplate(template, null);
    }
    
    private void UpdateOutRect()
    {
      Rect2f outRect = m_ShapeTemplateView.OutRect;
      m_OutLeftBottomInputBox.Text = outRect.LeftBottom.ToString();
      m_OutSizeInputBox.Text = outRect.Size.ToString();
    }
    
    private void UpdateSelectedSpatial()
    {
      m_SelectedPositionInputBox.Enabled = false;
      m_SelectedAngleInputBox.Enabled = false;
      m_SelectedPositionInputBox.ResetToDefaults();
      m_SelectedAngleInputBox.ResetToDefaults();
      
      if(this.SelectedManip != null)
      {
        if(this.SelectedManip.PositionUsed)
        {
          m_SelectedPositionInputBox.Enabled = true;
          m_SelectedPositionInputBox.Text = this.SelectedManip.Position.ToString();
        }
        
        if(this.SelectedManip.AngleUsed)
        {
          m_SelectedAngleInputBox.Enabled = true;
          m_SelectedAngleInputBox.Text = Helpers.Rad2Deg(this.SelectedManip.Angle).ToString();
        }
      }
      
      if(this.SelectedCircleSettings != null)
      {
        m_PositionCheckBox.Checked = this.SelectedCircleSettings.EnableOffset;
        m_AngleCheckBox.Checked = this.SelectedCircleSettings.EnableRotate;
      }
      else
      {
        m_PositionCheckBox.Checked = false;
        m_AngleCheckBox.Checked = false;
      }
    }
    
    #endregion
    
    #region Private data
    
    private readonly Dictionary<Type, string> m_TemplateTypeNames;
    private readonly Dictionary<ShapeTemplate, ShapeTemplate> m_OldNewTemplateMap;
    private ScenesSet m_ScenesSet;
    
    #endregion
  }
}

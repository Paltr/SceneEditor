namespace SceneEditor.Forms
{
  partial class EditShapeTemplateForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.m_ShapeTemplateView = new SceneEditor.Forms.Controls.ShapeTemplateView();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.m_EditableColorCheckbox = new System.Windows.Forms.CheckBox();
      this.m_PropertiesFilepathInputBox = new CustomControls.Controls.InputBox();
      this.label4 = new System.Windows.Forms.Label();
      this.m_ColorButton = new System.Windows.Forms.Button();
      this.m_PlaceOnBackgroudCheckbox = new System.Windows.Forms.CheckBox();
      this.m_TemplateDetailsButton = new System.Windows.Forms.Button();
      this.m_TypeComboBox = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.m_BrowsePropertiesPathButton = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.m_NameTextBox = new CustomControls.Controls.InputBox();
      this.label2 = new System.Windows.Forms.Label();
      this.m_CancelBtn = new System.Windows.Forms.Button();
      this.m_AcceptBtn = new System.Windows.Forms.Button();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.m_AngleCheckBox = new System.Windows.Forms.CheckBox();
      this.m_PositionCheckBox = new System.Windows.Forms.CheckBox();
      this.m_SelectedAngleInputBox = new CustomControls.Controls.InputBox();
      this.m_SelectedPositionInputBox = new CustomControls.Controls.InputBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.m_AutoOutRectButton = new System.Windows.Forms.Button();
      this.m_OutSizeInputBox = new CustomControls.Controls.InputBox();
      this.label6 = new System.Windows.Forms.Label();
      this.m_OutLeftBottomInputBox = new CustomControls.Controls.InputBox();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.m_TemplatesListBox = new CustomControls.Controls.ReorderableListBox();
      this.m_RemoveTemplateBtn = new System.Windows.Forms.Button();
      this.m_AddTemplateBtn = new System.Windows.Forms.Button();
      this.m_OpenPropertiesDialog = new System.Windows.Forms.OpenFileDialog();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.panel1);
      this.groupBox1.Location = new System.Drawing.Point(231, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(422, 431);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Preview";
      // 
      // panel1
      // 
      this.panel1.AutoScroll = true;
      this.panel1.Controls.Add(this.m_ShapeTemplateView);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 16);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(416, 412);
      this.panel1.TabIndex = 0;
      // 
      // m_ShapeTemplateView
      // 
      this.m_ShapeTemplateView.BackColor = System.Drawing.Color.Black;
      this.m_ShapeTemplateView.Location = new System.Drawing.Point(-3, 0);
      this.m_ShapeTemplateView.Name = "m_ShapeTemplateView";
      this.m_ShapeTemplateView.Size = new System.Drawing.Size(150, 150);
      this.m_ShapeTemplateView.TabIndex = 1;
      this.m_ShapeTemplateView.Template = null;
      this.m_ShapeTemplateView.VSync = false;
      this.m_ShapeTemplateView.OutRectChanged += new SceneEditor.Forms.Controls.ShapeTemplateView.OutRectChangedHandler(this.OnOutRectChanged);
      this.m_ShapeTemplateView.SelectedManipChanged += new SceneEditor.Forms.Controls.ShapesView.SelectedManipChangedHandler(this.OnShapeManipChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.m_EditableColorCheckbox);
      this.groupBox2.Controls.Add(this.m_PropertiesFilepathInputBox);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.m_ColorButton);
      this.groupBox2.Controls.Add(this.m_PlaceOnBackgroudCheckbox);
      this.groupBox2.Controls.Add(this.m_TemplateDetailsButton);
      this.groupBox2.Controls.Add(this.m_TypeComboBox);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.m_BrowsePropertiesPathButton);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.m_NameTextBox);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Location = new System.Drawing.Point(231, 497);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(419, 101);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Shape";
      // 
      // m_EditableColorCheckbox
      // 
      this.m_EditableColorCheckbox.AutoSize = true;
      this.m_EditableColorCheckbox.Location = new System.Drawing.Point(291, 20);
      this.m_EditableColorCheckbox.Name = "m_EditableColorCheckbox";
      this.m_EditableColorCheckbox.Size = new System.Drawing.Size(90, 17);
      this.m_EditableColorCheckbox.TabIndex = 22;
      this.m_EditableColorCheckbox.Text = "Editable color";
      this.m_EditableColorCheckbox.UseVisualStyleBackColor = true;
      this.m_EditableColorCheckbox.CheckedChanged += new System.EventHandler(this.OnEditableColorCheckedChanged);
      // 
      // m_PropertiesFilepathInputBox
      // 
      this.m_PropertiesFilepathInputBox.ApplyControl = this.m_PropertiesFilepathInputBox;
      this.m_PropertiesFilepathInputBox.CancelControl = this.m_PropertiesFilepathInputBox;
      this.m_PropertiesFilepathInputBox.CustomValueChecker = null;
      this.m_PropertiesFilepathInputBox.InputCheckerType = CustomControls.Controls.InputChecker.FILEPATH;
      this.m_PropertiesFilepathInputBox.Location = new System.Drawing.Point(70, 73);
      this.m_PropertiesFilepathInputBox.Name = "m_PropertiesFilepathInputBox";
      this.m_PropertiesFilepathInputBox.Size = new System.Drawing.Size(291, 20);
      this.m_PropertiesFilepathInputBox.TabIndex = 21;
      this.m_PropertiesFilepathInputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnPropertiesFilepathSubmitted);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(236, 21);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(31, 13);
      this.label4.TabIndex = 20;
      this.label4.Text = "Color";
      // 
      // m_ColorButton
      // 
      this.m_ColorButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.m_ColorButton.Location = new System.Drawing.Point(212, 17);
      this.m_ColorButton.Name = "m_ColorButton";
      this.m_ColorButton.Size = new System.Drawing.Size(20, 20);
      this.m_ColorButton.TabIndex = 19;
      this.m_ColorButton.UseVisualStyleBackColor = false;
      this.m_ColorButton.Click += new System.EventHandler(this.OnColorClicked);
      // 
      // m_BackgroudCheckbox
      // 
      this.m_PlaceOnBackgroudCheckbox.AutoSize = true;
      this.m_PlaceOnBackgroudCheckbox.Location = new System.Drawing.Point(291, 49);
      this.m_PlaceOnBackgroudCheckbox.Name = "m_BackgroudCheckbox";
      this.m_PlaceOnBackgroudCheckbox.Size = new System.Drawing.Size(122, 17);
      this.m_PlaceOnBackgroudCheckbox.TabIndex = 18;
      this.m_PlaceOnBackgroudCheckbox.Text = "Place on backgroud";
      this.m_PlaceOnBackgroudCheckbox.UseVisualStyleBackColor = true;
      this.m_PlaceOnBackgroudCheckbox.CheckedChanged += new System.EventHandler(this.OnPlaceOnBackgroudCheckedChanged);
      // 
      // m_TemplateDetailsButton
      // 
      this.m_TemplateDetailsButton.Enabled = false;
      this.m_TemplateDetailsButton.Location = new System.Drawing.Point(212, 46);
      this.m_TemplateDetailsButton.Name = "m_TemplateDetailsButton";
      this.m_TemplateDetailsButton.Size = new System.Drawing.Size(60, 20);
      this.m_TemplateDetailsButton.TabIndex = 17;
      this.m_TemplateDetailsButton.Text = "Details...";
      this.m_TemplateDetailsButton.UseVisualStyleBackColor = true;
      this.m_TemplateDetailsButton.Click += new System.EventHandler(this.OnTemplateDetailsBtnClick);
      // 
      // m_TypeComboBox
      // 
      this.m_TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.m_TypeComboBox.Enabled = false;
      this.m_TypeComboBox.FormattingEnabled = true;
      this.m_TypeComboBox.Location = new System.Drawing.Point(70, 45);
      this.m_TypeComboBox.Name = "m_TypeComboBox";
      this.m_TypeComboBox.Size = new System.Drawing.Size(136, 21);
      this.m_TypeComboBox.TabIndex = 16;
      this.m_TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.OnTemplateTypeChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(26, 49);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "Type:";
      // 
      // m_BrowsePropertiesPathButton
      // 
      this.m_BrowsePropertiesPathButton.Enabled = false;
      this.m_BrowsePropertiesPathButton.Location = new System.Drawing.Point(364, 73);
      this.m_BrowsePropertiesPathButton.Name = "m_BrowsePropertiesPathButton";
      this.m_BrowsePropertiesPathButton.Size = new System.Drawing.Size(49, 20);
      this.m_BrowsePropertiesPathButton.TabIndex = 14;
      this.m_BrowsePropertiesPathButton.Text = "...";
      this.m_BrowsePropertiesPathButton.UseVisualStyleBackColor = true;
      this.m_BrowsePropertiesPathButton.Click += new System.EventHandler(this.OnBrowsePropertiesPathBtnClick);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 76);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(57, 13);
      this.label5.TabIndex = 13;
      this.label5.Text = "Properties:";
      // 
      // m_NameTextBox
      // 
      this.m_NameTextBox.ApplyControl = this.m_NameTextBox;
      this.m_NameTextBox.CancelControl = this.m_NameTextBox;
      this.m_NameTextBox.CustomValueChecker = null;
      this.m_NameTextBox.Enabled = false;
      this.m_NameTextBox.InputCheckerType = CustomControls.Controls.InputChecker.VOID;
      this.m_NameTextBox.Location = new System.Drawing.Point(70, 18);
      this.m_NameTextBox.Name = "m_NameTextBox";
      this.m_NameTextBox.Size = new System.Drawing.Size(136, 20);
      this.m_NameTextBox.TabIndex = 7;
      this.m_NameTextBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnTemplateNameSubmitted);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(26, 22);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Name:";
      // 
      // m_CancelBtn
      // 
      this.m_CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_CancelBtn.Location = new System.Drawing.Point(575, 664);
      this.m_CancelBtn.Name = "m_CancelBtn";
      this.m_CancelBtn.Size = new System.Drawing.Size(75, 23);
      this.m_CancelBtn.TabIndex = 6;
      this.m_CancelBtn.Text = "Cancel";
      this.m_CancelBtn.UseVisualStyleBackColor = true;
      this.m_CancelBtn.Click += new System.EventHandler(this.OnCancelBtnClick);
      // 
      // m_AcceptBtn
      // 
      this.m_AcceptBtn.Location = new System.Drawing.Point(494, 664);
      this.m_AcceptBtn.Name = "m_AcceptBtn";
      this.m_AcceptBtn.Size = new System.Drawing.Size(75, 23);
      this.m_AcceptBtn.TabIndex = 5;
      this.m_AcceptBtn.Text = "Ok";
      this.m_AcceptBtn.UseVisualStyleBackColor = true;
      this.m_AcceptBtn.Click += new System.EventHandler(this.OnAcceptBtnClick);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.m_AngleCheckBox);
      this.groupBox3.Controls.Add(this.m_PositionCheckBox);
      this.groupBox3.Controls.Add(this.m_SelectedAngleInputBox);
      this.groupBox3.Controls.Add(this.m_SelectedPositionInputBox);
      this.groupBox3.Location = new System.Drawing.Point(231, 604);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(419, 54);
      this.groupBox3.TabIndex = 3;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Spatial";
      // 
      // m_AngleCheckBox
      // 
      this.m_AngleCheckBox.AutoSize = true;
      this.m_AngleCheckBox.Location = new System.Drawing.Point(239, 24);
      this.m_AngleCheckBox.Name = "m_AngleCheckBox";
      this.m_AngleCheckBox.Size = new System.Drawing.Size(56, 17);
      this.m_AngleCheckBox.TabIndex = 22;
      this.m_AngleCheckBox.Text = "Angle:";
      this.m_AngleCheckBox.UseVisualStyleBackColor = true;
      this.m_AngleCheckBox.CheckedChanged += new System.EventHandler(this.OnAngleSwitchCheckedChanged);
      // 
      // m_PositionCheckBox
      // 
      this.m_PositionCheckBox.AutoSize = true;
      this.m_PositionCheckBox.Location = new System.Drawing.Point(6, 24);
      this.m_PositionCheckBox.Name = "m_PositionCheckBox";
      this.m_PositionCheckBox.Size = new System.Drawing.Size(66, 17);
      this.m_PositionCheckBox.TabIndex = 21;
      this.m_PositionCheckBox.Text = "Position:";
      this.m_PositionCheckBox.UseVisualStyleBackColor = true;
      this.m_PositionCheckBox.CheckedChanged += new System.EventHandler(this.OnPositionSwitchCheckedChanged);
      // 
      // m_SelectedAngleInputBox
      // 
      this.m_SelectedAngleInputBox.ApplyControl = this.m_SelectedAngleInputBox;
      this.m_SelectedAngleInputBox.CancelControl = this.m_SelectedAngleInputBox;
      this.m_SelectedAngleInputBox.CustomValueChecker = null;
      this.m_SelectedAngleInputBox.Enabled = false;
      this.m_SelectedAngleInputBox.InputCheckerType = CustomControls.Controls.InputChecker.FLOAT;
      this.m_SelectedAngleInputBox.Location = new System.Drawing.Point(301, 22);
      this.m_SelectedAngleInputBox.Name = "m_SelectedAngleInputBox";
      this.m_SelectedAngleInputBox.Size = new System.Drawing.Size(98, 20);
      this.m_SelectedAngleInputBox.TabIndex = 20;
      this.m_SelectedAngleInputBox.Text = "0";
      this.m_SelectedAngleInputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnSelectedAngleSubmitted);
      // 
      // m_SelectedPositionInputBox
      // 
      this.m_SelectedPositionInputBox.ApplyControl = this.m_SelectedPositionInputBox;
      this.m_SelectedPositionInputBox.CancelControl = this.m_SelectedPositionInputBox;
      this.m_SelectedPositionInputBox.CustomValueChecker = null;
      this.m_SelectedPositionInputBox.Enabled = false;
      this.m_SelectedPositionInputBox.InputCheckerType = CustomControls.Controls.InputChecker.VECTOR2;
      this.m_SelectedPositionInputBox.Location = new System.Drawing.Point(78, 22);
      this.m_SelectedPositionInputBox.Name = "m_SelectedPositionInputBox";
      this.m_SelectedPositionInputBox.Size = new System.Drawing.Size(131, 20);
      this.m_SelectedPositionInputBox.TabIndex = 18;
      this.m_SelectedPositionInputBox.Text = "(0;0)";
      this.m_SelectedPositionInputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnSelectedPositionSubmitted);
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.m_AutoOutRectButton);
      this.groupBox4.Controls.Add(this.m_OutSizeInputBox);
      this.groupBox4.Controls.Add(this.label6);
      this.groupBox4.Controls.Add(this.m_OutLeftBottomInputBox);
      this.groupBox4.Controls.Add(this.label3);
      this.groupBox4.Location = new System.Drawing.Point(231, 446);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(419, 45);
      this.groupBox4.TabIndex = 7;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Viewport";
      // 
      // m_AutoOutRectButton
      // 
      this.m_AutoOutRectButton.Enabled = false;
      this.m_AutoOutRectButton.Location = new System.Drawing.Point(364, 15);
      this.m_AutoOutRectButton.Name = "m_AutoOutRectButton";
      this.m_AutoOutRectButton.Size = new System.Drawing.Size(49, 20);
      this.m_AutoOutRectButton.TabIndex = 23;
      this.m_AutoOutRectButton.Text = "Auto";
      this.m_AutoOutRectButton.UseVisualStyleBackColor = true;
      this.m_AutoOutRectButton.Click += new System.EventHandler(this.OnAutoBtnClicked);
      // 
      // m_OutSizeInputBox
      // 
      this.m_OutSizeInputBox.ApplyControl = this.m_OutSizeInputBox;
      this.m_OutSizeInputBox.CancelControl = this.m_OutSizeInputBox;
      this.m_OutSizeInputBox.CustomValueChecker = null;
      this.m_OutSizeInputBox.InputCheckerType = CustomControls.Controls.InputChecker.VECTOR2;
      this.m_OutSizeInputBox.Location = new System.Drawing.Point(238, 16);
      this.m_OutSizeInputBox.Name = "m_OutSizeInputBox";
      this.m_OutSizeInputBox.Size = new System.Drawing.Size(120, 20);
      this.m_OutSizeInputBox.TabIndex = 22;
      this.m_OutSizeInputBox.Text = "(0;0)";
      this.m_OutSizeInputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnOutRectSizeSubmitted);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(202, 19);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(30, 13);
      this.label6.TabIndex = 21;
      this.label6.Text = "Size:";
      // 
      // m_OutLeftBottomInputBox
      // 
      this.m_OutLeftBottomInputBox.ApplyControl = this.m_OutLeftBottomInputBox;
      this.m_OutLeftBottomInputBox.CancelControl = this.m_OutLeftBottomInputBox;
      this.m_OutLeftBottomInputBox.CustomValueChecker = null;
      this.m_OutLeftBottomInputBox.InputCheckerType = CustomControls.Controls.InputChecker.VECTOR2;
      this.m_OutLeftBottomInputBox.Location = new System.Drawing.Point(70, 16);
      this.m_OutLeftBottomInputBox.Name = "m_OutLeftBottomInputBox";
      this.m_OutLeftBottomInputBox.Size = new System.Drawing.Size(120, 20);
      this.m_OutLeftBottomInputBox.TabIndex = 20;
      this.m_OutLeftBottomInputBox.Text = "(0;0)";
      this.m_OutLeftBottomInputBox.InputBoxValueSubmitted += new CustomControls.Controls.InputBoxValueSubmittedHandler(this.OnOutRectLeftBottomSubmitted);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(17, 19);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(47, 13);
      this.label3.TabIndex = 19;
      this.label3.Text = "Position:";
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.m_TemplatesListBox);
      this.groupBox5.Controls.Add(this.m_RemoveTemplateBtn);
      this.groupBox5.Controls.Add(this.m_AddTemplateBtn);
      this.groupBox5.Location = new System.Drawing.Point(12, 12);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(213, 646);
      this.groupBox5.TabIndex = 8;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Templates";
      // 
      // m_TemplatesListBox
      // 
      this.m_TemplatesListBox.Behaviour = CustomControls.Controls.ReorderableListBox.BehaviourMask.REMOVE_ITEM_ON_DELETE;
      this.m_TemplatesListBox.LabelEdit = true;
      this.m_TemplatesListBox.Location = new System.Drawing.Point(6, 19);
      this.m_TemplatesListBox.Name = "m_TemplatesListBox";
      this.m_TemplatesListBox.Size = new System.Drawing.Size(199, 587);
      this.m_TemplatesListBox.TabIndex = 0;
      this.m_TemplatesListBox.SelectedIndexChanged += new CustomControls.Controls.SelectedIndexChangedHandler(this.OnSelectedTemplateChanged);
      this.m_TemplatesListBox.AfterItemLabelEdit += new CustomControls.Controls.AfterItemLabelEditHandler(this.OnAfterTemplateRenamed);
      // 
      // m_RemoveTemplateBtn
      // 
      this.m_RemoveTemplateBtn.Location = new System.Drawing.Point(113, 612);
      this.m_RemoveTemplateBtn.Name = "m_RemoveTemplateBtn";
      this.m_RemoveTemplateBtn.Size = new System.Drawing.Size(92, 23);
      this.m_RemoveTemplateBtn.TabIndex = 4;
      this.m_RemoveTemplateBtn.Text = "Remove";
      this.m_RemoveTemplateBtn.UseVisualStyleBackColor = true;
      this.m_RemoveTemplateBtn.Click += new System.EventHandler(this.OnRemoveTemplateBtnClick);
      // 
      // m_AddTemplateBtn
      // 
      this.m_AddTemplateBtn.Location = new System.Drawing.Point(6, 612);
      this.m_AddTemplateBtn.Name = "m_AddTemplateBtn";
      this.m_AddTemplateBtn.Size = new System.Drawing.Size(92, 23);
      this.m_AddTemplateBtn.TabIndex = 1;
      this.m_AddTemplateBtn.Text = "Create";
      this.m_AddTemplateBtn.UseVisualStyleBackColor = true;
      this.m_AddTemplateBtn.Click += new System.EventHandler(this.OnCreateBtnClick);
      // 
      // m_OpenPropertiesDialog
      // 
      this.m_OpenPropertiesDialog.DefaultExt = "xml";
      this.m_OpenPropertiesDialog.Filter = "xml (*.Xml)|*.Xml";
      // 
      // EditShapeTemplateForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(664, 693);
      this.Controls.Add(this.groupBox5);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.m_CancelBtn);
      this.Controls.Add(this.m_AcceptBtn);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditShapeTemplateForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Edit shape templates";
      this.groupBox1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button m_BrowsePropertiesPathButton;
    private System.Windows.Forms.Label label5;
    private CustomControls.Controls.InputBox m_NameTextBox;
    private System.Windows.Forms.Button m_CancelBtn;
    private System.Windows.Forms.Button m_AcceptBtn;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox m_TypeComboBox;
    private System.Windows.Forms.Button m_TemplateDetailsButton;
    private System.Windows.Forms.GroupBox groupBox3;
    private CustomControls.Controls.InputBox m_SelectedPositionInputBox;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button m_AutoOutRectButton;
    private CustomControls.Controls.InputBox m_OutSizeInputBox;
    private System.Windows.Forms.Label label6;
    private CustomControls.Controls.InputBox m_OutLeftBottomInputBox;
    private System.Windows.Forms.Label label3;
    private SceneEditor.Forms.Controls.ShapeTemplateView m_ShapeTemplateView;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.Button m_RemoveTemplateBtn;
    private System.Windows.Forms.Button m_AddTemplateBtn;
    private CustomControls.Controls.ReorderableListBox m_TemplatesListBox;
    private CustomControls.Controls.InputBox m_SelectedAngleInputBox;
    private System.Windows.Forms.OpenFileDialog m_OpenPropertiesDialog;
    private System.Windows.Forms.CheckBox m_AngleCheckBox;
    private System.Windows.Forms.CheckBox m_PositionCheckBox;
    private System.Windows.Forms.CheckBox m_PlaceOnBackgroudCheckbox;
    private System.Windows.Forms.Button m_ColorButton;
    private System.Windows.Forms.Label label4;
    private CustomControls.Controls.InputBox m_PropertiesFilepathInputBox;
    private System.Windows.Forms.CheckBox m_EditableColorCheckbox;
  }
}
Namespace GameEngine
    Partial Class Editor
        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.menuStrip = New System.Windows.Forms.MenuStrip()
            Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.noneRadioButton = New System.Windows.Forms.RadioButton()
            Me.decorRadioButton = New System.Windows.Forms.RadioButton()
            Me.objectsRadioButton = New System.Windows.Forms.RadioButton()
            Me.wallsRadioButton = New System.Windows.Forms.RadioButton()
            Me.listBox = New System.Windows.Forms.ListBox()
            Me.addButton = New System.Windows.Forms.Button()
            Me.removeButton = New System.Windows.Forms.Button()
            Me.xPosition = New System.Windows.Forms.NumericUpDown()
            Me.xLabel = New System.Windows.Forms.Label()
            Me.yLabel = New System.Windows.Forms.Label()
            Me.yPosition = New System.Windows.Forms.NumericUpDown()
            Me.wLabel = New System.Windows.Forms.Label()
            Me.width = New System.Windows.Forms.NumericUpDown()
            Me.hLabel = New System.Windows.Forms.Label()
            Me.height = New System.Windows.Forms.NumericUpDown()
            Me.objectTypes = New System.Windows.Forms.ListBox()
            Me.imagePathLabel = New System.Windows.Forms.Label()
            Me.imagePath = New System.Windows.Forms.TextBox()
            Me.loadImageButton = New System.Windows.Forms.Button()
            Me.layerDepthLabel = New System.Windows.Forms.Label()
            Me.layerDepth = New System.Windows.Forms.NumericUpDown()
            Me.gameGroupBox = New System.Windows.Forms.GroupBox()
            Me.resetNPC = New System.Windows.Forms.Button()
            Me.paused = New System.Windows.Forms.CheckBox()
            Me.mapSizeGroup = New System.Windows.Forms.GroupBox()
            Me.drawGridCheckBox = New System.Windows.Forms.CheckBox()
            Me.mapHeight = New System.Windows.Forms.NumericUpDown()
            Me.mapWidth = New System.Windows.Forms.NumericUpDown()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label1 = New System.Windows.Forms.Label()
            Me.decorSourceHeightLabel = New System.Windows.Forms.Label()
            Me.decorSourceHeight = New System.Windows.Forms.NumericUpDown()
            Me.decorSourceWidthLabel = New System.Windows.Forms.Label()
            Me.decorSourceWidth = New System.Windows.Forms.NumericUpDown()
            Me.decorSourceYLabel = New System.Windows.Forms.Label()
            Me.decorSourceY = New System.Windows.Forms.NumericUpDown()
            Me.decorSourceXLabel = New System.Windows.Forms.Label()
            Me.decorSourceX = New System.Windows.Forms.NumericUpDown()
            Me.sourceRectangleLabel = New System.Windows.Forms.Label()
            Me.drawSelected = New System.Windows.Forms.CheckBox()
            Me.menuStrip.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            CType((Me.xPosition), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.yPosition), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.width), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.height), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layerDepth), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.gameGroupBox.SuspendLayout()
            Me.mapSizeGroup.SuspendLayout()
            CType((Me.mapHeight), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.mapWidth), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.decorSourceHeight), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.decorSourceWidth), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.decorSourceY), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.decorSourceX), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem})
            Me.menuStrip.Location = New System.Drawing.Point(0, 0)
            Me.menuStrip.Name = "menuStrip"
            Me.menuStrip.Size = New System.Drawing.Size(284, 24)
            Me.menuStrip.TabIndex = 0
            Me.menuStrip.Text = "menuStrip1"
            AddHandler Me.menuStrip.MouseEnter, New System.EventHandler(AddressOf Me.menuStrip_MouseEnter)
            Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem})
            Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
            Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
            Me.fileToolStripMenuItem.Text = "File"
            Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
            Me.newToolStripMenuItem.ShortcutKeys = (CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N)), System.Windows.Forms.Keys))
            Me.newToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
            Me.newToolStripMenuItem.Text = "New"
            AddHandler Me.newToolStripMenuItem.Click, New System.EventHandler(AddressOf Me.newToolStripMenuItem_Click)
            Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
            Me.openToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
            Me.openToolStripMenuItem.Text = "Open"
            AddHandler Me.openToolStripMenuItem.Click, New System.EventHandler(AddressOf Me.openToolStripMenuItem_Click)
            Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
            Me.saveToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
            Me.saveToolStripMenuItem.Text = "Save"
            AddHandler Me.saveToolStripMenuItem.Click, New System.EventHandler(AddressOf Me.saveToolStripMenuItem_Click)
            Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
            Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
            Me.saveAsToolStripMenuItem.Text = "Save As..."
            AddHandler Me.saveAsToolStripMenuItem.Click, New System.EventHandler(AddressOf Me.saveAsToolStripMenuItem_Click)
            Me.groupBox1.Controls.Add(Me.noneRadioButton)
            Me.groupBox1.Controls.Add(Me.decorRadioButton)
            Me.groupBox1.Controls.Add(Me.objectsRadioButton)
            Me.groupBox1.Controls.Add(Me.wallsRadioButton)
            Me.groupBox1.Location = New System.Drawing.Point(6, 27)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(272, 118)
            Me.groupBox1.TabIndex = 1
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Create"
            Me.noneRadioButton.AutoSize = True
            Me.noneRadioButton.Location = New System.Drawing.Point(6, 21)
            Me.noneRadioButton.Name = "noneRadioButton"
            Me.noneRadioButton.Size = New System.Drawing.Size(51, 17)
            Me.noneRadioButton.TabIndex = 5
            Me.noneRadioButton.Text = "None"
            Me.noneRadioButton.UseVisualStyleBackColor = True
            AddHandler Me.noneRadioButton.CheckedChanged, New System.EventHandler(AddressOf Me.noneRadioButton_CheckedChanged)
            Me.decorRadioButton.AutoSize = True
            Me.decorRadioButton.Location = New System.Drawing.Point(6, 90)
            Me.decorRadioButton.Name = "decorRadioButton"
            Me.decorRadioButton.Size = New System.Drawing.Size(54, 17)
            Me.decorRadioButton.TabIndex = 4
            Me.decorRadioButton.Text = "Decor"
            Me.decorRadioButton.UseVisualStyleBackColor = True
            AddHandler Me.decorRadioButton.CheckedChanged, New System.EventHandler(AddressOf Me.decorRadioButton_CheckedChanged)
            Me.objectsRadioButton.AutoSize = True
            Me.objectsRadioButton.Location = New System.Drawing.Point(6, 67)
            Me.objectsRadioButton.Name = "objectsRadioButton"
            Me.objectsRadioButton.Size = New System.Drawing.Size(61, 17)
            Me.objectsRadioButton.TabIndex = 3
            Me.objectsRadioButton.Text = "Objects"
            Me.objectsRadioButton.UseVisualStyleBackColor = True
            AddHandler Me.objectsRadioButton.CheckedChanged, New System.EventHandler(AddressOf Me.objectsRadioButton_CheckedChanged)
            Me.wallsRadioButton.AutoSize = True
            Me.wallsRadioButton.Location = New System.Drawing.Point(6, 43)
            Me.wallsRadioButton.Name = "wallsRadioButton"
            Me.wallsRadioButton.Size = New System.Drawing.Size(51, 17)
            Me.wallsRadioButton.TabIndex = 2
            Me.wallsRadioButton.Text = "Walls"
            Me.wallsRadioButton.UseVisualStyleBackColor = True
            AddHandler Me.wallsRadioButton.CheckedChanged, New System.EventHandler(AddressOf Me.wallsRadioButton_CheckedChanged)
            Me.listBox.FormattingEnabled = True
            Me.listBox.Location = New System.Drawing.Point(6, 151)
            Me.listBox.Name = "listBox"
            Me.listBox.Size = New System.Drawing.Size(272, 147)
            Me.listBox.TabIndex = 2
            AddHandler Me.listBox.SelectedIndexChanged, New System.EventHandler(AddressOf Me.listBox_SelectedIndexChanged)
            Me.addButton.Location = New System.Drawing.Point(34, 304)
            Me.addButton.Name = "addButton"
            Me.addButton.Size = New System.Drawing.Size(75, 23)
            Me.addButton.TabIndex = 5
            Me.addButton.Text = "Add"
            Me.addButton.UseVisualStyleBackColor = True
            AddHandler Me.addButton.Click, New System.EventHandler(AddressOf Me.addButton_Click)
            Me.removeButton.Location = New System.Drawing.Point(162, 304)
            Me.removeButton.Name = "removeButton"
            Me.removeButton.Size = New System.Drawing.Size(75, 23)
            Me.removeButton.TabIndex = 6
            Me.removeButton.Text = "Remove"
            Me.removeButton.UseVisualStyleBackColor = True
            AddHandler Me.removeButton.Click, New System.EventHandler(AddressOf Me.removeButton_Click)
            Me.xPosition.Increment = New Decimal(New Integer() {128, 0, 0, 0})
            Me.xPosition.Location = New System.Drawing.Point(17, 333)
            Me.xPosition.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.xPosition.Minimum = New Decimal(New Integer() {90000, 0, 0, -2147483648})
            Me.xPosition.Name = "xPosition"
            Me.xPosition.Size = New System.Drawing.Size(50, 20)
            Me.xPosition.TabIndex = 7
            AddHandler Me.xPosition.ValueChanged, New System.EventHandler(AddressOf Me.xPosition_ValueChanged)
            Me.xLabel.AutoSize = True
            Me.xLabel.Location = New System.Drawing.Point(0, 335)
            Me.xLabel.Name = "xLabel"
            Me.xLabel.Size = New System.Drawing.Size(17, 13)
            Me.xLabel.TabIndex = 8
            Me.xLabel.Text = "X:"
            Me.yLabel.AutoSize = True
            Me.yLabel.Location = New System.Drawing.Point(68, 335)
            Me.yLabel.Name = "yLabel"
            Me.yLabel.Size = New System.Drawing.Size(17, 13)
            Me.yLabel.TabIndex = 10
            Me.yLabel.Text = "Y:"
            Me.yPosition.Increment = New Decimal(New Integer() {128, 0, 0, 0})
            Me.yPosition.Location = New System.Drawing.Point(85, 333)
            Me.yPosition.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.yPosition.Minimum = New Decimal(New Integer() {90000, 0, 0, -2147483648})
            Me.yPosition.Name = "yPosition"
            Me.yPosition.Size = New System.Drawing.Size(50, 20)
            Me.yPosition.TabIndex = 9
            AddHandler Me.yPosition.ValueChanged, New System.EventHandler(AddressOf Me.yPosition_ValueChanged)
            Me.wLabel.AutoSize = True
            Me.wLabel.Location = New System.Drawing.Point(137, 335)
            Me.wLabel.Name = "wLabel"
            Me.wLabel.Size = New System.Drawing.Size(21, 13)
            Me.wLabel.TabIndex = 12
            Me.wLabel.Text = "W:"
            Me.width.Increment = New Decimal(New Integer() {128, 0, 0, 0})
            Me.width.Location = New System.Drawing.Point(158, 333)
            Me.width.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.width.Name = "width"
            Me.width.Size = New System.Drawing.Size(50, 20)
            Me.width.TabIndex = 11
            AddHandler Me.width.ValueChanged, New System.EventHandler(AddressOf Me.width_ValueChanged)
            Me.hLabel.AutoSize = True
            Me.hLabel.Location = New System.Drawing.Point(209, 335)
            Me.hLabel.Name = "hLabel"
            Me.hLabel.Size = New System.Drawing.Size(18, 13)
            Me.hLabel.TabIndex = 14
            Me.hLabel.Text = "H:"
            Me.height.Increment = New Decimal(New Integer() {128, 0, 0, 0})
            Me.height.Location = New System.Drawing.Point(228, 333)
            Me.height.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.height.Name = "height"
            Me.height.Size = New System.Drawing.Size(50, 20)
            Me.height.TabIndex = 13
            AddHandler Me.height.ValueChanged, New System.EventHandler(AddressOf Me.height_ValueChanged)
            Me.objectTypes.FormattingEnabled = True
            Me.objectTypes.Location = New System.Drawing.Point(3, 359)
            Me.objectTypes.Name = "objectTypes"
            Me.objectTypes.Size = New System.Drawing.Size(275, 56)
            Me.objectTypes.TabIndex = 15
            Me.objectTypes.Visible = False
            Me.imagePathLabel.AutoSize = True
            Me.imagePathLabel.Location = New System.Drawing.Point(27, 364)
            Me.imagePathLabel.Name = "imagePathLabel"
            Me.imagePathLabel.Size = New System.Drawing.Size(39, 13)
            Me.imagePathLabel.TabIndex = 5
            Me.imagePathLabel.Text = "Image:"
            Me.imagePathLabel.Visible = False
            Me.imagePath.Enabled = False
            Me.imagePath.Location = New System.Drawing.Point(66, 361)
            Me.imagePath.Name = "imagePath"
            Me.imagePath.Size = New System.Drawing.Size(100, 20)
            Me.imagePath.TabIndex = 16
            Me.imagePath.Visible = False
            Me.loadImageButton.Location = New System.Drawing.Point(170, 359)
            Me.loadImageButton.Name = "loadImageButton"
            Me.loadImageButton.Size = New System.Drawing.Size(75, 23)
            Me.loadImageButton.TabIndex = 17
            Me.loadImageButton.Text = "Load"
            Me.loadImageButton.UseVisualStyleBackColor = True
            Me.loadImageButton.Visible = False
            AddHandler Me.loadImageButton.Click, New System.EventHandler(AddressOf Me.loadImageButton_Click)
            Me.layerDepthLabel.AutoSize = True
            Me.layerDepthLabel.Location = New System.Drawing.Point(27, 389)
            Me.layerDepthLabel.Name = "layerDepthLabel"
            Me.layerDepthLabel.Size = New System.Drawing.Size(39, 13)
            Me.layerDepthLabel.TabIndex = 18
            Me.layerDepthLabel.Text = "Depth:"
            Me.layerDepthLabel.Visible = False
            Me.layerDepth.DecimalPlaces = 3
            Me.layerDepth.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
            Me.layerDepth.Location = New System.Drawing.Point(66, 387)
            Me.layerDepth.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.layerDepth.Name = "layerDepth"
            Me.layerDepth.Size = New System.Drawing.Size(53, 20)
            Me.layerDepth.TabIndex = 19
            Me.layerDepth.Visible = False
            AddHandler Me.layerDepth.ValueChanged, New System.EventHandler(AddressOf Me.layerDepth_ValueChanged)
            Me.gameGroupBox.Controls.Add(Me.drawSelected)
            Me.gameGroupBox.Controls.Add(Me.resetNPC)
            Me.gameGroupBox.Controls.Add(Me.paused)
            Me.gameGroupBox.Location = New System.Drawing.Point(12, 510)
            Me.gameGroupBox.Name = "gameGroupBox"
            Me.gameGroupBox.Size = New System.Drawing.Size(127, 101)
            Me.gameGroupBox.TabIndex = 21
            Me.gameGroupBox.TabStop = False
            Me.gameGroupBox.Text = "Game"
            Me.resetNPC.Location = New System.Drawing.Point(7, 17)
            Me.resetNPC.Name = "resetNPC"
            Me.resetNPC.Size = New System.Drawing.Size(75, 23)
            Me.resetNPC.TabIndex = 10
            Me.resetNPC.Text = "Reset"
            Me.resetNPC.UseVisualStyleBackColor = True
            AddHandler Me.resetNPC.Click, New System.EventHandler(AddressOf Me.resetNPC_Click)
            Me.paused.AutoSize = True
            Me.paused.Location = New System.Drawing.Point(7, 73)
            Me.paused.Name = "paused"
            Me.paused.Size = New System.Drawing.Size(62, 17)
            Me.paused.TabIndex = 9
            Me.paused.Text = "Paused"
            Me.paused.UseVisualStyleBackColor = True
            AddHandler Me.paused.CheckedChanged, New System.EventHandler(AddressOf Me.paused_CheckedChanged)
            Me.mapSizeGroup.Controls.Add(Me.drawGridCheckBox)
            Me.mapSizeGroup.Controls.Add(Me.mapHeight)
            Me.mapSizeGroup.Controls.Add(Me.mapWidth)
            Me.mapSizeGroup.Controls.Add(Me.label2)
            Me.mapSizeGroup.Controls.Add(Me.label1)
            Me.mapSizeGroup.Location = New System.Drawing.Point(145, 510)
            Me.mapSizeGroup.Name = "mapSizeGroup"
            Me.mapSizeGroup.Size = New System.Drawing.Size(127, 101)
            Me.mapSizeGroup.TabIndex = 20
            Me.mapSizeGroup.TabStop = False
            Me.mapSizeGroup.Text = "Map Size"
            Me.drawGridCheckBox.AutoSize = True
            Me.drawGridCheckBox.Location = New System.Drawing.Point(6, 73)
            Me.drawGridCheckBox.Name = "drawGridCheckBox"
            Me.drawGridCheckBox.Size = New System.Drawing.Size(73, 17)
            Me.drawGridCheckBox.TabIndex = 5
            Me.drawGridCheckBox.Text = "Draw Grid"
            Me.drawGridCheckBox.UseVisualStyleBackColor = True
            Me.mapHeight.Location = New System.Drawing.Point(50, 44)
            Me.mapHeight.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
            Me.mapHeight.Name = "mapHeight"
            Me.mapHeight.Size = New System.Drawing.Size(46, 20)
            Me.mapHeight.TabIndex = 4
            Me.mapHeight.Value = New Decimal(New Integer() {17, 0, 0, 0})
            AddHandler Me.mapHeight.ValueChanged, New System.EventHandler(AddressOf Me.mapHeight_ValueChanged)
            Me.mapWidth.Location = New System.Drawing.Point(50, 17)
            Me.mapWidth.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
            Me.mapWidth.Name = "mapWidth"
            Me.mapWidth.Size = New System.Drawing.Size(46, 20)
            Me.mapWidth.TabIndex = 3
            Me.mapWidth.Value = New Decimal(New Integer() {30, 0, 0, 0})
            AddHandler Me.mapWidth.ValueChanged, New System.EventHandler(AddressOf Me.mapWidth_ValueChanged)
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(3, 46)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(41, 13)
            Me.label2.TabIndex = 0
            Me.label2.Text = "Height:"
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(3, 19)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(38, 13)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Width:"
            Me.decorSourceHeightLabel.AutoSize = True
            Me.decorSourceHeightLabel.Location = New System.Drawing.Point(209, 442)
            Me.decorSourceHeightLabel.Name = "decorSourceHeightLabel"
            Me.decorSourceHeightLabel.Size = New System.Drawing.Size(18, 13)
            Me.decorSourceHeightLabel.TabIndex = 29
            Me.decorSourceHeightLabel.Text = "H:"
            Me.decorSourceHeightLabel.Visible = False
            Me.decorSourceHeight.Location = New System.Drawing.Point(228, 440)
            Me.decorSourceHeight.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.decorSourceHeight.Name = "decorSourceHeight"
            Me.decorSourceHeight.Size = New System.Drawing.Size(50, 20)
            Me.decorSourceHeight.TabIndex = 28
            Me.decorSourceHeight.Visible = False
            AddHandler Me.decorSourceHeight.ValueChanged, New System.EventHandler(AddressOf Me.decorSourceHeight_ValueChanged)
            Me.decorSourceWidthLabel.AutoSize = True
            Me.decorSourceWidthLabel.Location = New System.Drawing.Point(137, 442)
            Me.decorSourceWidthLabel.Name = "decorSourceWidthLabel"
            Me.decorSourceWidthLabel.Size = New System.Drawing.Size(21, 13)
            Me.decorSourceWidthLabel.TabIndex = 27
            Me.decorSourceWidthLabel.Text = "W:"
            Me.decorSourceWidthLabel.Visible = False
            Me.decorSourceWidth.Location = New System.Drawing.Point(158, 440)
            Me.decorSourceWidth.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.decorSourceWidth.Name = "decorSourceWidth"
            Me.decorSourceWidth.Size = New System.Drawing.Size(50, 20)
            Me.decorSourceWidth.TabIndex = 26
            Me.decorSourceWidth.Visible = False
            AddHandler Me.decorSourceWidth.ValueChanged, New System.EventHandler(AddressOf Me.decorSourceWidth_ValueChanged)
            Me.decorSourceYLabel.AutoSize = True
            Me.decorSourceYLabel.Location = New System.Drawing.Point(68, 442)
            Me.decorSourceYLabel.Name = "decorSourceYLabel"
            Me.decorSourceYLabel.Size = New System.Drawing.Size(17, 13)
            Me.decorSourceYLabel.TabIndex = 25
            Me.decorSourceYLabel.Text = "Y:"
            Me.decorSourceYLabel.Visible = False
            Me.decorSourceY.Location = New System.Drawing.Point(85, 440)
            Me.decorSourceY.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.decorSourceY.Minimum = New Decimal(New Integer() {90000, 0, 0, -2147483648})
            Me.decorSourceY.Name = "decorSourceY"
            Me.decorSourceY.Size = New System.Drawing.Size(50, 20)
            Me.decorSourceY.TabIndex = 24
            Me.decorSourceY.Visible = False
            AddHandler Me.decorSourceY.ValueChanged, New System.EventHandler(AddressOf Me.decorSourceY_ValueChanged)
            Me.decorSourceXLabel.AutoSize = True
            Me.decorSourceXLabel.Location = New System.Drawing.Point(0, 442)
            Me.decorSourceXLabel.Name = "decorSourceXLabel"
            Me.decorSourceXLabel.Size = New System.Drawing.Size(17, 13)
            Me.decorSourceXLabel.TabIndex = 23
            Me.decorSourceXLabel.Text = "X:"
            Me.decorSourceXLabel.Visible = False
            Me.decorSourceX.Location = New System.Drawing.Point(17, 440)
            Me.decorSourceX.Maximum = New Decimal(New Integer() {90000, 0, 0, 0})
            Me.decorSourceX.Minimum = New Decimal(New Integer() {90000, 0, 0, -2147483648})
            Me.decorSourceX.Name = "decorSourceX"
            Me.decorSourceX.Size = New System.Drawing.Size(50, 20)
            Me.decorSourceX.TabIndex = 22
            Me.decorSourceX.Visible = False
            AddHandler Me.decorSourceX.ValueChanged, New System.EventHandler(AddressOf Me.decorSourceX_ValueChanged)
            Me.sourceRectangleLabel.AutoSize = True
            Me.sourceRectangleLabel.Location = New System.Drawing.Point(0, 420)
            Me.sourceRectangleLabel.Name = "sourceRectangleLabel"
            Me.sourceRectangleLabel.Size = New System.Drawing.Size(96, 13)
            Me.sourceRectangleLabel.TabIndex = 30
            Me.sourceRectangleLabel.Text = "Source Rectangle:"
            Me.sourceRectangleLabel.Visible = False
            Me.drawSelected.AutoSize = True
            Me.drawSelected.Checked = True
            Me.drawSelected.CheckState = System.Windows.Forms.CheckState.Checked
            Me.drawSelected.Location = New System.Drawing.Point(7, 49)
            Me.drawSelected.Name = "drawSelected"
            Me.drawSelected.Size = New System.Drawing.Size(112, 17)
            Me.drawSelected.TabIndex = 6
            Me.drawSelected.Text = "Highlight Selected"
            Me.drawSelected.UseVisualStyleBackColor = True
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 623)
            Me.Controls.Add(Me.sourceRectangleLabel)
            Me.Controls.Add(Me.decorSourceHeightLabel)
            Me.Controls.Add(Me.decorSourceHeight)
            Me.Controls.Add(Me.decorSourceWidthLabel)
            Me.Controls.Add(Me.decorSourceWidth)
            Me.Controls.Add(Me.decorSourceYLabel)
            Me.Controls.Add(Me.decorSourceY)
            Me.Controls.Add(Me.decorSourceXLabel)
            Me.Controls.Add(Me.decorSourceX)
            Me.Controls.Add(Me.gameGroupBox)
            Me.Controls.Add(Me.mapSizeGroup)
            Me.Controls.Add(Me.layerDepth)
            Me.Controls.Add(Me.layerDepthLabel)
            Me.Controls.Add(Me.loadImageButton)
            Me.Controls.Add(Me.imagePath)
            Me.Controls.Add(Me.imagePathLabel)
            Me.Controls.Add(Me.objectTypes)
            Me.Controls.Add(Me.hLabel)
            Me.Controls.Add(Me.height)
            Me.Controls.Add(Me.wLabel)
            Me.Controls.Add(Me.width)
            Me.Controls.Add(Me.yLabel)
            Me.Controls.Add(Me.yPosition)
            Me.Controls.Add(Me.xLabel)
            Me.Controls.Add(Me.xPosition)
            Me.Controls.Add(Me.removeButton)
            Me.Controls.Add(Me.addButton)
            Me.Controls.Add(Me.listBox)
            Me.Controls.Add(Me.groupBox1)
            Me.Controls.Add(Me.menuStrip)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MainMenuStrip = Me.menuStrip
            Me.MaximizeBox = False
            Me.Name = "Editor"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
            Me.Text = "Editor"
            AddHandler Me.FormClosing, New System.Windows.Forms.FormClosingEventHandler(AddressOf Me.Editor_FormClosing)
            Me.menuStrip.ResumeLayout(False)
            Me.menuStrip.PerformLayout()
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            CType((Me.xPosition), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.yPosition), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.width), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.height), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layerDepth), System.ComponentModel.ISupportInitialize).EndInit()
            Me.gameGroupBox.ResumeLayout(False)
            Me.gameGroupBox.PerformLayout()
            Me.mapSizeGroup.ResumeLayout(False)
            Me.mapSizeGroup.PerformLayout()
            CType((Me.mapHeight), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.mapWidth), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.decorSourceHeight), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.decorSourceWidth), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.decorSourceY), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.decorSourceX), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

        Private menuStrip As System.Windows.Forms.MenuStrip
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private decorRadioButton As System.Windows.Forms.RadioButton
        Private objectsRadioButton As System.Windows.Forms.RadioButton
        Private wallsRadioButton As System.Windows.Forms.RadioButton
        Private listBox As System.Windows.Forms.ListBox
        Private addButton As System.Windows.Forms.Button
        Private removeButton As System.Windows.Forms.Button
        Private xPosition As System.Windows.Forms.NumericUpDown
        Private xLabel As System.Windows.Forms.Label
        Private yLabel As System.Windows.Forms.Label
        Private yPosition As System.Windows.Forms.NumericUpDown
        Private wLabel As System.Windows.Forms.Label
        Private width As System.Windows.Forms.NumericUpDown
        Private hLabel As System.Windows.Forms.Label
        Private height As System.Windows.Forms.NumericUpDown
        Private objectTypes As System.Windows.Forms.ListBox
        Private imagePathLabel As System.Windows.Forms.Label
        Private imagePath As System.Windows.Forms.TextBox
        Private loadImageButton As System.Windows.Forms.Button
        Private layerDepthLabel As System.Windows.Forms.Label
        Private layerDepth As System.Windows.Forms.NumericUpDown
        Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Private newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Private openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Private saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Private saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Private gameGroupBox As System.Windows.Forms.GroupBox
        Private resetNPC As System.Windows.Forms.Button
        Private mapSizeGroup As System.Windows.Forms.GroupBox
        Private drawGridCheckBox As System.Windows.Forms.CheckBox
        Public mapHeight As System.Windows.Forms.NumericUpDown
        Public mapWidth As System.Windows.Forms.NumericUpDown
        Private label2 As System.Windows.Forms.Label
        Private label1 As System.Windows.Forms.Label
        Public paused As System.Windows.Forms.CheckBox
        Private decorSourceHeightLabel As System.Windows.Forms.Label
        Private decorSourceHeight As System.Windows.Forms.NumericUpDown
        Private decorSourceWidthLabel As System.Windows.Forms.Label
        Private decorSourceWidth As System.Windows.Forms.NumericUpDown
        Private decorSourceYLabel As System.Windows.Forms.Label
        Private decorSourceY As System.Windows.Forms.NumericUpDown
        Private decorSourceXLabel As System.Windows.Forms.Label
        Private decorSourceX As System.Windows.Forms.NumericUpDown
        Private sourceRectangleLabel As System.Windows.Forms.Label
        Private noneRadioButton As System.Windows.Forms.RadioButton
        Private drawSelected As System.Windows.Forms.CheckBox
    End Class
End Namespace

namespace GpsFlashControl
{
    partial class MainUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUnit));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDeviceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveRouteMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRouteMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.readCoordinatesMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dataToExelMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coordinatesMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelCoordinatesMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.autoCalcDistanceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateDistanceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setComPortMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.configDeviceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDeviceVersionMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDeviceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.setVirtualPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolBar = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.readCoordtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dataToExceltoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteCoordtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.calcDistancetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.playBtn = new System.Windows.Forms.ToolStripButton();
            this.stopBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.autoCalcDistanceCheckBox = new System.Windows.Forms.CheckBox();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainMenu.SuspendLayout();
            this.mainToolBar.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMainMenuItem,
            this.coordinatesMainMenuItem,
            this.serviceMainMenuItem,
            this.helpMainMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(927, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileMainMenuItem
            // 
            this.fileMainMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDeviceMainMenuItem,
            this.toolStripSeparator1,
            this.saveRouteMainMenuItem,
            this.loadRouteMainMenuItem,
            this.toolStripSeparator2,
            this.readCoordinatesMainMenuItem,
            this.toolStripSeparator3,
            this.dataToExelMainMenuItem,
            this.toolStripSeparator5,
            this.exitMainMenuItem});
            this.fileMainMenuItem.Name = "fileMainMenuItem";
            this.fileMainMenuItem.Size = new System.Drawing.Size(45, 20);
            this.fileMainMenuItem.Text = "Файл";
            // 
            // newDeviceMainMenuItem
            // 
            this.newDeviceMainMenuItem.Name = "newDeviceMainMenuItem";
            this.newDeviceMainMenuItem.Size = new System.Drawing.Size(252, 22);
            this.newDeviceMainMenuItem.Text = "Новое устройство...";
            this.newDeviceMainMenuItem.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // saveRouteMainMenuItem
            // 
            this.saveRouteMainMenuItem.Name = "saveRouteMainMenuItem";
            this.saveRouteMainMenuItem.Size = new System.Drawing.Size(252, 22);
            this.saveRouteMainMenuItem.Text = "Сохранить активный маршрут...";
            this.saveRouteMainMenuItem.Click += new System.EventHandler(this.saveRouteMainMenuItem_Click);
            // 
            // loadRouteMainMenuItem
            // 
            this.loadRouteMainMenuItem.Name = "loadRouteMainMenuItem";
            this.loadRouteMainMenuItem.Size = new System.Drawing.Size(252, 22);
            this.loadRouteMainMenuItem.Text = "Загрузить маршрут...";
            this.loadRouteMainMenuItem.Click += new System.EventHandler(this.loadRouteMainMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(249, 6);
            // 
            // readCoordinatesMainMenuItem
            // 
            this.readCoordinatesMainMenuItem.Name = "readCoordinatesMainMenuItem";
            this.readCoordinatesMainMenuItem.Size = new System.Drawing.Size(252, 22);
            this.readCoordinatesMainMenuItem.Text = "Считать координаты";
            this.readCoordinatesMainMenuItem.Click += new System.EventHandler(this.readCoordinatesMainMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(249, 6);
            // 
            // dataToExelMainMenuItem
            // 
            this.dataToExelMainMenuItem.Name = "dataToExelMainMenuItem";
            this.dataToExelMainMenuItem.Size = new System.Drawing.Size(252, 22);
            this.dataToExelMainMenuItem.Text = "Экспорт данных в Microsoft Exel";
            this.dataToExelMainMenuItem.Click += new System.EventHandler(this.dataToExelMainMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(249, 6);
            // 
            // exitMainMenuItem
            // 
            this.exitMainMenuItem.Name = "exitMainMenuItem";
            this.exitMainMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitMainMenuItem.Size = new System.Drawing.Size(252, 22);
            this.exitMainMenuItem.Text = "Выход";
            this.exitMainMenuItem.Click += new System.EventHandler(this.exitMainMenuItem_Click);
            // 
            // coordinatesMainMenuItem
            // 
            this.coordinatesMainMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.deleteSelCoordinatesMainMenuItem,
            this.toolStripSeparator6,
            this.autoCalcDistanceMainMenuItem,
            this.calculateDistanceMainMenuItem});
            this.coordinatesMainMenuItem.Name = "coordinatesMainMenuItem";
            this.coordinatesMainMenuItem.Size = new System.Drawing.Size(83, 20);
            this.coordinatesMainMenuItem.Text = "Координаты";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.selectAllToolStripMenuItem.Text = "Выделить все";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // deleteSelCoordinatesMainMenuItem
            // 
            this.deleteSelCoordinatesMainMenuItem.Name = "deleteSelCoordinatesMainMenuItem";
            this.deleteSelCoordinatesMainMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteSelCoordinatesMainMenuItem.Size = new System.Drawing.Size(293, 22);
            this.deleteSelCoordinatesMainMenuItem.Text = "Удалить выделенные координаты";
            this.deleteSelCoordinatesMainMenuItem.Click += new System.EventHandler(this.deleteSelCoordinatesMainMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(290, 6);
            // 
            // autoCalcDistanceMainMenuItem
            // 
            this.autoCalcDistanceMainMenuItem.Checked = true;
            this.autoCalcDistanceMainMenuItem.CheckOnClick = true;
            this.autoCalcDistanceMainMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoCalcDistanceMainMenuItem.Name = "autoCalcDistanceMainMenuItem";
            this.autoCalcDistanceMainMenuItem.Size = new System.Drawing.Size(293, 22);
            this.autoCalcDistanceMainMenuItem.Text = "Автоматически расчитывать дистанцию";
            this.autoCalcDistanceMainMenuItem.Click += new System.EventHandler(this.autoCalcDistanceMainMenuItem_Click);
            // 
            // calculateDistanceMainMenuItem
            // 
            this.calculateDistanceMainMenuItem.Name = "calculateDistanceMainMenuItem";
            this.calculateDistanceMainMenuItem.Size = new System.Drawing.Size(293, 22);
            this.calculateDistanceMainMenuItem.Text = "Рассчитать дистанцию";
            this.calculateDistanceMainMenuItem.Click += new System.EventHandler(this.calculateDistanceMainMenuItem_Click);
            // 
            // serviceMainMenuItem
            // 
            this.serviceMainMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setComPortMainMenuItem,
            this.toolStripSeparator10,
            this.configDeviceMainMenuItem,
            this.getDeviceVersionMainMenuItem,
            this.clearDeviceMainMenuItem,
            this.toolStripSeparator7,
            this.setVirtualPortToolStripMenuItem});
            this.serviceMainMenuItem.Name = "serviceMainMenuItem";
            this.serviceMainMenuItem.Size = new System.Drawing.Size(55, 20);
            this.serviceMainMenuItem.Text = "Сервис";
            // 
            // setComPortMainMenuItem
            // 
            this.setComPortMainMenuItem.Name = "setComPortMainMenuItem";
            this.setComPortMainMenuItem.Size = new System.Drawing.Size(258, 22);
            this.setComPortMainMenuItem.Text = "Задать COM-порт...";
            this.setComPortMainMenuItem.Click += new System.EventHandler(this.setComPortMainMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(255, 6);
            // 
            // configDeviceMainMenuItem
            // 
            this.configDeviceMainMenuItem.Name = "configDeviceMainMenuItem";
            this.configDeviceMainMenuItem.Size = new System.Drawing.Size(258, 22);
            this.configDeviceMainMenuItem.Text = "Конфигурация устройства...";
            this.configDeviceMainMenuItem.Click += new System.EventHandler(this.configDeviceMainMenuItem_Click);
            // 
            // getDeviceVersionMainMenuItem
            // 
            this.getDeviceVersionMainMenuItem.Name = "getDeviceVersionMainMenuItem";
            this.getDeviceVersionMainMenuItem.Size = new System.Drawing.Size(258, 22);
            this.getDeviceVersionMainMenuItem.Text = "Считать версию устройства";
            this.getDeviceVersionMainMenuItem.Click += new System.EventHandler(this.getDeviceVersionMainMenuItem_Click);
            // 
            // clearDeviceMainMenuItem
            // 
            this.clearDeviceMainMenuItem.Name = "clearDeviceMainMenuItem";
            this.clearDeviceMainMenuItem.Size = new System.Drawing.Size(258, 22);
            this.clearDeviceMainMenuItem.Text = "Очистить устройство";
            this.clearDeviceMainMenuItem.Click += new System.EventHandler(this.clearDeviceMainMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(255, 6);
            // 
            // setVirtualPortToolStripMenuItem
            // 
            this.setVirtualPortToolStripMenuItem.Name = "setVirtualPortToolStripMenuItem";
            this.setVirtualPortToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.setVirtualPortToolStripMenuItem.Text = "Настройка виртуального порта...";
            this.setVirtualPortToolStripMenuItem.Click += new System.EventHandler(this.setVirtualPortToolStripMenuItem_Click);
            // 
            // helpMainMenuItem
            // 
            this.helpMainMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMainMenuItem});
            this.helpMainMenuItem.Name = "helpMainMenuItem";
            this.helpMainMenuItem.Size = new System.Drawing.Size(59, 20);
            this.helpMainMenuItem.Text = "Помощь";
            // 
            // aboutMainMenuItem
            // 
            this.aboutMainMenuItem.Name = "aboutMainMenuItem";
            this.aboutMainMenuItem.Size = new System.Drawing.Size(161, 22);
            this.aboutMainMenuItem.Text = "О программе...";
            this.aboutMainMenuItem.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // mainToolBar
            // 
            this.mainToolBar.BackColor = System.Drawing.SystemColors.Control;
            this.mainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.toolStripSeparator,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator4,
            this.readCoordtoolStripButton,
            this.dataToExceltoolStripButton,
            this.toolStripSeparator8,
            this.deleteCoordtoolStripButton,
            this.toolStripSeparator11,
            this.calcDistancetoolStripButton,
            this.toolStripSeparator12,
            this.playBtn,
            this.stopBtn,
            this.toolStripSeparator9,
            this.helpToolStripButton,
            this.toolStripSeparator13});
            this.mainToolBar.Location = new System.Drawing.Point(0, 24);
            this.mainToolBar.Name = "mainToolBar";
            this.mainToolBar.Size = new System.Drawing.Size(927, 25);
            this.mainToolBar.TabIndex = 1;
            this.mainToolBar.Text = "mainToolBar";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&Новое устройство";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Загрузить маршрут";
            this.openToolStripButton.Click += new System.EventHandler(this.loadRouteMainMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Сохранить активный маршрут";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveRouteMainMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // readCoordtoolStripButton
            // 
            this.readCoordtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.readCoordtoolStripButton.Image = global::GpsFlashControl.Properties.Resources.arrow16;
            this.readCoordtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.readCoordtoolStripButton.Name = "readCoordtoolStripButton";
            this.readCoordtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.readCoordtoolStripButton.Text = "Считать координаты";
            this.readCoordtoolStripButton.Click += new System.EventHandler(this.readCoordinatesMainMenuItem_Click);
            // 
            // dataToExceltoolStripButton
            // 
            this.dataToExceltoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dataToExceltoolStripButton.Image = global::GpsFlashControl.Properties.Resources.excel16;
            this.dataToExceltoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataToExceltoolStripButton.Name = "dataToExceltoolStripButton";
            this.dataToExceltoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.dataToExceltoolStripButton.Text = "Передать координаты в Excel";
            this.dataToExceltoolStripButton.Click += new System.EventHandler(this.dataToExelMainMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // deleteCoordtoolStripButton
            // 
            this.deleteCoordtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteCoordtoolStripButton.Image = global::GpsFlashControl.Properties.Resources.delete16;
            this.deleteCoordtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteCoordtoolStripButton.Name = "deleteCoordtoolStripButton";
            this.deleteCoordtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteCoordtoolStripButton.Text = "Удалить выделенные координаты";
            this.deleteCoordtoolStripButton.Click += new System.EventHandler(this.deleteSelCoordinatesMainMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // calcDistancetoolStripButton
            // 
            this.calcDistancetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.calcDistancetoolStripButton.Image = global::GpsFlashControl.Properties.Resources.calcDist16;
            this.calcDistancetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.calcDistancetoolStripButton.Name = "calcDistancetoolStripButton";
            this.calcDistancetoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.calcDistancetoolStripButton.Text = "Рассчитать дистанцию";
            this.calcDistancetoolStripButton.Click += new System.EventHandler(this.calculateDistanceMainMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // playBtn
            // 
            this.playBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playBtn.Enabled = false;
            this.playBtn.Image = global::GpsFlashControl.Properties.Resources.plat16;
            this.playBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(23, 22);
            this.playBtn.Text = "Начать возпроизведение маршрута";
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopBtn.Enabled = false;
            this.stopBtn.Image = global::GpsFlashControl.Properties.Resources.stop16;
            this.stopBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(23, 22);
            this.stopBtn.Text = "Остановить воспроизвидение маршрута";
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "Помощь";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // autoCalcDistanceCheckBox
            // 
            this.autoCalcDistanceCheckBox.AutoSize = true;
            this.autoCalcDistanceCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.autoCalcDistanceCheckBox.Checked = true;
            this.autoCalcDistanceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoCalcDistanceCheckBox.Location = new System.Drawing.Point(287, 29);
            this.autoCalcDistanceCheckBox.Name = "autoCalcDistanceCheckBox";
            this.autoCalcDistanceCheckBox.Size = new System.Drawing.Size(236, 17);
            this.autoCalcDistanceCheckBox.TabIndex = 2;
            this.autoCalcDistanceCheckBox.Text = "Автоматически рассчитывать дистанцию";
            this.autoCalcDistanceCheckBox.UseVisualStyleBackColor = false;
            this.autoCalcDistanceCheckBox.Click += new System.EventHandler(this.autoCalcDistanceCheckBox_Click);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusMain.Location = new System.Drawing.Point(0, 471);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(927, 22);
            this.statusMain.TabIndex = 3;
            this.statusMain.Text = "statusMain";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Maximum = 4096;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "GPS file|*.gps";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "GPS file|*.gps";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 422);
            this.tabControl1.TabIndex = 4;
            // 
            // MainUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 493);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.autoCalcDistanceCheckBox);
            this.Controls.Add(this.mainToolBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "MainUnit";
            this.Text = "GPS - Навигатор";
            this.Load += new System.EventHandler(this.MainUnit_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUnit_FormClosing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainToolBar.ResumeLayout(false);
            this.mainToolBar.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coordinatesMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDeviceMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveRouteMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRouteMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem readCoordinatesMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem dataToExelMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem deleteSelCoordinatesMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem autoCalcDistanceMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateDistanceMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setComPortMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem configDeviceMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDeviceVersionMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDeviceMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMainMenuItem;
        private System.Windows.Forms.ToolStrip mainToolBar;
        private System.Windows.Forms.CheckBox autoCalcDistanceCheckBox;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton readCoordtoolStripButton;
        private System.Windows.Forms.ToolStripButton dataToExceltoolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton deleteCoordtoolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton calcDistancetoolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem setVirtualPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton stopBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton playBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    }
}


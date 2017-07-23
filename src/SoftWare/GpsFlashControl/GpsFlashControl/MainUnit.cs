using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GpsFlashControl
{
    public partial class MainUnit : Form
    {
        VoidString addTab;
        VoidControl addGrid;
        VoidString setProgress;
        GridVoid getSelectedGrid;
        VoidString setPlayBtnStatus;
        VoidString setStopBtnStatus;
        //VoidControl clearTab;
        
        static System.IO.Ports.SerialPort port;
        static SerialPortListener serial_listener;
        static int pagecount;
        static List<FlashRecord> records;
        static int currentRow;

        //static string currentDeviceId;

        SetPortDialog setPortDialog;
        SetVirtualPortDialog setVirtualPortDialog;
        SerialNET.VPort virtualport;
        System.Timers.Timer dummyTimer = new System.Timers.Timer(200);
        System.Timers.Timer workingTimer = new System.Timers.Timer(1000);
        DeviceConfigForm deviceConfigDialog;
        
        public MainUnit()
        {
            InitializeComponent();
        }

        ContextMenuStrip CreateNewTabContextMenu()
        {
            ContextMenuStrip strip = new ContextMenuStrip();
            strip.Items.Add("Выделить все", null, new EventHandler(selectAllToolStripMenuItem_Click));
            strip.Items.Add("Удалить выделенные координаты", null, new EventHandler(deleteSelCoordinatesMainMenuItem_Click));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add("Закрыть вкладку", null, new EventHandler(CloseThisTab));
            return strip;
        }

        void CloseThisTab(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                GetDeviceIdentifier();
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Сначала выберите COM-порт", "Не выбран COM-порт", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#pragma warning restore 168
        }

        private void MainUnit_Load(object sender, EventArgs e)
        {
            //SetPortParams();
            addTab = this.AddTab;
            setProgress = SetProgress;
            getSelectedGrid = this.GetSelectedGrid;
            //addGrid = tabControl1.SelectedTab.Controls.Add;
            pagecount = 0;
            records = new List<FlashRecord>();
            SerialNET.License license = new SerialNET.License();
            license.LicenseKey = "SZ7336762100000043NYj4iaj4qdBSAaFA79";
            virtualport = new SerialNET.VPort();
            virtualport.OnOpened += new SerialNET.OnOpened(virtualport_OnOpened);
            virtualport.OnClosed += new SerialNET.OnClosed(virtualport_OnClosed);
            dummyTimer.AutoReset = true;
            dummyTimer.Elapsed += new System.Timers.ElapsedEventHandler(dummyTimer_Elapsed);
            workingTimer.AutoReset = true;
            workingTimer.Elapsed += new System.Timers.ElapsedEventHandler(workingTimer_Elapsed);
            setPlayBtnStatus = new VoidString(SetPlayBtnStatus);
            setStopBtnStatus = new VoidString(SetStopBtnStatus);

            try
            {
                if (System.IO.File.Exists(Application.StartupPath + @"\port.cfg"))
                {
                    System.IO.Stream fstream = System.IO.File.OpenRead(Application.StartupPath + @"\port.cfg");
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    string portname = (string)bf.Deserialize(fstream);
                    SetupComPort(portname);
                    fstream.Close();
                }
            }
#pragma warning disable 168
            catch (Exception except)
            {
            }
#pragma warning restore 168
        }

        void workingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DataGridView grid = (this.Invoke(getSelectedGrid) as DataGridView);
                if (currentRow >= (grid.Rows.Count - 1))
                {
                    currentRow = 0;
                }
                else
                {
                    //(sender as System.Timers.Timer).Stop();
                    StringBuilder buf;

                    buf = new StringBuilder("GPRMC,");

                    DateTime time = DateTime.Parse((grid.Rows[currentRow].Cells[0].Value as string) + " " + (grid.Rows[currentRow].Cells[1].Value as string));
                    time = DateTime.SpecifyKind(time, DateTimeKind.Local).ToUniversalTime();
                    string timestr = time.ToLongTimeString();
                    if (timestr.Length == 7)
                        timestr = "0" + timestr;
                    buf.Append(timestr.Replace(":", ""));
                    buf.Append(".000,A,");

                    buf.Append((grid.Rows[currentRow].Cells[2].Value as string).Replace("°", "").Replace(',', '.'));
                    buf.Append(",N,0");
                    buf.Append((grid.Rows[currentRow].Cells[3].Value as string).Replace("°", "").Replace(',', '.'));
                    buf.Append(",E,27.00,243.66,");
                    //buf.Append((grid.Rows[currentRow].Cells[0].Value as string).Replace(".", ""));
                    buf.Append(time.ToShortDateString().Replace(".", ""));
                    buf.Remove(buf.Length - 4, 2);
                    buf.Append(",,,A");

                    /*buf = buf.Replace(":", "");
                    buf = buf.Replace(".", "");
                    buf = buf.Replace(",", "");
                    buf = buf.Replace("°", "");
                    buf = buf.Remove(buf.Length - 4, 2);*/

                    virtualport.DataToPort("$" + buf.ToString() + "*" + SerialPortListener.CalculateCRC(buf.ToString()) + "\r\n");
                    string ggaSentence = "GPGGA," + buf.ToString().Substring(7, 11) + buf.ToString().Substring(20, 25) + "1,06,1.4,180.5,M,27.8,M,,0000";
                    virtualport.DataToPort("$" + ggaSentence + "*" + SerialPortListener.CalculateCRC(ggaSentence) + "\r\n");
                    virtualport.DataToPort("$GPGSA,A,3,29,30,31,05,24,02,,,,,,,3.4,1.4,.1*39\r\n");

                    if (currentRow % 5 == 0)
                    {
                        virtualport.DataToPort("$GPGSV,3,1,11,30,80,169,21,29,70,267,46,05,53,140,31,24,45,178,31*7F\r\n");
                        virtualport.DataToPort("$GPGSV,3,2,11,12,40,128,,02,40,059,27,31,37,295,46,10,17,105,*72\r\n");
                        virtualport.DataToPort("$GPGSV,3,3,11,21,09,203,,04,02,039,,23,01,357,*4F\r\n");
                    }

                    currentRow++;
                }
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                this.Invoke(new EventHandler(stopBtn_Click), new object(), new EventArgs());
                MessageBox.Show("Нет данных для отображения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception except)
            {
            }
#pragma warning restore 168
        }

        void dummyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string message = "GPRMC,095344.000,V,5025.8244,N,03032.6861,E,0.00,243.66,211108,,,A";
                virtualport.DataToPort("$" + message + "*" + SerialPortListener.CalculateCRC(message) + "\r\n");
            }
#pragma warning disable 168
            catch (Exception except)
            {
            }
#pragma warning restore 168
        }

        void virtualport_OnClosed()
        {
            if (dummyTimer.Enabled)
                dummyTimer.Stop();
            if (workingTimer.Enabled)
                workingTimer.Stop();
            if (stopBtn.Enabled)
                this.Invoke(setStopBtnStatus, "false");
            if (playBtn.Enabled)
                this.Invoke(setPlayBtnStatus, "false");
            //workingTimer.Stop();
        }

        DataGridView GetSelectedGrid()
        {
            return (tabControl1.SelectedTab.Controls[0] as DataGridView);
        }

        void virtualport_OnOpened()
        {
            //DataGridView grid = (this.Invoke(getSelectedGrid) as DataGridView);
            //StringBuilder buf;

            //while (true)
            //{
            //    for (int i = 0; i < grid.Rows.Count - 1; i++)
            //    {
            //        //string buf = (row.Cells[2].Value as string) + "0" + (row.Cells[3].Value as string) + "N" + (row.Cells[1].Value as string) + (row.Cells[0].Value as string);

            //        buf = new StringBuilder("GPRMC,");

            //        buf.Append((grid.Rows[i].Cells[1].Value as string).Replace(":", "").Remove(buf.Length - 4, 2));
            //        buf.Append(".00,A,");

            //        buf.Append((grid.Rows[i].Cells[2].Value as string).Replace("°", "").Replace(',', '.'));
            //        buf.Append(",N,0");
            //        buf.Append((grid.Rows[i].Cells[3].Value as string).Replace("°", "").Replace(',', '.'));
            //        buf.Append(",E,0.0,0.0,");
            //        buf.Append((grid.Rows[i].Cells[0].Value as string).Replace(".", ""));
            //        buf.Append(",0.0,E,A");

            //        /*buf = buf.Replace(":", "");
            //        buf = buf.Replace(".", "");
            //        buf = buf.Replace(",", "");
            //        buf = buf.Replace("°", "");
            //        buf = buf.Remove(buf.Length - 4, 2);*/

            //        virtualport.DataToPort("$" + buf.ToString() + "*" + SerialPortListener.CalculateCRC(buf.ToString()) + "\r\n");
            //    }
            //}
            this.Invoke(setPlayBtnStatus, "true");
            dummyTimer.Start();
            //workingTimer.Start();
        }

        void serial_listener_EraseOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show("Память устройства успешно очищена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Invoke(setProgress, "0");
        }

        void serial_listener_ErasePageReceived(object sender, StringDataReceivedEventArgs e)
        {
            this.Invoke(setProgress, e.Message);
        }

        void serial_listener_DeviceErrorReceived(object sender, StringDataReceivedEventArgs e)
        {
            serial_listener.EndListening();
            MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void serial_listener_SetDeviceSerialSpeedOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            port.BaudRate = 230400;
            ReadNextPage();
        }

        void serial_listener_PageReceived(object sender, StringDataReceivedEventArgs e)
        {
            foreach (string s in e.Message.TrimEnd(new char[] { '?', 'A', 'U' }).Split(new char[] { 'A', 'B' }))
            {
                if (String.IsNullOrEmpty(s))
                    continue;
                else
                    records.Add(new FlashRecord(s, true));
            }
            this.Invoke(setProgress, pagecount.ToString());
            pagecount++;
            ReadNextPage();
        }

        void serial_listener_DeviceVersionReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show(e.Message, "Информация о устройстве", MessageBoxButtons.OK, MessageBoxIcon.Information);
            serial_listener.EndListening();
        }

        void AddTab(string tabname)
        {
            if (tabControl1.TabPages.ContainsKey(tabname))
            {
                tabControl1.SelectedIndex = tabControl1.TabPages.IndexOfKey(tabname);
            }
            else
            {
                tabControl1.TabPages.Add(tabname, tabname);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].ContextMenuStrip = CreateNewTabContextMenu();
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            }

            foreach (Control ctrl in tabControl1.SelectedTab.Controls)
                tabControl1.SelectedTab.Controls.Remove(ctrl);
            addGrid = tabControl1.SelectedTab.Controls.Add;
        }
        
        void serial_listener_DeviceIdReceived(object sender, StringDataReceivedEventArgs e)
        {
            tabControl1.Invoke(addTab, e.Message);
            serial_listener.EndListening();
        }

        private static void SetPortParams(string portname)
        {
            port = new System.IO.Ports.SerialPort(portname, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            port.NewLine = "\r\n";
        }

        void GetDeviceIdentifier()
        {
            serial_listener.BeginListening();
            string cmd = "PCHND";
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        void GetDeviceVersion()
        {
            serial_listener.BeginListening();
            string cmd = "PCVER";
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        void EraseFlash()
        {
            serial_listener.BeginListening();
            string cmd = "PCCEF";
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        void ReadPage(string pagenum)
        {
            string cmd = "PCREP " + pagenum;
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        void SetDefaults()
        {
            string cmd = "PCSFS";
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        void SetRecTimeInterval(string interval)
        {
            string cmd = "PCSDT " + interval;
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        void SetDeviceSpeed(string speed)
        {
            if (speed == "9600" || speed == "19200" || speed == "38400" || speed == "57600" || speed == "115200" || speed == "230400")
            {
                string cmd = "PCSCS " + speed;
                port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
            }
            else
                throw new ArgumentOutOfRangeException("speed", "Only 9600, 19200, 38400, 57600, 115200, 230400 values are allowed");
        }

        private void getDeviceVersionMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetDeviceVersion();
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Сначала выберите COM-порт", "Не выбран COM-порт", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#pragma warning restore 168
        }

        private void readCoordinatesMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                records = new List<FlashRecord>();
                GetDeviceIdentifier();
                serial_listener.BeginListening();
                SetDeviceSpeed("230400");
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Сначала выберите COM-порт", "Не выбран COM-порт", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#pragma warning restore 168
            //ReadNextPage();
        }

        private void ReadNextPage()
        {
            if (pagecount >= 0 && pagecount < 4096)
            {
                ReadPage(pagecount.ToString("D4"));
                //pagecount++;
            }
            else
            {
                serial_listener.EndListening();
                port.BaudRate = 9600;
                pagecount = 0;
                this.Invoke(setProgress, "0");
                records.Sort();
                DataGridView datagrid = CreateNewDatagrid();
                foreach (FlashRecord rec in records)
                    datagrid.Rows.Add(rec.ToStringArray());
                //tabControl1.SelectedTab.Controls.Add(datagrid);
                if (autoCalcDistanceCheckBox.Checked)
                    CalculateDistance(datagrid);
                tabControl1.Invoke(addGrid, datagrid);
            }
        }

        DataGridView CreateNewDatagrid()
        {
            DataGridView grid = new DataGridView();
            grid.ColumnCount = 6;
            grid.Columns[0].HeaderText = "Дата";
            grid.Columns[1].HeaderText = "Время";
            grid.Columns[2].HeaderText = "Широта";
            grid.Columns[3].HeaderText = "Долгота";
            grid.Columns[4].HeaderText = "От предыдущей, м";
            grid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[5].HeaderText = "Всего, м";
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.Dock = DockStyle.Fill;
            grid.AllowUserToResizeRows = false;
            return grid;
        }

        void SetProgress(string progress)
        {
            toolStripProgressBar1.Value = int.Parse(progress);
        }

        private void clearDeviceMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EraseFlash();
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Сначала выберите COM-порт", "Не выбран COM-порт", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#pragma warning restore 168
        }

        private void CalculateDistance(DataGridView grid)
        {
            double lat1, lat2, long1, long2, sum = 0;

            string[] buf;
            
            for (int i = 1; i < grid.Rows.Count - 1; i++)
            {
                buf = (grid.Rows[i - 1].Cells[2].Value as string).Split('°');
                lat1 = double.Parse(buf[0], System.Globalization.NumberStyles.Number) + (double.Parse(buf[1], System.Globalization.NumberStyles.Number)) / 60;

                buf = (grid.Rows[i - 1].Cells[3].Value as string).Split('°');
                long1 = double.Parse(buf[0], System.Globalization.NumberStyles.Number) + (double.Parse(buf[1], System.Globalization.NumberStyles.Number)) / 60;

                buf = (grid.Rows[i].Cells[2].Value as string).Split('°');
                lat2 = double.Parse(buf[0], System.Globalization.NumberStyles.Number) + (double.Parse(buf[1], System.Globalization.NumberStyles.Number)) / 60;

                buf = (grid.Rows[i].Cells[3].Value as string).Split('°');
                long2 = double.Parse(buf[0], System.Globalization.NumberStyles.Number) + (double.Parse(buf[1], System.Globalization.NumberStyles.Number)) / 60;

                double tmp = GetDistanceFromCoord(lat1, lat2, long1, long2);
                if (tmp < 0.1)
                    tmp = 0;
                sum += tmp;

                grid.Rows[i].Cells[4].Value = tmp.ToString("F2", System.Globalization.CultureInfo.CreateSpecificCulture("uk-UA"));
                grid.Rows[i].Cells[5].Value = sum.ToString("F2", System.Globalization.CultureInfo.CreateSpecificCulture("uk-UA"));
            }
        }

        double GetDistanceFromCoord(double lat1, double lat2, double long1, double long2)
        {
            double l1 = lat1 * Math.PI / 180;
            double l2 = lat2 * Math.PI / 180;

            double L = Math.Sin(l1) * Math.Sin(l2) + Math.Cos(l1) * Math.Cos(l2) * Math.Cos((long2 - long1) * Math.PI / 180);

            if (L > 1)
                L = 1;
            if (L < -1)
                L = -1;

            return 6378137 * Math.Acos(L);
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            (new AboutBox()).Show();
        }

        private void autoCalcDistanceCheckBox_Click(object sender, EventArgs e)
        {
            autoCalcDistanceMainMenuItem.Checked = (sender as CheckBox).Checked;
        }

        private void autoCalcDistanceMainMenuItem_Click(object sender, EventArgs e)
        {
            autoCalcDistanceCheckBox.Checked = (sender as ToolStripMenuItem).Checked;
        }

        private void saveRouteMainMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataGridView grid = tabControl1.SelectedTab.Controls[0] as DataGridView;

                    ReadSelectedRecordsFromTable(grid);
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.Stream stream = saveFileDialog1.OpenFile();
                    formatter.Serialize(stream, records);
                    stream.Close();
                }
#pragma warning disable 168
                catch (NullReferenceException except)
                {
                }
#pragma warning restore 168
            }
        }

        void ReadRecordsFromTable(DataGridView grid)
        {
            StringBuilder buf;
            
            records = new List<FlashRecord>();
            //foreach (DataGridViewRow row in grid.Rows)
            for (int i=0; i<grid.Rows.Count-1; i++)
            {
                //string buf = (row.Cells[2].Value as string) + "0" + (row.Cells[3].Value as string) + "N" + (row.Cells[1].Value as string) + (row.Cells[0].Value as string);
                
                buf = new StringBuilder(grid.Rows[i].Cells[2].Value as string);
                buf.Append('0');
                buf.Append(grid.Rows[i].Cells[3].Value as string);
                buf.Append('N');
                string timestring = grid.Rows[i].Cells[1].Value as string;
                string datestring = grid.Rows[i].Cells[0].Value as string;
                if (timestring.Length < 8)
                    timestring = "0" + timestring;
                if (datestring.Length < 8)
                    datestring = "0" + datestring;
                buf.Append(timestring);
                buf.Append(datestring);
                buf.Replace(":", "");
                buf.Replace(".", "");
                buf.Replace(",", "");
                buf.Replace("°", "");
                buf.Remove(buf.Length - 4, 2);

                /*buf = buf.Replace(":", "");
                buf = buf.Replace(".", "");
                buf = buf.Replace(",", "");
                buf = buf.Replace("°", "");
                buf = buf.Remove(buf.Length - 4, 2);*/

                records.Add(new FlashRecord(buf.ToString()));
            }
        }

        void ReadSelectedRecordsFromTable(DataGridView grid)
        {
            StringBuilder buf;

            records = new List<FlashRecord>();
            //foreach (DataGridViewRow row in grid.Rows)
            for (int i = 0; i < grid.SelectedRows.Count; i++)
            {
                //string buf = (row.Cells[2].Value as string) + "0" + (row.Cells[3].Value as string) + "N" + (row.Cells[1].Value as string) + (row.Cells[0].Value as string);
                if (grid.Rows.IndexOf(grid.SelectedRows[i]) != grid.Rows.Count - 1)
                {
                    buf = new StringBuilder(grid.SelectedRows[i].Cells[2].Value as string);
                    buf.Append('0');
                    buf.Append(grid.SelectedRows[i].Cells[3].Value as string);
                    buf.Append('N');
                    string timestring = grid.SelectedRows[i].Cells[1].Value as string;
                    string datestring = grid.SelectedRows[i].Cells[0].Value as string;
                    if (timestring.Length < 8)
                        timestring = "0" + timestring;
                    if (datestring.Length < 8)
                        datestring = "0" + datestring;
                    buf.Append(timestring);
                    buf.Append(datestring);
                    buf.Replace(":", "");
                    buf.Replace(".", "");
                    buf.Replace(",", "");
                    buf.Replace("°", "");
                    buf.Remove(buf.Length - 4, 2);
                    /*buf = buf.Replace(":", "");
                    buf = buf.Replace(".", "");
                    buf = buf.Replace(",", "");
                    buf = buf.Replace("°", "");
                    buf = buf.Remove(buf.Length - 4, 2);*/

                    records.Add(new FlashRecord(buf.ToString()));
                }
            }
        }

        private void loadRouteMainMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.Stream stream = openFileDialog1.OpenFile();
                    records = (List<FlashRecord>)formatter.Deserialize(stream);
                    records.Sort();

                    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    if (tabControl1.TabPages.ContainsKey(filename))
                        tabControl1.TabPages.RemoveByKey(filename);

                    tabControl1.TabPages.Add(filename, filename);
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].ContextMenuStrip = CreateNewTabContextMenu();
                    tabControl1.SelectTab(filename);
                    DataGridView grid = CreateNewDatagrid();
                    foreach (FlashRecord rec in records)
                    {
                        grid.Rows.Add(rec.ToStringArray());
                    }
                    if (autoCalcDistanceCheckBox.Checked)
                        CalculateDistance(grid);
                    tabControl1.SelectedTab.Controls.Add(grid);
                    stream.Close();
                }
#pragma warning disable 168
                catch (System.Runtime.Serialization.SerializationException except)
                {
                    MessageBox.Show("Не удается распознать формат файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#pragma warning restore
            }
        }

        private void calculateDistanceMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView grid = tabControl1.SelectedTab.Controls[0] as DataGridView;
                tabControl1.SelectedTab.Controls.Remove(grid);
                ReadRecordsFromTable(grid);
                grid = CreateNewDatagrid();
                foreach (FlashRecord rec in records)
                    grid.Rows.Add(rec.ToStringArray());
                CalculateDistance(grid);
                tabControl1.SelectedTab.Controls.Add(grid);
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
            }
#pragma warning restore 168
        }

        private void deleteSelCoordinatesMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView grid = tabControl1.SelectedTab.Controls[0] as DataGridView;
                if (MessageBox.Show("Вы действительно хотите удалить выделенные координаты?", "Удаление координат", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tabControl1.SelectedTab.Controls.Remove(grid);

                    int[] indexes = new int[grid.SelectedRows.Count];

                    for (int i = 0; i < grid.SelectedRows.Count; i++)
                    {
                        indexes[i] = grid.Rows.IndexOf(grid.SelectedRows[i]);
                    }

                    ReadRecordsFromTable(grid);
                    grid = CreateNewDatagrid();
                    foreach (FlashRecord rec in records)
                        grid.Rows.Add(rec.ToStringArray());
                    foreach (int index in indexes)
                    {
                        if (index != grid.Rows.Count - 1)
                            grid.Rows.RemoveAt(index);
                    }

                    if (autoCalcDistanceCheckBox.Checked)
                        CalculateDistance(grid);

                    tabControl1.SelectedTab.Controls.Add(grid);
                }
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
            }
#pragma warning restore 168
        }

        private void dataToExelMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbooks wbs = excel.Workbooks;

                object obj = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Add(obj);
                Microsoft.Office.Interop.Excel.Sheets ss = wb.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)ss.get_Item(1);

                //object[] headers = { "First", "Second", "Third1111111111" };
                //Microsoft.Office.Interop.Excel.Range range = ws.get_Range("A1", "C1");
                //range.Value2 = headers;
                //range.AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatReport1, obj, obj, obj, obj, obj, obj);

                //range.Font.Bold = true;


                //range = ws.get_Range("A2", obj);
                //range = range.get_Resize(3, 3);
                //range.Borders.Value = true;
                ////range.AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple, obj, obj, obj, obj, obj, obj);
                //object[,] data = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
                //range.Value2 = data;
                //excel.Visible = true;

                DataGridView grid = tabControl1.SelectedTab.Controls[0] as DataGridView;

                Microsoft.Office.Interop.Excel.Range range = ws.get_Range("A1", obj);
                range = range.get_Resize(grid.Rows.Count, 6);
                object[,] data = new object[grid.Rows.Count, 6];

                data[0, 0] = "Дата";
                data[0, 1] = "Время";
                data[0, 2] = "Широта";
                data[0, 3] = "Долгота";
                data[0, 4] = "От предыдущей, м";
                data[0, 5] = "Всего, м";

                for (int i = 0; i < grid.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        data[i + 1, j] = grid[j, i].Value;
                    }
                }
                range.Value2 = data;
                range.AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple, obj, obj, obj, obj, obj, obj);


                excel.Visible = true;
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Нет данных для отображения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception except)
            {
                MessageBox.Show("При передаче данных в Excel произошла ошибка", "Ошибка-", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#pragma warning restore 168
        }

        private void setComPortMainMenuItem_Click(object sender, EventArgs e)
        {
            setPortDialog = new SetPortDialog();
            if (setPortDialog.ShowDialog() == DialogResult.OK)
            {
                SetupComPort(setPortDialog.SelectedPort);
                System.IO.Stream fstream = System.IO.File.Open(Application.StartupPath + @"\port.cfg", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(fstream, setPortDialog.SelectedPort);
                fstream.Close();
            }
        }

        private void setVirtualPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setVirtualPortDialog = new SetVirtualPortDialog();
            setVirtualPortDialog.Show(this);
        }

        private void SetupComPort(string portname)
        {
            SetPortParams(portname);
            serial_listener = new SerialPortListener(port);
            serial_listener.DeviceIdReceived += new DeviceDataReceivedEventHandler(serial_listener_DeviceIdReceived);
            serial_listener.DeviceVersionReceived += new DeviceDataReceivedEventHandler(serial_listener_DeviceVersionReceived);
            serial_listener.PageReceived += new DeviceDataReceivedEventHandler(serial_listener_PageReceived);
            serial_listener.SetDeviceSerialSpeedOkReceived += new DeviceDataReceivedEventHandler(serial_listener_SetDeviceSerialSpeedOkReceived);
            serial_listener.DeviceErrorReceived += new DeviceDataReceivedEventHandler(serial_listener_DeviceErrorReceived);
            serial_listener.ErasePageReceived += new DeviceDataReceivedEventHandler(serial_listener_ErasePageReceived);
            serial_listener.EraseOkReceived += new DeviceDataReceivedEventHandler(serial_listener_EraseOkReceived);
        }

        public SerialNET.VPort VirtualPort
        {
            get
            {
                return virtualport;
            }
        }

        private void MainUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            virtualport.Dispose();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab.Controls.Count > 0)
                {
                    DataGridView grid = tabControl1.SelectedTab.Controls[0] as DataGridView;
                    grid.SelectAll();
                }
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
            }
#pragma warning restore 168
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            playBtn.Enabled = false;
            dummyTimer.Stop();
            workingTimer.Start();
            stopBtn.Enabled = true;
        }

        public bool PlayButtonEnabled
        {
            get
            {
                return playBtn.Enabled;
            }

            set
            {
                playBtn.Enabled = value;
            }
        }

        public bool DummyTimerEnabled
        {
            get
            {
                return dummyTimer.Enabled;
            }
        }

        public bool WorkingTimerEnabled
        {
            get
            {
                return workingTimer.Enabled;
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            playBtn.Enabled = true;
            workingTimer.Stop();
            dummyTimer.Start();
            stopBtn.Enabled = false;
        }

        void SetPlayBtnStatus(string input)
        {
            playBtn.Enabled = bool.Parse(input);
        }

        void SetStopBtnStatus(string input)
        {
            stopBtn.Enabled = bool.Parse(input);
        }

        internal SerialPortListener Listener
        {
            get
            {
                return serial_listener;
            }
        }

        public void SendCommand(string cmd)
        {
            serial_listener.BeginListening();
            port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
        }

        private void configDeviceMainMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (port == null)
                    MessageBox.Show("Сначала выберите COM-порт", "Не выбран COM-порт", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    deviceConfigDialog = new DeviceConfigForm();
                    if (deviceConfigDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        SendCommand("PCSDT " + deviceConfigDialog.timeBox.Text);
                        SendCommand("PCSDM " + deviceConfigDialog.distanceBox.Text);
                    }
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, except.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitMainMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

//foreach (string s in pagedata.Trim(new char[] { '?', 'A', 'U' }).Split(new char[] { 'A', 'B' }))
//            {
//                if (String.IsNullOrEmpty(s))
//                    continue;
//                else
//                    records.Add(new FlashRecord(s));
//            }
//                pagecount++;
//                this.Invoke(SetProgress, pagecount.ToString());
//                ReadNextPage();

//double lat1degree = 50 + (25.819 / 60);
//            double long1degree = 30 + (32.700 / 60);
//            double lat2degree = 50 + (25.830 / 60);
//            double long2degree = 30 + (32.703 / 60);

//            double lat1 = lat1degree * Math.PI / 180;
//            double long1 = long1degree * Math.PI / 180;
//            double lat2 = lat2degree * Math.PI / 180;
//            double long2 = long2degree * Math.PI / 180;

//            double L = Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1);

//            if (L > 1)
//                L = 1;
//            if (L < -1)
//                L = -1;

//            L = 6378137 * Math.Acos(L);
//            Console.WriteLine(L);

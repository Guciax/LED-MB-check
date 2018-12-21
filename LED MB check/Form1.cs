using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using JR.Utils.GUI.Forms;
using System.Text;
using System.Net;
using System.Net.Cache;

namespace LED_MB_check
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KListener.KeyDown += new RawKeyEventHandler(KListener_KeyDown);

            this.StartPosition = FormStartPosition.Manual;
            foreach (var scrn in Screen.AllScreens)
            {
                if (scrn.Bounds.Contains(this.Location))
                {
                    this.Location = new Point(scrn.Bounds.Right - this.Width, scrn.Bounds.Top);
                    return;
                }
            }
            
        }

        //private void LaunchProcess()
        //{
        //    Process myProcess = new Process();
        //    myProcess.StartInfo.FileName = @"C:\naklejki\naklejka led costam.nbl"; //not the full application path
        //    myProcess.Start();
        //}

        DataTable VisionTable = new DataTable();
        DataTable FunctionTable = new DataTable();
        DataTable HVTable = new DataTable();
        List<string> LogBuffer = new List<string>();
        KeyboardListener KListener = new KeyboardListener();
        public Dictionary<string, List<string[]>> PassFailDictFunc = new Dictionary<string, List<string[]>>();
        public Dictionary<string, List<string[]>> PassFailDictVis = new Dictionary<string, List<string[]>>();
        public Dictionary<string, List<string[]>> PassFailDictHV = new Dictionary<string, List<string[]>>();
        bool HVTestResult = false;
        List<bool> FuncResult = new List<bool>();
        List<bool> VisionResult = new List<bool>();
        bool justLoaded = true; //zeby nie zapisywac log przy wczytaniu pierwszej MB

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        public void DrawPointers()
        {
            if (panelDrawSettings.Visible)
            {
                IntPtr desktopPtr = GetDC(IntPtr.Zero);
                Graphics g = Graphics.FromHdc(desktopPtr);

                Pen p1 = new Pen(Color.Red);
                Pen p2 = new Pen(Color.Green);

                g.DrawEllipse(p1, new Rectangle((Int32)c1X.Value - 10, (Int32)c1Y.Value - 10, 20, 20));
                g.DrawLine(p1, (float)c1X.Value - 15, (float)c1Y.Value, (float)c1X.Value + 15, (float)c1Y.Value);
                g.DrawLine(p1, (float)c1X.Value, (float)c1Y.Value - 15, (float)c1X.Value, (float)c1Y.Value + 15);

                g.DrawEllipse(p2, new Rectangle((Int32)c2X.Value - 10, (Int32)c2Y.Value - 10, 20, 20));
                g.DrawLine(p2, (float)c2X.Value - 15, (float)c2Y.Value, (float)c2X.Value + 15, (float)c2Y.Value);
                g.DrawLine(p2, (float)c2X.Value, (float)c2Y.Value - 15, (float)c2X.Value, (float)c2Y.Value + 15);

                g.Dispose();
                ReleaseDC(IntPtr.Zero, desktopPtr);
            }
        }

        private void Autoclicker()
        {
            MouseInput.LeftClick((Int32)c1X.Value, (Int32)c1Y.Value);
            MouseInput.LeftClick((Int32)c2X.Value, (Int32)c2Y.Value);
            MouseInput.LeftClick((Int32)c2X.Value, (Int32)c2Y.Value);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p1 = new Pen(TB1_kolor, 1);
            Pen p2 = new Pen(TB2_kolor, 1);
            Graphics g = e.Graphics;
            int variance = 3;
            g.DrawRectangle(p1, new Rectangle(textBoxZlecenie.Location.X - variance, textBoxZlecenie.Location.Y - variance, textBoxZlecenie.Width + variance, textBoxZlecenie.Height + variance));
            //this.textBox1.Refresh();
            g.DrawRectangle(p2, new Rectangle(textBoxMB.Location.X - variance, textBoxMB.Location.Y - variance, textBoxMB.Width + variance, textBoxMB.Height + variance));

        }
        Color TB1_kolor = Color.Red;
        Color TB2_kolor = Color.Yellow;

        private Int32 GetMaxPCBNumer()
        {
            int result = 0;
            int rows = 1000;
            Int32.TryParse(ConfigurationManager.AppSettings["max_pcb_check"].ToString(), out rows);
            Debug.WriteLine("check depth: " + rows.ToString());
            int PCBnumber = 0;
            if (FunctionTable.Rows.Count < rows) rows = FunctionTable.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                Int32.TryParse(Regex.Match(FunctionTable.Rows[i]["NAME"].ToString(), @"\d+").Value, out PCBnumber);
                if (PCBnumber > result) result = PCBnumber;
            }
            return result;
        }
        HashSet<string> ListaSN = new HashSet<string>();
        private void timerThread_Tick (object sender, EventArgs e)
        {
            if (ThreadDone)
            {
                if (ConnectionSuccessed)
                {
                    
                    VisionTable = dataSet1.sp_vision.Copy();
                    FunctionTable = dataSet1.sp_function.Copy();
                    HVTable = hV_procedure_DataSet.sp_hv.Copy();

                    //Debug.WriteLine(VisionTable.Rows.Count + " " + FunctionTable.Rows.Count + " " + HVTable.Rows.Count);

                    dataGridLightUp.DataSource = VisionTable;
                    dataGridFunc.DataSource = FunctionTable;
                    //dataGridHV.DataSource = HVTable;

                    //przepisze wszystkie HV z func do HVgrid, zeby bylo po staremu
                    for (int i=0;i<FunctionTable.Rows.Count;i++)
                    {
                        if (FunctionTable.Rows[i]["HIGH_LIMIT"].ToString()=="4" & FunctionTable.Rows[i]["LOW_LIMIT"].ToString() == "0" & FunctionTable.Rows[i]["NAME"].ToString() == "Numeric")
                        {
                            DataRow newRow = HVTable.NewRow();
                            newRow["START_DATE_TIME"] = FunctionTable.Rows[i]["START_DATE_TIME"];
                            newRow["BATCH_SERIAL_NUMBER"] = FunctionTable.Rows[i]["BATCH_SERIAL_NUMBER"];
                            newRow["STATUS"] = FunctionTable.Rows[i]["STATUS"];
                            HVTable.Rows.Add(newRow);
                        }
                    }
                    dataGridHV.DataSource = HVTable;

                    ListaSN.Clear();

                    //Stopwatch stoper = new Stopwatch();
                    //stoper.Start();
                    foreach (DataRow row in VisionTable.Rows)
                    {
                        ListaSN.Add(row["BATCH_SERIAL_NUMBER"].ToString().TrimEnd());
                    }

                    foreach (DataRow row in FunctionTable.Rows)
                    {
                        ListaSN.Add(row["BATCH_SERIAL_NUMBER"].ToString().TrimEnd());
                    }

                    foreach (DataRow row in HVTable.Rows)
                    {
                        ListaSN.Add(row["BATCH_SERIAL_NUMBER"].ToString().TrimEnd());
                    }

                    //stoper.Stop();
                    //Debug.WriteLine("elapsed: "+stoper.ElapsedMilliseconds.ToString());

                    pictureBoxLoading.Visible = false;
                    timerThread.Enabled = false;
                    //if (VisionTable.Rows.Count > 0)
                    MakeLabelz(GetMaxPCBNumer());

                    if (VisionTable.Rows.Count > 0 || FunctionTable.Rows.Count > 0)
                    {
                        TB1_kolor = Color.Lime;
                        label3status.Text = "Wczytano zlecenie: " + textBoxZlecenie.Text+"\r                                "+ListaSN.Count.ToString()+" pozycji";
                        textBoxMB.Enabled = true;
                    }
                    else
                    {
                        TB1_kolor = Color.Red;
                        label3status.Text = "Brak danych dla zlecenia: " + textBoxZlecenie.Text;
                        textBoxMB.Enabled = false;
                    }
                    this.Refresh();

                }
                else
                {
                    label3status.Text = "Błąd połączenia.";
                    pictureBoxLoading.Visible = false;
                }
            }
        }

        Boolean ConnectionSuccessed = false;
        Boolean ThreadDone = false;
        public void LoadZlecenie()
        {

            ThreadDone = false;
            pictureBoxLoading.Visible = true;
            label3status.Text = "wczytywanie zlecenia...";
            timerThread.Enabled = true;
            new Thread(delegate ()
            {
                try
                {
                    this.sp_functionTableAdapter.Fill(dataSet1.sp_function, textBoxZlecenie.Text);
                    this.sp_visionTableAdapter.Fill(dataSet1.sp_vision, textBoxZlecenie.Text);
                    //this.sp_hvTableAdapter.Fill(hV_procedure_DataSet.sp_hv, textBoxZlecenie.Text);
                    ConnectionSuccessed = true;
                    ThreadDone = true;
                }
                catch (Exception e)
                {
                    if (e.HResult == -2146232060)
                        TopMostMessageBox.Show("Brak połączenia z serwerem!" + "\r" + e.Message);
                    else
                        TopMostMessageBox.Show(e.Message);
                    ConnectionSuccessed = false;
                    ThreadDone = true;
                }

            }).Start();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!justLoaded)
                writeLog();

            LoadZlecenie();
            justLoaded = true;
        }

        int TotalNumberOfPCBs = 0;
        string CurrentProduct12NC = "";
        private void FilterTables()
        {
            TotalNumberOfPCBs = 0;
            int PCBNumber = 0;
            //Stopwatch PerformanceWatch = new Stopwatch();
            //PerformanceWatch.Start();
            DataTable FilteredFunctionTable = new DataTable();
            DataTable FilteredVisionTable = new DataTable();
            DataTable FilteredHVTable = new DataTable();

            FilteredFunctionTable = FunctionTable.Clone();
            FilteredVisionTable = VisionTable.Clone();
            FilteredHVTable = HVTable.Clone();

            foreach (DataRow row in FunctionTable.Rows)
            {
                string SN = Regex.Replace(row[1].ToString(), @"\s+", "");
                Int32.TryParse(Regex.Match(row["NAME"].ToString(), @"\d+").Value, out PCBNumber);
                if (PCBNumber > TotalNumberOfPCBs) TotalNumberOfPCBs = PCBNumber;
                if (SN == textBoxMB.Text & row["NAME"].ToString().Contains("Measurement"))
                {
                    FilteredFunctionTable.Rows.Add(row.ItemArray);
                }
            }

            if (FilteredFunctionTable.Rows.Count>0) CurrentProduct12NC = FilteredFunctionTable.Rows[0]["PRODUCT_NAME"].ToString().Substring(0,14);

            foreach (DataRow row in VisionTable.Rows)
            {
                string SN = Regex.Replace(row[1].ToString(), @"\s+", "");

                // Debug.WriteLine(SN + "--" + textBox2.Text);

                if (SN == textBoxMB.Text & row["DATA"].ToString().Trim() + row["STATUS"].ToString().Trim() != "Failed")
                {
                    //Debug.WriteLine(">" + row["DATA"].ToString().Trim() + "<");
                    FilteredVisionTable.Rows.Add(row.ItemArray);
                }
                //PerformanceWatch.Stop();

            }

            foreach (DataRow row in HVTable.Rows)
            {
                string SN = Regex.Replace(row[1].ToString(), @"\s+", "");
                if (SN == textBoxMB.Text)
                    FilteredHVTable.Rows.Add(row.ItemArray);
            }

            TrimDatatable(FilteredFunctionTable);
            TrimDatatable(FilteredVisionTable);
            TrimDatatable(FilteredFunctionTable);

            dataGridFunc.DataSource = FilteredFunctionTable;
            dataGridLightUp.DataSource = FilteredVisionTable;
            dataGridHV.DataSource = FilteredHVTable;

            
            //Debug.WriteLine("Filteredtables: " + PerformanceWatch.ElapsedMilliseconds.ToString());
            //PerformanceWatch.Reset();
            //Debug.WriteLine("PCB's: " + TotalNumberOfPCBs.ToString());
        }

        private bool HV_Dictionary_Maker()
        {
            bool result = true;
            PassFailDictHV.Clear();
            if (dataGridHV.Rows.Count > 0)
                foreach (DataGridViewRow row in dataGridHV.Rows)
                {
                    string START_DATE_TIME = "TEST " + row.Cells["START_DATE_TIME"].Value.ToString();

                    if (!PassFailDictHV.ContainsKey(START_DATE_TIME)) //dodajemy nowy klucz
                    {
                        PassFailDictHV.Add(START_DATE_TIME, new List<string[]>());
                    }
                    if (row.Cells["STATUS"].Value != null)
                    {
                        if (row.Cells["STATUS"].Value.ToString().Contains("Passed"))
                        {
                            row.DefaultCellStyle.BackColor = Color.Lime;
                            for (int i = 0; i < TotalNumberOfPCBs; i++)
                                PassFailDictHV[START_DATE_TIME].Add(new string[] { (i + 1).ToString(), "Test HV", "OK" });
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            for (int i = 0; i < TotalNumberOfPCBs; i++)
                                PassFailDictHV[START_DATE_TIME].Add(new string[] { (i + 1).ToString(), "Test HV", "WADA" });
                            result = false;
                        }
                    }
                }
            else return false;

            return result;
        }

        private bool Func_Dictionary_Maker()
        {
            //int OKcounter = 0;
            bool result = false;
            PassFailDictFunc.Clear();
            Regex regexObj = new Regex(@"[^\d]");
            bool BothTestsOK = false;
            if (dataGridFunc.Rows.Count > 0)
                for (int i = 0; i < dataGridFunc.Rows.Count; i = i + 2)
                {
                    string START_DATE_TIME = "TEST " + dataGridFunc.Rows[i].Cells["START_DATE_TIME"].Value.ToString();
                    if (!PassFailDictFunc.ContainsKey(START_DATE_TIME)) //dodajemy nowy klucz
                    {
                        if (BothTestsOK)  result = true;
                        BothTestsOK = true;
                        //OKcounter = 0;
                        PassFailDictFunc.Add(START_DATE_TIME, new List<string[]>());
                        
                    }
                    string[] LineToDict = new string[3];
                    
                    for (int o = 0; o < 2; o++)
                    {

                        string MeasureUnit = "";

                        if (dataGridFunc.Rows[i + o].Cells["Name"].Value.ToString().Replace("Measurement ", "").Trim().Contains("c"))
                        { //Current 
                            MeasureUnit = "mA";
                        }
                        else
                        { //Voltage 
                            MeasureUnit = "V";
                        }
                        string NrPCB = regexObj.Replace(dataGridFunc.Rows[i + o].Cells["Name"].Value.ToString(), "");
                        string pomiar = dataGridFunc.Rows[i + o].Cells["DATA"].Value.ToString();
                        string Low_limit = dataGridFunc.Rows[i + o].Cells["LOW_LIMIT"].Value.ToString();
                        string High_limit = dataGridFunc.Rows[i + o].Cells["HIGH_LIMIT"].Value.ToString();
                        LineToDict[1] += String.Format("{0}{1} ({2}-{3})  ", pomiar, MeasureUnit, Low_limit, High_limit);
                        LineToDict[0] = NrPCB;
                        
                        if (dataGridFunc.Rows[i + o].Cells["STATUS"].Value.ToString() == "Passed")
                        {
                            dataGridFunc.Rows[i + o].DefaultCellStyle.BackColor = Color.Lime;
                            if (LineToDict[2] == null)
                                LineToDict[2] = "OK";
                        }
                        else
                        {
                            dataGridFunc.Rows[i + o].DefaultCellStyle.BackColor = Color.Red;
                            LineToDict[2] = "WADA";
                            BothTestsOK = false;
                        }
                    }
                    PassFailDictFunc[START_DATE_TIME].Add(LineToDict);
                }
            if (BothTestsOK) result = true;
            return result;
        }

        private bool Vision_DictionaryMakre()
        {
            bool result = false; ;
            PassFailDictVis.Clear();
            if (dataGridLightUp.Rows.Count > 0)
                for (int i = 0; i < dataGridLightUp.Rows.Count; i++)
                {
                    string START_DATE_TIME = "TEST " + dataGridLightUp.Rows[i].Cells["START_DATE_TIME"].Value.ToString();
                    if (!PassFailDictVis.ContainsKey(START_DATE_TIME)) //dodajemy nowy klucz
                    {
                        PassFailDictVis.Add(START_DATE_TIME, new List<string[]>());
                    }


                    if (dataGridLightUp.Rows[i].Cells["STATUS"].Value.ToString() == "Passed")
                    {
                        for (int o = 0; o < TotalNumberOfPCBs; o++)
                        {
                            PassFailDictVis[START_DATE_TIME].Add(new string[] { (o + 1).ToString(), "Test zaświecenia", "OK" });
                        }
                        result = true; 
                    }
                    else
                    {
                        


                        string[] ArrayofFailures = dataGridLightUp.Rows[i].Cells["Data"].Value.ToString().Split(',');
                        if (ArrayofFailures[0] != "0")
                            for (int o = 0; o < TotalNumberOfPCBs; o++)
                            {
                                if (ArrayofFailures.Contains((o + 1).ToString()))
                                {
                                    PassFailDictVis[START_DATE_TIME].Add(new string[] { (o + 1).ToString(), "Test zaświecenia", "WADA" });
                                }
                                else
                                {
                                    PassFailDictVis[START_DATE_TIME].Add(new string[] { (o + 1).ToString(), "Test zaświecenia", "OK" });
                                }
                            }
                        else
                        {
                            for (int o = 0; o < TotalNumberOfPCBs; o++)
                            {
                                PassFailDictVis[START_DATE_TIME].Add(new string[] { (o + 1).ToString(), "Błąd testu", "BŁĄD" });
                            }
                        }
                    }
                }
            else return false;
            return result;
        }



        private void Dictionary_To_Listview(Dictionary<string, List<string[]>> dict, ListView lv )
        {
            lv.Items.Clear();
            lv.Groups.Clear();
            List<string[]> tempList = new List<string[]>();
            if (dict.Count > 0) 
            foreach (KeyValuePair<string, List<string[]>> key in dict)
            {
                    tempList.Clear();
                    ListViewGroup grupa = new ListViewGroup(key.Key.ToString());
                lv.Groups.Add(grupa);
                bool emptyGroup = true;

                if (lv == listViewFunc)
                {
                    for (int k = 1; k < key.Value.Count + 1; k++)
                    {
                        for (int l = 0; l < key.Value.Count;l++)
                        {
                            if (key.Value[l][0] == k.ToString())
                            {
                                tempList.Add(key.Value[l]);
                                break;
                            }
                        }
                        //tempList = key.Value.OrderBy(x => x[0]).ToList();
                    }
                }
                else
                {
                    tempList = new List<string[]> ( key.Value);
                }


                for (int i = 0; i < tempList.Count; i++) 
                {
                    string[] line = tempList[i];
                    {
                        ListViewItem item = new ListViewItem(line, grupa);
                        if (line[2] == "OK")
                        {
                            if (checkBoxShowAll.Checked & i != tempList.Count - 1) continue;

                            if (!checkBoxShowAll.Checked)
                            {
                                item.ForeColor = Color.Green;
                                emptyGroup = false;
                                lv.Items.Add(item);
                            }
                        }
                        else
                        {
                            if (line[2] == "WADA")
                            {
                                item.ForeColor = Color.Red;
                            }
                            else
                                item.ForeColor = Color.Orange;
                            emptyGroup = false;
                            lv.Items.Add(item);
                        }


                        
                    }

                    if (i==key.Value.Count-1 & emptyGroup)
                    {
                        ListViewItem AllGoodItem = new ListViewItem(new string[] { "", "Panel " + textBoxMB.Text, "OK" }, grupa);
                        AllGoodItem.ForeColor = Color.Green;
                        lv.Items.Add(AllGoodItem);
                    }
                }
            }
            else
            {
                ListViewGroup grupa = new ListViewGroup("BRAK DANYCH");
                lv.Groups.Add(grupa);
                ListViewItem item = new ListViewItem(new string[] { "", "BRAK DANYCH", "" }, grupa);
                item.ForeColor = Color.OrangeRed;
                lv.Items.Add(item);
            }
        }

        private void InterpretacjaWynikow()
        {

            if (HV_Dictionary_Maker())
            {
                panelHV.BackColor = Color.Lime;
                labelHVResult.Text = "OK";
            }
            else
            {
                if (dataGridHV.Rows.Count > 0)
                {
                    panelHV.BackColor = Color.Red;
                    labelHVResult.Text = "WADA";
                }

                else
                {
                    panelHV.BackColor = Color.Yellow;
                    labelHVResult.Text = "BRAK DANYCH";
                }
            }



            if (Func_Dictionary_Maker())
            {
                panelFunc.BackColor = Color.Lime;
                labelFuncResult.Text = "OK";
            }
            else
            {
                if (dataGridFunc.Rows.Count > 0)
                {
                    panelFunc.BackColor = Color.Red;
                    labelFuncResult.Text = "WADA";
                }
                else
                {
                    panelFunc.BackColor = Color.Yellow;
                    labelFuncResult.Text = "BRAK DANYCH";
                }
            }





            if (Vision_DictionaryMakre())
            {
                panelVision.BackColor = Color.Lime;
                labelVisionResult.Text = "OK";
            }
            else
            {
                if (dataGridLightUp.Rows.Count > 0)
                {
                    panelVision.BackColor = Color.Red;
                    labelVisionResult.Text = "WADA";
                }
                else
                {
                    panelVision.BackColor = Color.Yellow;
                    labelVisionResult.Text = "BRAK DANYCH";
                }
            }

            if (labelHVResult.Text == "BRAK DANYCH" & labelVisionResult.Text == "BRAK DANYCH" & labelFuncResult.Text == "BRAK DANYCH")
                TB2_kolor = Color.Red;
            else TB2_kolor = Color.Lime;
            ColorPCBs();

            Dictionary_To_Listview(PassFailDictHV, listViewHV);
            Dictionary_To_Listview(PassFailDictFunc, listViewFunc);
            Dictionary_To_Listview(PassFailDictVis, listViewLightUp);

            if (listViewHV.Items.Count > 0) 
                listViewHV.Items[listViewHV.Items.Count - 1].EnsureVisible();
            if (listViewFunc.Items.Count > 0)
                listViewFunc.Items[listViewFunc.Items.Count - 1].EnsureVisible();
            if (listViewLightUp.Items.Count > 0)
                listViewLightUp.Items[listViewLightUp.Items.Count - 1].EnsureVisible();

            this.Refresh();
        }

        private void writeLog()
        {
            string MB = labelTestReusltInfo.Text.Split(' ')[labelTestReusltInfo.Text.Split(' ').Length - 1];
            if (MB.Contains("_"))
            {
                string filename = System.DateTime.Now.ToString("MM-yyyy") + ".log";

                LogBuffer.Add(System.DateTime.Now.ToShortDateString() + "\t" 
                    + System.DateTime.Now.ToShortTimeString() + "\t"
                    + Logon_textbox.Text + "\t"
                + textBoxZlecenie.Text + "\t"
                + MB + "\t"
                + "wynik_HV:" + labelHVResult.Text + "\t"
                + "wynik_F:" + labelFuncResult.Text + "\t"
                + "wynik_V:" + labelVisionResult.Text + "\t"
                + PCB_to_log());


                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        foreach (var line in LogBuffer)
                        {
                            writer.WriteLine(line);
                            LogBuffer.Remove(line);
                        }
                    }
                    Debug.WriteLine("logged");
                }
                catch (Exception e) { Debug.WriteLine(e.Message); }
            }
        }

        private Int32 ValueInListOfArray(List<string[]> list,int index,string value)
        {
            int result = -1;

            for (int i=0;i<list.Count;i++)
            {
                if (list[i][index] == value)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        private void DoTheMagic()
        {
            if(!justLoaded)
            writeLog();
            RestorePCBColor();
            //resetTests();

            panelVision.BackColor = Color.Gray;
            panelFunc.BackColor = Color.Gray;

            FilterTables();



            InterpretacjaWynikow();

            labelTestReusltInfo.Text = "Wyniki testu panelu MB nr: " + textBoxMB.Text;
            
            if (labelHVResult.Text != "BRAK DANYCH" & labelFuncResult.Text != "BRAK DANYCH" & labelVisionResult.Text != "BRAK DANYCH")
            {
                labelTestReusltInfo.Text = "Wyniki testu panelu MB nr: " + textBoxMB.Text;
                if (checkBox1.Checked) Autoclicker();
            }
            else
                labelTestReusltInfo.Text = "Brak panelu w bazie danych: " + textBoxMB.Text;
            justLoaded = false; //po pierwszym wczytaniu loguj

            PNGFilesList.Clear();
            PNGFilesList = MakePNGList();
            if (PNGFilesList.Count > 0) buttonViewVisFailures.Visible = true;
            else buttonViewVisFailures.Visible = false;
        }

        private string PCB_to_log()
        {
            string result = "";
            foreach (var pcb in PCBArray)
            {
                if (pcb != null)
                {

                    if (pcb.BackColor == Color.Black)
                    {
                        result += "Brak_mech;";
                        continue;
                    }
                    if (pcb.BackColor == Color.Red)
                    {
                        result += "Brak_test;";
                        continue;
                    }
                    if (pcb.BackColor == Color.Orange)
                    {
                        result += "Brak_pol;";
                        continue;
                    }
                    if (pcb.BackColor == Color.Brown)
                    {
                        result += "Brak_zabr;";
                        continue;
                    }
                    if (pcb.BackColor == Color.Lime) result += "OK;";
                }
            }
            Debug.WriteLine(result);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (buttonListen.BackColor == Color.Red)
                buttonListen.BackColor = Color.Lime;
            else
                buttonListen.BackColor = Color.Red;

        }


        private void TrimDatatable(DataTable table)
        {

            DataColumn[] stringColumns = table.Columns.Cast<DataColumn>()
            .Where(c => c.DataType == typeof(string))
            .ToArray();

            foreach (DataRow row in table.Rows)
                foreach (DataColumn col in stringColumns)
                    if (col.ColumnName != "Wynik")
                    {
                        try { row.SetField<string>(col, row.Field<string>(col).Trim()); }
                        catch { Debug.WriteLine(col, row.Field<string>(col).ToString()); };
                    }
        }
        
        

        private bool ColorGridFailures(string Test)
        {
            
            Stopwatch PerformanceWatch = new Stopwatch();
            PerformanceWatch.Start();
            DataGridView DGrid = dataGridHV;
            ListView LView = listViewFunc;

            if (Test == "HV")
            {
                DGrid = dataGridHV;
                LView = listViewHV;
            }

            if (Test == "Func")
            {
                DGrid = dataGridFunc;
                LView = listViewFunc;
            }

            if (Test == "LightUp")
            {
                DGrid = dataGridLightUp;
                LView = listViewLightUp;
            }



            var PassFailDict = new Dictionary<string, List<string[]>>(); //[Key=Data testu] lista pomiarow wadliwych[] (NrPCB, pomiar (min-max), OK/Wada)
            bool BooltestResult = false;
            Regex regexObj = new Regex(@"[^\d]");

            PassFailDict.Clear();

            foreach (DataGridViewRow row in DGrid.Rows)
            {

                string[] FailureLineToDict = new string[3];
                string TestResult = "NotTranslated";
                string START_DATE_TIME = "TEST " + row.Cells["START_DATE_TIME"].Value.ToString();


                if (!PassFailDict.ContainsKey(START_DATE_TIME))
                {
                    if (Test == "HV") PassFailDictHV.Add(START_DATE_TIME, new List<string[]>());
                    if (Test=="Func") PassFailDictFunc.Add(START_DATE_TIME, new List<string[]>());
                    if (Test == "LightUp") PassFailDictVis.Add(START_DATE_TIME, new List<string[]>());

                    PassFailDict.Add(START_DATE_TIME, new List<string[]>());
                }

                if (row.Cells["STATUS"].Value != null)
                    if (row.Cells["STATUS"].Value.ToString().Contains("Passed")) //jezeli rekord OK
                    {
                        row.DefaultCellStyle.BackColor = Color.Lime;
                        if (Test == "HV") //HV ok to wszystkie PCB OK
                        {
                            for (int x = 1; x < TotalNumberOfPCBs; x++)
                            {
                                PassFailDictHV[START_DATE_TIME].Add(new string[] { x.ToString(), "OK", "" });
                            }
                        }
                        row.DefaultCellStyle.BackColor = Color.Lime;
                        if (Test == "LightUp") // LightUp ok to wszystkie PCB OK
                        {
                            for (int x = 1; x < TotalNumberOfPCBs; x++)
                            {
                                PassFailDictVis[START_DATE_TIME].Add(new string[] { x.ToString(), "OK", "" });
                            }
                        }
                        if (Test == "Func") //Func OK to robie nowy wpis w liscie PassFailDictFunc lub dodaje do istniejacego
                        {
                            string NrPCB = regexObj.Replace(row.Cells["Name"].Value.ToString(), "");
                            int ind = ValueInListOfArray(PassFailDictFunc[START_DATE_TIME], 0, NrPCB);
                            string MeasureUnit = "";

                            if (row.Cells["Name"].Value.ToString().Replace("Measurement ", "").Trim().Contains("c"))
                            { //Current 
                                MeasureUnit = "mA";
                            }
                            else
                            { //Voltage 
                                MeasureUnit = "V";
                            }

                            string pomiar = row.Cells["DATA"].Value.ToString();
                            string Low_limit = row.Cells["LOW_LIMIT"].Value.ToString();
                            string High_limit = row.Cells["HIGH_LIMIT"].Value.ToString();

                            if (ind > 0)
                            {
                                PassFailDictFunc[START_DATE_TIME][ind][1] += String.Format("{0}{1} ({2}{1}-{3}{1})", pomiar, MeasureUnit, Low_limit, High_limit);
                            }
                            else
                            {
                                PassFailDictFunc[START_DATE_TIME].Add(new string[] { NrPCB, String.Format("{0}{1} ({2}{1}-{3}{1})", pomiar, MeasureUnit, Low_limit, High_limit), "OK" });
                            }
                        }


                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;

                        if (Test == "HV")//tylko test HV
                        {
                            //FailureLineToDict[0] = "1";
                            //FailureLineToDict[1] = "WADA TEST HV";
                            //FailureLineToDict[3] = "";
                            //PassFailDict[START_DATE_TIME].Add(new string[] { FailureLineToDict[0], FailureLineToDict[1], FailureLineToDict[2] });

                            for (int x = 1; x < TotalNumberOfPCBs; x++)
                            {
                                PassFailDictHV[START_DATE_TIME].Add(new string[] { x.ToString(), "WADA TEST HV", "" });
                            }
                        }

                        if (DGrid == dataGridFunc)  //tylko funkcyjny
                        {
                            string NrPCB = regexObj.Replace(row.Cells["Name"].Value.ToString(), "");
                            int ind = ValueInListOfArray(PassFailDictFunc[START_DATE_TIME], 0, NrPCB);
                            string MeasureUnit = "";

                            if (row.Cells["Name"].Value.ToString().Replace("Measurement ", "").Trim().Contains("c"))
                            { //Current 
                                MeasureUnit = "mA";
                            }
                            else
                            { //Voltage 
                                MeasureUnit = "V";
                            }

                            string pomiar = row.Cells["DATA"].Value.ToString();
                            string Low_limit = row.Cells["LOW_LIMIT"].Value.ToString();
                            string High_limit = row.Cells["HIGH_LIMIT"].Value.ToString();

                            if (ind > 0)
                            {
                                PassFailDictFunc[START_DATE_TIME][ind][1] += String.Format("{0}{1} ({2}{1}-{3}{1})", pomiar, MeasureUnit, Low_limit, High_limit);
                                PassFailDictFunc[START_DATE_TIME][ind][2] = "Wada";
                            }
                            else
                            {
                                PassFailDictFunc[START_DATE_TIME].Add(new string[] { NrPCB, String.Format("{0}{1} ({2}{1}-{3}{1})", pomiar, MeasureUnit, Low_limit, High_limit), "Wada" });
                            }
                        }

                        var VisionFailuresDict = new Dictionary<string, List<string>>();
                        if (DGrid == dataGridLightUp) //tylko LightUp
                        {
                            string[] ArrayofFailures = row.Cells["Data"].Value.ToString().Split(new string[] { ",", System.Environment.NewLine }, StringSplitOptions.None);

                            string LED_Vision = "";
                            if (ArrayofFailures[0].Contains("Nr"))
                                for (int i = 1; i < ArrayofFailures.Length; i = i + 4) //stary sposob zapisu wad vision
                                {
                                    string PCB_Vision = regexObj.Replace(ArrayofFailures[i], "");

                                    if (!VisionFailuresDict.ContainsKey(PCB_Vision))
                                    {
                                        VisionFailuresDict.Add(PCB_Vision, new List<string>());
                                    }

                                    LED_Vision = "D" + regexObj.Replace(ArrayofFailures[i - 1], "");
                                    VisionFailuresDict[PCB_Vision].Add(LED_Vision);
                                }
                            else //nowy sposob zapisu vision - tylko nr pcb
                            {
                                for (int i = 0; i < ArrayofFailures.Length; i++)
                                {
                                    string PCB_Vision = ArrayofFailures[i];

                                    if (!VisionFailuresDict.ContainsKey(PCB_Vision))
                                    {
                                        VisionFailuresDict.Add(PCB_Vision, new List<string>());
                                    }

                                }
                            }



                            if (row.Cells["STATUS"].Value.ToString() == "Failed") TestResult = "Wada";
                            if (row.Cells["STATUS"].Value.ToString() == "Passed") TestResult = "OK";



                            foreach (KeyValuePair<string, List<string>> key in VisionFailuresDict)
                            {

                                if (key.Value.Count > 0)
                                {
                                    FailureLineToDict[0] = key.Key.ToString();
                                    foreach (var item in key.Value)
                                    {
                                        FailureLineToDict[1] += item + ", ";
                                    }
                                    FailureLineToDict[1] = FailureLineToDict[1].Remove(FailureLineToDict[1].Length - 2, 1);

                                    FailureLineToDict[2] = TestResult;

                                    PassFailDictVis[START_DATE_TIME].Add(new string[] { FailureLineToDict[0], FailureLineToDict[1], FailureLineToDict[2] });
                                    Array.Clear(FailureLineToDict, 0, FailureLineToDict.Length);
                                }
                                else
                                {
                                    int failed_pcb = 0;
                                    Int32.TryParse(key.Key.ToString(), out failed_pcb);
                                    do
                                    {
                                        PassFailDict[START_DATE_TIME].Add(new string[] { (PassFailDict[START_DATE_TIME].Count+1).ToString(), "", "OK" });
                                    } while (PassFailDictVis[START_DATE_TIME].Count < failed_pcb);

                                        FailureLineToDict[0] = key.Key.ToString();
                                        FailureLineToDict[1] = "Błąd zaświecenia";
                                        FailureLineToDict[2] = TestResult;
                                    PassFailDictVis[START_DATE_TIME].Add(new string[] { FailureLineToDict[0], FailureLineToDict[1], FailureLineToDict[2] });
                                        Array.Clear(FailureLineToDict, 0, FailureLineToDict.Length);
                                    }
                            }
                        }

                    }
            }

            //var lista = PassFailDictFunc[""].OrderBy(x => x[0]).ToList();

            /*foreach (KeyValuePair<string, List<string[]>> key in PassFailDict)
            {
                
                //Debug.WriteLine("key: " + key.Key.ToString()+" "+key.Value.Count);
                ListViewGroup Grupa = new ListViewGroup(key.Key.ToString());
                LView.Groups.Add(Grupa);

                if (key.Value.Count() > 0)
                {
                    for (int x = 0; x < 60; x++)
                    {
                        for (int i = 0; i < key.Value.Count; i++)
                        {

                            if (key.Value[i][0] == x.ToString())
                            {
                                if (key.Value[i][0] != "0")
                                {
                                    // int PCB = Int32.Parse(key.Value[i][0]);
                                    //PCBArray[PCB].BackColor = Color.Red;
                                    ListViewItem item = new ListViewItem(key.Value[i], Grupa);
                                    //Debug.WriteLine("---" + key.Value[i][0]);
                                    item.ForeColor = Color.Red;
                                    LView.Items.Add(item);
                                }
                                else
                                {
                                    key.Value[i][0] = "";
                                    key.Value[i][1] = "Nieudany test";
                                    key.Value[i][2] = "";
                                    ListViewItem item = new ListViewItem(key.Value[i], Grupa);
                                    //Debug.WriteLine("---" + key.Value[i][0]);
                                    item.ForeColor = Color.Orange;
                                    LView.Items.Add(item);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ListViewItem item = new ListViewItem(new string[] { "", "MB " + textBoxMB.Text + "", "OK" }, Grupa);
                    item.ForeColor = Color.Green;
                    LView.Items.Add(item);
                    BooltestResult = true;
                    if (LView.Scrollable) LView.Items[LView.Items.Count - 1].EnsureVisible();
                }
            }*/

            //szer. kolumn
            for (int c = 0; c < DGrid.Columns.Count; c++)
            {
                //if (c == DGrid.Columns.Count - 1)
                DGrid.Columns[c].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //else
                //DGrid.Columns[c].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            }

            PerformanceWatch.Stop();
            Debug.WriteLine("Color " + Test + ": " + PerformanceWatch.ElapsedMilliseconds.ToString());
            return BooltestResult;


        }

        private void DictToListView(bool show_all)
        {
            //var lista = s.OrderBy(x => x[0]).ToList();

            foreach (KeyValuePair<string, List<string[]>> key in PassFailDictHV)
            {
                ListViewGroup Grupa = new ListViewGroup(key.Key.ToString());
                listViewHV.Groups.Add(Grupa);
                foreach (var arr in key.Value)
                {
                    if (arr[2]=="OK")
                    {
                        if (show_all)
                        {
                            ListViewItem item = new ListViewItem(arr, Grupa);
                            //Debug.WriteLine("---" + key.Value[i][0]);
                            item.ForeColor = Color.Lime;
                            listViewHV.Items.Add(item);
                        }
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem(arr, Grupa);
                        //Debug.WriteLine("---" + key.Value[i][0]);
                        item.ForeColor = Color.Red;
                        listViewHV.Items.Add(item);
                    }
                }
                if (Grupa.Items.Count==0)
                {
                    ListViewItem item = new ListViewItem(new string[] {"", "test HV OK","" }, Grupa);
                    //Debug.WriteLine("---" + key.Value[i][0]);
                    item.ForeColor = Color.Lime;
                    listViewHV.Items.Add(item);
                }

            }
        }

        private void ColorPCBs()
        {
            if (labelHVResult.Text=="BRAK DANYCH" || labelFuncResult.Text == "BRAK DANYCH" || labelVisionResult.Text == "BRAK DANYCH")
            {
                for (int x = 0; x < TotalNumberOfPCBs; x++)
                {
                    if (PCBArray[x + 1]!=null)
                    PCBArray[x + 1].BackColor = Color.Yellow;

                }
                return;
            }


            bool[] HV_PCB_Colors = new bool[TotalNumberOfPCBs];
            bool[] Func_PCB_Colors = new bool[TotalNumberOfPCBs];
            bool[] Vision_PCB_Colors = new bool[TotalNumberOfPCBs];

            foreach (KeyValuePair<string, List<string[]>> key in PassFailDictHV)
            {
                for (int i = 0; i < key.Value.Count; i++) 
                {
                    if (key.Value[i][2] == "OK") HV_PCB_Colors[i] = true; 
                }
            }

            foreach (KeyValuePair<string, List<string[]>> key in PassFailDictFunc)
            {
                for (int i = 0; i < key.Value.Count; i++)
                {
                    if (key.Value[i][2] == "OK")

                    {
                        int pcb = Int32.Parse(key.Value[i][0]);
                        Func_PCB_Colors[pcb - 1] = true;
                    }
                }
            }

            foreach (KeyValuePair<string, List<string[]>> key in PassFailDictVis)
            {
                for (int i = 0; i < key.Value.Count; i++)
                {
                    if (key.Value[i][2] == "OK") Vision_PCB_Colors[i] = true;
                }
            }

            for (int x = 0; x < TotalNumberOfPCBs; x++)
            {
                if (PCBArray[x + 1] != null)
                    if (HV_PCB_Colors[x] & Func_PCB_Colors[x] & Vision_PCB_Colors[x])
                        PCBArray[x + 1].BackColor = Color.Lime;
                    else
                        PCBArray[x + 1].BackColor = Color.Red;
            }
        }

        private void RestorePCBColor()
        {
            for (int i = 0; i < PCBArray.Length; i++)
            {
                if (PCBArray[i] != null)
                {
                    PCBArray[i].BackColor = Color.Red;
                    PCBArray[i].ForeColor = Color.Black;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!justLoaded)
                writeLog();

            Logon_textbox.Text = "";
            load_panel.Visible = true;
        }

        private void radioShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radioShowAll.Checked)
            {
                dataGridHV.Visible = true;
                listViewHV.Visible = false;
                dataGridFunc.Visible = true;
                listViewFunc.Visible = false;
                dataGridLightUp.Visible = true;
                listViewLightUp.Visible = false;

            }
            else
            {
                dataGridHV.Visible = false;
                listViewHV.Visible = true;
                dataGridFunc.Visible = false;
                listViewFunc.Visible = true;
                dataGridLightUp.Visible = false;
                listViewLightUp.Visible = true;
            }
        }

        Stopwatch Stoper = new Stopwatch();
        void KListener_KeyDown(object sender, RawKeyEventArgs args)
        {
            if (buttonListen.BackColor == Color.Lime & !load_panel.Visible & textBoxMB.Enabled)
            {
                if (!Stoper.IsRunning)
                {
                    Stoper.Start();
                    timer1.Enabled = true;
                }

                Stoper.Restart();
                if (TypingFinished)
                {
                    textBoxMB.Text = args.ToString();
                    TypingFinished = false;
                }
                else
                    textBoxMB.Text += args.ToString();
            }

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        Boolean TypingFinished = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Debug.WriteLine("Elapsed " + Stoper.ElapsedMilliseconds.ToString());
            if (Stoper.ElapsedMilliseconds > 800)
            {
                timer1.Enabled = false;
                TypingFinished = true;
                Stoper.Stop();
                DoTheMagic();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (buttonListen.BackColor == Color.Red)
            {
                if (e.KeyValue == 13)
                    DoTheMagic();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!justLoaded)
                writeLog();

            if (e.KeyValue == 13) LoadZlecenie();
        }

        Label[] PCBArray = new Label[61];
        private void Form1_Load(object sender, EventArgs e)
        {
            Height = Screen.PrimaryScreen.WorkingArea.Height;

            dataGridHV.Height = this.Height - 365;
            dataGridLightUp.Height = this.Height - 365;
            dataGridFunc.Height = this.Height - 365;
            listViewFunc.Height = this.Height - 365;
            listViewHV.Height = this.Height - 365;
            listViewLightUp.Height = this.Height - 365;
            //LaunchProcess();
            flowLayoutPanel1.Location = new Point(5, this.Height - 34);
            //panel1.Location = new Point(5, this.Height - 32);
            c1X.Value = Decimal.Parse(ConfigurationManager.AppSettings["x1"].ToString());
            c2X.Value = Decimal.Parse(ConfigurationManager.AppSettings["x2"].ToString());
            c1Y.Value = Decimal.Parse(ConfigurationManager.AppSettings["y1"].ToString());
            c2Y.Value = Decimal.Parse(ConfigurationManager.AppSettings["y2"].ToString());
            this.TopMost = true;
            GetMaxPCBNumer();
            
            load_panel.Parent = this;
            load_panel.Size = this.Size;
            load_panel.BringToFront();

            
            Logon_textbox.Parent = load_panel;
            Logon_textbox.Font = new Font("Arial", 32);
            Logon_textbox.KeyDown += Logon_textbox_KeyDown;
            Logon_textbox.TextChanged += Logon_text_TextChanged;
            Logon_textbox.Location = new Point(450,800);
            Logon_textbox.Size = new Size(450, 30);
            Logon_textbox.CharacterCasing = CharacterCasing.Upper;

            Label logon_info_label = new Label();
            logon_info_label.Parent = load_panel;
            logon_info_label.Font = new Font("Arial", 32);
            logon_info_label.Location = new Point(550,730);
            logon_info_label.Text = "Zaloguj się";
            logon_info_label.ForeColor = Color.Black;
            logon_info_label.Size = new Size(300, 50);

            Button Exit_button = new Button();
            Exit_button.Parent = load_panel;
            Exit_button.Size = new Size(200, 50);
            Exit_button.Text = "ZAMKNIJ";
            Exit_button.Location = new Point(this.Width - 200, this.Height - 50);
            Exit_button.Click += Exit_button_click;

            picBox_OK.Parent = load_panel;
            picBox_OK.Location = new Point(890,750);
            picBox_OK.Image = LED_MB_check.Properties.Resources.OK_Icon;
            picBox_OK.Size = LED_MB_check.Properties.Resources.OK_Icon.Size;
            picBox_OK.Visible = false;

            
            button_panel.Size = new Size(100, 120);
            button_panel.Parent = this;
            button_panel.BringToFront();
            button_panel.Visible = false;
            button_panel.Validated += button_panel_lostFocus;
            //button_panel.Location = new Point(X, Y);

            Button Defect_panel_hide = new Button();
            Defect_panel_hide.Parent = button_panel;
            Defect_panel_hide.Location = new Point(0, 0);
            Defect_panel_hide.Size = new Size(100, 30);
            Defect_panel_hide.Text = "Wyczyść";
            Defect_panel_hide.BackColor = Color.Lime;
            Defect_panel_hide.TextAlign = ContentAlignment.MiddleCenter;Defect_panel_hide.Click += Defect_panel_hide_clicked;

            mech_defect_button.Size = new Size(100, 30);
            mech_defect_button.Parent = button_panel;
            mech_defect_button.Location = new Point(0, 90);
            mech_defect_button.BackColor = Color.Black;
            mech_defect_button.ForeColor = Color.White;
            mech_defect_button.Text = "Mechaniczny";
            soil_defect_button.TextAlign = ContentAlignment.MiddleCenter;
            mech_defect_button.Click += mech_defect_button_pressed;

            location_defect_button.Size = new Size(100, 30);
            location_defect_button.Parent = button_panel;
            location_defect_button.Location = new Point(0, 30);
            location_defect_button.BackColor = Color.Orange;
            location_defect_button.ForeColor = Color.White;
            location_defect_button.Text = "Położenie/brak";
            soil_defect_button.TextAlign = ContentAlignment.MiddleCenter;
            location_defect_button.Click += location_defect_button_pressed;

            soil_defect_button.Size = new Size(100, 30);
            soil_defect_button.Parent = button_panel;
            soil_defect_button.Location = new Point(0, 60);
            soil_defect_button.BackColor = Color.Brown;
            soil_defect_button.ForeColor = Color.White;
            soil_defect_button.Text = "Zabrudzenie";
            soil_defect_button.TextAlign = ContentAlignment.MiddleCenter;
            soil_defect_button.Click += soil_defect_button_pressed;

            MakePictureForm();
            Logon_textbox.Select();

            NetworkCredential shareCredential = new NetworkCredential("vision_viewer", "mst");
            CredentialCache CredChache = new CredentialCache();
            CredChache.Add(@"\\10.0.10.116", 139, "Basic", shareCredential);
            //Debug.WriteLine(System.IO.Directory.GetDirectories(@"10.0.10.116\Logs")[0]);
        }
        PictureBox picBox_OK = new PictureBox();
        Panel button_panel = new Panel();
        Button mech_defect_button = new Button();
        Button location_defect_button = new Button();
        Button soil_defect_button = new Button();

        private void button_panel_lostFocus(object sender, EventArgs e)
        {
            button_panel.Visible = false;
        }

        private void Defect_panel_hide_clicked(object sender, EventArgs e)
        {
            PCBArray[PCB_Defect_index].BackColor = Color.Lime;
            PCBArray[PCB_Defect_index].ForeColor = Color.Black;
            button_panel.Visible = false;
        }

        private void mech_defect_button_pressed(object sender, EventArgs e)
        {
            PCBArray[PCB_Defect_index].BackColor = Color.Black;
            PCBArray[PCB_Defect_index].ForeColor = Color.White;
            button_panel.Visible = false;
        }

        private void location_defect_button_pressed(object sender, EventArgs e)
        {
            PCBArray[PCB_Defect_index].BackColor = Color.Orange;
            PCBArray[PCB_Defect_index].ForeColor = Color.White;
            button_panel.Visible = false;
        }

        private void soil_defect_button_pressed(object sender, EventArgs e)
        {
            PCBArray[PCB_Defect_index].BackColor = Color.Brown;
            PCBArray[PCB_Defect_index].ForeColor = Color.White;
            button_panel.Visible = false;
        }

        private void Logon_text_TextChanged(object sender, EventArgs e)
        {
            
            if (Logon_textbox.Text.Length > 3) picBox_OK.Visible = true;
            else
                picBox_OK.Visible = false;
        }
        private void Exit_button_click(object sender, EventArgs e)
        {
            this.Close();
        }


        TextBox Logon_textbox = new TextBox();
        Panel load_panel = new Panel();
        private void Logon_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                if (Logon_textbox.Text.Length > 3)
                {
                    load_panel.Visible = false;
                    labelOperator.Text = "Zalogowano: " + Logon_textbox.Text;
                }
        }

        private void resetTests()
        {
            for (int i = 0; i < FuncResult.Count; i++)
            {
                FuncResult[i] = false;
                VisionResult[i] = false;
            }
            HVTestResult = false;
        }

        private void MakeLabelz(int count)
        {
            for (int x = 1; x < PCBArray.Length; x++)
            {
                flowLayoutPanel1.Controls.Remove(PCBArray[x]);
                
            }

            for (int h=0;h<PCBArray.Length;h++)
            {
                PCBArray[h] = null;
            }
            
            if (count > 0)
            {
                int margin = (Int32)Math.Truncate((decimal)(1275 / count)) - 1;
                
                for (int i = 1; i < count + 1; i++)
                {
                    PCBArray[i] = new Label();
                    PCBArray[i].Click += PCBArray_Click;
                    PCBArray[i].Text = i.ToString();
                    PCBArray[i].Parent = flowLayoutPanel1;
                    PCBArray[i].Location = new Point(0, 0);
                    PCBArray[i].Margin = new Padding(0, 0, 1, 0);
                    PCBArray[i].AutoSize = false;
                    PCBArray[i].Size = new Size(margin, 32);
                    PCBArray[i].BorderStyle = BorderStyle.FixedSingle;
                    PCBArray[i].BackColor = Color.LightGray;
                    PCBArray[i].ForeColor = Color.Black;
                    PCBArray[i].TextAlign = ContentAlignment.MiddleCenter;

                    //FuncResult.Add(false);
                    //VisionResult.Add(false);
                }
            }
        }

        private void PCBArray_Click(object sender, EventArgs e)
        {

            /*int ind = Array.IndexOf(PCBArray, sender);

            if (PCBArray[ind].BackColor == Color.Lime)
            {
                PCBArray[ind].ForeColor = Color.White;
                PCBArray[ind].BackColor = Color.Black;
            }
            else
            if (PCBArray[ind].BackColor == Color.Black)
            {
                PCBArray[ind].BackColor = Color.Lime;
                PCBArray[ind].ForeColor = Color.Black;
            }*/

            
            int ind = Array.IndexOf(PCBArray, sender);
            int X = PCBArray[ind].Location.X;
            int button_half_size = (Int32)Math.Truncate((decimal) PCBArray[ind].Size.Width / 2);
            if (X + button_half_size + 100 > 1280)
                X = 1180;
            else
                X = X + button_half_size;
            if (PCBArray[ind].BackColor != Color.Red)
            {
                PCB_Defect_Type(ind, X);
                button_panel.Focus();
            }
        }

        int PCB_Defect_index = 0;
        private void PCB_Defect_Type(int pcb_array_index, int X)
        {
            button_panel.Location = new Point(X, flowLayoutPanel1.Location.Y-90);
            PCB_Defect_index = pcb_array_index;
            button_panel.Visible = true;
        }

        private void SaveConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (!panelDrawSettings.Visible) panelDrawSettings.Visible = true;
            else panelDrawSettings.Visible = false;
            DrawPointers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelDrawSettings.Visible = false;
            SaveConfig("x1", c1X.Value.ToString());
            SaveConfig("y1", c1Y.Value.ToString());
            SaveConfig("x2", c2X.Value.ToString());
            SaveConfig("y2", c2Y.Value.ToString());
        }

        private void c1X_ValueChanged(object sender, EventArgs e)
        {
            DrawPointers();
        }

        private void c1Y_ValueChanged(object sender, EventArgs e)
        {
            DrawPointers();
        }

        private void c2X_ValueChanged(object sender, EventArgs e)
        {
            DrawPointers();
        }

        private void c2Y_ValueChanged(object sender, EventArgs e)
        {
            DrawPointers();
        }

        private void textBoxMB_Enter(object sender, EventArgs e)
        {
            if (buttonListen.BackColor == Color.Lime)
                button2_Click(sender, e);
        }

        private void textBoxMB_Leave(object sender, EventArgs e)
        {
            if (buttonListen.BackColor == Color.Red)
                button2_Click(sender, e);
        }

        private void textBoxZlecenie_Enter(object sender, EventArgs e)
        {
            if (buttonListen.BackColor == Color.Lime)
                button2_Click(sender, e);
        }

        private void textBoxZlecenie_Leave(object sender, EventArgs e)
        {
            if (buttonListen.BackColor == Color.Red)
                button2_Click(sender, e);
        }

        private void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
        {
            Dictionary_To_Listview(PassFailDictHV, listViewHV);
            Dictionary_To_Listview(PassFailDictFunc, listViewFunc);
            Dictionary_To_Listview(PassFailDictVis, listViewLightUp);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!justLoaded)
                //writeLog();
        }

        private List<FileInfo> MakePNGList()
        {
            List<FileInfo> result = new List<FileInfo>();


            if (dataGridLightUp.Rows.Count > 0)
            {
                string VisionPath = ConfigurationManager.AppSettings["VisionPath"];
                

                List<string> ListaDat = new List<string>();
                for (int g = 0; g < listViewLightUp.Groups.Count; g++) 
                {
                    string data_testu = listViewLightUp.Groups[g].Header.ToString().Substring(7, 8);
                    if (!ListaDat.Contains(data_testu)) ListaDat.Add(data_testu);
                }

                foreach (var data_testu in ListaDat)
                {
                    string path = VisionPath + CurrentProduct12NC + "\\" + data_testu;
                    if (Directory.Exists(path))
                    {
                        string[] PNGFiles = System.IO.Directory.GetFiles(path, "*.png");

                        foreach (string item in PNGFiles)
                        {
                            if (item.Contains(textBoxMB.Text + "_") & !(item.Contains("Oryginal")))
                            {
                                result.Add(new FileInfo(item));
                            }
                        }
                    }
                }

                // + "\\" + textBoxMB.Text + "_" + czas_testu + ".png";
                
            }
            return result;
        }

        private void ShowFailureImages()
        {
            FailBMP.Clear();
            foreach (var item in PNGFilesList)
            {
                FailBMP.Add(Image.FromFile(item.FullName));
            }

            PictureForm.Visible = true;
            PicBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureIDX = 0;
            PicBox.Image = FailBMP[pictureIDX];
            imgIndexLabel.Text = (pictureIDX + 1).ToString() + @"/" + (FailBMP.Count).ToString();
            PicFileName.Text = PNGFilesList[pictureIDX].Name;
        }

        private void MakePictureForm()  
        {
            PictureForm.FormBorderStyle = FormBorderStyle.None;
            PictureForm.WindowState = FormWindowState.Maximized;
            PictureForm.Visible = false;
            PictureForm.BackColor = Color.Black;
            PictureForm.TopMost = true;

            PicBox.Parent = PictureForm;
            PicBox.Dock = DockStyle.Fill;
            PicBox.Click += PicBox_clicked;

            Button ExitPicView = new Button();
            ExitPicView.Parent = PicBox;
            ExitPicView.Size = new Size(60, 20);
            ExitPicView.Location = new Point(0, 40);
            ExitPicView.Text = "Wyjście";
            ExitPicView.BackColor = Color.Silver;
            ExitPicView.Click += ExitPicView_clicked;

            Button FailDecription = new Button();
            FailDecription.Size = new Size(60, 20);
            FailDecription.Location = new Point(0, 20);
            FailDecription.Parent = PicBox;
            FailDecription.Click += FailDecription_Clicked;
            FailDecription.BackColor = Color.Silver;
            FailDecription.Text = "Opis wady";

            Button left = new Button();
            left.Size = new Size(30, 20);
            left.Location = new Point(0, 0);
            left.Parent = PicBox;
            left.Click += left_clicked;
            left.BackColor = Color.Silver;
            left.BackgroundImage = LED_MB_check.Properties.Resources.arrow_light;

            Button right = new Button();
            right.Size = new Size(30, 20);
            right.Location = new Point(30, 0);
            right.Parent = PicBox;
            right.Click += right_Clicked;
            right.BackColor = Color.Silver;
            right.BackgroundImage = LED_MB_check.Properties.Resources.arrow_right;

            imgIndexLabel.Parent = PicBox;
            imgIndexLabel.Location = new Point(50, 0);
            imgIndexLabel.ForeColor = Color.Red;
            imgIndexLabel.Font = new Font("Arial", 12);
            imgIndexLabel.AutoSize = true;
            imgIndexLabel.BackColor = Color.Transparent;
            imgIndexLabel.Visible = true;

            PicFileName.Parent = PicBox;
            PicFileName.Location = new Point(50, 15);
            PicFileName.ForeColor = Color.Red;
            PicFileName.Font = new Font("Arial", 12);
            PicFileName.AutoSize = true;
            PicFileName.BackColor = Color.Transparent;
            PicFileName.Visible = true;
        }

        private void PicBox_clicked(object sender, EventArgs e)
        {
            if (imgIndexLabel.Visible)
            {
                imgIndexLabel.Visible = false;
                PicFileName.Visible = false;
            }
            else
            {
                imgIndexLabel.Visible = true;
                PicFileName.Visible = true;
            }
        }

        private void FailDecription_Clicked(object sender, EventArgs e)
        {
            if (PNGFilesList.Count > 0)
            {
                string path = PNGFilesList[pictureIDX].FullName.Replace(".png", ".txt");
                string[] plik = System.IO.File.ReadAllLines(path);
                string msg = "";

                foreach (var item in plik)
                {
                    if (item != null)
                    {
                        Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                        Encoding utf8 = Encoding.Unicode;
                        byte[] utfBytes = utf8.GetBytes(item);
                        byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                        //string messsg = iso.GetString(isoBytes);
                        msg += iso.GetString(isoBytes).Replace("?", "ł") + "\r";
                    }

                }
                FlexibleMessageBox.Show(PicBox, msg, "Lista błędów wizji");
            }
        }

        private void ExitPicView_clicked(object sender, EventArgs e)
        {
            PictureForm.Visible = false;
        }

        private void left_clicked(object sender, EventArgs e)
        {
            if (pictureIDX > 0)
                pictureIDX--;
            else
                pictureIDX = FailBMP.Count - 1;
            PicBox.Image = FailBMP[pictureIDX];
            imgIndexLabel.Text = (pictureIDX + 1).ToString() + @"/" + (FailBMP.Count).ToString();
            PicFileName.Text = PNGFilesList[pictureIDX].Name;
        }

        private void right_Clicked(object sender, EventArgs e)
        {
            if (pictureIDX == FailBMP.Count - 1)
                pictureIDX = 0;
            else
                pictureIDX++;
            PicBox.Image = FailBMP[pictureIDX];
            imgIndexLabel.Text = (pictureIDX + 1).ToString() + @"/" + (FailBMP.Count).ToString();
            PicFileName.Text = PNGFilesList[pictureIDX].Name;
        }

        int pictureIDX = 0;
        Form PictureForm = new Form();
        List<Image> FailBMP = new List<Image>();
        PictureBox PicBox = new PictureBox();
        List<FileInfo> PNGFilesList = new List<FileInfo>();
        Label imgIndexLabel = new Label();
        Label PicFileName = new Label();
        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowFailureImages();
        }

        private void label3status_DoubleClick(object sender, EventArgs e)
        {
            string msg = "";
            foreach (var SN in ListaSN)
            {
                msg += SN + "\r";
            }
            FlexibleMessageBox.Show(this, msg);
        }
    }
}

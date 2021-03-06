﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FEMAIDE0._0
{
    public partial class TrueFXMissionConrtol : Form
    {
        private delegate void tabledelegateaddrow(
            String currency, String millisecondtimestamp,
            String bidprice, String offerprice, String high,
            String low, String open);
        private delegate void tabledelegateclearrow();
        private delegate void chartdelegateplotpoint(String[] information, int i);
        chartdelegateplotpoint chartagentplotpoint = chartdelegatemethodplotpoint;
        //public delegate void tabledelegatesetuptable();
        tabledelegateaddrow tableagentaddrow = tabledelegatemethodaddrow;
        tabledelegateclearrow tableagentclearrow = tabledelegatemethodclearrow;
        private static DataGridView table;
        private static Object key2 = new object();
        private static Object key = new Object();
        private static ArrayList requestedcurrenciestobelogged = new ArrayList();
        private static StreamWriter csvexchangedata;
        private static int lastcheckedindex;
        private static Boolean graphcsvdata;
        private static ArrayList requestedcurrencies = new ArrayList();
        private static Boolean didreadjust = false;
        private static Chart currencychart;
        private static String foreignexchangedatafolderpath = null;

        private static readonly String[] ALLCURRENCIES =
            {"EUR/USD","USD/JPY","GBP/USD","EUR/GBP","CAD/CHF","CAD/JPY","CHF/JPY",
             "EUR/AUD","USD/CHF","EUR/JPY","EUR/CHF","USD/CAD","AUD/USD","GBP/JPY",
             "AUD/CAD","AUD/CHF","AUD/JPY","AUD/NZD","EUR/CAD","EUR/NOK","EUR/NZD",
             "GBP/CAD","GBP/CHF","NZD/JPY","NZD/USD","USD/NOK","USD/SEK"};
        public TrueFXMissionConrtol()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

            //statest.Cells[0].Value = "Hello";
            //ForexMarketUpdatesTable.Rows.
            currencychart = CurrencyGraph;
            table = ForexMarketUpdatesTable;
            InitializeForexTable(table);
            UpdateTableBackgroundWorker.RunWorkerAsync();
            UpdateChartBackgroundWorker.RunWorkerAsync();
            UpdateCSVFileBackgroundWorker.RunWorkerAsync();
            TrueFX.initialize();
        }

        private void InitializeForexTable ( DataGridView forextable )
        {
 
            foreach (String currency in ALLCURRENCIES)
            {
                forextable.Rows.Add(currency);
            }
            

        }

        private void UpdateTableBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Equivalent to about 5 seconds
            int frequencyinmilliseconds = 5000;
            String[] information;
            String bidbigfigurestring;
            String bidpointsstring;
            String offerbigfigurestring;
            String offerpointsstring;
            String currency;
            String millisecondtimestamp;
            String high;
            String low;
            String open;
            while (true)
            {
                if ( TrueFX.getpastmodestate() == true )
                {
                    foreach (String currencyname in requestedcurrencies)
                    {

                        //This while loop is gonna be a problem for past mode
                        //Because we dont exactly need the market to be open in order for 
                        //Past mode to be enabled, but here it does. Same thing for the 
                        //The UpdaetChart method below too. Have to fix that.

                        //This logic will run for a bit, but it will crash eventually. You need to
                        //add a method that will peek to see if there is any more data to log.


                        while (((TrueFX.getinformationbatch() == /*null*/ Environment.NewLine) || (TrueFX.getinformationbatch() == null)) && (TrueFX.getpastmodestate() == false)) { }
                        if (TrueFX.getpastmodestate() == false)
                        {
                            break;
                        }
                        information = TrueFX.RequestCurrency(currencyname);

                        //currency = information[0].Remove(information[0].IndexOf('\n'), information[0].IndexOf('\n') + 1);
                        currency = information[0];
                        millisecondtimestamp = information[1];
                        bidbigfigurestring = information[2];
                        bidpointsstring = information[3];
                        offerbigfigurestring = information[4];
                        offerpointsstring = information[5];
                        high = information[6];
                        low = information[7];
                        open = information[8];

                        table.Invoke(tableagentaddrow,
                            currency, millisecondtimestamp, bidbigfigurestring + bidpointsstring,
                            offerbigfigurestring + offerpointsstring, high, low, open);
                        //This should still work because theoretically only 
                        //One currency is checked at a time still so
                        //currencychart.Invoke(chartagentplotpoint, information, 0);

                    }
                }

                //Whats happening is past mode is getting enabled in the middle of the else loop, 
                //Forcing thre rest of the currencies to do the requested thing for all currencies
                //But you might not have the data for it. Ill have to fix this.

                else
                {
                  foreach (String currencyname in ALLCURRENCIES)
                  {

                    //This while loop is gonna be a problem for past mode
                    //Because we dont exactly need the market to be open in order for 
                    //Past mode to be enabled, but here it does. Same thing for the 
                    //The UpdaetChart method below too. Have to fix that.

                    while ( ( ( TrueFX.getinformationbatch() == /*null*/ Environment.NewLine ) || (TrueFX.getinformationbatch() == null) ) && ( TrueFX.getpastmodestate() == false ) )  {  }
                    if ( TrueFX.getpastmodestate() )
                        {
                            break;
                        }
                    information = TrueFX.RequestCurrency(currencyname);

                    //currency = information[0].Remove(information[0].IndexOf('\n'), information[0].IndexOf('\n') + 1);
                    currency = information[0];
                    millisecondtimestamp = information[1];
                    bidbigfigurestring = information[2];
                    bidpointsstring = information[3];
                    offerbigfigurestring = information[4];
                    offerpointsstring = information[5];
                    high = information[6];
                    low = information[7];
                    open = information[8];          

                    table.Invoke(tableagentaddrow,
                        currency, millisecondtimestamp, bidbigfigurestring + bidpointsstring,
                        offerbigfigurestring + offerpointsstring, high, low, open);
                    //This should still work because theoretically only 
                    //One currency is checked at a time still so
                    //currencychart.Invoke(chartagentplotpoint, information, 0);

                   } 
                }
                
                System.Threading.Thread.Sleep(frequencyinmilliseconds);

                //This method really isn't very visually pleasing
                //table.Invoke( tableagentclearrow );
            }

        }


        private static void tabledelegatemethodaddrow(String currency, String millisecondtimestamp,
            String bidprice, String offerprice, String high,
            String low, String open)
        {
            for ( int i = 0; i < table.Rows.Count; i++ )
            {
                if ( table [ 0, i ].Value.ToString() == currency )
                {

                    if ( table [ 2, i ].Value == null )
                    {
                        table[1, i].Value = millisecondtimestamp;
                        table[2, i].Value = bidprice;
                        table[3, i].Value = offerprice;
                        table[4, i].Value = high;
                        table[5, i].Value = low;
                        table[6, i].Value = open;
                        break;
                    }


                    table[1, i].Value = millisecondtimestamp;
                    if ( Double.Parse( table[2, i].Value.ToString() ) < Double.Parse ( bidprice ) )
                    {
                        table[2, i].Style.BackColor = Color.Green;
                        //table[2, i].Style.ForeColor = Color.Black;
                    }
                    else if (Double.Parse(table[2, i].Value.ToString()) > Double.Parse(bidprice))
                    {
                        table[2, i].Style.BackColor = Color.IndianRed;
                        //table[2, i].Style.ForeColor = Color.Black;
                    }
                    else { table[2, i].Style.ForeColor = Color.Black; table[2, i].Style.BackColor = Color.White; }
                    /////
                    if (Double.Parse(table[3, i].Value.ToString()) < Double.Parse(offerprice))
                    {
                        table[3, i].Style.BackColor = Color.Green;
                        //table[3, i].Style.ForeColor = Color.Black;
                    }
                    else if (Double.Parse(table[3, i].Value.ToString()) > Double.Parse(offerprice))
                    {
                        table[3, i].Style.BackColor = Color.IndianRed;
                        //table[3, i].Style.ForeColor = Color.Black;
                    }
                    else { table[3, i].Style.ForeColor = Color.Black; table[3, i].Style.BackColor = Color.White; }

                    table[2, i].Value = bidprice;
                    table[3, i].Value = offerprice;
                    table[4, i].Value = high;
                    table[5, i].Value = low;
                    table[6, i].Value = open;
                    break;
                }
            }
            //table.Rows.Add(
            //            currency, millisecondtimestamp, bidprice, offerprice, high, low, open);
        }


        private static void tabledelegatemethodclearrow() { table.Rows.Clear(); }

        private void ChartTabPage_Click(object sender, EventArgs e)
        {
            //
        }

        private void CurrencyToBeLoggedRequestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (foreignexchangedatafolderpath == null)
            {
                CurrencyToBeLoggedRequestList.SelectedItem = CheckState.Unchecked;
                return;
            }

                lock (key2)
            {

                CheckedListBox.CheckedItemCollection checkedcurrencies = CurrencyToBeLoggedRequestList.CheckedItems;
                requestedcurrencies.Clear();
                foreach (String c in checkedcurrencies) { requestedcurrenciestobelogged.Add(c); }
            }
        }

        private static void logdata(String textresponse)
        {
            //It is important to note that "textresponse" is the full information
            //batch from the TrueFX server, not specific to any currency.

            //You can pretty much place log currecny wherevr you want, because
            //It will shift through and only pick out the currencies requested

            if (foreignexchangedatafolderpath == null)
            {
                return;
            }

                lock (key2)
            {
                String streamwriterpath;
                String relevantdata;
                int firstindex;
                int secondindex;
                for (int i = 0; i < requestedcurrenciestobelogged.Count; i++)
                {
                    if (textresponse == null) { return; }
                    if (!(textresponse.Contains((String)requestedcurrenciestobelogged[i]))) { return; }
                    streamwriterpath = ((String)requestedcurrenciestobelogged[i]).Replace('/', 'v');
                    csvexchangedata = File.AppendText(foreignexchangedatafolderpath + "\\" + streamwriterpath + ".txt");
                    firstindex = textresponse.IndexOf((String)requestedcurrenciestobelogged[i]);
                    relevantdata = textresponse.Substring(firstindex);
                    secondindex = textresponse.IndexOf('\n');
                    relevantdata = relevantdata.Substring(0, secondindex);
                    csvexchangedata.WriteLine(relevantdata);
                    csvexchangedata.Flush();
                    csvexchangedata.Close();
                }
            }
        }


        private void GraphPastData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( foreignexchangedatafolderpath == null ) {
                GraphPastData.SelectedItem = CheckState.Unchecked;
                return;
            }
            CheckedListBox.CheckedItemCollection graphthedata = GraphPastData.CheckedItems;
            if (graphthedata.Count > 1)
            {
                CheckState uncheck = CheckState.Unchecked;
                GraphPastData.SetItemCheckState(lastcheckedindex, uncheck);
            }
            lastcheckedindex = GraphPastData.SelectedIndex;
            foreach (String c in graphthedata)
            {
                if (c.Equals("Graph Past CSV Data on Currency")) { graphcsvdata = true; break; }
                else { graphcsvdata = false; }
            }
        }

        private void CurrencyToLogLabel_Click(object sender, EventArgs e)
        {

        }

        private void GraphLoggedDataLabel_Click(object sender, EventArgs e)
        {

        }

        private void CurrencyRequestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (key)
            {
                //With this new implementation, there should only be one checked item at a time.
                //So using requestedcurrencieslist[0] shouldn't be a wrong use of it.
                CheckedListBox.CheckedItemCollection checkedcurrencies = CurrencyRequestList.CheckedItems;
                if (checkedcurrencies.Count > 1)
                {
                    CheckState uncheck = CheckState.Unchecked;
                    CurrencyRequestList.SetItemCheckState(lastcheckedindex, uncheck);
                }

                didreadjust = false;
                //CheckedListBox.CheckedItemCollection checkedcurrencies = CurrencyRequestList.CheckedItems;
                requestedcurrencies.Clear();
                foreach (String c in checkedcurrencies) { requestedcurrencies.Add(c); }
                lastcheckedindex = CurrencyRequestList.SelectedIndex;
                //This is where the graph from a csv file needs to happen.

                //With the if statement in graph past csv data the method, I dont think
                //This will ever activate if there is no folder selected, so I dont think
                //We nee to worry about this too much.
                if (graphcsvdata)
                {
                    String temporarystring;
                    String supertemp;
                    String[] information = new String[9];
                    String csvreaderpath = ((String)requestedcurrencies[0]).Replace('/', 'v');
                    String currentlineinformation;
                    StreamReader csvreader = File.OpenText(foreignexchangedatafolderpath + csvreaderpath + ".txt");
                    //This is where a loop is going to go
                    //This loop is just to read from the csv of forex data built up
                    //No need to be alarmed about the invoke at the end
                    while (!(csvreader.Peek() == -1))
                    {
                        currentlineinformation = csvreader.ReadLine();
                        Console.WriteLine(currentlineinformation);
                        temporarystring = currentlineinformation;
                        for (int j = 0; j < information.Length; j++)
                        {
                            if (j == information.Length - 1) { information[j] = temporarystring.Substring(0); continue; }
                            //temporaryint = temporarystring.IndexOf(',');
                            supertemp = temporarystring.Substring(0, temporarystring.IndexOf(','));
                            //firstindex = temporaryint;
                            information[j] = supertemp;
                            temporarystring = temporarystring.Substring(temporarystring.IndexOf(',') + 1);
                        }
                        //The number right next to information represents the requestedcurrencies[index].
                        //Zero should be appropriate for the commented above reasons.
                        currencychart.Invoke(chartagentplotpoint, new Object[] { information, 0 });
                    }

                }
                return;

            }
        }

        public static void chartdelegatemethodplotpoint(String[] information, int i)
        {

            double deltaautoscale = 5; //how many points to scale for auto resize
            double millisecondtimestamp = Double.Parse(information[1]);
            double temp = ((((Double.Parse(information[1])) / 1000) / 60) / 60);
            double bidbigfigure = Double.Parse(information[2]);
            String bidbigfigurestring;
            double bidpoints = double.Parse(information[3]);
            String bidpointsstring;
            double bidprice = (Double.Parse(information[2] + information[3]));
            //int minutes = (int)(((millisecondtimestamp / 1000) / 60) % 60);
            //String minutesstring = minutes.ToString();
            //String minutesrawstring = (((millisecondtimestamp / 1000) / 60) % 60).ToString();
            //int hour = (int)((((millisecondtimestamp / 1000) / 60) / 60) % 24);
            //String hourstring = hour.ToString();
            //double timestamp = Double.Parse(hourstring + "." + minutesstring);
            //String secondsraw = (((Double.Parse(minutesrawstring.Substring(minutesrawstring.IndexOf('.'))) * 6).ToString()).Replace(".", ""));
            //if (!(secondsraw.Length < 2)) { secondsraw = secondsraw.Substring(0, 2); }
            //t seconds = (int)secondsraw;
            //double timestamp = Double.Parse(hourstring + minutesstring + "." + secondsraw);
            //Console.WriteLine(minutesrawstring.Substring(minutesrawstring.IndexOf('.')));
            //Console.WriteLine(secondsraw);
            //timestamp = (timestamp - 700);
            //Console.WriteLine(((millisecondtimestamp / 1000) / 60) % 60);
            //Console.WriteLine(timestamp);

            //Readjustment should happen automatically for the first time no matter whats going on in the program.
            if ( /*((Math.Abs(currencychart.ChartAreas[0].AxisY.Maximum - bidprice) > 0.00005)) &&*/ !(didreadjust))
            {
                //On any cuurency where the bidbigfigure has a decimal point in it, it gets lost in the to string, and
                //ends up being a gigantic number on the graph.
                if (!((bidbigfigure.ToString()).Contains("."))) { bidbigfigurestring = (bidbigfigure.ToString() + "."); }
                else if (bidbigfigure.ToString().Length == 3) { bidbigfigurestring = bidbigfigure.ToString() + "0"; }
                else { bidbigfigurestring = bidbigfigure.ToString(); }
                bidpointsstring = (bidpoints + deltaautoscale).ToString();
                if (bidpointsstring.Length < 3) { bidpointsstring = ("0" + bidpointsstring); }
                currencychart.ChartAreas[0].AxisY.Maximum = Double.Parse(bidbigfigurestring + bidpointsstring);
                bidpointsstring = (bidpoints - deltaautoscale).ToString();
                if (bidpointsstring.Length < 3) { bidpointsstring = ("0" + bidpointsstring); }
                currencychart.ChartAreas[0].AxisY.Minimum = Double.Parse(bidbigfigurestring + bidpointsstring);
                didreadjust = true;
            }
            if ((bidprice >= currencychart.ChartAreas[0].AxisY.Maximum))
            {
                if (!((bidbigfigure.ToString()).Contains("."))) { bidbigfigurestring = (bidbigfigure.ToString() + "."); }
                else if (bidbigfigure.ToString().Length == 3) { bidbigfigurestring = bidbigfigure.ToString() + "0"; }
                else { bidbigfigurestring = bidbigfigure.ToString(); }
                if ((bidpoints + deltaautoscale) > 999)
                {
                    if (!(bidbigfigurestring.EndsWith(".")))
                    {
                        int count = ((bidbigfigurestring.Substring(bidbigfigurestring.IndexOf(".")).Length) - 1);
                        String additionstring = "1";
                        for (; count != 0; count--) { additionstring = additionstring.Insert(0, "0"); }
                        if (additionstring.Contains("0")) { additionstring = additionstring.Insert((additionstring.IndexOf("0") + 1), "."); }
                        bidbigfigure += Double.Parse(additionstring);
                    }
                    else { bidbigfigure += 1; }
                    //Only works for sane amounts of deltaautoscale
                    bidpoints = (bidpoints + deltaautoscale) % 1000;
                    bidpointsstring = bidpoints.ToString();
                }
                else { bidpointsstring = (bidpoints - deltaautoscale).ToString(); }
                bidpointsstring = (bidpoints + deltaautoscale).ToString();
                if (bidpointsstring.Length < 3) { bidpointsstring = ("0" + bidpointsstring); }
                currencychart.ChartAreas[0].AxisY.Maximum = Double.Parse(bidbigfigurestring + bidpointsstring);
            }
            if (bidprice < currencychart.ChartAreas[0].AxisY.Minimum)
            {
                if (!((bidbigfigure.ToString()).Contains("."))) { bidbigfigurestring = (bidbigfigure.ToString() + "."); }
                else if (bidbigfigure.ToString().Length == 3) { bidbigfigurestring = bidbigfigure.ToString() + "0"; }
                else { bidbigfigurestring = bidbigfigure.ToString(); }
                if ((bidpoints - deltaautoscale) < 0)
                {
                    if (!(bidbigfigurestring.EndsWith(".")))
                    {
                        int count = ((bidbigfigurestring.Substring(bidbigfigurestring.IndexOf(".")).Length) - 1);
                        String subtractionstring = "1";
                        for (; count != 0; count--) { subtractionstring = subtractionstring.Insert(0, "0"); }
                        if (subtractionstring.Contains("0")) { subtractionstring = subtractionstring.Insert((subtractionstring.IndexOf("0") + 1), "."); }
                        bidbigfigure -= Double.Parse(subtractionstring);
                    }
                    else { bidbigfigure -= 1; }
                    bidpoints = (999 - Math.Abs(bidpoints - deltaautoscale));
                    bidpointsstring = bidpoints.ToString();
                }
                else { bidpointsstring = (bidpoints - deltaautoscale).ToString(); }
                if (bidpointsstring.Length < 3) { bidpointsstring = ("0" + bidpointsstring); }
                currencychart.ChartAreas[0].AxisY.Minimum = Double.Parse(bidbigfigurestring + bidpointsstring);
            }
            currencychart.Series[(String)requestedcurrencies[i]].Points.AddXY(temp, bidprice);
        }


        private void UpdateChartBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {


            int frequencyinmilliseconds = 5000;
            String[] information = new String[9];

            while ( true ) { 

                foreach ( String currency in requestedcurrencies)
                {
                    //There really should only be one string in requested currencies in
                    //light of the whole only able to chose one currecny at a time from
                    //the box list on the actual form so.
                    while (((TrueFX.getinformationbatch() == /*null*/ Environment.NewLine) || (TrueFX.getinformationbatch() == null)) && (TrueFX.getpastmodestate() == false)) { }
                    information = TrueFX.RequestCurrency(currency);
                    //The zero thing should work for the commented reason above
                    currencychart.Invoke(chartagentplotpoint, information, 0);
                }
                System.Threading.Thread.Sleep(frequencyinmilliseconds);
                //PROBLEM: Past mode will not work for the chart becuase their is no mechanism 
                //to check for pastmode. So, it will not gather data for pastmode? But then again,
                //if pastmode is enabled, it should check right within the TrueFX Class. So I don't
                //know

            }
            




        }

        private void UpdateCSVFileBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int frequencyinmilliseconds = 5000; //In milliseconds
            while ( true )
            {
                logdata(TrueFX.getinformationbatch());
                System.Threading.Thread.Sleep(frequencyinmilliseconds);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void FEDataFolderSelectorButton_Click(object sender, EventArgs e)
        {
            if (FEDataFolderSelector.ShowDialog() == DialogResult.OK)
            {
                foreignexchangedatafolderpath = FEDataFolderSelector.SelectedPath;
                FEDFolderSelectButton1.Text = FEDataFolderSelector.SelectedPath;
                TrueFX.setfedatafolderpath(FEDataFolderSelector.SelectedPath);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if ( FEDataFolderSelector.ShowDialog() == DialogResult.OK )
            {
                foreignexchangedatafolderpath = FEDataFolderSelector.SelectedPath;
                FEDFolderSelectButton1.Text = FEDataFolderSelector.SelectedPath;
                TrueFX.setfedatafolderpath(FEDataFolderSelector.SelectedPath);
            }

        }

        private void PastModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ( PastModeCheckBox.Checked == true )
            {
                TrueFX.enablepastmode();
                CheckedListBox copy = (CheckedListBox)(TrueFXMissionControlControl.Controls.Find("GraphPastData", true)[0]);
                copy.SetItemCheckState(0, CheckState.Unchecked);
                copy.SelectedIndex = 0; //To trigger the selected index change for the handler above.
                TrueFXMissionControlControl.Controls.Find("GraphPastData", true)[0].Visible = false;
            }
            else { TrueFXMissionControlControl.Controls.Find("GraphPastData", true)[0].Visible = true; TrueFX.disablepastmode();  }
        }

        private void AILogFormatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ( AILogFormatCheckBox.Checked == true )
            {

            }
        }

        /* CheckState uncheck = CheckState.Unchecked;
         CurrencyRequestList.SetItemCheckState(CurrencyRequestList.SelectedIndex, uncheck);
         didreadjust = false;
         //CheckedListBox.CheckedItemCollection checkedcurrencies = CurrencyRequestList.CheckedItems;
         requestedcurrencies.Clear();
         foreach (String c in checkedcurrencies) { requestedcurrencies.Add(c); }
         return;*/
    }

        ////////
    }

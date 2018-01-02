using Binance.Api;
using Binance.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binance_API_2
{
    public partial class VolumeInfo : Form
    {
        public VolumeInfo()
        {
            InitializeComponent();
            TableInit();
            RefreshButton.Visible = false;
            FontSet();
            GetVolumes();
            
        }
        //1200 Requests a minute
        //10 Orders a second
        //100,000 Orders a day
        DataTable CoinData = new DataTable();
        DataTable CoinData2 = new DataTable();
        private IEnumerable<SymbolPrice> coinprices;
        private IEnumerable<SymbolPrice> _pricesArray;
        public void TableInit()
        {
            CoinData.Columns.Add("Symbol", typeof(string));
            CoinData.Columns.Add("Scanned", typeof(DateTime));
            CoinData.Columns.Add("8HrAv", typeof(int));
            CoinData.Columns.Add("NowPercent", typeof(decimal));
            CoinData.Columns.Add("15MinPercent", typeof(decimal));
            CoinData.Columns.Add("GreenCandles", typeof(decimal));
            CoinData2.Columns.Add("Symbol", typeof(string));
            CoinData2.Columns.Add("Scanned", typeof(DateTime));
            CoinData2.Columns.Add("8HrAv", typeof(int));
            CoinData2.Columns.Add("NowPercent", typeof(decimal));
            CoinData2.Columns.Add("15MinPercent", typeof(decimal));
            CoinData2.Columns.Add("GreenCandles", typeof(decimal));
        }

        private async Task<DataTable> SetupCoinData()
        {
            if (CoinData.Rows.Count == 0)
            {
                BinanceApi api = new BinanceApi();
                
               coinprices = await api.GetPricesAsync(CancellationToken.None); // Grab all coin symbols
                _pricesArray = coinprices.ToArray().Where(o =>
                    o.Symbol == "BTCUSDT" || o.Symbol == "ETHBTC" || o.Symbol.EndsWith("ETH"));
                foreach (SymbolPrice price in _pricesArray)
                {
                    DataRow row = CoinData.NewRow();
                    row[0] = price.Symbol;
                    row[1] = DateTime.UtcNow;
                    CoinData.Rows.Add(row);
                }
            }
            return CoinData;
        }

        
        public async Task<DataTable> GetVolumes()
        {
            BinanceApi api = new BinanceApi();
            await SetupCoinData();
            while (true)
            {
                foreach (DataRow pricer in CoinData.Rows)
                {
                    IEnumerable<Candlestick> eighthourcandles = await api.GetCandlesticksAsync(pricer[0].ToString(),
                        CandlestickInterval.Minutes_5, 0, 0L, 0L, CancellationToken.None);
                    IOrderedEnumerable<Candlestick> candlearray = eighthourcandles.OrderByDescending(e => e.CloseTime);
                    decimal averagevolumeeighthours = candlearray.Average(candlestick => candlestick.Volume);
                    decimal fifteenminavg = (candlearray.ElementAt(1).Volume + candlearray.ElementAt(2).Volume +
                                             candlearray.ElementAt(0).Volume) / 3;
                    DataRow row = CoinData.Rows.Cast<DataRow>().First(o => o[0].ToString() == pricer[0].ToString());
                    row[1] = DateTime.UtcNow;
                    row[2] = Decimal.Round(averagevolumeeighthours, MidpointRounding.AwayFromZero);
                    row[3] = PercentGive(candlearray.First().Volume, averagevolumeeighthours);
                    row[4] = PercentGive(fifteenminavg, averagevolumeeighthours);
                    int greencandles = 0;
                    int i = 0;
                    while (i != 20)
                    {
                        if (candlearray.ElementAt(i).Close > candlearray.ElementAt(i + 1).Close)
                        {
                            greencandles++;
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    row[5] = greencandles;
                    CoinData.DefaultView.RowFilter = "NowPercent > 0";
                    //var filtered = CoinData.Select().Where(o => Convert.ToDecimal(o[3].ToString()) > Convert.ToDecimal(o[4].ToString()));
                    DataViewColours();
                    coindatagridview.DataSource = CoinData;
                    coindatagridview.Sort(coindatagridview.Columns[3], ListSortDirection.Descending);
                    
                }

                



            }
            
            return CoinData;
        }

        //private async Task<DataRow> RowUpdater()
        //{
            
        //}

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Visible = false;
            await GetVolumes();
        }

        private decimal PercentGive(decimal newvolume, decimal oldvolume)
        {
            return Decimal.Round(100 * (newvolume - oldvolume) / oldvolume, MidpointRounding.AwayFromZero);
        }

        private void DataViewColours()
        {
            if (coindatagridview.DataSource != null)
            {
                
                foreach (DataGridViewRow viewRow in coindatagridview.Rows)
                {
                    //CellCheck(viewRow, 3);
                    //CellCheck(viewRow, 4);
                    CellCheck(viewRow, 5);
                }
                
            }
            //int i = coindatagridview.Rows.Count - 1;
            //while (i >= 0)
            //{
            //    CellCheck(coindatagridview.Rows[i], 3);
            //    CellCheck(coindatagridview.Rows[i], 4);
            //    CellCheck(coindatagridview.Rows[i], 5);
            //    i--;
            //}
            //foreach (DataGridViewRow viewRow in coindatagridview2.Rows)
            //{
            //    CellCheck(viewRow, 3);
            //    CellCheck(viewRow, 4);
            //    CellCheck(viewRow, 5);
            //}
        }

        


        private decimal CellCheck(DataGridViewRow viewRow, int i)
        {
            if (viewRow.Cells[i].Value.ToString() == "")
            {
                return 0;
            }
            else
            {
                if (Convert.ToDecimal(viewRow.Cells[3].Value) > Convert.ToDecimal(viewRow.Cells[4].Value) || Convert.ToDecimal(viewRow.Cells[2].Value) > 1000)
                {
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[coindatagridview.DataSource];
                    currencyManager1.SuspendBinding();
                    coindatagridview.Rows[viewRow.Index].Visible = true;
                    currencyManager1.ResumeBinding();
                }
               if(Convert.ToDecimal(viewRow.Cells[2].Value) < 1000 || Convert.ToDecimal(viewRow.Cells[3].Value) < Convert.ToDecimal(viewRow.Cells[4].Value))
                {
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[coindatagridview.DataSource];
                    currencyManager1.SuspendBinding();
                    coindatagridview.Rows[viewRow.Index].Visible = false;
                    currencyManager1.ResumeBinding();
                }
                if (i == 5)
                {
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) >= 4)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.LightGreen;
                        viewRow.Cells[i].Style.ForeColor = Color.Black;
                    }
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) >= 6)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.Green;
                        viewRow.Cells[i].Style.ForeColor = Color.White;
                    }
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) < 4)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.White;
                        viewRow.Cells[i].Style.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) >= 0)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.LightGreen;
                        viewRow.Cells[i].Style.ForeColor = Color.Black;
                  
                    }
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) >= 5)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.Green;
                        viewRow.Cells[i].Style.ForeColor = Color.White;
                    }
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) < 0)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.LightCoral;
                        viewRow.Cells[i].Style.ForeColor = Color.Black;
                       
                    }
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) <= -10)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.Red;
                        viewRow.Cells[i].Style.ForeColor = Color.Black;
                    }
                    if (Convert.ToDecimal(viewRow.Cells[i].Value) <= -30)
                    {
                        viewRow.Cells[i].Style.BackColor = Color.DarkRed;
                        viewRow.Cells[i].Style.ForeColor = Color.White;
                    }

                }
                return Convert.ToDecimal(viewRow.Cells[i].Value);
            }
        }

        private void coindatagridview_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in coindatagridview.SelectedCells)
            {
   
            }
        }

        private void FontSet()
        {
            foreach (Control control in Controls)
            {
                {
                    control.Font = new Font("Arial", 10, FontStyle.Bold);
                }
            }
        }

        private void coindatagridview_Sorted(object sender, EventArgs e)
        {
            
          
            DataViewColours();
        }



        private ListSortDirection SortOrder(SortOrder sortOrder)
        {
            if (sortOrder.ToString() == "Descending")
            {
                return ListSortDirection.Descending;
            }
            else
            {
                return ListSortDirection.Ascending;
            }
        }
    }
}

using Binance.Api;
using Binance.Market;
using System;
using System.Collections.Generic;
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
            SetupCoinData();
            GetVolumes();
            coindatagridview.DataSource = CoinData;
        }
        //1200 Requests a minute
        //10 Orders a second
        //100,000 Orders a day
        DataTable CoinData = new DataTable();

        public void TableInit()
        {
            CoinData.Columns.Add("Symbol", typeof(string));
            CoinData.Columns.Add("Scanned", typeof(DateTime));
            CoinData.Columns.Add("8HrAv", typeof(int));
            CoinData.Columns.Add("NowPercent", typeof(decimal));
            CoinData.Columns.Add("15MinPercent", typeof(decimal));
            CoinData.Columns.Add("GreenCandles", typeof(decimal));
        }

        private async void SetupCoinData()
        {
            BinanceApi api = new BinanceApi();
            IEnumerable<SymbolPrice> coinprices = await api.GetPricesAsync(CancellationToken.None); // Grab all coin symbols
            pricesArray = coinprices.ToArray()
                .Where(o => o.Symbol == "BTCUSDT" || o.Symbol == "ETHBTC" || o.Symbol.EndsWith("ETH"));
        }

        private IEnumerable<SymbolPrice> pricesArray;
        public async Task<DataTable> GetVolumes()
        {
            
            BinanceApi api = new BinanceApi();
            
            foreach (SymbolPrice pricer in pricesArray)
            {
                IEnumerable<Candlestick> eighthourcandles = await api.GetCandlesticksAsync(pricer.Symbol, CandlestickInterval.Minutes_5, 0, 0L, 0L, CancellationToken.None);
                IOrderedEnumerable<Candlestick> candlearray = eighthourcandles.OrderByDescending(e => e.CloseTime);
                decimal averagevolumeeighthours = candlearray.Average(candlestick => candlestick.Volume);
                decimal fifteenminavg = (candlearray.ElementAt(1).Volume + candlearray.ElementAt(2).Volume +
                                         candlearray.ElementAt(0).Volume) / 3;
                DataRow row = CoinData.NewRow();
                row[0] = pricer.Symbol;
                row[1] = DateTime.UtcNow;
                row[2] = Decimal.Round(averagevolumeeighthours, MidpointRounding.AwayFromZero); 
                row[3] = PercentGive(candlearray.First().Volume, averagevolumeeighthours);
                row[4] = PercentGive(fifteenminavg, averagevolumeeighthours);
                int greencandles = 0;
                int i = 0;
                while (i != 9)
                {
                    if (candlearray.ElementAt(i).Close > candlearray.ElementAt(i + 1).Close)
                    {
                        greencandles++;
                    }
                    i++;
                }
                row[5] = greencandles;
                CoinData.Rows.Add(row);
                DataViewColours();
            }
            Flashing.FlashWindowEx(this);
            System.Media.SystemSounds.Beep.Play();
            RefreshButton.Visible = true;
            return CoinData;
        }


        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Visible = false;
            CoinData.Rows.Clear();
            await GetVolumes();
        }

        private decimal PercentGive(decimal newvolume, decimal oldvolume)
        {
            return Decimal.Round(100 * (newvolume - oldvolume) / oldvolume, MidpointRounding.AwayFromZero);
        }

        private void DataViewColours()
        {
            foreach (DataGridViewRow viewRow in coindatagridview.Rows)
            {
                CellCheck(viewRow, 3);
                CellCheck(viewRow, 4);
                CellCheck(viewRow, 5);
            }
        }

        private void CellCheck(DataGridViewRow viewRow, int i)
        {
            if (Convert.ToDecimal(viewRow.Cells[i].Value) > 5)
            {
                viewRow.Cells[i].Style.BackColor = Color.Green;
                viewRow.Cells[i].Style.ForeColor = Color.White;
            }
            if (Convert.ToDecimal(viewRow.Cells[i].Value) > 5)
            {
                viewRow.Cells[i].Style.BackColor = Color.Green;
                viewRow.Cells[i].Style.ForeColor = Color.White;
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
                    control.Font = new Font("Arial", 12, FontStyle.Bold);
                    //control.BackColor = Color.Black;
                    //control.ForeColor = Color.White;
                }
            }
        }
    }
}

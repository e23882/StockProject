using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MahAppBase
{
	public class MainComponent: ViewModelBase
	{
		#region Declarations
		private ObservableCollection<StockData> _StockData = new ObservableCollection<StockData>();
		const int maximum = 100;
		const int minimum = 1;
		private int _UpdateFrequence = 1;

		#endregion

		#region Property
		/// <summary>
		/// 增加價格跳動頻率
		/// </summary>
		public NoParameterCommand AddFrequenceCommand { get; set; }

		/// <summary>
		/// 減少價格跳動頻率
		/// </summary>
		public NoParameterCommand ReduceFrequenceCommand { get; set; }
		/// <summary>
		/// 價格跳動頻率
		/// </summary>
		public int UpdateFrequence
		{
			get
			{
				return _UpdateFrequence;
			}
			set
			{
				_UpdateFrequence = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// 股票資料集合
		/// </summary>
		public ObservableCollection<StockData> StockData
		{
			get
			{
				return _StockData;
			}
			set
			{
				_StockData = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region MemberFunction
		public MainComponent()
		{
			//模擬假的200檔股票
			InitialMockQuoteDataCollection();

			//初始化Command
			InitialCommand();

			//模擬股票報價服務
			InitialMockQuoteService();

		}

		private void InitialCommand()
		{
			AddFrequenceCommand = new NoParameterCommand(AddFrequenceCommandAction);
			ReduceFrequenceCommand = new NoParameterCommand(ReduceFrequenceCommandAction);

		}

		/// <summary>
		/// 減少價格跳動頻率
		/// </summary>
		private void ReduceFrequenceCommandAction()
		{
			UpdateFrequence--;

		}

		/// <summary>
		/// 增加價格跳動頻率
		/// </summary>
		private void AddFrequenceCommandAction()
		{
			UpdateFrequence++;
		}

		/// <summary>
		/// 模擬假的200檔股票
		/// </summary>
		private void InitialMockQuoteDataCollection()
		{

			Random radom = new Random();
			for (int i = 1;i < 201;i++)
			{
				double yesterdayClosePrice = Math.Round(radom.NextDouble() * (maximum - minimum) + minimum, 2);
				StockData.Add(new MahAppBase.StockData()
				{
					StockName = $"股票00{i}({i.ToString().PadLeft(4, '0')})",
					Price = yesterdayClosePrice,
					QuoteChange = 0.0,
					ClosePrice = yesterdayClosePrice
				});
			}

		}

		/// <summary>
		/// 初始化模擬報價服務
		/// </summary>
		private async void InitialMockQuoteService()
		{
			await Task.Run(async () =>
			{
				Random radom = new Random();
				while (true)
				{
					var updateDataIndex = radom.Next(1, 200);
					StockData[updateDataIndex].Price = Math.Round(radom.NextDouble() * (maximum - minimum) + minimum, 2);
					await Task.Delay(UpdateFrequence);
				}
			});
		}
		#endregion
	}
}
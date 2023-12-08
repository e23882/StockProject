using System;

namespace MahAppBase
{
	public class StockData:ViewModelBase
	{
		#region Declarations
		private string _StockName;
		private double _Price = 0.0;
		private double _QuoteChange = 0.0;
		#endregion

		#region Property
		/// <summary>
		/// 前一日收盤價
		/// </summary>
		public double ClosePrice 
		{
			get;
			set;
		}		

		/// <summary>
		/// 股票漲跌幅
		/// </summary>
		public double QuoteChange 
		{
			get 
			{
				return Math.Round((ClosePrice - Price) / ClosePrice, 2);
			}
			set 
			{
				_QuoteChange = value;
				OnPropertyChanged();
			}
		}
		/// <summary>
		/// 成交價
		/// </summary>
		public double Price 
		{
			get 
			{
				return _Price;
			}
			set 
			{
			
				_Price = value;
				OnPropertyChanged();
				OnPropertyChanged("QuoteChange");

			}
		}
		/// <summary>
		/// 股票名稱
		/// </summary>
		public string StockName
		{
			get
			{
				return _StockName;
			}
			set
			{
				_StockName = value;
				OnPropertyChanged();
			}
		}
		#endregion
	}
}

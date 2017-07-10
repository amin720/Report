using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Report.ViewModel
{
	public class AccountingViewModel
	{
		[DisplayName("شرح")]
		public string Description { get; set; }
		[DisplayName("بدهکار")]
		public long? Debtor { get; set; }
		[DisplayName("بستانکار")]
		public long? Creditor { get; set; }
		[DisplayName("حساب تفصیلی")]
		public int DetailedAccountCode { get; set; }
		public string DetailedAccountName { get; set; }
		[DisplayName("حساب معین")]
		public int CertainAccountCode { get; set; }
		public string CertainAccountName { get; set; }
		[DisplayName("حساب کل")]
		public int TotalAccountCode { get; set; }
		public string TotalAccountName { get; set; }

	}
}
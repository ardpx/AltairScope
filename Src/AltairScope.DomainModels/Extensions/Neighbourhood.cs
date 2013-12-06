using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels
{
	public partial class Neighbourhood
	{
		public void Add(IAppContext appContext)
		{
			var db = appContext.GetPersistenceContext();
			db.Neighbourhoods.Add(this);
		}

		public void ValidateAndSave()
		{
			this.p_r_rate = rental / median_price;
			this.appreciation_mean = (appreciation_1990 + appreciation_10y + appreciation_5y + appreciation_2y + appreciation_1y + appreciation_1q) / 6;
		}
	}
}

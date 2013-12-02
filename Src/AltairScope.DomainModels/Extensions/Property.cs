using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels
{
	public partial class Property
	{
		public void Add(IAppContext appContext)
		{
			var db = appContext.GetPersistenceContext();
			db.Properties.Add(this);
		}

		public void ValidateAndSave()
		{
			_CalculateTaxRateAndActualTax();
			_CalculateBuildingRatio();
			_CalculateMeanOfFMV();
			_CalculateMeanOfRental();
			if (this.id == Guid.Empty)
				_RecordCreationInfo();
		}

		private void _CalculateTaxRateAndActualTax()
		{
			if (taxable_tax.HasValue && taxable_total.HasValue)
			{
				tax_rate = taxable_tax.Value / taxable_total.Value;
				Property_Sale.tax = (int)(tax_rate.Value * Property_Sale.price);
			}
		}

		private void _CalculateBuildingRatio()
		{
			if (taxable_additions.HasValue && taxable_total.HasValue)
				addition_total_ratio = taxable_additions.Value / taxable_total.Value;
		}

		private void _CalculateMeanOfFMV()
		{
			List<int> fmvList = new List<int>();
			if (fmv_zestimate.HasValue)
				fmvList.Add(fmv_zestimate.Value);
			if (fmv_smartzip.HasValue)
				fmvList.Add(fmv_smartzip.Value);
			if (fmv_eappraisal.HasValue)
				fmvList.Add(fmv_eappraisal.Value);
			if (fmvList.Count != 0)
			{
				fmv_mean = (int)fmvList.Average();
				Property_Sale.list_fmv_diff = Property_Sale.price - fmv_mean.Value;
			}
		}

		private void _CalculateMeanOfRental()
		{
			List<int> rentalList = new List<int>();
			if (rental_zrent.HasValue)
				rentalList.Add(rental_zrent.Value);
			if (rental_rentometer.HasValue)
				rentalList.Add(rental_rentometer.Value);
			if (rental_zilpy.HasValue)
				rentalList.Add(rental_zilpy.Value);

			if (rentalList.Count != 0)
				rental_mean = (int)rentalList.Average();
		}

		private void _RecordCreationInfo()
		{
			create_date = DateTime.Now;
			created_by = 2; 
		}
	}
}

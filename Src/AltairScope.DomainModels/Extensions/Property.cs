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

			var changeHistory = new Property_Change_History()
			{
				change_type = ChangeType.Edited,
				updated_date = DateTime.Now,
				updated_by = 2
			};

			this.Property_Change_Histories.Add(changeHistory);

			if (this.id == Guid.Empty)
			{
				_RecordCreationInfo();
				changeHistory.change_type = ChangeType.Created;
			}
		}

		private void _CalculateTaxRateAndActualTax()
		{
			if (taxable_tax.HasValue && taxable_total.HasValue)
			{
				tax_rate = (decimal)taxable_tax.Value / (decimal)taxable_total.Value;
				Property_Sale.tax = (int)(tax_rate.Value * Property_Sale.price);
			}
		}

		private void _CalculateBuildingRatio()
		{
			if (taxable_additions.HasValue && taxable_total.HasValue)
				addition_total_ratio = (decimal)taxable_additions.Value / (decimal)taxable_total.Value;
		}

		private void _CalculateMeanOfFMV()
		{
			List<int> fmvList = new List<int>();
			if (fmv_zestimate.HasValue)
			{
				fmvList.Add(fmv_zestimate.Value); fmvList.Add(fmv_zestimate.Value);
			}

			if (fmv_smartzip.HasValue)
			{
				fmvList.Add(fmv_smartzip.Value); fmvList.Add(fmv_smartzip.Value);
			}
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
			this.Property_Evaluation = new Property_Evaluation();
			this.Property_Sale.status = DecisionStatusType.NOT_DECIDED;
			create_date = DateTime.Now;
			created_by = 2; 
		}
	}
}

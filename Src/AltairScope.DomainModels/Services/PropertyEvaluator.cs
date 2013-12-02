using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels.Services
{
	public class PropertyEvaluator
	{
		private int _Price;

		private int _FMV;

		private decimal _LoanToValue = 0.80m;

		private decimal _LoanInterestRate = 0.04875m;

		private decimal _EstimatedAnnualAppreciationRate;

		private int _TermsOfLoan = 360;

		private int _FirstYearImprovements = 2500;

		private int _Rental_Annual;

		private decimal _FirstYearVacancy;

		private decimal _SubsequentYearVacancy;

		private int _PropertyTax_Annual;

		private decimal _ManagementFeeRatio_Annual = 0.1m;

		private decimal _RepairFeeRation_Annual = 0.1m;

		private decimal _AdditionalExpense_Annual;

		private decimal _YearsOfDepreciation = 27.5m;

		private decimal _DepreciationRatio;

		private decimal _MarginalTaxRate = 0.15m;

		private decimal _RentalIncrease_Annual = 0.02m;

		private decimal _InflationRate_Annual = 0.02m;

		private decimal _ApproxBuyingCosts = 0.02m;

		private decimal _ApproxSellingCosts = 0.02m;

		private decimal _CapticalGainsTaxRate = 0.15m;

		private decimal _DepreciationRecapture = 0.25m;

		private decimal _Mortgage_Monthly;

		private Property _Property = null;
		private Property_Sale _PropertySale = null;
		private Property_Evaluation _PropertyEvaluation = null;
		private Neighbourhood _Neighbourhood = null;

		public void Evaluate(Property property)
		{
			_Evaluate_Setup(property);
		}

		private void _Evaluate_Setup(Property property)
		{
			_Property = property;

			if (property.Property_Sale == null)
				throw new Exception("no property sale data");
			_PropertySale = property.Property_Sale;

			if (property.Neighbourhood == null)
				throw new Exception("no neighbourhood data");
			_Neighbourhood = property.Neighbourhood;

			_Price = _PropertySale.price;
			_FMV = _Property.fmv_mean ?? _Price;
			_EstimatedAnnualAppreciationRate = (_Neighbourhood.appreciation_mean.Value + _Neighbourhood.appreciation_10y) / 2;
			_Rental_Annual = _Property.rental_mean.Value * 12;
			_FirstYearVacancy = _Neighbourhood.vacancy_ratio > 0.1666m ? (_Neighbourhood.vacancy_ratio + 0.1666m) / 2 : 0.1666m;
			_SubsequentYearVacancy = _Neighbourhood.vacancy_ratio > 0.0833m ? (_Neighbourhood.vacancy_ratio + 0.0833m) / 2 : 0.0833m;
			_PropertyTax_Annual = _PropertySale.tax;
			_AdditionalExpense_Annual = _Property.hoa;
			_DepreciationRatio = _Property.addition_total_ratio.Value;

		}

		private void _Calculate_Mortgage()
		{
			//_Mortgage_Monthly = 
		}
	}
}

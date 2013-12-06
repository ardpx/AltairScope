using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels.Services
{
	public class PropertyEvaluator
	{
		private bool _Fixed_Vacancy_Ratio;
		private int _Price;

		private int _FMV;

		private decimal _LoanToValue = 0.80m;

		private decimal _LoanInterestRate = 0.04875m;

		private int _TermsOfLoan_Months = 360;

		private int _FirstYearImprovements = 2500;

		private int _Rental_Annual;

		

		private int _PropertyTax_Annual;

		private double _ManagementFeeRatio_Annual = 0.1d;

		private double _RepairFeeRatio_Annual = 0.1d;

		private int _AdditionalExpense_Annual;

		private decimal _YearsOfDepreciation = 27.5m;

		private double _DepreciationRatio; //building ratio

		private double _MarginalTaxRate = 0.15d;

		private double _RentalIncrease_Annual = 0.02d;

		private double _InflationRate_Annual = 0.02d;

		private double _ApproxBuyingCosts = 0.02d;

		private double _ApproxSellingCosts = 0.02d;

		private double _CapticalGainsTaxRate = 0.15d;

		private double _DepreciationRecapture = 0.25d;
		

		private Property _Property = null;
		private Property_Sale _PropertySale = null;
		private Property_Evaluation _PropertyEvaluation = null;
		private Neighbourhood _Neighbourhood = null;

		// results calculated
		private double _MortgagePayment_Annual;
		private double _Depreciation_Annual;
		private double[] _TotalGrossIncome = new double[10];
		private double[] _GrossOperationIncome = new double[10];
		private double[] _TotalExpense = new double[10];
		private double[] _NetOperationIncome = new double[10];

		public double[] _MortgageInterest = new double[10];
		public double[] _MortgagePrincipal = new double[10];


		public double EstimatedAnnualAppreciationRate;
		public double FirstYearVacancy;
		public double SubsequentYearVacancy;

		public double[] CashflowAfterTax = new double[10];
		public double[] ReturnRateOnInitialCash = new double[10];
		public int InitialCash;

		public PropertyEvaluator()
		{
		}

		public PropertyEvaluator(int price, int rental_monthly, bool fixed_vacancy_ratio)
		{
			_Price = price;
			_Rental_Annual = rental_monthly * 12;
			_Fixed_Vacancy_Ratio = fixed_vacancy_ratio;
		}

		public void Evaluate(Property property)
		{
			_Evaluate_Setup(property);
			_Calculate();
			_RecordResults();
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

			if (_Price == 0)
				_Price = _PropertySale.price;
			_FMV = _Property.fmv_mean ?? _Price;
			EstimatedAnnualAppreciationRate = (double)(_Neighbourhood.appreciation_mean.Value + _Neighbourhood.appreciation_10y) / 2;
			if(_Rental_Annual == 0)
				_Rental_Annual = _Property.rental_mean.Value * 12;
			if (_Fixed_Vacancy_Ratio)
			{
				FirstYearVacancy = 0.1666d;
				SubsequentYearVacancy = 0.0833d;
			}
			else
			{
				FirstYearVacancy = _Neighbourhood.vacancy_ratio > 0.1666m ? (double)(_Neighbourhood.vacancy_ratio + 0.1666m) / 2 : 0.1666d;
				SubsequentYearVacancy = _Neighbourhood.vacancy_ratio > 0.0833m ? (double)(_Neighbourhood.vacancy_ratio + 0.0833m) / 2 : 0.0833d;
			}
				_PropertyTax_Annual = _PropertySale.tax;
			_AdditionalExpense_Annual = _Property.hoa;
			_DepreciationRatio = (double)(_Property.addition_total_ratio ?? 0.75m);

		}

		private void _Calculate()
		{
			_CalculateNetOperationIncomes();
			_CalculateMortgage();
			_CalculateCashflowAfterTax();
			_CalculateReturnRateOnInitialCash();
		}

		private void _CalculateNetOperationIncomes()
		{
			__CalculateGrossOperationIncomes();
			__CalculateExpenses();

			for (int i = 0; i <= 9; i++)
			{
				_NetOperationIncome[i] = _GrossOperationIncome[i] - _TotalExpense[i];
			}
		}

		private void __CalculateGrossOperationIncomes()
		{
			_TotalGrossIncome[0] = _Rental_Annual;
			_GrossOperationIncome[0] = _TotalGrossIncome[0] * ( 1 - FirstYearVacancy);
			for(int i = 1; i <= 9; i++)
			{
				_TotalGrossIncome[i] = _TotalGrossIncome[i - 1] * (1 + _RentalIncrease_Annual);
				_GrossOperationIncome[i] = _TotalGrossIncome[i] * ( 1 - SubsequentYearVacancy);
			}
		}

		private void __CalculateExpenses()
		{
			double[] propertyTax = new double[10];
			double[] additionalExpense = new double[10];

			propertyTax[0] = _PropertyTax_Annual;
			additionalExpense[0] = _AdditionalExpense_Annual;
			_TotalExpense[0] = propertyTax[0] + additionalExpense[0] + _GrossOperationIncome[0] * (_RepairFeeRatio_Annual + _ManagementFeeRatio_Annual);
			for (int i = 1; i <= 9; i++)
			{
				propertyTax[i] = propertyTax[i - 1] * (1 + _InflationRate_Annual);
				additionalExpense[i] = additionalExpense[i - 1] * (1 + _InflationRate_Annual);

				_TotalExpense[i] = propertyTax[i] + additionalExpense[i] + 
								  _GrossOperationIncome[i] * (_RepairFeeRatio_Annual + _ManagementFeeRatio_Annual);
			}
		}

		private void _CalculateMortgage()
		{
			double[] interestToPayAannual = new double[10];
			double beginningBalance = (double)(_Price * _LoanToValue);
			double monthlyLoanInterestRate = (double)_LoanInterestRate / 12;
			double mortgagePayment_Monthly = beginningBalance * (double)monthlyLoanInterestRate * Math.Pow((1 + monthlyLoanInterestRate), _TermsOfLoan_Months) / (Math.Pow((1 + monthlyLoanInterestRate), _TermsOfLoan_Months) - 1);
			_MortgagePayment_Annual = mortgagePayment_Monthly * 12;

			double principalPaid = 0;
			double currentBalance = beginningBalance;
			for (int i = 0; i <= 9; i++)
			{
				_MortgageInterest[i] = __CalculateMortgageInterests(currentBalance, mortgagePayment_Monthly, monthlyLoanInterestRate);
				principalPaid = _MortgagePrincipal[i] = _MortgagePayment_Annual - _MortgageInterest[i];
				currentBalance -= principalPaid;
			}
		}

		private double __CalculateMortgageInterests(double currentBalance, double mortgagePaymentMonthly, double monthlyLoanInterestRate)
		{
			double interestAnnual = 0;

			double interestMonthly = 0;
			double principalMonthly = 0;

			for (int i = 1; i <= 12; i++)
			{
				interestMonthly = currentBalance * monthlyLoanInterestRate;
				interestAnnual += interestMonthly;
				principalMonthly = mortgagePaymentMonthly - interestMonthly;

				currentBalance -= principalMonthly;
			}

			return interestAnnual;
		}

		private void _CalculateCashflowAfterTax()
		{
			__CalculateDepreciation();
			
			for (int i = 0; i <= 9; i++)
			{
				CashflowAfterTax[i] = _NetOperationIncome[i] - _MortgagePayment_Annual - 
									  (_NetOperationIncome[i] - _MortgageInterest[i] - _Depreciation_Annual) * _MarginalTaxRate; // taxable income
			}
			CashflowAfterTax[0] -= 2500; //first year improvement
		}

		private void __CalculateDepreciation()
		{
			_Depreciation_Annual = _Price * _DepreciationRatio / (double)_YearsOfDepreciation +
								  _FirstYearImprovements / (double)_YearsOfDepreciation;
		}

		private void _CalculateReturnRateOnInitialCash()
		{
			InitialCash = _FirstYearImprovements + (int)(_Price * (1 - _LoanToValue));

			double[] appreciatedFMV = new double[10];
			double[] appreciation = new double[10];

			appreciatedFMV[0] = _FMV * (1 + EstimatedAnnualAppreciationRate);
			appreciation[0] = appreciatedFMV[0] - _Price;
 
			double totalEarning = 0;
			for (int i = 0; i <= 9; i++)
			{
				if (i > 0)
				{
					appreciatedFMV[i] = appreciatedFMV[i - 1] * (1 + EstimatedAnnualAppreciationRate);
					appreciation[i] = appreciatedFMV[i] - appreciatedFMV[i - 1];
				}
				totalEarning = CashflowAfterTax[i] + _MortgagePrincipal[i] + appreciation[i];

				ReturnRateOnInitialCash[i] = totalEarning / InitialCash;
			}
			
		}

		private void _RecordResults()
		{
			var propertyEvaluation = _Property.Property_Evaluation;
			if (propertyEvaluation == null)
			{
				propertyEvaluation = new Property_Evaluation();
				_Property.Property_Evaluation = propertyEvaluation;
			}
			propertyEvaluation.initial_cash = InitialCash;
			propertyEvaluation.appreciation_rate = (decimal)EstimatedAnnualAppreciationRate;
			propertyEvaluation.vacancy_ratio_first_year = (decimal)FirstYearVacancy;
			propertyEvaluation.vacancy_ratio_subsequent_years = (decimal)SubsequentYearVacancy;
			propertyEvaluation.price = _Price;
			propertyEvaluation.rental = _Rental_Annual / 12;
			propertyEvaluation.mortgage_monthly = (int)(_MortgagePayment_Annual / 12d);

			propertyEvaluation.cash_flow_y1 = (int)CashflowAfterTax[0];
			propertyEvaluation.cash_flow_y2 = (int)CashflowAfterTax[1];
			propertyEvaluation.cash_flow_y3 = (int)CashflowAfterTax[2];
			propertyEvaluation.cash_flow_y4 = (int)CashflowAfterTax[3];
			propertyEvaluation.cash_flow_y5 = (int)CashflowAfterTax[4];
			propertyEvaluation.cash_flow_y6 = (int)CashflowAfterTax[5];
			propertyEvaluation.cash_flow_y7 = (int)CashflowAfterTax[6];
			propertyEvaluation.cash_flow_y8 = (int)CashflowAfterTax[7];
			propertyEvaluation.cash_flow_y9 = (int)CashflowAfterTax[8];
			propertyEvaluation.cash_flow_y10 = (int)CashflowAfterTax[9];

			propertyEvaluation.return_rate_y1 = (decimal)ReturnRateOnInitialCash[0];
			propertyEvaluation.return_rate_y2 = (decimal)ReturnRateOnInitialCash[1];
			propertyEvaluation.return_rate_y3 = (decimal)ReturnRateOnInitialCash[2];
			propertyEvaluation.return_rate_y4 = (decimal)ReturnRateOnInitialCash[3];
			propertyEvaluation.return_rate_y5 = (decimal)ReturnRateOnInitialCash[4];
			propertyEvaluation.return_rate_y6 = (decimal)ReturnRateOnInitialCash[5];
			propertyEvaluation.return_rate_y7 = (decimal)ReturnRateOnInitialCash[6];
			propertyEvaluation.return_rate_y8 = (decimal)ReturnRateOnInitialCash[7];
			propertyEvaluation.return_rate_y9 = (decimal)ReturnRateOnInitialCash[8];
			propertyEvaluation.return_rate_y10 = (decimal)ReturnRateOnInitialCash[9];

			_Property.Property_Sale.cash_flow_mean = (int)(CashflowAfterTax.Sum() - CashflowAfterTax[0])/ 9;
			_Property.Property_Sale.return_rate_mean = (decimal)ReturnRateOnInitialCash.Average();
			_Property.Property_Sale.score = _Property.Property_Sale.cash_flow_mean / 1200 + _Property.Property_Sale.return_rate_mean / 0.4m;

			var changeHistory = new Property_Change_History()
			{
				change_type = ChangeType.Evaluation,
				updated_date = DateTime.Now,
				updated_by = 2
			};
			_Property.Property_Change_Histories.Add(changeHistory);
			_Property.Last_Property_Change = changeHistory;
		}
	}
}

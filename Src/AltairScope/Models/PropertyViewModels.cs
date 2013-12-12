using AltairScope.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AltairScope.Models
{
	public class PropertyViewModel
	{
		public Guid Id { set; get; }
		public string DecisionStatus { set; get; }
		public string Address { set; get; }
		public string Neighbourhood { set; get; }
		public string SaleType { set; get; }
		public decimal Score { set; get; }
		public string Availability { set; get; }
		public string Url_Redfin { set; get; }
		public string Url_Zillow { set; get; }
		public int Bed { set; get; }
		public decimal Tax { set; get; }
		public int Price { set; get; }
		public int Fmv_Mean { set; get; }
		public int Price_Fmv_Diff { set; get; }
		public int Hoa { set; get; }
		public int Rent_Mean { set; get; }
		public decimal Return_Rate_Mean { set; get; }
		public int Cash_Flow_Mean { set; get; }
	}

	public class NewPropertyViewModel
	{
		[Required]
		[Display(Name = "Address", Order = 1)]
		public string Address { set; get; }

		[Required]
		[Display(Name = "Neighbourhood", Order = 2)]
		public string Neighbourhood { set; get; }

		[Required]
		[Display(Name = "Type", Order = 3)]
		public SaleType SaleType { set; get; }

		[Required]
		[Display(Name = "Availability", Order = 4)]
		public AvailabilityType Availability { set; get; }

		[Display(Name = "Url Redfin", Order = 5)]
		public string Url_Redfin { set; get; }

		[Display(Name = "Url Zillow", Order = 6)]
		public string Url_Zillow { set; get; }

		[Display(Name = "Url Ziprealty", Order = 7)]
		public string Url_Ziprealty { set; get; }

		[Required]
		[Display(Name = "Bed", Order = 8)]
		public int Bed { set; get; }

		[Display(Name = "Square ft", Order = 9)]
		public int? SquareFoot { set; get; }

		[Required]
		[Display(Name = "List Price", Order = 10)]
		public int Price { set; get; }

		[Display(Name = "HOA", Order = 11)]
		public int? Hoa { set; get; }

		[Display(Name = "Previous Tax", Order = 12)]
		public int? TaxableTax { set; get; }

		[Display(Name = "Previous Additions", Order = 13)]
		public int? TaxableAdditions { set; get; }

		[Display(Name = "Previous Total Value", Order = 14)]
		public int? TaxableTotal { set; get; }

		[Display(Name = "FMV Zestimate", Order = 15)]
		public int? FMV_Zestimate { set; get; }

		[Display(Name = "FMV Smartzip", Order = 16)]
		public int? FMV_Smartzip { set; get; }

		[Display(Name = "FMV Eappraisal", Order = 17)]
		public int? FMV_Eappraisal { set; get; }

		[Display(Name = "Zrent", Order = 18)]
		public int? Rental_Zrent { set; get; }

		[Display(Name = "Rentometer", Order = 19)]
		public int? Rental_Rentometer { set; get; }

		[Display(Name = "Zilpy", Order = 20)]
		public int? Rental_Zilpy { set; get; }

		[Display(Name = "Zillow Growth Rate", Order = 21)]
		//[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? ZillowGrowthRate { set; get; }
	}

	public class EditPropertyViewModel : NewPropertyViewModel
	{
		[Required]
		[Display(Name = "Id", Order = 0)]
		public Guid Id { set; get; }
	}

	public class ViewablePropertyViewModel : EditPropertyViewModel
	{
		[Display(Name = "Neighbourhood Url", Order = 0)]
		public string NeighbourhoodUrl { set; get; }

		[Display(Name = "Offer Price", Order = 0)]
		public int? OfferPrice { set; get; }

		[Display(Name = "Offer Date", Order = 0)]
		public DateTime? OfferDate { set; get; }

		[Display(Name = "Tax Rate", Order = 1)]
		[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? TaxRate { set; get; }

		[Display(Name = "Actual Tax", Order = 2)]
		public int? Tax { set; get; }

		[Display(Name = "Building %", Order = 3)]
		[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? BuildingRatio { set; get; }

		[Display(Name = "FMV Mean", Order = 4)]
		public int? FMV_Mean { set; get; }

		[Display(Name = "Rental Mean", Order = 5)]
		public int? Rental_Mean { set; get; }

		[Display(Name = "Return Rate Mean", Order = 6)]
		[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? ReturnRate_Mean { set; get; }

		[Display(Name = "Cash Flow Mean", Order = 7)]
		public int? CashFlow_Mean { set; get; }

		[Display(Name = "Score", Order = 8)]
		public decimal? Score { set; get; }

		[Display(Name = "Initial Cash", Order = 9)]
		public int? InitialCash { set; get; }

		[Display(Name = "First Year Vacancy", Order = 10)]
		[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? VacancyFirstYear { set; get; }

		[Display(Name = "Subsequent Year Vacancy", Order = 12)]
		[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? VacancySubsequentYear { set; get; }

		[Display(Name = "Estimated Appreciation Rate", Order = 13)]
		[DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
		public decimal? EstimatedAppreciationRate { set; get; }

		public List<decimal?> ReturnRateList { set; get; }

		public List<int?> CashFlowList { set; get; }

		[Display(Name = "Decision", Order = 14)]
		public DecisionStatusType DecisionStatus { set; get; }

		[Display(Name = "Evaluate Price", Order = 15)]
		public int? EvaluatePrice { set; get; }

		[Display(Name = "Evaluate Rental", Order = 16)]
		public int? EvaluateRental { set; get; }

		[Display(Name = "Mortgage Monthly", Order = 16)]
		public int? MortgageMonthly { set; get; }
	}

	public class ChangeStatusPropertyViewModel
	{
		[Required]
		[Display(Name = "Id", Order = 0)]
		public Guid Id { set; get; }

		[Display(Name = "Address", Order = 1)]
		public string Address { set; get; }

		[Display(Name = "List Price", Order = 2)]
		public int Price { set; get; }

		[Display(Name = "Availability", Order = 3)]
		public AvailabilityType Availability { set; get; }

		[Display(Name = "Status", Order = 4)]
		public DecisionStatusType DecisionStatus { set; get; }

		[Display(Name = "Offer Price", Order = 5)]
		public int? OfferPrice { set; get; }

		[Display(Name = "Url Redfin", Order = 1)]
		public string Url_Redfin { set; get; }
	}
}
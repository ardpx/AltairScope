using AltairScope.DomainModels;
using AltairScope.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace AltairScope.Services
{
	public class PropertyViewModelServices
	{
		public List<PropertyViewModel> ConvertToViewModel(List<Property> propertyList)
		{
			var propertyViewModelList = new List<PropertyViewModel>();
			if (propertyList != null)
			{
				foreach (var property in propertyList)
				{
					var propertyViewModel = new PropertyViewModel();

					propertyViewModel.Id = property.id;
					propertyViewModel.DecisionStatus = property.Property_Sale.status.Value.ToString().Replace("_", " ");
					propertyViewModel.Address = property.address;
					propertyViewModel.SaleType = property.Property_Sale.type.ToString().Replace("_", " ");
					propertyViewModel.Score = property.Property_Sale.score ?? 0;
					propertyViewModel.Availability = property.Property_Sale.availability.ToString().Replace("_", " ");
					propertyViewModel.Url_Redfin = property.Property_Sale.url_redfin;
					propertyViewModel.Url_Zillow = property.Property_Sale.url_zillow;
					propertyViewModel.Bed = property.bed.Value;
					propertyViewModel.Tax = property.Property_Sale.tax;
					propertyViewModel.Price = property.Property_Sale.price;
					propertyViewModel.OfferPrice = property.Property_Sale.offer_price ?? 0;
					propertyViewModel.Fmv_Mean = property.fmv_mean.Value;
					propertyViewModel.Price_Fmv_Diff = property.Property_Sale.list_fmv_diff.Value;
					propertyViewModel.Hoa = property.hoa;
					propertyViewModel.Rent_Mean = property.rental_mean.Value;
					propertyViewModel.Return_Rate_Mean = property.Property_Sale.return_rate_mean?? 0;
					propertyViewModel.Cash_Flow_Mean = property.Property_Sale.cash_flow_mean ?? 0;
					if (property.Neighbourhood != null)
						propertyViewModel.Neighbourhood = property.Neighbourhood.name;
					propertyViewModel.Remark = property.Property_Sale.remark;
					if (property.Property_Sale.listing_date != null)
					{
						propertyViewModel.ListingDaysCount = (int)DateTime.Today.Subtract(property.Property_Sale.listing_date.Value).TotalDays;
					}

					propertyViewModelList.Add(propertyViewModel);
				}
			}
			return propertyViewModelList;
		}

		public Property ConvertToDomainModel(NewPropertyViewModel newPropertyViewModel)
		{
			var property = new Property();
			var propertySale = new Property_Sale();
			property.Property_Sale = propertySale;

			UpdateDomainModel(property, newPropertyViewModel);

			return property;
		}

		public EditPropertyViewModel ConvertToEditViewModel(Property property)
		{
			var editPropertyViewModel = new EditPropertyViewModel();

			editPropertyViewModel.Id = property.id;
			editPropertyViewModel.Address = property.address;
			editPropertyViewModel.Neighbourhood = property.neighbourhood_id != null ? property.neighbourhood_id.ToString() : null;

			editPropertyViewModel.SaleType = property.Property_Sale.type;
			editPropertyViewModel.Availability = property.Property_Sale.availability;

			editPropertyViewModel.Url_Redfin = property.Property_Sale.url_redfin;
			editPropertyViewModel.Url_Zillow = property.Property_Sale.url_zillow;
			editPropertyViewModel.Url_Ziprealty = property.Property_Sale.url_ziprealty;
			editPropertyViewModel.Bed = property.bed.Value;
			editPropertyViewModel.SquareFoot = property.square_foot;

			editPropertyViewModel.Price = property.Property_Sale.price;
			editPropertyViewModel.Hoa = property.hoa;

			editPropertyViewModel.TaxableTax = property.taxable_tax;
			editPropertyViewModel.TaxableAdditions = property.taxable_additions;
			editPropertyViewModel.TaxableTotal = property.taxable_total;

			editPropertyViewModel.FMV_Zestimate = property.fmv_zestimate;
			editPropertyViewModel.FMV_Smartzip = property.fmv_smartzip;
			editPropertyViewModel.FMV_Eappraisal = property.fmv_eappraisal;
			editPropertyViewModel.FMV_Homeseeker = property.fmv_homeseeker;

			editPropertyViewModel.Rental_Zrent = property.rental_zrent;
			editPropertyViewModel.Rental_Rentometer = property.rental_rentometer;
			editPropertyViewModel.Rental_Zilpy = property.rental_zilpy;

			editPropertyViewModel.ZillowGrowthRate = property.zillow_growth_rate;

			if (property.Property_Sale.listing_date != null)
			{
				editPropertyViewModel.ListingDaysCount = (int)DateTime.Today.Subtract(property.Property_Sale.listing_date.Value).TotalDays;
			}

			return editPropertyViewModel;
		}

		public void UpdateDomainModel(Property property, NewPropertyViewModel editPropertyViewModel)
		{
			var propertySale = property.Property_Sale;

			property.address = editPropertyViewModel.Address.Trim();
			property.neighbourhood_id = new Guid(editPropertyViewModel.Neighbourhood);
			propertySale.type = editPropertyViewModel.SaleType;
			propertySale.availability = editPropertyViewModel.Availability;

			propertySale.url_redfin = editPropertyViewModel.Url_Redfin;
			propertySale.url_zillow = editPropertyViewModel.Url_Zillow;
			propertySale.url_ziprealty = editPropertyViewModel.Url_Ziprealty;

			property.bed = editPropertyViewModel.Bed;
			property.square_foot = editPropertyViewModel.SquareFoot;
			propertySale.price = editPropertyViewModel.Price;
			property.hoa = editPropertyViewModel.Hoa ?? 0;

			property.taxable_tax = editPropertyViewModel.TaxableTax;
			property.taxable_additions = editPropertyViewModel.TaxableAdditions;
			property.taxable_total = editPropertyViewModel.TaxableTotal;

			property.fmv_zestimate = editPropertyViewModel.FMV_Zestimate;
			property.fmv_smartzip = editPropertyViewModel.FMV_Smartzip;
			property.fmv_eappraisal = editPropertyViewModel.FMV_Eappraisal;
			property.fmv_homeseeker = editPropertyViewModel.FMV_Homeseeker;

			property.rental_zrent = editPropertyViewModel.Rental_Zrent;
			property.rental_rentometer = editPropertyViewModel.Rental_Rentometer;
			property.rental_zilpy = editPropertyViewModel.Rental_Zilpy;

			property.zillow_growth_rate = editPropertyViewModel.ZillowGrowthRate;

			if (editPropertyViewModel.ListingDaysCount != 0)
			{
				propertySale.listing_date = DateTime.Today.AddDays(0 - editPropertyViewModel.ListingDaysCount);
			}
		}

		public ViewablePropertyViewModel ConvertToViewableViewModel(Property property)
		{
			var viewablePropertyViewModel = new ViewablePropertyViewModel();

			viewablePropertyViewModel.Id = property.id;
			if (property.Neighbourhood != null)
			{
				viewablePropertyViewModel.Neighbourhood = property.Neighbourhood.name;
				viewablePropertyViewModel.NeighbourhoodVacancy = property.Neighbourhood.vacancy_ratio;
				viewablePropertyViewModel.NeighbourhoodUrl = property.Neighbourhood.url;
				viewablePropertyViewModel.NeighbourhoodIncomeScore = property.Neighbourhood.score_all;
				viewablePropertyViewModel.NeighbourhoodId = property.Neighbourhood.id;
				
			}
			viewablePropertyViewModel.Address = property.address;
			viewablePropertyViewModel.SaleType = property.Property_Sale.type;
			viewablePropertyViewModel.Availability = property.Property_Sale.availability;

			viewablePropertyViewModel.Url_Redfin = property.Property_Sale.url_redfin;
			viewablePropertyViewModel.Url_Zillow = property.Property_Sale.url_zillow;
			viewablePropertyViewModel.Url_Ziprealty = property.Property_Sale.url_ziprealty;
			viewablePropertyViewModel.Bed = property.bed.Value;
			viewablePropertyViewModel.SquareFoot = property.square_foot;
			
			viewablePropertyViewModel.Price = property.Property_Sale.price;
			viewablePropertyViewModel.TaxRate = property.tax_rate;
			viewablePropertyViewModel.Tax = property.Property_Sale.tax;
			viewablePropertyViewModel.Hoa = property.hoa;

			viewablePropertyViewModel.TaxableAdditions = property.taxable_additions;
			viewablePropertyViewModel.TaxableTax = property.taxable_tax;
			viewablePropertyViewModel.TaxableTotal = property.taxable_total;
			viewablePropertyViewModel.BuildingRatio = property.addition_total_ratio;

			viewablePropertyViewModel.FMV_Zestimate = property.fmv_zestimate;
			viewablePropertyViewModel.FMV_Smartzip = property.fmv_smartzip;
			viewablePropertyViewModel.FMV_Eappraisal = property.fmv_eappraisal;
			viewablePropertyViewModel.FMV_Homeseeker = property.fmv_homeseeker;
			viewablePropertyViewModel.FMV_Mean = property.fmv_mean;

			viewablePropertyViewModel.Rental_Zrent = property.rental_zrent;
			viewablePropertyViewModel.Rental_Rentometer = property.rental_rentometer;
			viewablePropertyViewModel.Rental_Zilpy = property.rental_zilpy;
			viewablePropertyViewModel.Rental_Mean = property.rental_mean;

			viewablePropertyViewModel.ZillowGrowthRate = property.zillow_growth_rate;

			viewablePropertyViewModel.OfferPrice = property.Property_Sale.offer_price;
			viewablePropertyViewModel.OfferDate = property.Property_Sale.offer_date;

			MapDomainModelToViewableViewModelForEvaluation(property, viewablePropertyViewModel);

			viewablePropertyViewModel.DecisionStatus = property.Property_Sale.status ?? DecisionStatusType.NOT_DECIDED;
			viewablePropertyViewModel.Remark = property.Property_Sale.remark;

			if (property.Property_Sale.listing_date != null)
			{
				viewablePropertyViewModel.ListingDaysCount = (int)DateTime.Today.Subtract(property.Property_Sale.listing_date.Value).TotalDays;
			}

			return viewablePropertyViewModel;
		}

		public ViewablePropertyViewModel ConvertToViewableViewModelForEvaluation(Property property)
		{
			var viewablePropertyViewModelForEvaluation = new ViewablePropertyViewModel();
			MapDomainModelToViewableViewModelForEvaluation(property, viewablePropertyViewModelForEvaluation);
			return viewablePropertyViewModelForEvaluation;
		}

		private void MapDomainModelToViewableViewModelForEvaluation(Property property, ViewablePropertyViewModel viewablePropertyViewModel)
		{
			viewablePropertyViewModel.Price = property.Property_Sale.price;
			viewablePropertyViewModel.InitialCash = property.Property_Evaluation.initial_cash;
			viewablePropertyViewModel.EstimatedAppreciationRate = property.Property_Evaluation.appreciation_rate;
			viewablePropertyViewModel.VacancyFirstYear = property.Property_Evaluation.vacancy_ratio_first_year;
			viewablePropertyViewModel.VacancySubsequentYear = property.Property_Evaluation.vacancy_ratio_subsequent_years;

			viewablePropertyViewModel.Score = property.Property_Sale.score;
			viewablePropertyViewModel.ReturnRate_Mean = property.Property_Sale.return_rate_mean;
			viewablePropertyViewModel.CashFlow_Mean = property.Property_Sale.cash_flow_mean;
			
			viewablePropertyViewModel.EvaluatePrice = property.Property_Evaluation.price;
			viewablePropertyViewModel.EvaluateRental = property.Property_Evaluation.rental;
			viewablePropertyViewModel.MortgageMonthly = property.Property_Evaluation.mortgage_monthly;
			viewablePropertyViewModel.EvaluateLoanRate = property.Property_Evaluation.loan_rate;
			
			viewablePropertyViewModel.ReturnRateList = new List<decimal?>();
			viewablePropertyViewModel.CashFlowList = new List<int?>();
			var propertyEvaluation = property.Property_Evaluation;
			if (propertyEvaluation != null)
			{
				var returnRateList = new List<decimal?>();
				returnRateList.Add(propertyEvaluation.return_rate_y1);
				returnRateList.Add(propertyEvaluation.return_rate_y2);
				returnRateList.Add(propertyEvaluation.return_rate_y3);
				returnRateList.Add(propertyEvaluation.return_rate_y4);
				returnRateList.Add(propertyEvaluation.return_rate_y5);
				returnRateList.Add(propertyEvaluation.return_rate_y6);
				returnRateList.Add(propertyEvaluation.return_rate_y7);
				returnRateList.Add(propertyEvaluation.return_rate_y8);
				returnRateList.Add(propertyEvaluation.return_rate_y9);
				returnRateList.Add(propertyEvaluation.return_rate_y10);
				viewablePropertyViewModel.ReturnRateList = returnRateList;

				var cashFlowList = new List<int?>();
				cashFlowList.Add(propertyEvaluation.cash_flow_y1);
				cashFlowList.Add(propertyEvaluation.cash_flow_y2);
				cashFlowList.Add(propertyEvaluation.cash_flow_y3);
				cashFlowList.Add(propertyEvaluation.cash_flow_y4);
				cashFlowList.Add(propertyEvaluation.cash_flow_y5);
				cashFlowList.Add(propertyEvaluation.cash_flow_y6);
				cashFlowList.Add(propertyEvaluation.cash_flow_y7);
				cashFlowList.Add(propertyEvaluation.cash_flow_y8);
				cashFlowList.Add(propertyEvaluation.cash_flow_y9);
				cashFlowList.Add(propertyEvaluation.cash_flow_y10);
				viewablePropertyViewModel.CashFlowList = cashFlowList;
			}
		}

		public ChangeStatusPropertyViewModel ConvertToChangeStatusPropertyViewModel(Property property)
		{
			var changeStatusPropertyViewModel = new ChangeStatusPropertyViewModel()
			{
				Id = property.id,
				Address = property.address,
				Price = property.Property_Sale.price,
				Availability = property.Property_Sale.availability,
				DecisionStatus = property.Property_Sale.status.Value,
				OfferPrice = property.Property_Sale.offer_price,
				Url_Redfin = property.Property_Sale.url_redfin,
				Remark = property.Property_Sale.remark
			};
			return changeStatusPropertyViewModel;
		}

		public PropertyTrendViewModel ConvertToTrendViewModel(Property property)
		{
			List<Property_FMV_Tracking> fmvTrend = property.Property_FMV_Tracking.OrderBy(ft => ft.create_date).ToList();
			List<Property_Rental_Tracking> rentalTrend = property.Property_Rental_Tracking.OrderBy(ft => ft.create_date).ToList();
			
			if(fmvTrend == null)
				return new PropertyTrendViewModel();

			string[] label = null;
			int?[] fmvMeanArray = null;
			int?[] rentalMeanArray = null;

			if (fmvTrend.Count == 1)
			{
				label = new string[1];
				fmvMeanArray = new int?[1];
				rentalMeanArray = new int?[1];

				label[0] = fmvTrend.First().create_date.ToShortDateString();
				fmvMeanArray[0] = fmvTrend.First().fmv_mean;
				rentalMeanArray[0] = rentalTrend.First().rental_mean;
			}
			else
			{
				var startDate = fmvTrend.First().create_date;
				var propertyFMVTrendDataList = fmvTrend.Select(ft => new
				{
					FMV = ft,
					Day = ft.create_date.Subtract(startDate).Days
				}).ToList();
				int labelCount = propertyFMVTrendDataList.Last().Day + 1;

				label = new string[labelCount];
				fmvMeanArray = new int?[labelCount];
				rentalMeanArray = new int?[labelCount];

				int startIndex = 0;
				label[startIndex] = fmvTrend.First().create_date.ToShortDateString();
				fmvMeanArray[startIndex] = propertyFMVTrendDataList.First().FMV.fmv_mean;
				rentalMeanArray[startIndex] = rentalTrend.First().rental_mean;

				for (int i = 1; i < propertyFMVTrendDataList.Count; i++)
				{
					for (int j = startIndex + 1; j < propertyFMVTrendDataList[i].Day; j++)
					{
						label[j] = "";
						fmvMeanArray[j] = fmvMeanArray[startIndex];
						rentalMeanArray[j] = rentalMeanArray[startIndex];
					}

					startIndex = propertyFMVTrendDataList[i].Day;
					label[startIndex] = propertyFMVTrendDataList[i].FMV.create_date.ToShortDateString();
					fmvMeanArray[startIndex] = propertyFMVTrendDataList[i].FMV.fmv_mean;
					rentalMeanArray[startIndex] = rentalTrend[i].rental_mean;
				}
				
			}
			var jsSerializer = new JavaScriptSerializer();
			var trendViewMode = new PropertyTrendViewModel()
			{
				Label =  new HtmlString(jsSerializer.Serialize(label)),
				FMVMeanTrend = new HtmlString(jsSerializer.Serialize(fmvMeanArray)),
				RentalMeanTrend = new HtmlString(jsSerializer.Serialize(rentalMeanArray)),
			};

			return trendViewMode;
			
		}
	}
}
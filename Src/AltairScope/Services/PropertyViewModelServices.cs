using AltairScope.DomainModels;
using AltairScope.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
					propertyViewModel.Score = property.Property_Sale.score.Value;
					propertyViewModel.Availability = property.Property_Sale.availability.ToString().Replace("_", " ");
					propertyViewModel.Url_Redfin = property.Property_Sale.url_redfin;
					propertyViewModel.Url_Zillow = property.Property_Sale.url_zillow;
					propertyViewModel.Bed = property.bed.Value;
					propertyViewModel.Tax = property.Property_Sale.tax;
					propertyViewModel.Price = property.Property_Sale.price;
					propertyViewModel.Fmv_Mean = property.fmv_mean.Value;
					propertyViewModel.Price_Fmv_Diff = property.Property_Sale.list_fmv_diff.Value;
					propertyViewModel.Hoa = property.hoa;
					propertyViewModel.Rent_Mean = property.rental_mean.Value;
					propertyViewModel.Return_Rate_Mean = property.Property_Sale.return_rate_mean.Value;
					propertyViewModel.Cash_Flow_Mean = property.Property_Sale.cash_flow_mean.Value;
					if (property.Neighbourhood != null)
						propertyViewModel.Neighbourhood = property.Neighbourhood.name;

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

			editPropertyViewModel.FMV_Zestimate = property.fmv_zestimate;
			editPropertyViewModel.FMV_Smartzip = property.fmv_smartzip;
			editPropertyViewModel.FMV_Eappraisal = property.fmv_eappraisal;

			editPropertyViewModel.Rental_Zrent = property.rental_zrent;
			editPropertyViewModel.Rental_Rentometer = property.rental_rentometer;
			editPropertyViewModel.Rental_Zilpy = property.rental_zilpy;

			editPropertyViewModel.ZillowGrowthRate = property.zillow_growth_rate;

			return editPropertyViewModel;
		}

		public void UpdateDomainModel(Property property, NewPropertyViewModel editPropertyViewModel)
		{
			var propertySale = property.Property_Sale;

			property.address = editPropertyViewModel.Address;
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

			property.rental_zrent = editPropertyViewModel.Rental_Zrent;
			property.rental_rentometer = editPropertyViewModel.Rental_Rentometer;
			property.rental_zilpy = editPropertyViewModel.Rental_Zilpy;

			property.zillow_growth_rate = editPropertyViewModel.ZillowGrowthRate;
		}

		public ViewablePropertyViewModel ConvertToViewableViewModel(Property property)
		{
			var viewablePropertyViewModel = new ViewablePropertyViewModel();

			viewablePropertyViewModel.Id = property.id;
			viewablePropertyViewModel.Neighbourhood = property.Neighbourhood != null ? property.Neighbourhood.name : null;
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
			viewablePropertyViewModel.InitialCash = property.Property_Evaluation.initial_cash;
			viewablePropertyViewModel.EstimatedAppreciationRate = property.Property_Evaluation.appreciation_rate;
			viewablePropertyViewModel.VacancyFirstYear = property.Property_Evaluation.vacancy_ratio_first_year;
			viewablePropertyViewModel.VacancySubsequentYear = property.Property_Evaluation.vacancy_ratio_subsequent_years;

			viewablePropertyViewModel.Score = property.Property_Sale.score;
			viewablePropertyViewModel.ReturnRate_Mean = property.Property_Sale.return_rate_mean;
			viewablePropertyViewModel.CashFlow_Mean = property.Property_Sale.cash_flow_mean;


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
			};
			return changeStatusPropertyViewModel;
		}
	}
}
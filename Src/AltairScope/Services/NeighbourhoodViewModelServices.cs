using AltairScope.DomainModels;
using AltairScope.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltairScope.Services
{
	public class NeighbourhoodViewModelServices
	{
		public Neighbourhood ConvertToDomainModel(NewNeighbourhoodViewModel newNeighbourhoodViewModel)
		{
			var neighbourhood = new Neighbourhood();

			UpdateDomainModel(neighbourhood, newNeighbourhoodViewModel);

			return neighbourhood;
		}

		public void UpdateDomainModel(Neighbourhood neighbourhood, NewNeighbourhoodViewModel editNeighbourhoodViewModel)
		{
			neighbourhood.name = editNeighbourhoodViewModel.Name;
			neighbourhood.median_price = editNeighbourhoodViewModel.MedianPrice;
			neighbourhood.rental = editNeighbourhoodViewModel.Rental;
			neighbourhood.income = editNeighbourhoodViewModel.Income;
			neighbourhood.url = editNeighbourhoodViewModel.Url;
			neighbourhood.education = editNeighbourhoodViewModel.Education;
			neighbourhood.educational_rating = editNeighbourhoodViewModel.EducationalRating;
			neighbourhood.appreciation_1990 = editNeighbourhoodViewModel.Appreciation_1990;
			neighbourhood.appreciation_10y = editNeighbourhoodViewModel.Appreciation_10Y;
			neighbourhood.appreciation_5y = editNeighbourhoodViewModel.Appreciation_5Y;
			neighbourhood.appreciation_2y = editNeighbourhoodViewModel.Appreciation_2Y;
			neighbourhood.appreciation_1y = editNeighbourhoodViewModel.Appreciation_1Y;
			neighbourhood.appreciation_1q = editNeighbourhoodViewModel.Appreciation_1Q;
			neighbourhood.crime_index = editNeighbourhoodViewModel.CrimeIndex;
			neighbourhood.vacancy_ratio = editNeighbourhoodViewModel.VacancyRatio;
		}

		public ViewableNeighbourhoodViewModel ConvertToViewableModel(Neighbourhood neighbourhood)
		{
			var viewableNeighbourhoodViewModel = new ViewableNeighbourhoodViewModel()
			{
				Id = neighbourhood.id,
				Url = neighbourhood.url,
				Name = neighbourhood.name,
				MedianPrice = neighbourhood.median_price,
				Rental = neighbourhood.rental,
				Income = neighbourhood.income ?? 0,
				CrimeIndex = neighbourhood.crime_index,
				VacancyRatio = neighbourhood.vacancy_ratio,
				Appreciation_Mean = neighbourhood.appreciation_mean ?? 0,
				Appreciation_1990 = neighbourhood.appreciation_1990,
				Appreciation_10Y = neighbourhood.appreciation_10y,
				Appreciation_5Y = neighbourhood.appreciation_5y,
				Appreciation_2Y = neighbourhood.appreciation_2y,
				Appreciation_1Y = neighbourhood.appreciation_1y,
				Appreciation_1Q = neighbourhood.appreciation_1q,
				Education = neighbourhood.education,
				EducationalRating = neighbourhood.educational_rating,
				IncomeScore = neighbourhood.score_all ?? 0,
				PRRatio = neighbourhood.p_r_rate ?? 0
			};
			return viewableNeighbourhoodViewModel;
			
		}

		public EditNeighbourhoodViewModel ConvertToEditModel(Neighbourhood neighbourhood)
		{
			var editNeighbourhoodViewModel = new EditNeighbourhoodViewModel()
			{
				Id = neighbourhood.id,
				Name = neighbourhood.name,
				Url = neighbourhood.url,
				MedianPrice = neighbourhood.median_price,
				Rental = neighbourhood.rental,
				Income = neighbourhood.income ?? 0,
				CrimeIndex = neighbourhood.crime_index,
				VacancyRatio = neighbourhood.vacancy_ratio,
				Appreciation_1990 = neighbourhood.appreciation_1990,
				Appreciation_10Y = neighbourhood.appreciation_10y,
				Appreciation_5Y = neighbourhood.appreciation_5y,
				Appreciation_2Y = neighbourhood.appreciation_2y,
				Appreciation_1Y = neighbourhood.appreciation_1y,
				Appreciation_1Q = neighbourhood.appreciation_1q,
				Education = neighbourhood.education,
				EducationalRating= neighbourhood.educational_rating
			};
			return editNeighbourhoodViewModel;
		}
	}
}
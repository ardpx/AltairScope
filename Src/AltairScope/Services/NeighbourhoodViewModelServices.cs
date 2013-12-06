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
	}
}
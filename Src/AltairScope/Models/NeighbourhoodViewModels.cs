﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AltairScope.Models
{
	public class NewNeighbourhoodViewModel
	{
		[Required]
		[Display(Name = "Address", Order = 1)]
		public string Name { set; get; }

		[Required]
		[Display(Name = "Median Price", Order = 2)]
		public int MedianPrice { set; get; }

		[Required]
		[Display(Name = "Rental", Order = 3)]
		public int Rental { set; get; }

		[Required]
		[Display(Name = "Url", Order = 3)]
		public string Url { set; get; }

		[Required]
		[Display(Name = "Income", Order = 4)]
		public int Income { set; get; }

		[Required]
		[Display(Name = "Education", Order = 4)]
		public int Education { set; get; }

		[Required]
		[Display(Name = "Educational Rating", Order = 5)]
		public int EducationalRating { set; get; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		[Display(Name = "Appreciation Since 1990", Order = 6)]
		public decimal Appreciation_1990 { set; get; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		[Display(Name = "Appreciation 10 Years", Order = 8)]
		public decimal Appreciation_10Y { set; get; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		[Display(Name = "Appreciation 5 Years", Order = 8)]
		public decimal Appreciation_5Y { set; get; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		[Display(Name = "Appreciation 2 Years", Order = 9)]
		public decimal Appreciation_2Y { set; get; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		[Display(Name = "Appreciation 1 Year", Order = 10)]
		public decimal Appreciation_1Y { set; get; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		[Display(Name = "Appreciation 1 Quater", Order = 11)]
		public decimal Appreciation_1Q { set; get; }

		[Required]
		[Display(Name = "Crime Index", Order = 12)]
		public int CrimeIndex { set; get; }

		[Required]
		[Display(Name = "Vacancy", Order = 13)]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		public decimal VacancyRatio { set; get; }
	}

	public class EditNeighbourhoodViewModel : NewNeighbourhoodViewModel
	{
		[Required]
		[Display(Name = "Id", Order = 0)]
		public Guid Id { set; get; }
	}

	public class ViewableNeighbourhoodViewModel : EditNeighbourhoodViewModel
	{
		[Display(Name = "Mean Appreciation", Order = 0)]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		public decimal Appreciation_Mean { set; get; }

		[Display(Name = "Income Score", Order = 1)]
		public decimal IncomeScore { set; get; }

		[Display(Name = "PR Ratio", Order = 2)]
		[DisplayFormat(DataFormatString = "{0:P2}")]
		public decimal PRRatio { set; get; }
	}
}
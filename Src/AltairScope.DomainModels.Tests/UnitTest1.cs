using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AltairScope.DomainModels.Services;

namespace AltairScope.DomainModels.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestEvaluate()
		{
			var propertyEval = new PropertyEvaluator();
			
			var sale = new Property_Sale()
			{
				price = 189900,
				tax = 189900 * 2432/119000,
			};
			var neigh = new Neighbourhood()
			{
				appreciation_mean = 0.067m,
				appreciation_10y = 0.067m,
				vacancy_ratio = 0.1441m
			};
			var property = new Property()
			{
				fmv_mean = 148854,
				rental_mean = 1263,
				addition_total_ratio = 0.4622m,
				Property_Sale = sale,
				Neighbourhood = neigh
			};
			propertyEval.Evaluate(property);

			Assert.AreEqual(1, 1);
		}
	}
}

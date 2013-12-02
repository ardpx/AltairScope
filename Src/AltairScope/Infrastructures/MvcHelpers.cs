using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AltairScope.Infrastructures
{
	public static class MvcHelpers
	{
		public static HtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> modelExpression, object firstElement = null)
		{
			var typeOfProperty = modelExpression.ReturnType;
			if (!typeOfProperty.IsEnum)
				throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

			var selectList = ToSelectList(typeOfProperty, firstElement);
			
			return htmlHelper.DropDownListFor(modelExpression, selectList);
		}

		private static SelectList ToSelectList(Type eunmType, object firstElement)
		{
			List<SelectListItem> enumItemList = new List<SelectListItem>();
			foreach (var i in Enum.GetValues(eunmType))
			{
				var enumItem = new SelectListItem
				{
					Value = i.ToString(),
					Text = Enum.GetName(eunmType, i)
				};
				
				enumItemList.Add(enumItem);
			}
			return new SelectList(enumItemList, "Value", "Text", firstElement);
		}
	}
}
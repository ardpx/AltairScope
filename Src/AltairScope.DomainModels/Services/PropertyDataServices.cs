using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels.Services
{
	public class PropertyDataServices
	{
		public List<Property> GetPropertyList(IAppContext appContext, PropertyEagerLoadMode eagerLoadMode = PropertyEagerLoadMode.Sale, Expression<Func<Property, bool>> where = null)
		{
			var db = appContext.GetPersistenceContext();
			DbQuery<Property> query = GetQuery(db, eagerLoadMode);
			var orderedQuery = query.OrderByDescending(q => q.Property_Change_Histories.Max(ch => ch.updated_date));
			List<Property> propertyList;
			if (where != null)
				propertyList = orderedQuery.Where(where).ToList();
			else
				propertyList = orderedQuery.ToList();
			return propertyList;
		}

		public Property GetPropertyById(IAppContext appContext, Guid id, PropertyEagerLoadMode eagerLoadMode = PropertyEagerLoadMode.Sale)
		{
			var db = appContext.GetPersistenceContext();
			DbQuery<Property> query = GetQuery(db, eagerLoadMode);
			Property propertyList = query.Where(p => p.id == id)
										 .FirstOrDefault();
			return propertyList;
		}

		private DbQuery<Property> GetQuery(AltairScopeEntities db, PropertyEagerLoadMode eagerLoadMode)
		{
			DbQuery<Property> query = db.Properties;
			switch (eagerLoadMode)
			{
				case PropertyEagerLoadMode.None: break;
				case PropertyEagerLoadMode.Sale:
					query = query.Include("Property_Sale");
					break;
				case PropertyEagerLoadMode.Sale_Neighbourhood:
					query = query.Include("Property_Sale").Include("Neighbourhood");
					break;
				case PropertyEagerLoadMode.Sale_Evaluation_Neighbourhood:
					query = query.Include("Property_Sale").Include("Neighbourhood").Include("Property_Evaluation");
					break;
			}
			return query;
		}

		public bool GetPropertyByAddress(IAppContext appContext, string address)
		{
			var db = appContext.GetPersistenceContext();
			var query = GetQuery(db, PropertyEagerLoadMode.None).Where(p => p.address == address);
			return query.Any();
		}
	}

	public enum PropertyEagerLoadMode
	{
		None = 0,
		Sale,
		Sale_Neighbourhood,
		Sale_Evaluation_Neighbourhood
	}
}

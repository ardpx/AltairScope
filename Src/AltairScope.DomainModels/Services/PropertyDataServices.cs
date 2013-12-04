using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels.Services
{
	public class PropertyDataServices
	{
		public List<Property> GetPropertyList(IAppContext appContext, PropertyEagerLoadMode eagerLoadMode = PropertyEagerLoadMode.Sale)
		{
			var db = appContext.GetPersistenceContext();
			DbQuery<Property> query = GetQuery(db, eagerLoadMode);
			List<Property> propertyList = query.OrderByDescending(q => q.last_update_id).ToList();
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
	}

	public enum PropertyEagerLoadMode
	{
		None = 0,
		Sale,
		Sale_Neighbourhood,
		Sale_Evaluation_Neighbourhood
	}
}

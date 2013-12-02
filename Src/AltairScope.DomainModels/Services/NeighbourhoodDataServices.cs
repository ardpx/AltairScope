using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels.Services
{
	public class NeighbourhoodDataServices
	{
		public Guid TryGetIdByName(IAppContext appContext, string name)
		{
			var db = appContext.GetPersistenceContext();
			var id = db.Neighbourhoods.Where(n => n.name == name)
						  .Select(n => n.id)
						  .FirstOrDefault();
			return id;
		}
	}
}

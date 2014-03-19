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

		public List<Neighbourhood> GetAll(IAppContext appContext)
		{
			var db = appContext.GetPersistenceContext();
			var neighbbourhoodList = db.Neighbourhoods.ToList();
			return neighbbourhoodList;
		}

		public Neighbourhood GetNeighbourhoodById(IAppContext appContext, Guid id)
		{
			var db = appContext.GetPersistenceContext();
			var neighbbourhood = db.Neighbourhoods.Where(n => n.id == id).FirstOrDefault();
			return neighbbourhood;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels.Services
{
	public class UserDataServices
	{
		public User GetUserByName(IAppContext appContext, string name)
		{
			var db = appContext.GetPersistenceContext();
			User user = db.Users.Where(u => u.name == name).FirstOrDefault();
			return user;
		}
		
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels
{
	public interface IAppContext : IUnitOfWork, IDisposable
	{
		/// <summary>
		/// Return the persistent context of Entities models. If it does not exist, create new one
		/// </summary>
		AltairScopeEntities GetPersistenceContext();

		/// <summary>
		/// Initialize and create new persistence context
		/// </summary>
		void InitializePersistenceContext();

		/// <summary>
		/// Terminiate the persistence context and choose whether to save current changes or not
		/// </summary>
		void TerminatePersistenceContext(bool saveChanges);

		void ClearPersistenceContext();

		//void Dispose();
	}
}

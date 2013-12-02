using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltairScope.DomainModels
{
	public interface IUnitOfWork
	{
		/// <summary>
		/// Flushes content of the context to the underlying data storage.
		/// </summary>
		void Commit();
	}
}

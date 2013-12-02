using AltairScope.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltairScope.Infrastructures
{
	public class WebAppContext : IAppContext
	{
		private AltairScopeEntities _entityContext = null;

		public WebAppContext()
		{
		}

		public WebAppContext(AltairScopeEntities entityContext)
			: this()
		{
			_entityContext = entityContext;
		}

		/// <summary>
		/// Static current member for easy usage
		/// </summary>
		public static WebAppContext Current
		{
			get
			{
				if (HttpContext.Current == null)
				{
					throw new Exception("Currnet http context is null");
				}

				string ocKey = "ocm_" + HttpContext.Current.GetHashCode().ToString("x");
				if (!HttpContext.Current.Items.Contains(ocKey))
				{
					HttpContext.Current.Items.Add(ocKey, new WebAppContext());
				}

				return HttpContext.Current.Items[ocKey] as WebAppContext;
			}
		}

		#region IEntityContext Members

		public AltairScopeEntities GetPersistenceContext()
		{
			if (_entityContext == null)
			{
				InitializePersistenceContext();
			}
			return _entityContext;
		}

		public void InitializePersistenceContext()
		{
			_entityContext = new AltairScopeEntities();
			//_dbContext.CommandTimeout = 300;
		}

		public void TerminatePersistenceContext(bool saveChanges)
		{
			if (_entityContext != null)
			{
				if (saveChanges)
				{
					_entityContext.SaveChanges();
				}
				_entityContext.Dispose();
				_entityContext = null;
			}
		}

		public void ClearPersistenceContext()
		{
			_entityContext.Dispose();
			_entityContext = null;
		}

		public void Dispose()
		{
			TerminatePersistenceContext(false);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region IUnitOfWork Members

		public void Commit()
		{
			if (_entityContext != null)
			{
				_entityContext.SaveChanges();
			}
		}

		#endregion

	}
}
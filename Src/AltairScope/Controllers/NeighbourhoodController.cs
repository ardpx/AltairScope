using AltairScope.DomainModels;
using AltairScope.DomainModels.Services;
using AltairScope.Infrastructures;
using AltairScope.Models;
using AltairScope.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltairScope.Controllers
{
    public class NeighbourhoodController : Controller
    {
		NeighbourhoodDataServices _NeighbourhoodDataServices = null;
		NeighbourhoodViewModelServices _NeighbourhoodVMServices = null;
        //
        // GET: /Neigbourhood/

        public ActionResult Index()
        {
			_NeighbourhoodDataServices = new NeighbourhoodDataServices();
			var neighbourhoodViewList = _NeighbourhoodDataServices.GetAll(WebAppContext.Current);

			_NeighbourhoodVMServices = new NeighbourhoodViewModelServices();
			var neighbourhoodViewableModelList = new List<ViewableNeighbourhoodViewModel>();
			if(neighbourhoodViewList != null)
			{
				foreach (Neighbourhood neighbourhood in neighbourhoodViewList)
				{
					neighbourhoodViewableModelList.Add(_NeighbourhoodVMServices.ConvertToViewableModel(neighbourhood));
				}
			}

			return View(neighbourhoodViewableModelList);
			
        }

        //
        // GET: /Neigbourhood/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Neigbourhood/Create

        public ActionResult New()
        {
            return View();
        }

        //
        // POST: /Neigbourhood/Create

        [HttpPost]
		public ActionResult Create(NewNeighbourhoodViewModel newNeighbourhoodViewModel)
        {
            try
            {
				_NeighbourhoodVMServices = new NeighbourhoodViewModelServices();
				var newNeighbourhood = _NeighbourhoodVMServices.ConvertToDomainModel(newNeighbourhoodViewModel);
				newNeighbourhood.Add(WebAppContext.Current);
				newNeighbourhood.ValidateAndSave();
				WebAppContext.Current.Commit();
				var newId = newNeighbourhood.id;

				return RedirectToAction("View", new { id = newId });
            }
            catch
            {
                throw;
            }
        }

		public ActionResult View(Guid id)
		{
			_NeighbourhoodDataServices = new NeighbourhoodDataServices();
			var neighbourhood = _NeighbourhoodDataServices.GetNeighbourhoodById(WebAppContext.Current, id);

			_NeighbourhoodVMServices = new NeighbourhoodViewModelServices();
			var viewableNeighbourhoodViewModel = _NeighbourhoodVMServices.ConvertToViewableModel(neighbourhood);

			return View(viewableNeighbourhoodViewModel);
		}

        //
        // GET: /Neigbourhood/Edit/5

        public ActionResult Edit(Guid id)
        {
			_NeighbourhoodDataServices = new NeighbourhoodDataServices();
			var neighbourhood = _NeighbourhoodDataServices.GetNeighbourhoodById(WebAppContext.Current, id);

			_NeighbourhoodVMServices = new NeighbourhoodViewModelServices();
			var editNeighbourhoodViewModel = _NeighbourhoodVMServices.ConvertToEditModel(neighbourhood);

			return View(editNeighbourhoodViewModel);
        }

        //
        // POST: /Neigbourhood/Edit/5

        [HttpPost]
		public ActionResult Edit(EditNeighbourhoodViewModel editNeighbourhoodViewModel)
        {
			var id = editNeighbourhoodViewModel.Id;
			try
			{
				_NeighbourhoodDataServices = new NeighbourhoodDataServices();
				var exisitngNeighbourhood = _NeighbourhoodDataServices.GetNeighbourhoodById(WebAppContext.Current, id);

				if (exisitngNeighbourhood == null)
					throw new Exception("Neighbourhood not found");

				_NeighbourhoodVMServices = new NeighbourhoodViewModelServices();
				_NeighbourhoodVMServices.UpdateDomainModel(exisitngNeighbourhood, editNeighbourhoodViewModel);
				exisitngNeighbourhood.ValidateAndSave();
				WebAppContext.Current.Commit();

				return RedirectToAction("View", new { id = exisitngNeighbourhood.id });
			}
			catch
			{
				return View("Edit", new { id = id });
			}
        }

        //
        // GET: /Neigbourhood/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Neigbourhood/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		public JsonResult Check(string name)
		{
			_NeighbourhoodDataServices = new NeighbourhoodDataServices();
			var neighbourhoodId = _NeighbourhoodDataServices.TryGetIdByName(WebAppContext.Current, name);
			if (neighbourhoodId == Guid.Empty)
				throw new Exception("no match neighbourhood");
			return Json(new { id = neighbourhoodId.ToString() }, JsonRequestBehavior.AllowGet);
		}
    }
}

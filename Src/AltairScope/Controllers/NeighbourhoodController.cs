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
            return View();
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
                return View();
            }
        }

        //
        // GET: /Neigbourhood/Edit/5

        public ActionResult Edit(Guid id)
        {
            return View();
        }

        //
        // POST: /Neigbourhood/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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

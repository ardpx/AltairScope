﻿using AltairScope.DomainModels;
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
	[Authorize]
	public class PropertyController : Controller
	{
		PropertyDataServices _propertyDataServices = null;
		PropertyViewModelServices _propertyVMServices = null;

        //
        // GET: /Property/

		public ActionResult Index()
        {
			_propertyDataServices = new PropertyDataServices();
			var propertyList = _propertyDataServices.GetPropertyList(WebAppContext.Current, PropertyEagerLoadMode.Sale_Neighbourhood);

			_propertyVMServices = new PropertyViewModelServices();
			var propertyViewModelList = _propertyVMServices.ConvertToViewModel(propertyList);
			
			return View(propertyViewModelList);
		}

		//
        // GET: /Property/Create

		public ActionResult New()
		{
			return View();
		}

		//
        // POST: /Property/Create

		[HttpPost]
		public ActionResult Create(NewPropertyViewModel newPropertyViewModel)
        {
            try
            {
				_propertyVMServices = new PropertyViewModelServices();
				var newProperty = _propertyVMServices.ConvertToDomainModel(newPropertyViewModel);
				newProperty.Add(WebAppContext.Current);
				newProperty.ValidateAndSave();
				WebAppContext.Current.Commit();
				var newId = newProperty.id;

				return RedirectToAction("View", new { id = newId });
            }
            catch
            {
                return View();
            }
        }

		//
        // GET: /Property/Edit/5

		public ActionResult Edit(Guid id)
        {
			_propertyDataServices = new PropertyDataServices();
			var property = _propertyDataServices.GetPropertyById(WebAppContext.Current, id);

			_propertyVMServices = new PropertyViewModelServices();
			var editPropertyViewModel = _propertyVMServices.ConvertToEditViewModel(property);

			return View(editPropertyViewModel);
        }

        //
        // POST: /Property/Edit/5

        [HttpPost]
		public ActionResult Edit(EditPropertyViewModel editPropertyViewModel)
        {
			var id = editPropertyViewModel.Id;
            try
            {
				_propertyDataServices = new PropertyDataServices();
				var exisitngProperty = _propertyDataServices.GetPropertyById(WebAppContext.Current, id);

				if (exisitngProperty == null)
					throw new Exception("property not found");

				_propertyVMServices = new PropertyViewModelServices();
				_propertyVMServices.UpdateDomainModel(exisitngProperty, editPropertyViewModel);
				exisitngProperty.ValidateAndSave();
				WebAppContext.Current.Commit();

				return RedirectToAction("View", new { id = exisitngProperty.id });
            }
            catch
            {
				return View("Edit", new { id = id });
            }
        }

		public ActionResult View(Guid id)
		{
			_propertyDataServices = new PropertyDataServices();
			var property = _propertyDataServices.GetPropertyById(WebAppContext.Current, id, PropertyEagerLoadMode.Sale_Evaluation_Neighbourhood);

			_propertyVMServices = new PropertyViewModelServices();
			var viewablePropertyViewModel = _propertyVMServices.ConvertToViewableViewModel(property);

			return View(viewablePropertyViewModel);
		}

		[HttpPost]
		public ActionResult Evaluate(Guid id)
		{
			_propertyDataServices = new PropertyDataServices();
			var property = _propertyDataServices.GetPropertyById(WebAppContext.Current, id, PropertyEagerLoadMode.Sale_Evaluation_Neighbourhood);

			PropertyEvaluator evaluator = new PropertyEvaluator();
			evaluator.Evaluate(property);

			_propertyVMServices = new PropertyViewModelServices();
			var viewablePropertyViewModelForEvaluation = _propertyVMServices.ConvertToViewableViewModelForEvaluation(property);

			return PartialView("_Evaluate", viewablePropertyViewModelForEvaluation);
		}

		public ActionResult ChangeStatus(Guid id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult ChangeStatus(ChangeStatusPropertyViewModel changeStatusPropertyViewModel)
		{
			return View();
		}
    }
}
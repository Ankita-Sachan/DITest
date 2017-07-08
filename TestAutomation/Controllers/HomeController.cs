using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesAndInterfaces.Interfaces;
using System.ComponentModel;

namespace TestAutomation.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager _manager;

      
        public HomeController(IDataManager manager)
        {
            _manager = manager;
        }
      
        public ActionResult Index()
        {
            return View(_manager.GetAll());
        }
       
       
    }
}
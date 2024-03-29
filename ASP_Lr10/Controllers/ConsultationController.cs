using ASP_Lr10.Models;
using Microsoft.AspNetCore.Mvc;
namespace ASP_Lr10.Controllers
{
    public class ConsultationController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ConsultationRegistration registration)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            return View(registration);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}

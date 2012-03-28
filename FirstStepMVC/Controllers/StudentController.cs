using System.Web.Mvc;
using Domain;

namespace FirstStepMVC.Controllers
{
    public class StudentController : Controller
    {
        //
        // ADD: /Student/
        [HttpGet]
        public ActionResult Add()
        {
            var student = new Student();
            return View(student);
        }

    }
}

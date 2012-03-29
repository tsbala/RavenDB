using System.Web.Mvc;
using FirstStepMVC.Code;
using FirstStepMVC.Models;

namespace FirstStepMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentApplicationService _studentApplicationService;

        public StudentController(IStudentApplicationService studentApplicationService)
        {
            _studentApplicationService = studentApplicationService;
        }

        //
        // ADD: /Student/
        [HttpGet]
        public ActionResult Add()
        {
            var student = new StudentViewModel();
            return View(student);
        }

        [HttpPost]
        public ActionResult Add(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                _studentApplicationService.AddStudent(student);
            }
            return View(student);
        }

        [ActionName("Index")]
        [HttpGet]
        public ActionResult GetStudents()
        {
            var studentViewModels = _studentApplicationService.GetStudents();
            return View(studentViewModels);
        }
    }
}

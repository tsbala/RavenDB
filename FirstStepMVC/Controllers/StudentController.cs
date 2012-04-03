using System.Web.Mvc;
using FirstStepMVC.Code.ApplicationService.Student;
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
        public ActionResult GetStudents(string name, SearchMode searchMode = SearchMode.NotSet)
        {
            var studentViewModels = _studentApplicationService.GetStudents(name, searchMode);
            return View(studentViewModels);
        }
    }
}

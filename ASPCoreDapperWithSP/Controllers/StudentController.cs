using ASPCoreDapperWithSP.Models;
using ASPCoreDapperWithSP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreDapperWithSP.Controllers
{
    public class StudentController : Controller
    {
       // private readonly IConfiguration _configuration;

        private readonly IStudentServices _studentServices;

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        public IActionResult Index()
        {
            var StudentList =_studentServices.GetStudentList().ToList();
            return View(StudentList);
           
        }

        [HttpGet]
        public IActionResult Create(int id=0)
        {
            if (id != 0)
            {
                var std1 = _studentServices.GetById(id);
                return View(std1);
            }
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
           
            if(std.StudentName!=null&&std.EmailAddress!=null&&std.City!=null&&std.CreatedBy!=null) 
            { 
                _studentServices.InsertStudent(std);
                TempData["msg"] = "Save Successfully";
                return RedirectToAction("Index");
            }
           
            else
            {
               TempData["wrong"] = "Something gets wrong";
            }
            return View();

        }
    }
}

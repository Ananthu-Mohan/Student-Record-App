using Microsoft.AspNetCore.Mvc;
using Students.Data.DTO;
using Students.Data.Models;
using Students.Data.Repositories;
using StudentsRecordApplication.Models;
using StudentsRecordApplication.Services;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace StudentsRecordApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentsDataService _studentsDataService;
        private readonly IAddressService _addressService;
        private readonly ICourseService _courseService;
        private readonly ICourseEnrollmentService _courseEnrollmentService;
        public HomeController(IStudentsDataService studentsDataService,
            IAddressService addressService,
            ICourseService courseService,
            ICourseEnrollmentService courseEnrollmentService)
        {
           _studentsDataService = studentsDataService;
           _addressService = addressService;
           _courseService = courseService; 
           _courseEnrollmentService = courseEnrollmentService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchParams? searchParams,int pageNumber=1, int pageSize=10)
        {
            if (searchParams == null)
            {
                searchParams = new SearchParams();
            }
            var totalItemsCount = await _studentsDataService.GetStudentsCount(searchParams);
            searchParams.Paginator.PageNumber = pageNumber;
            searchParams.Paginator.PageSize = pageSize;
            var data = await _studentsDataService.GetAllStudentsData(searchParams);
            if(data != null)
            {
                var model = new PaginatedList<StudentsData>(
                    data,
                    totalItemsCount,
                    pageNumber,
                    pageSize
                    );
                return View(model);
            }
            return View();
        }
        public async Task<IActionResult> Details(long id)
        {
            var student = await _studentsDataService.GetStudentsData(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentDetails = new
            {
                firstName = student.FirstName,
                lastName = student.LastName,
                email = student.Email,
                dateOfBirth = student.DateOfBirth?.ToString("yyyy-MM-dd"),
                phoneNumber = student.PhoneNumber,
                gender = student.Gender,
                major = student.Major,
                gpa = student.GPA,
                address = new
                {
                    student.Address?.Street,
                    student.Address?.City,
                    student.Address?.State,
                    student.Address?.PostalCode,
                    student.Address?.Country,
                    student.Address?.ApartmentNumber,
                    student.Address?.District
                }
            };

            return Json(studentDetails);
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _studentsDataService.DeleteStudent(id);
                return Json(new { success = true, message = "Student deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting student." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id,StudentsData student)
        {
            try
            {
                await _studentsDataService.UpdateStudent(id,student);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentsData student)
        {
            if (ModelState.IsValid)
            {
                var newData  = await _studentsDataService.CreateNewStudent(student,student.Address);
                if (newData != null)
                {
                    return Json(new { success = true, message = "Student created successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Error creating student." });
                }
            }
            return Json(new { success = false, message = "Error creating student." });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Students.Data.DTO;
using Students.Data.Models;
using Students.Data.UnitOfWork;

namespace StudentsRecordApplication.Services
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAllCourses(SearchParams searchParams);
        public Task<Course> GetCourse(int id);
    }
    public class CourseService : ICourseService
    {
        public IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<List<Course>> GetAllCourses(SearchParams searchParams)
         => await _unitOfWork.CourseRepository.GetAllCourse(searchParams);

        public async Task<Course> GetCourse(int id)
        => await _unitOfWork.CourseRepository.GetCourse(id);
    }
}

using Students.Common.Models;
using Students.Data.Models;
using Students.Data.UnitOfWork;

namespace StudentsRecordApplication.Services
{
    public interface ICourseEnrollmentService
    {
        public Task<List<CourseEnrollment>> GetAllCourseEnrollment(SearchParams searchParams);
        public Task<CourseEnrollment> GetCourseEnrollment(int id);
    }
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        public IUnitOfWork _unitOfWork;
        public CourseEnrollmentService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<List<CourseEnrollment>> GetAllCourseEnrollment(SearchParams searchParams)
        => await _unitOfWork.CourseEnrollmentRepository.GetAllCourseEnrollment(searchParams);

        public async Task<CourseEnrollment> GetCourseEnrollment(int id)
        => await _unitOfWork.CourseEnrollmentRepository.GetCourseEnrollment(id);
    }
}

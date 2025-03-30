using Students.Common.Models;
using Students.Data.Models;
using Students.Data.UnitOfWork;

namespace StudentsRecordApplication.Services
{
    public interface IStudentsDataService
    {
        public Task<(int, List<StudentsData>)> GetAllStudentsData(SearchParams searchParams);
        public Task<StudentsData> GetStudentsData(long id);
        public Task<int> GetStudentsCount(SearchParams searchParams);

        public Task<StudentsData> CreateNewStudent(StudentsData student, Address address);
        public Task DeleteStudent(long id);
        public Task UpdateStudent(long id,StudentsData student);
        public Task<List<StudentsData>> GetCsvRecord(SearchParams searchParams);
    }
    public class StudentsDataService : IStudentsDataService
    {
        public IUnitOfWork _unitOfWork;
        public StudentsDataService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<(int, List<StudentsData>)> GetAllStudentsData(SearchParams searchParams)
            => await _unitOfWork.StudentsDataRepository.GetAllStudentsData(searchParams);

        public async Task<StudentsData> GetStudentsData(long id)
            =>await _unitOfWork.StudentsDataRepository.GetStudentsData(id);
        public async Task<int> GetStudentsCount(SearchParams searchParams)
         => await _unitOfWork.StudentsDataRepository.GetStudentsCount(searchParams);

        public async Task<StudentsData> CreateNewStudent(StudentsData student, Address address)
        => await _unitOfWork.StudentsDataRepository.CreateNewStudent(student,address);

        public async Task DeleteStudent(long id)
        => await _unitOfWork.StudentsDataRepository.DeleteStudent(id);

        public async Task UpdateStudent(long id,StudentsData student)
         => await _unitOfWork.StudentsDataRepository.UpdateStudent(id,student);

        public async Task<List<StudentsData>> GetCsvRecord(SearchParams searchParams)
        => await _unitOfWork.StudentsDataRepository.GetCsvRecord(searchParams);
    }
}

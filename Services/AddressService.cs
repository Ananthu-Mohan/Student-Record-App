using Students.Common.Models;
using Students.Data.Models;
using Students.Data.UnitOfWork;

namespace StudentsRecordApplication.Services
{
    public interface IAddressService
    {
        public Task<List<Address>> GetAllAddress(SearchParams searchParams);
        public Task<Address> GetAddress(int id);
    }
    public class AddressService : IAddressService
    {
        public IUnitOfWork _unitOfWork;
        public AddressService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Address> GetAddress(int id)
        => await _unitOfWork.AddressRepository.GetAddress(id);

        public async Task<List<Address>> GetAllAddress(SearchParams searchParams)
        => await _unitOfWork.AddressRepository.GetAllAddress(searchParams);
    }
}

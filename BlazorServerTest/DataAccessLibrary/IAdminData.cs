using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IAdminData
    {
        Task<List<AdminModel>> Load();
    }
}
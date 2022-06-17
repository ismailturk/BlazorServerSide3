using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface ICacheCoachData
    {
        Task<List<CoachModel>> GetCoachCache();
    }
}
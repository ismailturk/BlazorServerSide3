using DataAccessLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DataAccessLibrary
{
    public class CacheCoachData
    {


        private readonly IMemoryCache memoryCache;

        private readonly ISqlDataAccess db;
        public CacheCoachData(ISqlDataAccess db, IMemoryCache memoryCache)
        {
            this.db = db;
            this.memoryCache = memoryCache;
        }


        public async Task<List<CoachModel>> GetCoachCache()
        {
            string key = "getCoachCache";
            List<CoachModel> output;
            if (!memoryCache.TryGetValue(key, out output))
            {
                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    Priority = CacheItemPriority.Normal
                };

                string sql = "select * from dbo.Coach";
                output = await db.LoadData<CoachModel, dynamic>(sql, new { });

                memoryCache.Set(key, output, cacheExpOptions);
            }

           

            return await Task.FromResult(output);

        }

    }
}

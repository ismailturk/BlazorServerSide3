using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class AdminData : IAdminData
    {
        private readonly ISqlDataAccess db;

        public AdminData(ISqlDataAccess db)
        {
            this.db = db;
        }

        public Task<List<AdminModel>> Load()
        {
            string sql = "select * from dbo.AdminDB";
            return db.LoadData<AdminModel, dynamic>(sql, new { });
        }


    }
}


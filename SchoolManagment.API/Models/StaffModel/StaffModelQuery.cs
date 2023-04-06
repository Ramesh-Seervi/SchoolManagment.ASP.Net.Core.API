using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models.StaffModel
{
    public class StaffModelQuery
    {
        public AppDb Db { get; }

        public StaffModelQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<StaffModel> FindOneAsync(int StaffID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`GetStaffbyId`(@_StaffID)";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@_StaffID",
                DbType = DbType.Int32,
                Value = StaffID,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<StaffModel>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`GetStaffData`();";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<StaffModel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<StaffModel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new StaffModel(Db)
                    {
                        StaffID = reader.GetInt32(0),
                        StaffName = reader.GetString(1),
                        StaffDepartment = reader.GetString(2),
                        StaffWork = reader.GetString(3),
                        StaffExperience = reader.GetString(4),
                        StaffShift = reader.GetString(5),
                        StaffDOB = reader.GetString(6),
                        StaffContactNo = reader.GetString(7),
                        StaffJoiningDate = reader.GetString(8),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}


using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models.TeacherModel
{
    public class TeacherModelQuery
    {
        public AppDb Db { get; }

        public TeacherModelQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<TeacherModel> FindOneAsync(int TeacherID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`GetTeacherbyId`(@_TeacherID)";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@_TeacherID",
                DbType = DbType.Int32,
                Value = TeacherID,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<TeacherModel>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`GetTeachersDetails`();";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<TeacherModel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<TeacherModel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new TeacherModel(Db)
                    {
                        TeacherID = reader.GetInt32(0),
                        TeacherName = reader.GetString(1),
                        TeacherDOB = reader.GetString(2),
                        TeacherGender = reader.GetString(3),
                        TeacherSubject = reader.GetString(4),
                        TeacherContactNo = reader.GetString(5),
                        TeacherJoiningDate = reader.GetString(6),
                        Addresss = reader.GetString(7),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}


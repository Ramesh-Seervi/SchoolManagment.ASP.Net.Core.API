using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models
{
    public class StudentModelQuery
    {
        public AppDb Db { get; }

        public StudentModelQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<StudentModel> FindOneAsync(int StudentRollno)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`GetStudentbyId`(@StudentRollno)";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentRollno",
                DbType = DbType.Int32,
                Value = StudentRollno,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<StudentModel>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`GetStudentData`();";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<StudentModel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<StudentModel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new StudentModel(Db)
                    {
                        StudentRollno = reader.GetInt32(0),
                        StudentName = reader.GetString(1),
                        StudentDOB = reader.GetString(2),
                        StudentClass = reader.GetString(3),
                        StudentContactNo = reader.GetString(4),
                        StudentGender = reader.GetString(5),
                        StudentJoiningDate = reader.GetString(6),
                        StudentParentName = reader.GetString(7),
                        StudentParentNumber = reader.GetString(8),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}

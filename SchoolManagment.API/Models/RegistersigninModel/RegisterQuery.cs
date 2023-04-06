using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models.RegistersigninModel
{
    public class RegisterQuery
    {
        public AppDb Db { get; }

        public RegisterQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Register> FindOneAsync(string Email, string Password)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"call blog.SignIn(@_Email, @_Password)";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@_Email",
                DbType = DbType.String,
                Value = Email,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@_Password",
                DbType = DbType.String,
                Value = Password,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Register>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"call blog.getRegisterDetails();";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<Register>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Register>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Register(Db)
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Email = reader.GetString(2),
                        Mobile = reader.GetString(3),
                        Password = reader.GetString(4),
                        
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}

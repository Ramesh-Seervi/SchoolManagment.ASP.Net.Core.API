using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models.RegistersigninModel
{
    public class Register
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Conformpassword { get; set; }
        public bool UserActive { get; set; }

        internal AppDb Db { get; set; }

        public Register()
        {
        }

        internal Register(AppDb db)
        {
            Db = db;
        }

        public async Task Registration()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"call blog.Registration(@Id, @UserName, @Email, @Mobile, @Passwords, @ConformPassword);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }
        public async Task Login()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"call blog.Registration(@Id, @UserName, @Email, @Mobile, @Passwords);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`UpdateStaffbyId`(@StaffID, @StaffName, @StaffDepartment, @StaffWork, @StaffExperience, @StaffShift, @StaffDOB,@StaffContactNo,@StaffJoiningData);";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`DeleteStaffbyId`(@StaffID);";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });

        }

        private void BindParams(MySqlCommand cmd)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@UserName",
                DbType = DbType.String,
                Value = UserName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Email",
                DbType = DbType.String,
                Value = Email,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Mobile",
                DbType = DbType.String,
                Value = Mobile,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Passwords",
                DbType = DbType.String,
                Value = Password,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ConformPassword",
                DbType = DbType.String,
                Value = Password,
            });

        }

    }
}

using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models.TeacherModel
{
    public class TeacherModel
    {
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherDOB { get; set; }
        public string TeacherGender { get; set; }
        public string TeacherSubject { get; set; }
        public string TeacherContactNo { get; set; }
        public string TeacherJoiningDate { get; set; }
        public string Addresss { get; set; }
       

        internal AppDb Db { get; set; }

        public TeacherModel()
        {
        }

        internal TeacherModel(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`InsertTeachersData`(@TeacherID, @TeacherName, @TeacherDOB, @TeacherGender, @TeacherSubject, @TeacherContactNo, @TeacherJoiningDate, @Addresss);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            TeacherID = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`UpdateTeacherbyId`(@TeacherID, @TeacherName, @TeacherDOB, @TeacherGender, @TeacherSubject, @TeacherContactNo, @TeacherJoiningDate,@TeacherJoiningDate);";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`DeleteTeacherbyId`(@TeacherID);";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherID",
                DbType = DbType.Int32,
                Value = TeacherID,
            });

        }

        private void BindParams(MySqlCommand cmd)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherName",
                DbType = DbType.String,
                Value = TeacherName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherDOB",
                DbType = DbType.String,
                Value = TeacherDOB,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherGender",
                DbType = DbType.String,
                Value = TeacherGender,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherSubject",
                DbType = DbType.String,
                Value = TeacherSubject,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherContactNo",
                DbType = DbType.String,
                Value = TeacherContactNo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@TeacherJoiningDate",
                DbType = DbType.String,
                Value = TeacherJoiningDate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Addresss",
                DbType = DbType.String,
                Value = Addresss,
            });
           
        }
    }
}

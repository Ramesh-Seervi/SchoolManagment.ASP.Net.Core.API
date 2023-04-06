using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models
{
    public class StudentModel
    {
        public int  StudentRollno { get; set; }
        public string  StudentName { get; set; }
        public string  StudentDOB { get; set; }
        public string  StudentClass { get; set; }
        public string  StudentContactNo { get; set; }
        public string  StudentGender { get; set; }
        public string  StudentJoiningDate { get; set; }
        public string  StudentParentName { get; set; }
        public string  StudentParentNumber { get; set; }


        internal AppDb Db { get; set; }

        public StudentModel()
        {
        }

        internal StudentModel(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`InsertStudentData`(@StudentRollno, @StudentName, @StudentDOB, @StudentClass, @StudentContactNo, @StudentGender, @StudentJoiningDate,@StudentParentName, @StudentParentNumber);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            StudentRollno = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`UpdateStudentbyId`(@StudentRollno, @StudentName, @StudentDOB, @StudentClass, @StudentContactNo, @StudentGender, @StudentJoiningDate,@StudentParentName, @StudentParentNumber);";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`DeleteStudentbyId`(@StudentRollno);";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentRollno",
                DbType = DbType.Int32,
                Value = StudentRollno,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentName",
                DbType = DbType.String,
                Value = StudentName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentDOB",
                DbType = DbType.String,
                Value = StudentDOB,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentClass",
                DbType = DbType.String,
                Value = StudentClass,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentContactNo",
                DbType = DbType.String,
                Value = StudentContactNo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentGender",
                DbType = DbType.String,
                Value = StudentGender,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentJoiningDate",
                DbType = DbType.String,
                Value = StudentJoiningDate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentParentName",
                DbType = DbType.String,
                Value = StudentParentName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StudentParentNumber",
                DbType = DbType.String,
                Value = StudentParentNumber,
            });
        }
    }
}

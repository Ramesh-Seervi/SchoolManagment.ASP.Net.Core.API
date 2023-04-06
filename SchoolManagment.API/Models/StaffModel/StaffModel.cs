using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.API.Models.StaffModel
{
    public class StaffModel
    {
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public string StaffDepartment { get; set; }
        public string StaffWork { get; set; }
        public string StaffExperience { get; set; }
        public string StaffShift { get; set; }
        public string StaffDOB { get; set; }
        public string StaffContactNo { get; set; }
        public string StaffJoiningDate { get; set; }


        internal AppDb Db { get; set; }

        public StaffModel()
        {
        }

        internal StaffModel(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"CALL `blog`.`InsertStaffData`(@StaffID, @StaffName, @StaffDepartment, @StaffWork, @StaffExperience, @StaffShift, @StaffDOB, @StaffContactNo,@StaffJoiningData);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            StaffID = (int)cmd.LastInsertedId;
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
                ParameterName = "@StaffID",
                DbType = DbType.Int32,
                Value = StaffID,
            });

        }

        private void BindParams(MySqlCommand cmd)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffName",
                DbType = DbType.String,
                Value = StaffName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffDepartment",
                DbType = DbType.String,
                Value = StaffDepartment,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffWork",
                DbType = DbType.String,
                Value = StaffWork,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffExperience",
                DbType = DbType.String,
                Value = StaffExperience,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffShift",
                DbType = DbType.String,
                Value = StaffShift,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffDOB",
                DbType = DbType.String,
                Value = StaffDOB,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffContactNo",
                DbType = DbType.String,
                Value = StaffContactNo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@StaffJoiningData",
                DbType = DbType.String,
                Value = StaffJoiningDate,
            });

        }
    }
}

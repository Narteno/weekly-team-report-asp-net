using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CM.WeeklyTeamReport.Domain.Repositories
{
    public class WeeklyReportRepository : IRepository<WeeklyReport>
    {
        private const string connectionString = "Server=DESKTOP-HK7EOFM;Database=WeeklyTeamReport;Trusted_Connection=true;";
        private static SqlConnection GetSqlConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        private static WeeklyReport MapReport(SqlDataReader reader)
        {
            return new WeeklyReport((Condition)(int)reader["MoraleId"], reader["MoraleMessage"].ToString(), 
                (Condition)(int)reader["StressId"], reader["StressMessage"].ToString(),
                (Condition)(int)reader["WorkloadId"], reader["WorkloadMessage"].ToString(),
                reader["YourHigh"].ToString(), reader["YourLow"].ToString(), reader["AnythingElse"].ToString(),
                (DateTime)reader["DateOfReport"])
                {
                    ReportId = (int)reader["ReportId"],
                    FK_TeamMemberId = (int)reader["TeamMemberId"]
                };
        }
        public WeeklyReport Create(WeeklyReport report)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Insert into Reports (TeamMemberId, MoraleId, MoraleMessage, StressId, StressMessage," +
                    " WorkloadId, WorkloadMessage, YourHigh, YourLow, AnythingElse, DateOfReport)" +
                    " Values(@TeamMemberId, @MoraleId, @MoraleMessage, @StressId, @StressMessage, @WorkloadId, @WorkloadMessage," +
                    " @YourHigh, @YourLow, @AnythingElse, @DateOfReport);" +
                    " Select * From Reports where ReportId = SCOPE_IDENTITY()", connection);
                SqlParameter memberId = new SqlParameter("@TeamMemberId", SqlDbType.Int)
                {
                    Value = report.FK_TeamMemberId
                };
                SqlParameter moraleId = new SqlParameter("@MoraleId", SqlDbType.Int)
                {
                    Value = (int)report.Morale
                };
                SqlParameter stressId = new SqlParameter("@StressId", SqlDbType.Int)
                {
                    Value = (int)report.stress
                };
                SqlParameter workloadId = new SqlParameter("@WorkloadId", SqlDbType.Int)
                {
                    Value = (int)report.workload
                };
                SqlParameter moraleMessage = new SqlParameter("@MoraleMessage", SqlDbType.NVarChar, 300)
                {
                    Value = report.moraleMsg
                };
                SqlParameter stressMessage = new SqlParameter("@StressMessage", SqlDbType.NVarChar, 300)
                {
                    Value = report.stressMsg
                };
                SqlParameter workloadMessage = new SqlParameter("@WorkloadMessage", SqlDbType.NVarChar, 300)
                {
                    Value = report.workloadMsg
                };
                SqlParameter yourHigh = new SqlParameter("@YourHigh", SqlDbType.NVarChar, 300)
                {
                    Value = report.YourHigh
                };
                SqlParameter yourLow = new SqlParameter("@YourLow", SqlDbType.NVarChar, 300)
                {
                    Value = report.YourLow
                };
                SqlParameter anythingElse = new SqlParameter("@AnythingElse", SqlDbType.NVarChar, 300)
                {
                    Value = report.AnythingElse
                };
                SqlParameter dateOfReport = new SqlParameter("@DateOfReport", SqlDbType.NVarChar, 300)
                {
                    Value = report.dateOfRepot
                };
                command.Parameters.Add(moraleId);
                command.Parameters.Add(stressId);
                command.Parameters.Add(workloadId);
                command.Parameters.Add(moraleMessage);
                command.Parameters.Add(stressMessage);
                command.Parameters.Add(workloadMessage);
                command.Parameters.Add(yourHigh);
                command.Parameters.Add(yourLow);
                command.Parameters.Add(anythingElse);
                command.Parameters.Add(dateOfReport);
                command.Parameters.Add(memberId);

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapReport(reader);
                }
            }
            return null;
        }
        public List<WeeklyReport> ReadAll()
        {
            return new();
        }
        public WeeklyReport Read(int ReportId)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Select * From Reports" +
                    " Where ReportId = @ReportId", connection);
                SqlParameter reportId = new SqlParameter("@ReportId", SqlDbType.Int)
                {
                    Value = ReportId
                };
                command.Parameters.Add(reportId);

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapReport(reader);
                }
            }
            return null;
        }
        public void Update(WeeklyReport report)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Update Reports" +
                    " SET MoraleId = @MoraleId, MoraleMessage = @MoraleMessage" +
                    "  Where ReportId = @ReportId", connection);
                SqlParameter moraleId = new SqlParameter("@MoraleId", SqlDbType.Int)
                {
                    Value = report.Morale
                };
                SqlParameter moraleMessage = new SqlParameter("@MoraleMessage", SqlDbType.NVarChar, 300)
                {
                    Value = report.moraleMsg
                };
                SqlParameter reportId = new SqlParameter("@ReportId", SqlDbType.Int)
                {
                    Value = report.ReportId
                };
                command.Parameters.Add(moraleId);
                command.Parameters.Add(moraleMessage);
                command.Parameters.Add(reportId);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int ReportId)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Delete From Reports" +
                    " Where ReportId = @ReportId", connection);
                SqlParameter reportId = new SqlParameter("@ReportId", SqlDbType.Int)
                {
                    Value = ReportId
                };
                command.Parameters.Add(reportId);
                command.ExecuteNonQuery();
            }
        }

        public List<WeeklyReport> ReadAllByParentId(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}

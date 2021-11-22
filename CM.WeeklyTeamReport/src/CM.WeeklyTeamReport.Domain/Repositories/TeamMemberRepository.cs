using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain.Repositories
{
    public class TeamMemberRepository : IRepository<TeamMember>
    {
        private const string connectionString = "Server=DESKTOP-HK7EOFM;Database=WeeklyTeamReport;Trusted_Connection=true;";
        private static SqlConnection GetSqlConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        private static TeamMember MapMember(SqlDataReader reader)
        {
            return new TeamMember(reader["FirstName"].ToString(), reader["LastName"].ToString(), (TypeOfTeam)(int)(reader["TypeId"]))
            { 
                TeamMemberId = (int)reader["TeamMemberId"],
                FK_CompanyId = (int)reader["CompanyId"]
            };
        }
        public TeamMember Create(TeamMember teamMember)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Insert into TeamMembers (CompanyId, FirstName, LastName, TypeId)" +
                    " Values(@CompanyId, @FirstName, @LastName, @TypeId);" +
                    " Select * From TeamMembers where TeamMemberId = SCOPE_IDENTITY()", connection);
                SqlParameter companyId = new SqlParameter("@CompanyId", SqlDbType.Int)
                {
                    Value = teamMember.FK_CompanyId
                };
                SqlParameter firstName = new SqlParameter("@FirstName", SqlDbType.NVarChar, 100)
                {
                    Value = teamMember.FirstName
                };
                SqlParameter lastName = new SqlParameter("@LastName", SqlDbType.NVarChar, 100)
                {
                    Value = teamMember.LastName
                };
                SqlParameter typeId = new SqlParameter("@TypeId", SqlDbType.Int)
                {
                    Value = (int)teamMember.typeOfTeam
                };
                command.Parameters.Add(companyId);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(typeId);

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapMember(reader);
                }
            }
            return null;
        }
        public TeamMember Read(int TeamMemberId)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Select * From TeamMembers" +
                    " Where TeamMemberId = @TeamMemberId", connection);
                SqlParameter teamMemberId = new SqlParameter("@TeamMemberId", SqlDbType.Int)
                {
                    Value = TeamMemberId
                };
                command.Parameters.Add(teamMemberId);

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapMember(reader);
                }
            }
            return null;
        }
        public void Update(TeamMember teamMember)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Update TeamMembers" +
                    " SET FirstName = @FirstName, LastName = @LastName" +
                    "  Where TeamMemberId = @TeamMemberId", connection);
                SqlParameter firstName = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
                {
                    Value = teamMember.FirstName
                };
                SqlParameter lastName = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = teamMember.LastName
                };
                SqlParameter teamMemberId = new SqlParameter("@TeamMemberId", SqlDbType.Int)
                {
                    Value = teamMember.TeamMemberId
                };
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(teamMemberId);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int TeamMemberId)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Delete From TeamMembers" +
                    " Where TeamMemberId = @TeamMemberId", connection);
                SqlParameter teamMemberId = new SqlParameter("@TeamMemberId", SqlDbType.Int)
                {
                    Value = TeamMemberId
                };
                command.Parameters.Add(teamMemberId);
                command.ExecuteNonQuery();
            }
        }
    }
}

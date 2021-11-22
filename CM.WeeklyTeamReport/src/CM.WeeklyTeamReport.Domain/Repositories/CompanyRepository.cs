using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CM.WeeklyTeamReport.Domain.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private const string connectionString = "Server=DESKTOP-HK7EOFM;Database=WeeklyTeamReport;Trusted_Connection=true;";
        private static SqlConnection GetSqlConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        private static Company MapCompany(SqlDataReader reader)
        {
            return new Company(reader["Name"].ToString(), (DateTime)reader["JoinedDate"])
            { CompanyId = (int)reader["CompanyId"]};
        }
        public Company Create(Company company)
        {
            using(var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Insert into Company (Name, JoinedDate)" +
                    " Values(@CompanyName, @JoinedDate);" +
                    " Select * From Company where CompanyId = SCOPE_IDENTITY()", connection);
                SqlParameter companyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 50)
                {
                    Value = company.CompanyName
                };
                SqlParameter joinedDate = new SqlParameter("@JoinedDate", SqlDbType.DateTime)
                {
                    Value = company.JoinedDate
                };
                command.Parameters.Add(companyName);
                command.Parameters.Add(joinedDate);

                var reader = command.ExecuteReader();
                if(reader.Read())
                {
                    return MapCompany(reader);
                }
            }
            return null;
        }
        public Company Read(int companyId)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Select * From Company" +
                    " Where CompanyId = @CompanyId", connection);
                SqlParameter CompanyId = new SqlParameter("@CompanyId", SqlDbType.Int)
                {
                    Value = companyId
                };
                command.Parameters.Add(CompanyId);

                var reader = command.ExecuteReader();
                if(reader.Read())
                {
                    return MapCompany(reader);
                }
            }
            return null;
        }
        public void Update(Company company)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Update Company" +
                    " SET Name = @CompanyName, JoinedDate = @JoinedDate" +
                    "  Where CompanyId = @CompanyId", connection);
                SqlParameter companyId = new SqlParameter("@CompanyId", SqlDbType.Int)
                {
                    Value = company.CompanyId
                };
                SqlParameter companyName = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 50)
                {
                    Value = company.CompanyName
                };
                SqlParameter joinedDate = new SqlParameter("@JoinedDate", SqlDbType.DateTime)
                {
                    Value = company.JoinedDate
                };
                command.Parameters.Add(companyId);
                command.Parameters.Add(companyName);
                command.Parameters.Add(joinedDate);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int companyId)
        {
            using (var connection = GetSqlConnection(connectionString))
            {
                var command = new SqlCommand("Delete From Company" +
                    " Where CompanyId = @CompanyId", connection);
                SqlParameter CompanyId = new SqlParameter("@CompanyId", SqlDbType.Int)
                {
                    Value = companyId
                };
                command.Parameters.Add(CompanyId);
                command.ExecuteNonQuery();
            }
        }
    }
}

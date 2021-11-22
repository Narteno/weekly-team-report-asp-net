using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public int FK_CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeOfTeam typeOfTeam { get; set; }
        public List<WeeklyReport> teamMemberReports { get; set; } = new();
        public TeamMember(string FirstName, string LastName, TypeOfTeam typeOfTeam)
        {
            this.FirstName = FirstName; this.LastName = LastName; this.typeOfTeam = typeOfTeam;
        }
        
        
        public void CreateMemberReport(WeeklyReport report)
        {
            teamMemberReports.Add(report);
        }
        public void ChangeInformation(string? FirstName, string? LastName)
        {
            if (FirstName != null)
                this.FirstName = FirstName;
            if (LastName != null)
                this.LastName = LastName;
        }
        public void DeleteReport(int idOfReport)
        {
            teamMemberReports.RemoveAt(idOfReport);
        }
    }
    public enum TypeOfTeam
    {
        none,Immediate, Extended
    }
}

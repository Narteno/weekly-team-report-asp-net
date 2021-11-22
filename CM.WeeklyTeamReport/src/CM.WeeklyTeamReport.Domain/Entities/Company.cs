using System;
using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime JoinedDate { get; set; }
        public List<TeamMember> TeamMembers { get; set; } = new();
        public Company(string CompanyName, DateTime JoinedDate)
        {
            this.CompanyName = CompanyName;
            this.JoinedDate = JoinedDate;
        }
        public Company(string CompanyName, DateTime JoinedDate, List<TeamMember> teamMembers)
        {
            TeamMembers = teamMembers;
            this.CompanyName = CompanyName;
            this.JoinedDate = JoinedDate;
        }

    }
}

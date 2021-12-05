using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReport
    {
        public WeeklyReport(Condition morale, string moraleMsg, Condition stress, string stressMsg,
            Condition workload, string workloadMsg, string YourHigh, string YourLow, string AnythingElse,
            DateTime dateTime)
        {
            Morale = morale; this.stress = stress; this.workload = workload; this.dateOfRepot = dateTime;
            this.moraleMsg = moraleMsg; this.stressMsg = stressMsg; this.workloadMsg = workloadMsg;
            this.YourHigh = YourHigh; this.YourLow = YourLow; this.AnythingElse = AnythingElse;
        }
        public WeeklyReport(int reportId, int teamMemberId, Condition morale, string moraleMsg, Condition stress, string stressMsg,
            Condition workload, string workloadMsg, string YourHigh, string YourLow, string AnythingElse,
            DateTime dateTime)
        {
            ReportId = reportId; FK_TeamMemberId = teamMemberId;
            Morale = morale; this.stress = stress; this.workload = workload; this.dateOfRepot = dateTime;
            this.moraleMsg = moraleMsg; this.stressMsg = stressMsg; this.workloadMsg = workloadMsg;
            this.YourHigh = YourHigh; this.YourLow = YourLow; this.AnythingElse = AnythingElse;
        }
        public int ReportId { get; set; }
        public int FK_TeamMemberId { get; set; }
        public Condition Morale { get; private set; }
        public Condition stress { get; private set; }
        public Condition workload { get; private set; }
        public DateTime dateOfRepot { get; private set; }
        public string moraleMsg { get; private set; }
        public string stressMsg { get; private set; }
        public string workloadMsg { get; private set; }
        public string YourHigh { get; private set; }
        public string YourLow { get; private set; }
        public string AnythingElse { get; private set; }
    }
    public enum Condition
    {
        none,VeryLow, Low, Okay, Good, Great
    }
}


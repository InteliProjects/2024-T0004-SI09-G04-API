using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Model
{
    public class CertificatesCountbyMonth
    {
        public string? Month { get; set; }
        public int Atestados { get; set; }
        public double Difference { get; set; }
    }

    public class CertificatesDaysAggregatedModel
    {
        public string? Month { get; set; }
        public int TotalAtestados { get; set; }
        public double MediaDias { get; set; }
    }

    public class ZenklubMetricAverageModel
    {
        public double AverageSessionsPerPerson { get; set; }
        public double DifferenceFromLastYear { get; set; }
    }

    public class MonthlyAverageDaysOffModel
    {
        public string? Month { get; set; }
        public double AverageDaysOff { get; set; }
    }



    public class MonthlyCertificatesLocationModel
    {
        public string? Month { get; set; }
        public string? Location { get; set; }
        public double AverageDaysOff { get; set; }
    }

    public class TopDiseasesModel
    {
        public string? Disease { get; set; }
        public int Quantity { get; set; }
    }

    public class TopDiseasesCauseModel
    {
        public string? Disease { get; set; }
        public string? Cause { get; set; } 
        public int Quantity { get; set; }
    }

    public class CidsRoleModel
    {
        public string? Role { get; set; }
        public int? Atestados2021 { get; set; }
        public int? Atestados2022 { get; set; }
        public int? Atestados2023 { get; set; }
    }

    public class CidsByDirectoryModel
    {
        public string? Directorate { get; set; }
        public decimal Atestados { get; set; }
    }


    public class ZenklubEmployeeSessionModel
    {
        public string? Month { get; set; }
        public string? Location { get; set; }
        public int EmployeeCount { get; set; } 
        public int TotalSessions { get; set; } 
        public double SessionsPerEmployee { get; set; } 
    }

    public class CidTrend
    {
        public string? Month { get; set; }
        public string? RootCause { get; set; }
        public int Quantity { get; set; }
    }


    public class SessionCertificatesModel
    {
        public int Atestados { get; set; }
        public int Sessions { get; set; }
    }
}

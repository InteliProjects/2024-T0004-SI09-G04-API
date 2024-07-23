using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Repository
{
    public class Stiba2023
    {
        public double Elegiveis { get; set; }
        public double Respondentes { get; set; }
    }

    public class GptwEngage
    {
        public int Year { get; set; }
        public double EngagementPercent { get; set; }
    }

    public class StibaNota
    {
        public double Nota2023 { get; set; }
        public double Nota2022 { get; set; }
        public double VariacaoPercentual { get; set; }
    }

    public class GptwCompanyData
    {
        public string? Pergunta { get; set; }
        public string? Escala { get; set; }
        public List<double> Valores { get; set; } = new List<double>();
    }
}

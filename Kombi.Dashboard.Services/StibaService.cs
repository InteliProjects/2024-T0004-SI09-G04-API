using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Repository;
using Kombi.Dashboard.Services;

namespace Kombi.Dashboard.Services
{
    public class StibaService : IStibaService
    {
        private readonly IStibaRepository _stibaRepository;

        public StibaService(IStibaRepository stibaRepository)
        {
            _stibaRepository = stibaRepository;
        }
        public async Task<IEnumerable<StibaModel>> GetStibaAnswers()
        {
            return await _stibaRepository.GetStibaAnswers();
        }
        public async Task<IEnumerable<StibaModel>> GetStibaAnswers23()
        {
            return await _stibaRepository.GetStibaAnswers();
        }
    }
}




        /* public Task<IEnumerable<StibaModel>> GetStibaAnswersAsync()
         {
             throw new NotImplementedException();
         }
 */





        /*public async Task<StibaSummaryModel> GetStibaAnswersAsync()
        {
            var answers = await _stibaRepository.GetStibaAnswers();
            var totalEligibles2022 = answers.Sum(a => a.Elegiveis2022 ?? 0);
            var totalEligibles2023 = answers.Sum(a => a.Elegiveis2023 ?? 0);
            var totalRespondents2022 = answers.Sum(a => a.Respondentes2022 ?? 0);
            var totalRespondents2023 = answers.Sum(a => a.Respondentes2023 ?? 0);

            var percentageRespondents2022 = (double)totalRespondents2022 / totalEligibles2022 * 100;
            var percentageRespondents2023 = (double)totalRespondents2023 / totalEligibles2023 * 100;

            return new StibaSummaryModel
            {
                TotalEligibles2022 = totalEligibles2022,
                TotalEligibles2023 = totalEligibles2023,
                TotalRespondents2022 = totalRespondents2022,
                TotalRespondents2023 = totalRespondents2023,
                PercentageRespondents2022 = (decimal)Math.Round(percentageRespondents2022, 2),
                PercentageRespondents2023 = (decimal)Math.Round(percentageRespondents2023, 2)
            };
        }

        private static double CalculatePercentageChange(decimal? previousScore, decimal? currentScore)
        {
            if (!previousScore.HasValue || !currentScore.HasValue || previousScore.Value == 0)
                return 0;

            return (double)Math.Round(((currentScore.Value - previousScore.Value) / previousScore.Value) * 100, 2);
        }

        public async Task<StibaSummaryScore> CalculateStibaScoresAsync()
        {
            var score = await _stibaRepository.GetStibaScore();

            var percentageChange = CalculatePercentageChange(score.NotaStiba2022, score.NotaStiba2023);

            return new StibaSummaryScore
            {
                Score2022 = score.NotaStiba2022 ?? 0,
                Score2023 = score.NotaStiba2023 ?? 0,
                ChangePercentage = (decimal)percentageChange
            };
        }

        public async Task<IEnumerable<CultureScore>> GetTopFiveCultureScoresAsync()
        {
            var allScores = await GetSortedCultureScoresAsync();
            return allScores.OrderByDescending(x => x.Score).Take(5);
        }

        public async Task<IEnumerable<CultureScore>> GetBottomFiveCultureScoresAsync()
        {
            var allScores = await GetSortedCultureScoresAsync();
            return allScores.Take(5);
        }

        private async Task<IEnumerable<CultureScore>> GetSortedCultureScoresAsync()
        {
            var cultureScores = await _stibaRepository.GetCultureScore();
            return (IEnumerable<CultureScore>)cultureScores.OrderBy(x => x.Score);
        }

        *//*public async Task<IEnumerable<DirectoryScore>> GetScoresByDirectoryAsync()
        {
            var directoryScores = await _stibaRepository.GetDirectoryScoreYearsAsync();

            return directoryScores.Select(ds => new DirectoryScore
            {
                Directory = ds.DirectoryDescription,
                AverageScores = ds.ScoresByYear
            });
        }*//*

        public async Task<IEnumerable<QuestionYearlyAverage>> GetQuestionYearlyAveragesAsync()
        {
            return await _stibaRepository.GetQuestionYearlyAveragesAsync();
        }

        public async Task<IEnumerable<DirectoryAnswerScoreModel>> GetScoresByQuestionAsync()
        {
            var directoryAnswerScores = await _stibaRepository.GetDirectoryAnswerScoreAsync();

            return directoryAnswerScores.Select(das => new DirectoryAnswerScoreModel
            {
                Directory = das.Directory ?? string.Empty,

                ScoresByQuestion = das.ScoresByQuestion?.ToDictionary(
                    score => score.Key,
                    score => Convert.ToDecimal(score.Value)
                ) ?? new Dictionary<string, decimal>(),
            }).ToList();
        }


        public Task<IEnumerable<DirectoryScore>> GetAverageScoresByDirectoryAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<DirectoryAnswerScore>> IStibaService.GetScoresByQuestionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DirectoryScore>> GetDirectoryScoreYearsAsync()
        {
            throw new NotImplementedException();
        }


       
    }
}*/

using System.Collections.Generic;
using System.Threading.Tasks;


namespace Kombi.Dashboard.Repository
{
    public interface IStibaRepository
    {
        Task<IEnumerable<StibaModel>> GetStibaAnswers();
        Task<IEnumerable<StibaModel>> GetStibaAnswers23();
        /* Task<StibaModel> GetStibaScore(); 
         Task<IEnumerable<StibaModel>> GetDirectoryScore();
         Task<IEnumerable<StibaModel>> GetCultureScore();
         Task<IEnumerable<StibaModel>> GetDirectoryAnswerScoreYears();
         Task<IEnumerable<StibaModel>> GetDirectoryAnswerScore();
         Task<IEnumerable<StibaModel>> GetQuestionScore();
         Task<IEnumerable<QuestionYearlyAverage>> GetQuestionYearlyAveragesAsync();
         Task<IEnumerable<DirectoryAnswerScore>> GetDirectoryAnswerScoreAsync();
         Task<IEnumerable<object>> GetDirectoryScoreYearsAsync();*/
    }
}

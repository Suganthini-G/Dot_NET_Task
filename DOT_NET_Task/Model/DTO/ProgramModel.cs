using System.ComponentModel.DataAnnotations;

namespace Dot_NET_Task.Model.DTO
{
    public class ProgramModel
    {
        [Key] public string id { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }
        public List<PersonalInformationModel> PersonalInformations { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Dot_NET_Task.Model.DTO
{
    public class QuestionSubmissionModel
    {
        [Key] public string id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResident { get; set; }
        public string IDNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public List<QuestionAnswerModel> QuestionAnswers { get; set; }
    }
}

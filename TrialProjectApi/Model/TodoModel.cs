using System.ComponentModel.DataAnnotations;

namespace TrialProjectApi.Model
{
    public class TodoModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public int priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

            public void Todo()
            {
                IsComplete = false;
            }
    }
}

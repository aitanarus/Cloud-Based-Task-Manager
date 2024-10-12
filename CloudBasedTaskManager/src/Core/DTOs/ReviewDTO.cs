using Core.Enum;

namespace Core.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public MoodType Mood { get; set; }
        public ReviewDTO() { }

        public ReviewDTO(string comment, MoodType mood)
        {
            Comment = comment;
            Mood = mood;
        }
    }
}

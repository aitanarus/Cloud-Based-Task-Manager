using Core.Enum;

namespace Core.DTOs
{
    // MoodDTO to represent user feelings
    public class MoodDTO
    {
        public int MoodId  { get; set; }
        public MoodType Mood { get; set; } 
        public MoodDTO() { }

        public MoodDTO(int moodId, MoodType mood)
        {
            MoodId = moodId;
            Mood = mood;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    // ResourceDTO to represent links, directions, or other related resources for the task
    public class ResourceDTO
    {
        [Required]
        public string Title { get; set; }  // Title of the resource (e.g., "Map to the venue")

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string Url { get; set; }  // URL of the resource (e.g., link to a website or map)

        // Parameterless constructor
        public ResourceDTO() { }

        // Parameterized constructor
        public ResourceDTO(string title, string url)
        {
            Title = title;
            Url = url;
        }
    }
}

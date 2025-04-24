using System.ComponentModel.DataAnnotations;

namespace WebApp_Uppgift.Models
{
    public class ProjectCreateFormModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Display(Name = "Project Name", Prompt = "Enter project name")]
        [Required(ErrorMessage = "Required.")]
        public string ProjectName { get; set; } = null!;

        [Display(Name = "Client Name", Prompt = "Enter client name")]
        [Required(ErrorMessage = "Required.")]
        public string ClientName { get; set; } = null!;

        [Display(Name = "Description", Prompt = "Enter a Description")]
        [Required(ErrorMessage = "Required.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Date", Prompt = "Enter A Start Date")]
        [Required(ErrorMessage = "Required.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Date", Prompt = "Enter A End Date")]
        [Required(ErrorMessage = "Required.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Budget", Prompt = "Enter a Budget")]
        [Required(ErrorMessage = "Required.")]
        public int Budget { get; set; }

        public ClientCreateFormModel Client { get; set; } = null!;

    }
}

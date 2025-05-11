namespace WebApp_Uppgift.ViewModels
{
    public class EditProjectFormData
    {
        public int Id { get; set; } 
        public string ProjectName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}

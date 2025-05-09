namespace WebApp_Uppgift.ViewModels
{
    public class ProjectDisplayViewModel
    {
        public int Id { get; set; }

        public string ProjectName { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = "NotStarted";
    }
}

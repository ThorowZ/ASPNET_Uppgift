namespace WebApp_Uppgift.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string ProjectName { get; set; } = null!;


        public string Description { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

    }
}

namespace WebApp_Uppgift.Models
{
    public class ClientCreateFormModel
    {
        public int Id { get; set; }

        public string ClientName { get; set; } = null!;

        public string ContactPerson { get; set; } = null!;

        public string Email { get; set; } = null!;



    }
}

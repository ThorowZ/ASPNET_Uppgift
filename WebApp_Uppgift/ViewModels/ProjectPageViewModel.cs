namespace WebApp_Uppgift.ViewModels

{
    public class ProjectPageViewModel
    {
        public List<ProjectDisplayViewModel> Projects { get; set; } = new();
        public AddProjectModalViewModel Modal { get; set; } = new();

        public EditProjectViewModel EditProjectFormData { get; set; } = new();
    }
}

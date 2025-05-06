using Business.Dtos;
using Domain.Models;

namespace WebApp_Uppgift.ViewModels
{
    internal class ProjectsViewModel
    {
        public ProjectResult<IEnumerable<Project>> Projects { get; set; }
    }
}
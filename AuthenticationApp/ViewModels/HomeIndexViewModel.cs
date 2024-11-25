using AuthenticationApp.Domain.Models;

namespace AuthenticationApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public string FirstName { get; set; }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Films.Core.Concrete.Models
{
    public class User : IdentityUser
    {
        public ICollection<Video> Videos { get; set; }
        
        public User()
        {
            Videos = new Collection<Video>();
        }
    }
}
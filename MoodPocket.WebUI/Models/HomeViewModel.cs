using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodPocket.WebUI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<UserCard > UserCards { get; set; }
    }

    public class UserCard
    {
        public User User { get; set; }

        public Gallery Gallery { get; set; }

        public string BackgroundPictureUrl { get; set; }

        public int SavedMemesCount { get; set; }
    }
}
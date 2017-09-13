using MoodPocket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodPocket.WebUI.Models
{
    public class GalleryViewModel
    {
        public IEnumerable<PictureModel> Pictures { get; set; }

        public string HostedBy { get; set; }

    }
}
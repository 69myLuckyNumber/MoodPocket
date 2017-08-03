using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Entities
{
	public class GalleryPicture
	{
		[Key, Column(Order = 0)]
		public int GalleryID { get; set; }

		[Key, Column(Order = 1)]
		public int PictureID { get; set; }

		public UserGallery Gallery { get; set; }

		public UserPicture Picture { get; set; }
	}
}

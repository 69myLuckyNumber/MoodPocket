using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{
	public class UserGallery
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GalleryID { get; set; }

		public string Name { get; set; }

		public int UserID { get; set; }
		public User User { get; set; }

		public virtual ICollection<GalleryPicture> GalleryPictures { get; set; }
	}

}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{
	public class Gallery
	{
		[Key, ForeignKey("User")]
		public int Id { get; set; }
		[Required]
		public virtual User User { get; set; }

		public string Name { get; set; }

		public virtual ICollection<GalleryPicture> GalleryPictures { get; set; }
	}

}

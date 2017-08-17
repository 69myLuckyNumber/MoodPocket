using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{
	public class Picture
	{
		[Key]
		public int Id { get; set; }

		public string Url { get; set; }

		public virtual ICollection<GalleryPicture> GalleryPictures { get; set; }
	}
}

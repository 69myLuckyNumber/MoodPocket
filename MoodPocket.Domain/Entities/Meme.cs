using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{
	public class Meme
	{
		[Key]
		public int Id { get; set; }

		public string Url { get; set; }

		public virtual ICollection<GalleryMeme> GalleryMemes { get; set; }
	}
}

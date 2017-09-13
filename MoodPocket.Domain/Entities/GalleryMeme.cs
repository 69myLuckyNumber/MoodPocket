using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{
	public class GalleryMeme
	{
		[Key, Column(Order = 0)]
		public int GalleryID { get; set; }

		[Key, Column(Order = 1)]
		public int MemeID { get; set; }

		public virtual Gallery Gallery { get; set; }

		public virtual Meme Meme { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{
	public class GalleryPicture
	{
		[Key, Column(Order = 0)]
		public int GalleryID { get; set; }

		[Key, Column(Order = 1)]
		public int PictureID { get; set; }

		public Gallery Gallery { get; set; }

		public Picture Picture { get; set; }
	}
}

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

		public virtual Gallery Gallery { get; set; }

		public virtual Picture Picture { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Entities
{
	public class UGallery
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GalleryID { get; set; }

		public string Name { get; set; }

		public virtual ICollection<GalleryPicture> GalleryPictures { get; set; }
	}

}

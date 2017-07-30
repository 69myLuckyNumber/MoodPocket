﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.Domain.Entities
{
	public class UPicture
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PictureID { get; set; }

		public string Url { get; set; }

		public virtual ICollection<GalleryPicture> GalleryPictures { get; set; }
	}
}

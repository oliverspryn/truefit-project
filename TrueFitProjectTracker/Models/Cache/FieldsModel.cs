using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models.Cache {
	[Table("FieldsModel", Schema = "dbo")]
	public class FieldsModel {
		public string ID { get; set; }
		[Key]
		public string Name { get; set; }
	}
}
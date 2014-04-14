using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models.Dashboard {
	public class TaskModel {
		public DateTime Created { get; set; }
		public string Description { get; set; }
		public DateTime DueDate { get; set; }
		public string Epic { get; set; }
		public string Issue { get; set; }
		public string Name { get; set; }
		public int Percent { get; set; }
		public string Priority { get; set; }
		public string Resolution { get; set; }
		public DateTime ResolutionDate { get; set; }
		public string Sprint { get; set; }
		public string Status { get; set; }
	}
}
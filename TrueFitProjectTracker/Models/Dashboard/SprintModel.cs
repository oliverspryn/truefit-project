using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models.Dashboard {
	public class SprintModel {
		public DateTime CompleteDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public string State { get; set; }
		public List<TaskModel> Tasks { get; set; }
	}
}
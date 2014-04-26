using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models.Dashboard {
	public class ProgressModel {
		public int Committed { get; set; }
		public int Expected { get; set; }
		public int Percent { get; set; }
	}
}
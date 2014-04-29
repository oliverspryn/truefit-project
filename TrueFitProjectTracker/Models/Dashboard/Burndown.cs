using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models.Dashboard {
	public class Burndown {
		public List<double> Data { get; set; }
		public Tuple<int, int> End { get; set; }
		public Tuple<int, int> Start { get; set; }
	}
}
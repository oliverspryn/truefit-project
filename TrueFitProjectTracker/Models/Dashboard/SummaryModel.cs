using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models.Dashboard {
	public class SummaryModel {
		private int completed;
		private int remaining;
		private int total = -1;

		public SummaryModel() {
			Burndown = new Burndown();
			RecentComplete = new List<int>();
		}

		public Burndown Burndown { get; set; }
		public int Completed {
			get {
				return completed;
			}
			set {
				completed = value;
				Total = (Total == -1) ? value : (Total + value);
			}
		}
		public int Percent { get; set; }
		public int Recent { get; set; }
		public List<int> RecentComplete { get; set; }
		public int Remaining {
			get {
				return remaining;
			}
			set {
				remaining = value;
				Total = (Total == -1) ? value : (Total + value);
			}
		}
		public int Total {
			get { return total; }
			set { total = value; }
		}
	}
}
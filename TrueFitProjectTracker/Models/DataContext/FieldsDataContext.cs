using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrueFitProjectTracker.Models.Cache;

namespace TrueFitProjectTracker.Models.DataContext {
	public class FieldsDataContext : DbContext {
		public DbSet<FieldsModel> Fields { get; set; }

		static FieldsDataContext() {
			Database.SetInitializer<FieldsDataContext>(new DropCreateDatabaseIfModelChanges<FieldsDataContext>());
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace tryMVC.Models
{
    public class DBContextInit : DropCreateDatabaseIfModelChanges<DBContext>
    {
        protected override void Seed(DBContext context)
        {

            var countries = AllNationalities.Countries();
            foreach(var item in countries)
            {
                context.AllNationalities.Add(new AllNationalities() {
                   nationalityName =  item });
            }
            context.SaveChanges();

        }
    }
}
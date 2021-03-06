﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace tryMVC.Models
{
    public class DBContext : DbContext
    {
        public DbSet<NationalitiesModel> Nationalities { get; set; }
        public DbSet<AllNationalities> AllNationalities { get; set; }

        public DbSet<CustomersModel> Customers { get; set; }
        public DbSet<ServiceItemsModel> ServiceItems { get; set; }
        public DbSet<ServicesModel> Services { get; set; }

        public DbSet<WorkModel> Work { get; set; }
    }
}
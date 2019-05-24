using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stores.Models.DAL
{
    public class ProjectContext:DbContext
    {

        public ProjectContext():base("DBStore")
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Users_Privileges> Users_Privileges { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Storehouse> Storehouse { get; set; }
        public DbSet<Produt_Price> Produt_Price { get; set; }
        public DbSet<Clients_Type> Clients_Type { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<BillsCategory> BillsCategory { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<BillsContent> BillsContent { get; set; }
        public DbSet<ExpensesType> ExpensesType { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Payments> Payments { get; set; }

        public System.Data.Entity.DbSet<Stores.Models.CommonClasses.BillsWithExten> BillsWithExtens { get; set; }
    }
}
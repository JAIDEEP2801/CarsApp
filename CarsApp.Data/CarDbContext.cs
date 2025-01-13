using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarsApp.Data.Models;

namespace CarsApp.Data
{
    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
    }
}

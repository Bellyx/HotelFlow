using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelFlow.Models;

namespace HotelFlow.Data
{
    public class HotelFlowContext : DbContext
    {
        public HotelFlowContext (DbContextOptions<HotelFlowContext> options)
            : base(options)
        {
        }

        public DbSet<HotelFlow.Models.Addroom> Addroom { get; set; } = default!;

    }
}

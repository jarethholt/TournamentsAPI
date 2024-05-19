using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentsAPI.Core.Entities;

namespace TournamentsAPI.API.Data
{
    public class TournamentsContext : DbContext
    {
        public TournamentsContext (DbContextOptions<TournamentsContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentsAPI.Core.Entities.Tournament> Tournament { get; set; } = default!;
        public DbSet<TournamentsAPI.Core.Entities.Game> Game { get; set; } = default!;
    }
}

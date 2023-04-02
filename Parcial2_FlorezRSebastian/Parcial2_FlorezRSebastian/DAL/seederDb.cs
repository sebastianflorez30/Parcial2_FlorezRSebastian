using Parcial2_FlorezRSebastian.DAL.Entities;
using System.Diagnostics.Metrics;


namespace Parcial2_FlorezRSebastian.DAL
{
    public class SeederDb
    {


        private readonly DatabaseContext _context;


        public SeederDb(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // me reemplaza el comando update-database
            await PopulateTicketsAsync();
            await _context.SaveChangesAsync();   
        }

        private async Task PopulateTicketsAsync()
        {
            if (!_context.Tickets.Any())//any halla al menos 1 registro, ! no halla al menos 1 registro
            {
                for (int i = 0; i < 50; i++)
                {
                    _context.Tickets.Add(new Ticket  { });
                }

            }
        }

    }
}


using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class RentalRepository
    {
        private readonly CarRentalDbContext _context;

        public RentalRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Task<List<RentalEntity>> GetAllAsync(CancellationToken ct = default) =>
            _context.Rentals.AsNoTracking()
                .Include(r => r.Car)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync(ct);

        public Task<RentalEntity?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _context.Rentals.AsNoTracking()
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == id, ct);

        public async Task<RentalEntity> AddAsync(RentalEntity rental, CancellationToken ct = default)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync(ct);
            return rental;
        }

        public async Task UpdateAsync(RentalEntity rental, CancellationToken ct = default)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id, ct);
            if (existing == null) return;

            _context.Rentals.Remove(existing);
            await _context.SaveChangesAsync(ct);
        }
    }
}


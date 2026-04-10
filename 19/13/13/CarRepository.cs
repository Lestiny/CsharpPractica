using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class CarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Task<List<CarEntity>> GetAllAsync(CancellationToken ct = default) =>
            _context.Cars.AsNoTracking().OrderBy(c => c.Make).ThenBy(c => c.Model).ToListAsync(ct);

        public Task<CarEntity?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _context.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<CarEntity> AddAsync(CarEntity car, CancellationToken ct = default)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync(ct);
            return car;
        }

        public async Task UpdateAsync(CarEntity car, CancellationToken ct = default)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (existing == null) return;

            _context.Cars.Remove(existing);
            await _context.SaveChangesAsync(ct);
        }
    }
}


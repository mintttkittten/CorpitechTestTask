using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Data
{
    public class WorkerRepository
    {
        private readonly AppDbContext _context;

        public WorkerRepository()
        {
            _context = new AppDbContext();
        }

        public async Task<IEnumerable<Worker>> GetAllWorkersAsync()
        {
            return await _context.Workers.ToListAsync();
        }

        public async Task<Worker?> GetWorkerByIdAsync(int id)
        {
            return await _context.Workers.FindAsync(id);
        }

        public async Task AddWorkerAsync(Worker worker)
        {
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorkerAsync(Worker worker)
        {
            var existingWorker = await _context.Workers.FindAsync(worker.Id);
            if (existingWorker != null)
            {
                _context.Entry(existingWorker).CurrentValues.SetValues(worker);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteWorkerAsync(int id)
        {
            var workerToDelete = await _context.Workers.FindAsync(id);
            if (workerToDelete != null)
            {
                _context.Workers.Remove(workerToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}

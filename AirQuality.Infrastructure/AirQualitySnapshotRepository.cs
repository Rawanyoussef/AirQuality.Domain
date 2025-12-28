using AirQuality.Application.Interfaces;
using AirQuilty.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Infrastructure
{
    public class AirQualitySnapshotRepository : IAirQualitySnapshotRepository
    {
        private readonly AppDbContext _context;

        public AirQualitySnapshotRepository(AppDbContext context)
        { 
            _context = context;
        }
        public  async Task AddAsync (AirQualitySnapshot airQuality)
        {
            await _context.AirQualitySnapshots.AddAsync(airQuality);
            await _context.SaveChangesAsync();
        }

        public  async Task<AirQualitySnapshot> GetMostPollutedParisAsync()
        {
            return await _context.AirQualitySnapshots
            .Where(a => a.City == "Paris")
            .OrderByDescending(a => a.AqiUS)
            .FirstOrDefaultAsync();
        }
    }
}

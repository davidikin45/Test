using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Dto;

namespace Test.Data.Repositories
{
    public interface IStatsRepository
    {
        Task<List<RoomProgress>> GetRoomSummaryAsync();
    }

    public class StatsRepository : IStatsRepository
    {
        private readonly ApplicationDbContext _context;

        public StatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<RoomProgress>> GetRoomSummaryAsync()
        {
            return _context.RoomProgressStats.FromSqlRaw(@$"
                    SELECT 
                    R.Name, 
                    J.Status,
                    Count(*) as Count,
                    Total
                    FROM RX_RoomType R
                    CROSS JOIN (
                    SELECT 1 as StatusNum
                    UNION
                    SELECT 2 as StatusNum
                    UNION
                    SELECT 3 as StatusNum
                    UNION
                    SELECT 4 as StatusNum
                    ) as S
                    LEFT JOIN (
                        SELECT RoomTypeId, Count(*) as Total
                        From RX_Job
                        Group By RoomTypeId
                    ) as T
                    ON R.Id = T.RoomTypeId
                    LEFT JOIN RX_Job J
                    ON J.RoomTypeId = R.Id
                    AND J.StatusNum = S.StatusNum
                    GROUP BY 
                    R.Name, S.StatusNum, J.Status, T.Total
                    ORDER BY R.Name
                ").ToListAsync();
        }
    }
}

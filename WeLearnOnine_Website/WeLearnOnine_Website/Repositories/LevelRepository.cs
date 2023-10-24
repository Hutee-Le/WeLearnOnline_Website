using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly StWelearnContext _context;

        public LevelRepository(StWelearnContext context)
        {
            _context = context;
        }

        public List<Level> GetAllLevels()
        {
            return _context.Levels.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DerekmodeWeLearnSystemContext _context;

        public RoleRepository(DerekmodeWeLearnSystemContext context)
        {
            _context = context;
        }

        public List<AspNetRole> GetAll()
        {
            return _context.AspNetRoles.ToList();
        }

        public AspNetRole GetByID(Guid id)
        {
            return _context.AspNetRoles.Find(id.ToString());
        }

        public void Add(AspNetRole aspNetRole)
        {
            if (aspNetRole == null)
                throw new ArgumentNullException(nameof(aspNetRole));

            _context.AspNetRoles.Add(aspNetRole);
            _context.SaveChanges();
        }

        public void Update(AspNetRole aspNetRole)
        {
            if (aspNetRole == null)
                throw new ArgumentNullException(nameof(aspNetRole));

            _context.Entry(aspNetRole).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var role = _context.AspNetRoles.Find(id.ToString());
            if (role != null)
            {
                _context.AspNetRoles.Remove(role);
                _context.SaveChanges();
            }
        }
    }
}

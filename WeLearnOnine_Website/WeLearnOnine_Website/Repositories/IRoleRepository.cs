using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models
{
    public interface IRoleRepository
    {
        List<AspNetRole> GetAll();
        AspNetRole GetByID(Guid id);
        void Add(AspNetRole aspNetRole);
        void Update(AspNetRole aspNetRole);
        void Delete(Guid id);
    }
}

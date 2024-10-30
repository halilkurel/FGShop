using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.Interfaces;
using FGShop.DataAccessLayer.Repositories;
using FGShop.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DataAccessLayer.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly FGShopContext _context;

        public Uow(FGShopContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

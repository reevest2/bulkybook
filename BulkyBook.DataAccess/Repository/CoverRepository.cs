using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverRepository: Repository<Cover>, ICoverRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Cover obj)
        {
            _db.Covers.Update(obj);
        }
    }
}

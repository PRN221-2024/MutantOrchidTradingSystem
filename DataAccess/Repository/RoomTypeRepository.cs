using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly FUMiniHotelManagementContext _context;

        public RoomTypeRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }

        public RoomType GetById(int id)
        {
            return _context.RoomTypes.Find(id);
        }

        public List<RoomType> GetAll()
        {
            return _context.RoomTypes.ToList();
        }

        public void Add(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            _context.SaveChanges();
        }

        public void Update(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            _context.SaveChanges();
        }

        public void Delete(RoomType roomType)
        {
            _context.RoomTypes.Remove(roomType);
            _context.SaveChanges();
        }


        public int GetIndexByID(int id)
        {
            return _context.RoomTypes.ToList().Where(i=> i.RoomTypeId == id).Count();
        }
    }
}

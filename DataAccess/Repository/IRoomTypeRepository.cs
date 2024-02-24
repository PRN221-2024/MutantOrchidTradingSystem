using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IRoomTypeRepository
    {
        public RoomType GetById(int id);
        public List<RoomType> GetAll();
        public int GetIndexByID(int id);
        public void Add(RoomType roomType);
        public void Update(RoomType roomType);
        public void Delete(RoomType roomType);
    }
}

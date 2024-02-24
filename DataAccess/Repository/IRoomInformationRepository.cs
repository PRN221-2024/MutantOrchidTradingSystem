using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IRoomInformationRepository
    {
        public RoomInformation GetById(int id);
        public List<RoomInformation> GetAll();
        public List<RoomInformation> SearchByRoomNumber(string roomNumber);
        public bool Add(RoomInformation roomInformation);
        public bool Update(RoomInformation roomInformation);
        public bool Delete(RoomInformation roomInformation);
        public void UpdateStatusDel(int id);
    }
}

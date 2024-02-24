using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;

namespace BusinessObject
{
    public class RoomObject
    {
        private readonly IRoomInformationRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IBookingDetailRepository _bookingDetailRepositoryRepository;

        private readonly IMapper _mapper;
        public RoomObject()
        {
            _roomRepository = new RoomInformationRepository(); 
            _roomTypeRepository = new RoomTypeRepository();
            _bookingDetailRepositoryRepository = new BookingDetailRepository();
        }

        public List<RoomInformation> GetAllRooms() => _roomRepository.GetAll();
        public bool Create(RoomInformation room) => _roomRepository.Add(room);
        public List<RoomInformation> Search(string roomNumber) => _roomRepository.SearchByRoomNumber(roomNumber);
        public List<RoomType> GetRoomTypes() => _roomTypeRepository.GetAll();
        public RoomType GetRoomTypeById(int id) => _roomTypeRepository.GetById(id);
        public RoomInformation GetRoomById(int id) => _roomRepository.GetById(id);


        public bool UpdateRoom(RoomInformation updatedRoom)
        {
            try
            {
                // Kiểm tra xem đối tượng cần cập nhật có tồn tại trong cơ sở dữ liệu không
                var existingRoom = _roomRepository.GetById(updatedRoom.RoomId);

                if (existingRoom != null)
                {
                    // Cập nhật các thuộc tính của đối tượng hiện tại với giá trị từ đối tượng được chuyển đến
                    existingRoom.RoomNumber = updatedRoom.RoomNumber;
                    existingRoom.RoomDetailDescription = updatedRoom.RoomDetailDescription;
                    existingRoom.RoomMaxCapacity = updatedRoom.RoomMaxCapacity;
                    existingRoom.RoomTypeId = updatedRoom.RoomTypeId;
                    existingRoom.RoomStatus = updatedRoom.RoomStatus;
                    existingRoom.RoomPricePerDay = updatedRoom.RoomPricePerDay;

                    // Gọi phương thức Update của Repository để cập nhật vào cơ sở dữ liệu
                    _roomRepository.Update(existingRoom);

                    return true; // Cập nhật thành công
                }
                else
                {
                    // Đối tượng không tồn tại trong cơ sở dữ liệu
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                Console.WriteLine($"Error updating room - RoomObject: {ex.Message}");
                return false;
            }
        }
        public bool DeleteRoom(RoomInformation room)
        {
            var listRoomWasBooked = _bookingDetailRepositoryRepository.GetAllRoomInBookingDetail();
            if(!listRoomWasBooked.Contains(room, new RoomInformationEqualityComparer()))
            {
                return _roomRepository.Delete(room);
            }
            else
            {
                _roomRepository.UpdateStatusDel(room.RoomId);
                return true;
            }
        }
    }
}

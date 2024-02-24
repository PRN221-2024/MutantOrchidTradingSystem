using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class RoomInformationRepository : IRoomInformationRepository
	{
		private readonly FUMiniHotelManagementContext _context;

		public RoomInformationRepository()
		{
			_context = new FUMiniHotelManagementContext();
		}

		public RoomInformation GetById(int id)
		{
			return _context.RoomInformations
				.Include(room => room.RoomType)
				.SingleOrDefault(room => room.RoomId == id);
		}

		public List<RoomInformation> GetAll()
		{
			try
			{
				return _context.RoomInformations.Include(t => t.RoomType).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetAll - CustomerRepository: {ex.Message}");
				throw;
			}
		}

		public bool Add(RoomInformation roomInformation)
		{
			try
			{
				_context.RoomInformations.Add(roomInformation);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Add - RoomInformationRepository: {ex.Message}");
				throw;
				return false;
			}
		}

		public bool Update(RoomInformation roomInformation)
		{
			try
			{
				_context.RoomInformations.Update(roomInformation);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Update - RoomInformationRepository: {ex.Message}");
				throw;
				return false;
			}
		}

		public bool Delete(RoomInformation roomToDelete)
		{
			try
			{

				if (roomToDelete != null)
				{
					_context.RoomInformations.Remove(roomToDelete);
					_context.SaveChanges();

					return true;
				}

				return false; // Phòng không tồn tại
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Delete - RoomInformationRepository: {ex.Message}");
				return false;
			}
		}

		public List<RoomInformation> SearchByRoomNumber(string roomNumber)
		{
			try
			{
				return _context.RoomInformations
					.Where(room => room.RoomNumber.Contains(roomNumber))
					.Include(t => t.RoomType)
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred in SearchByRoomNumber - RoomInformationRepository: {ex.Message}");
				return new List<RoomInformation>();
			}
		}

		public void UpdateStatusDel(int id)
		{
			try
			{
				var getRoomUpd = _context.RoomInformations
					.Where(r => r.RoomId == id).FirstOrDefault();
				if (getRoomUpd != null)
				{
					getRoomUpd.RoomStatus = 0;
					_context.Update(getRoomUpd);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred in SearchByRoomNumber - RoomInformationRepository: {ex.Message}");
			}
		}
	}
}

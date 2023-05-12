using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services
{
    public class RoomService : IRoomService
    {
        public const int MaxIngredientsForPotions = 5;
        private readonly HogwartsContext _context;

        public RoomService(HogwartsContext context)
        {
            _context = context;
        }

        

        public async Task AddRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> GetRoom(long roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Room>> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoom(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            throw new NotImplementedException();
        }
    }
}

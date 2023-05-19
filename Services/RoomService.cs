using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

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
            _context.Rooms.Add(room);

            await _context.SaveChangesAsync();
           

        }

        public async Task<Room> GetRoom(long roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            return room;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return rooms;
        }

        public async Task UpdateRoom( Room room)
        {
            var roomToUpdate = await _context.Rooms.FindAsync(room.Id);
            if (roomToUpdate != null)
            {
                roomToUpdate.Id = room.Id;
                roomToUpdate.Capacity = room.Capacity;
                roomToUpdate.Residents = room.Residents;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null) _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {   
            var rooms = await _context.Rooms.Where(room => !IsCatOrOwlInTheRoom(room)).ToListAsync();
            return rooms;
        }

        private bool IsCatOrOwlInTheRoom(Room room)
        {
            bool result = false;
            foreach(var student in room.Residents)
            {
                if(student.PetType == PetType.Cat|| student.PetType == PetType.Owl)
                {
                    result = true;
                    break;
                }
                else result = false;
            }
            return result;
        }
    }
}

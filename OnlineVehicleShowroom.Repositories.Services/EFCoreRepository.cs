using Microsoft.EntityFrameworkCore;
using OnlineVehicleShowroom.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Repositories.Services
{
    //Declaring EFCoreRepository class to implement IRepository interface with generic T 
    public class EFCoreRepository<T> :IRepository<T> where T : class
    {
        private readonly DbContext _context;

        //Declaring Constructor based Dependency Injection
        public EFCoreRepository(DbContext context)
        {
            _context = context;
        }

        //Implementing generic method for Adding entity
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Implementing generic method for Deleting entity by ID
        public async Task<T> DeleteAsync(int id)
        {
            var entity = default(T);
            try
            {
                entity = await _context.Set<T>().FindAsync(id);
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        //Implementing generic method for List all entities
        public async Task<List<T>> GetAllAsync()
        {
            var entities = default(List<T>);
            try
            {
                entities = await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entities;
        }

        //Implementing generic method for Searching entity by ID
        public async Task<T> GetAsync(int id)
        {
            var entity = default(T);
            try
            {
                entity = await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        //Implementing generic method for Updating entity
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

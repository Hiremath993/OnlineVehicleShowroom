using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineVehicleShowroom.Repositories.Contracts
{
    //Declaring interface named IRepository with generic T which is a class
    public interface IRepository<T> where T : class
    {
        //Declaring generic abstract methods for CRUD Operations
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(int id);
    }
}

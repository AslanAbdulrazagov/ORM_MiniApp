﻿using ORM_MiniApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Repositories.Abstractions.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<List<T>> GetAllAsync(params string[] includes);
       
        Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> expression, params string[] includes);

        Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<int> SaveChangesAsync();

    }
}

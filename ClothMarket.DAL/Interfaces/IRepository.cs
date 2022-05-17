﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> Create(T entity);
        Task<T> Get(int id);
        Task<List<T>> Select();
        Task<bool> Delete(T entity);
        Task<T> Update(T entity);
    }
}

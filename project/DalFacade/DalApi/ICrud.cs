﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DO;
namespace DalApi
{
    public interface ICrud<T>
    {
        public int Add(T obj);
        public void Delete(int id);
        public void Update(T obj);
        public IEnumerable<T> GetAll();
        public T Get(int id);
    }
}

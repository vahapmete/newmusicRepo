﻿using System;
using System.Collections.Generic;
using System.Text;
using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;

namespace MainMusicStore.DataAccess.MainRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Company=new CompanyRepository(_db);
            Product= new ProductRepository(_db);
            CoverType=new CoverTypeRepository(_db);
            category=new CategoryRepository(_db);
            sp_call = new SPCallRepository(_db);
            ApplicationUser=new ApplicationUserRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICategoryRepository category { get; private set;}
        public ICompanyRepository Company { get; private set; }
        public IProductRepository  Product { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public ISPCallRepository sp_call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        ICategoryRepository category { get; }
        ICompanyRepository Company { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        ISPCallRepository sp_call { get; }
        void Save();
    }
}

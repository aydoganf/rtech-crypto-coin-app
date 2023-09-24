using Microsoft.Extensions.DependencyInjection;
using System;

namespace RTech.CryptoCoin.UnitOfWork;

public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUnitOfWork Create()
    {
        return _serviceProvider.GetRequiredService<IUnitOfWork>();
        //try
        //{
        //    var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();

        //    unitOfWork.Disposed += (sender, args) =>
        //    {
        //        scope.Dispose();
        //    };

        //    return unitOfWork;
        //}
        //catch
        //{
        //    scope.Dispose();
        //    throw;
        //}
    }
}
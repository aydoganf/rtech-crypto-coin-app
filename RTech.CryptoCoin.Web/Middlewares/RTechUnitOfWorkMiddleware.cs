using RTech.CryptoCoin.UnitOfWork;

namespace RTech.CryptoCoin.Web.Middlewares;

public class RTechUnitOfWorkMiddleware : IMiddleware
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public RTechUnitOfWorkMiddleware(IUnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path.Value.Contains(".css"))
        {
            return;
        }

        using var uow = _unitOfWorkManager.Create();
        
        await next(context);

        await uow.CompleteAsync();
    }
}

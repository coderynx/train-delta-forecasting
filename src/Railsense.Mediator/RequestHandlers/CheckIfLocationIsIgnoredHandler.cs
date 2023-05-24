using MediatR;
using Microsoft.EntityFrameworkCore;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class CheckIfLocationIsIgnoredHandler : IRequestHandler<CheckIfLocationIsIgnoredRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public CheckIfLocationIsIgnoredHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(CheckIfLocationIsIgnoredRequest request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.IgnoredLocations.SingleOrDefaultAsync(x => x.Name.Equals(request.Name),
            cancellationToken);
        return result is not null;
    }
}
using portfolio.entity;
using portfolio.data.Abstract;
using portfolio.data.Concrete.EfCore;

namespace portfolio.data.Concrete.EfCore
{
    public class EfCoreResumeRepository: EfCoreGenericRepository<Resume, PortfolioContext>, IResumeRepository
    {
        
    }
}
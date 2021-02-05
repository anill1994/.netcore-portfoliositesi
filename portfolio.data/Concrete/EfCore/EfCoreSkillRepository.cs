using portfolio.data.Abstract;
using portfolio.entity;

namespace portfolio.data.Concrete.EfCore
{
    public class EfCoreSkillRepository : EfCoreGenericRepository<Skill, PortfolioContext>, ISkillRepository
    {
        
    }
}
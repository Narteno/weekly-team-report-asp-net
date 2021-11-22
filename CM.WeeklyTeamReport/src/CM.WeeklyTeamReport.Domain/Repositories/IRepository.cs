using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);

        TEntity Read(int entityId);

        void Update(TEntity entity);

        void Delete(int entityId);
    }
}

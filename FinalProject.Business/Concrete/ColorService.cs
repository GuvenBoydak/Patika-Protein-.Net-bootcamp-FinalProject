using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class ColorService : GenericRepository<Color>, IColorRepository
    {
        public ColorService(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }
}

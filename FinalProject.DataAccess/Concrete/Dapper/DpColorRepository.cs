using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpColorRepository : GenericRepository<Color>, IColorRepository
    {
        public DpColorRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }



}

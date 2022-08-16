using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IRepository<Category> repository, ICategoryRepository categoryRepository) : base(repository)
        {
            _categoryRepository = categoryRepository;
        }


        public void Delete(int id)
        {
            try
            {
                _categoryRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error {typeof(Category).Name} =>  {e.Message}");
            }
        }

        public async Task<Category> GetCategoryWithProductsAsync(int id)
        {
            try
            {
               return await _categoryRepository.GetCategoryWithProductsAsync(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Get_Category_Error {typeof(Category).Name} =>  {e.Message}");
            }
        }

        public async Task UpdateAsync(Category entity)
        {
            try
            {
                await _categoryRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error {typeof(Category).Name} =>  {e.Message}");
            }
        }
    }

}

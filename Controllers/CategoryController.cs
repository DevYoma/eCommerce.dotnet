using eCommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ECommerceDbContext _dbContext;

        public CategoryController(ECommerceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetCategory")]
        //public List<CategoryModel> GetAllCategory()
        public ActionResult GetAllCategory()
        {
            var categoryList = _dbContext.Categories.ToList();
            // select * from categoryMaster
            return Ok(categoryList);
        }

        [HttpPost("CreateNewCategory")]
        public IActionResult AddNew(CategoryModel categoryModel)
        {
            _dbContext.Categories.Add(categoryModel);
            _dbContext.SaveChanges();
            //return Created("Category created", categoryModel);
            return StatusCode(201, categoryModel);
        }

        [HttpPut("UpdateCategory")]
        public IActionResult Update(int id,  CategoryModel categoryModel)
        {
            var currentCategory = _dbContext.Categories.SingleOrDefault(m => m.categoryId == id);
            if(currentCategory == null)
            {
                return NotFound("Category not found");
            }
            currentCategory.categoryName = categoryModel.categoryName;
            currentCategory.categoryLogoUrl = categoryModel.categoryLogoUrl;
            // you do not updaate the primary key

            _dbContext.SaveChanges();
            return Ok(currentCategory);
        }

        [HttpDelete("DeleteCategoryById")] // this works with IActionResult. 
        public IActionResult DeleteById(int categoryId)
        {
            var record = _dbContext.Categories.Find(categoryId);
            var record2 = _dbContext.Categories.FirstOrDefault(m => m.categoryId == categoryId);

            if(record == null)
            {
                //return BadRequest("Id of {0} does not exist", categoryId);
                return BadRequest();
            }

            _dbContext.Categories.Remove(record);
            _dbContext.SaveChanges();
            return Ok(record);

        }

    }
}

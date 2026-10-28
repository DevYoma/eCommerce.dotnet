using eCommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ECommerceDbContext _dbContext;

        public ProductController(ECommerceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [Route("getAllProducts")]
        public IActionResult GetProducts()
        {
            var list = _dbContext.ProductModels.ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("getAllProductsWithCategoryName")]
        public IActionResult GetProductsWithCategoryName()
        {
            // we want to return ProductData and CategoryId value(name)
            var list = (from p in _dbContext.ProductModels
                        join cat in _dbContext.Categories on p.productCategoryId equals cat.categoryId
                        select new ProductModelList
                        {
                            categoryName = cat.categoryName, 
                            createdDate = p.createdDate, 
                            isAvailable = p.isAvailable,
                            productCategoryId = p.productCategoryId,
                            productDescription = p.productDescription,
                            productName = p.productName,
                            productId = p.productId,
                            productPrice = p.productPrice,
                        }
                        ).ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("getProductByCategory")]
        public IActionResult FilterProduct(int categoryId)
        {
            // we want to return ProductData and CategoryId value(name)
            var list = (from p in _dbContext.ProductModels
                        join cat in _dbContext.Categories on p.productCategoryId equals cat.categoryId
                        where p.productCategoryId == categoryId
                        select new ProductModelList
                        {
                            categoryName = cat.categoryName,
                            createdDate = p.createdDate,
                            isAvailable = p.isAvailable,
                            productCategoryId = p.productCategoryId,
                            productDescription = p.productDescription,
                            productName = p.productName,
                            productId = p.productId,
                            productPrice = p.productPrice,
                        }
                        ).ToList();
            return Ok(list);
        }

        [HttpPost]
        [Route("AddNewProduct")]
        public IActionResult AddNewProduct(ProductModel proModelObj)
        {
            // product name should not be duplicate
            var isExist = _dbContext.ProductModels.SingleOrDefault(m => m.productName == proModelObj.productName);
            if (isExist != null)
            {
                return BadRequest("Product Name already exists");
            }
            else
            {
                _dbContext.ProductModels.Add(proModelObj);
                _dbContext.SaveChanges();
                //return Ok()
                return Created("Product Created", proModelObj);
            }
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(int id, ProductModel proModelObj)
        {
            var currentProduct = _dbContext.ProductModels.SingleOrDefault(m => m.productId == id);

            //if (currentProduct )
            if (currentProduct == null || id <= 0)
            {
                return NotFound("Product not found");
            }
            else
            {
                currentProduct.productName = proModelObj.productName;
                currentProduct.productPrice = proModelObj.productPrice;
                currentProduct.productCategoryId = proModelObj.productCategoryId;
                currentProduct.productDescription = proModelObj.productDescription;
                currentProduct.isAvailable = proModelObj.isAvailable;
                currentProduct.createdDate = proModelObj.createdDate;
                _dbContext.SaveChanges();
                return Ok(currentProduct);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var currentProduct = _dbContext.ProductModels.SingleOrDefault(m => m.productId == id);
            if (currentProduct == null || id <= 0)
            {
                return NotFound("Product not found");
            }
            else
            {
                _dbContext.ProductModels.Remove(currentProduct);
                _dbContext.SaveChanges();
                return Ok("Product deleted");
            }
        }
    }
}

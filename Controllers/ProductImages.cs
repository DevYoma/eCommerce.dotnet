using eCommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// one product has multiple images, bulk upload api

namespace eCommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImages : ControllerBase
    {
        private ECommerceDbContext _dbContext;

        public ProductImages(ECommerceDbContext dbContext)
        {
           this._dbContext = dbContext;
        }


        [HttpGet]
        [Route("getImageByProductId")]
        public IActionResult GetImagesByProductId(int productId)
        {
            var list = _dbContext.ProductImageModels.Where(m => m.productId == productId).ToList();
            return Ok(list);
        }

        // bulk upload
        [HttpPost("AddBulkImages")] // passing as an array of objects from the client
        public IActionResult AddBulkProductImages(List<ProductImageModel> list)
        {
            _dbContext.ProductImageModels.AddRange(list);
            _dbContext.SaveChanges();

            return Created("Bulk Update Success", list);
        }

        [HttpDelete("DeleteImage/{id}")]
        public IActionResult DeleteImageById(int id)
        {
            var image = _dbContext.ProductImageModels.SingleOrDefault(m => m.productImageId == id);
            if (image == null)
            {
                return NotFound("Image not found");
            }
            else
            {
                _dbContext.ProductImageModels.Remove(image);
                _dbContext.SaveChanges();
                return Ok("Image deleted");
            }
        }

        //[HttpDelete("DeleteImagesByProductId/{productId}")]

        [HttpDelete("DeleteImagesByProductId/{productId}")]
        public IActionResult DeleteImagesByProductId(int productId)
        {
            var images = _dbContext.ProductImageModels.Where(m => m.productId == productId).ToList();
            if (images == null || images.Count == 0)
            {
                return NotFound("Images not found");
            }
            else
            {
                _dbContext.ProductImageModels.RemoveRange(images);
                _dbContext.SaveChanges();
                return Ok("Images deleted");
            }
        }
    }
}

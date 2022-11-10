using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try 
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
            return new BadRequestResult();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return new OkObjectResult(await _context.Products.ToListAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new BadRequestResult();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return new OkObjectResult(await _context.Products.FirstOrDefaultAsync(x => x.Id == id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new BadRequestResult();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Product product)
        {
            try
            {
                var _product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (_product != null)
                {
                    _product.ArtNumber = product.ArtNumber;
                    _product.ProductName = product.ProductName;
                    _product.Price = product.Price;
                    _product.Description = product.Description;

                    _context.Update(_product);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new BadRequestResult();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var _product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (_product != null)
                {
                    _context.Remove(_product);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new BadRequestResult();
        }
    }
}

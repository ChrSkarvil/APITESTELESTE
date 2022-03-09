
using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Data.Entities;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiController]
    public class ProductInfoController : ControllerBase
    {
        private readonly IElgigantenRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ProductInfoController(IElgigantenRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ProductInfoModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllProductInfosAsync();

                return _mapper.Map<ProductInfoModel[]>(results);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        //[HttpGet("{productInfoID}")]
        //[MapToApiVersion("1.0")]
        //public async Task<ActionResult<ProductInfoModel>> Get(int productInfoID)
        //{
        //    try
        //    {

        //            var result = await _repository.GetProductInfoAsync(productInfoID);

        //        if (result == null) return NotFound();

        //        return _mapper.Map<ProductInfoModel>(result);

        //    }
        //    catch (Exception)
        //    {

        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }
        //}

        [HttpGet("{ean}/{gtin}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ProductInfoModel>> Get(long? ean, long? gtin)
        {
            try
            {

                var result = await _repository.GetProductInfoAsync(ean, gtin);

                if (result == null) return NotFound();

                return _mapper.Map<ProductInfoModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        public async Task<ActionResult<ProductInfoModel>> Post(ProductInfoModel model)
        {
            try
            {
                if (model.EAN != null)
                {
                    var existing = await _repository.GetProductInfoAsync(model.EAN, null);
                    if (existing != null)
                    {
                        return BadRequest("EAN in Use");
                    }
                }
                else if (model.GTIN != null)
                {
                    var existing = await _repository.GetProductInfoAsync(model.GTIN, null);
                    if (existing != null)
                    {
                        return BadRequest("GTIN in Use");
                    }
                }

                if (model.EAN != null)
                {
                    var location = _linkGenerator.GetPathByAction("Get", "ProductInfo", new { ean = model.EAN });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current EAN");
                    }

                    //Create a new ProductInfo
                    var productInfo = _mapper.Map<ProductInfo>(model);
                    _repository.Add(productInfo);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/ProductInfo/{productInfo.EAN}", _mapper.Map<ProductInfoModel>(productInfo));
                    }

                }
                else if (model.GTIN != null)
                {
                    var location = _linkGenerator.GetPathByAction("Get", "ProductInfo", new { gtin = model.GTIN });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current GTIN");
                    }

                    //Create a new ProductInfo
                    var productInfo = _mapper.Map<ProductInfo>(model);
                    _repository.Add(productInfo);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/ProductInfo/{productInfo.GTIN}", _mapper.Map<ProductInfoModel>(productInfo));
                    }
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{ean}/{gtin}")]
        public async Task<ActionResult<ProductInfoModel>> Put(long? ean, long? gtin, ProductInfoModel model)
        {
            try
            {
                var oldProductInfo = await _repository.GetProductInfoAsync(ean, gtin);
                if (oldProductInfo == null) return NotFound($"Could not find productinfo{ean} {gtin}");

                _mapper.Map(model, oldProductInfo);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<ProductInfoModel>(oldProductInfo);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{ean}/{gtin}")]
        public async Task<IActionResult> Delete(long? ean, long? gtin)
        {
            try
            {
                var oldProductInfo = await _repository.GetProductInfoAsync(ean, gtin);
                if (oldProductInfo == null) return NotFound();

                _repository.Delete(oldProductInfo);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Successfully deleted Product Info");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Product Info");
        }

    }
}

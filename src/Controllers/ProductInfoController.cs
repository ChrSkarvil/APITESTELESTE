﻿
using AutoMapper;
using CoreCodeCamp.Data;
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

        [HttpGet("{productInfoID}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ProductInfoModel>> Get(int productInfoID)
        {
            try
            {
                var result = await _repository.GetProductInfoAsync(productInfoID);

                if (result == null) return NotFound();

                return _mapper.Map<ProductInfoModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        //public async Task<ActionResult<CampModel>> Post(CampModel model)
        //{
        //    try
        //    {
        //        var existing = await _repository.GetCampAsync(model.Moniker);
        //        if (existing != null)
        //        {
        //            return BadRequest("Moniker in Use");
        //        }

        //        var location = _linkGenerator.GetPathByAction("Get", "Camps", new { moniker = model.Moniker });

        //        if (string.IsNullOrWhiteSpace(location))
        //        {
        //            return BadRequest("Could not use current moniker");
        //        }

        //        //Create a new Camp
        //        var camp = _mapper.Map<Camp>(model);
        //        _repository.Add(camp);
        //        if (await _repository.SaveChangesAsync())
        //        {
        //            return Created($"/api/camps/{camp.Moniker}", _mapper.Map<CampModel>(camp));
        //        }


        //    }
        //    catch (Exception)
        //    {

        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }

        //    return BadRequest();
        //}

        //[HttpPut("{moniker}")]
        //public async Task<ActionResult<CampModel>> Put(string moniker, CampModel model)
        //{
        //    try
        //    {
        //        var oldCamp = await _repository.GetCampAsync(moniker);
        //        if (oldCamp == null) return NotFound($"Could not find camp with moniker of {moniker}");

        //        _mapper.Map(model, oldCamp);

        //        if (await _repository.SaveChangesAsync())
        //        {
        //            return _mapper.Map<CampModel>(oldCamp);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }

        //    return BadRequest();
        //}

        //[HttpDelete("{moniker}")]
        //public async Task<IActionResult> Delete(string moniker)
        //{
        //    try
        //    {
        //        var oldCamp = await _repository.GetCampAsync(moniker);
        //        if (oldCamp == null) return NotFound();

        //        _repository.Delete(oldCamp);

        //        if (await _repository.SaveChangesAsync())
        //        {
        //            return Ok();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }

        //    return BadRequest("Failed to delete the camp");
        //}
    }
}
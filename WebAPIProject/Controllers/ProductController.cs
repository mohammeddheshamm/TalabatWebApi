using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using TalabatAPIS.Dtos;
using TalabatAPIS.Errors;
using TalabatAPIS.Helpers;

namespace TalabatAPIS.Controllers
{
    
    public class ProductController : BaseApiController
    {
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<ProductBrand> _brandRepo;
        //private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(
            IUnitOfWork unitOfWork
            // IGenericRepository<Product> productRepo 
            //,IGenericRepository<ProductBrand> brandRepo
            //,IGenericRepository<ProductType> typesRepo
            ,IMapper mapper)
        {
            //_productRepo = productRepo;
            //_brandRepo = brandRepo;
            //_typesRepo = typesRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        [HttpGet] // Get : api/Product
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(productParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFiltersForCountSpecifications(productParams);

            var Count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,Count,Data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product , ProductToReturnDto>(product));
        }

        [HttpGet("brands")] //GET : api/Product/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")] //GET : api/Product/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        // This constructor is used to Get all Products
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams productParams) :base(
            P => 
                (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search))&&
                (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) &&
                (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value))
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            ApplyPagination(productParams.PageSize * (productParams.PageIndex -1) , productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                //Fy 7aleet anha m4 null hina ma3naha aniha gaya b value aw way to sort fa han sort hinaa
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name); 
                        break;
                }
            }

        }
        // This Constructor is used to get Specific Product by Id
        public ProductWithBrandAndTypeSpecifications(int Id):base(P => P.Id == Id) 
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

    }
}

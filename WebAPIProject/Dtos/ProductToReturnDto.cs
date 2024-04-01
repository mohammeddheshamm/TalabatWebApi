using Talabat.Core.Entities;

namespace TalabatAPIS.Dtos
{
    //Dto : Data Transfer Object
    // This is the object which carry the data from the database to the Front end
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }
}

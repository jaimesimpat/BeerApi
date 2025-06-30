using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class BeerInsertDto
    {
        public string Name { get; set; }
        public int BrandID { get; set; }
        public decimal Alcohol { get; set; }
        public BeerType BeerType { get; set; }
    }
}

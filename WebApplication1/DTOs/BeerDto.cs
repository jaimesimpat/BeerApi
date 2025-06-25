using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class BeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }
        public decimal Alcohol { get; set; }
        public BeerType BeerType { get; set; }
        public string BeerTypeText { get { return BeerType.ToString(); } }
    }
}

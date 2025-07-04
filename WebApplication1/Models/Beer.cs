﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }
        public BeerType BeerType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Alcohol { get; set; }

        [ForeignKey(nameof(BrandID))]
        public virtual Brand Brand { get; set; }
    }

    public enum BeerType 
    {
        Lager,
        Ale,
        Stout,
        Porter,
        Pilsner,
        Wheat,
        Sour,
        IPA,
        Saison
    }

}

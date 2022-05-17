using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.Domain.Enum
{
    public enum Category
    {
        [Display(Name = "Головной убор")]
        Headdress = 0,
        [Display(Name = "Футболка")]
        TShirt = 1,
        [Display(Name = "Штаны")]
        Trousers = 2,
        [Display(Name = "Трусы")]
        Underpants = 3,
        [Display(Name = "Куртка")]
        Jacket = 4,
        [Display(Name = "Рубашка")]
        Shirt = 5


    }
}

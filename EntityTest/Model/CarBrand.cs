using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTest.Model
{
    public class CarBrand
    {
        private string _brand;
        
        public int Id { get; set; }  
        
        public int CountryProducingId { get; set; }
        [ConcurrencyCheck] 
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string Brand {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
            }
        } 

        [ConcurrencyCheck]
        public byte[] Logo { get; set; }

        public CountryProducing CountryProducings { get; set; } 
    }
}

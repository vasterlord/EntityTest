using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityTest.Model
{
   public class CountryProducing
    {
        private string _country; 
        public int Id { get; set; }
         
        /// <summary>
        /// How disable auto sorting after create unique key
        /// </summary>
        [ConcurrencyCheck]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Field can not be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }
    }
}

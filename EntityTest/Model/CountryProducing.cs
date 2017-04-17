using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace EntityTest.Model
{
   public class CountryProducing
    {
        private string _country;
        public int Id { get; set; } 
        /// <summary>
        /// Question: How disable auto sorting after create unique key
        /// </summary>
        [ConcurrencyCheck]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string CountryName
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

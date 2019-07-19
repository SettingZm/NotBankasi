using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Not_Bankası.Models.iletisim
{
    public class iletisimSinifi
    {
        [Required, Display(Name = "İsminiz ")]
        public string isim { get; set; }
        [Required, Display(Name = "Mail Adresiniz"), EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Konu { get; set; }
        [Required]
        public string Mesaj { get; set; }

    }
}
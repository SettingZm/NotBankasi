using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Not_Bankası.Controllers.UyeKontrolleri
{
    public class UyeKontrol
    {

        [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
        [Display(Name = "Adınız")]
        public string Isim { get; set; }

        [Required(ErrorMessage = "Lütfen Soyadınız Giriniz")]
        [Display(Name = "Soyadınız")]
        public string Soyisim { get; set; }


        [Required(ErrorMessage = "Lütfen Email Adresinizi Giriniz")]
        [Display(Name = "Email")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Email Adresiniz Doğru Biçinde Giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Giriniz")]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Onaylama Şifresini Giriniz")]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre Onayla")]
        [Compare("Sifre", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string SifreOnay { get; set; }
    }
}
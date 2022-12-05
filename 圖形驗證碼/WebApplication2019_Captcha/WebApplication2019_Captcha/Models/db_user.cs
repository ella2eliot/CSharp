using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;


namespace WebApplication2019_Captcha.Models
{

    public partial class db_user
    {
        [Key]
        public int ID { get; set; }

        //[Required]
        [StringLength(20)]
        [Display(Name = "帳號")]
        public string UserName { get; set; }

        //[Required]
        [StringLength(50)]
        [DataType(DataType.Password)]   // 輸入密碼時，隱匿文字 （以 *符號 代替）
        [Display(Name = "密碼")]
        public string UserPassword { get; set; }

        //*********************************************
        [NotMapped]
        //[Required]
        public string ValidatePictureCode { get; set; }   // 圖形驗證碼
        //*********************************************

        public int UserRank { get; set; }

        [StringLength(1)]
        public string UserApproved { get; set; }
    }
}

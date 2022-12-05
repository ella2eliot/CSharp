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
        [Display(Name = "�b��")]
        public string UserName { get; set; }

        //[Required]
        [StringLength(50)]
        [DataType(DataType.Password)]   // ��J�K�X�ɡA���Τ�r �]�H *�Ÿ� �N���^
        [Display(Name = "�K�X")]
        public string UserPassword { get; set; }

        //*********************************************
        [NotMapped]
        //[Required]
        public string ValidatePictureCode { get; set; }   // �ϧ����ҽX
        //*********************************************

        public int UserRank { get; set; }

        [StringLength(1)]
        public string UserApproved { get; set; }
    }
}

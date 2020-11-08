﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MainMusicStore.Models.DbModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Category name is required!")]
        [StringLength(50,MinimumLength = 3, ErrorMessage = "Lütfen uygun bir Category giriniz")]
        public string  CategoryName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o login!")]
        public String Login { get; set; }
        [Required(ErrorMessage = "Digite a senha!")]
        public String Passwd { get; set; }
    }
}
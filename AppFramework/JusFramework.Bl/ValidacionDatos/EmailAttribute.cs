﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JusFramework.Bl.ValidacionDatos
{
    public class EmailAttribute : RegularExpressionAttribute
    {
         public EmailAttribute()
             :base(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}")

         {
             this.ErrorMessage = "POR FAVOR INGRESE UNA DIRECCIÓN DE CORREO ELECTRÓNICA VALIDA";
         }
    }
}
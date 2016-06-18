using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JusFramework.Bl.ValidacionDatos
{
    public class PersonaNombreAttribute : RegularExpressionAttribute
    {
        public PersonaNombreAttribute()
            : base(@"[A-Z]+[a-zA-Z\s]*[a-zA-Z]+")
         {
             this.ErrorMessage = "POR FAVOR INGRESE UN NOMBRE VALIDO";
            
         }
    }
}

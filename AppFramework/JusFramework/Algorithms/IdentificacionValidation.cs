using System;
using System.Linq;

namespace JusFramework.Algorithms
{
    public class IdentificacionValidation
    {
        public static bool VerificarCedula(string cedula)
        {
            if (cedula.Length != 10)
            {
                return false;
            }
            if (cedula.Any(r => !char.IsDigit(r)))
            {
                return false;
            }
            return IsValid(cedula);
        }

        public static bool VerificarRuc(string ruc)
        {
            if (ruc.Length != 13)
            {
                return false;
            }
            if (ruc.Any(r => !char.IsDigit(r)))
            {
                return false;
            }
            return VerificarCedula(ruc.Substring(1, 10));
        }

        private static bool IsValid(string cedula)
        {
            if (cedula == null)
                return false;
            string[] array = new string[10];
            int num = cedula.Length;
            if (num == 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    array[i] = cedula.Substring(i, 1);
                }

                int total = 0;
                int digito = (Convert.ToInt32(array[9])*1);
                for (int i = 0; i < (num - 1); i++)
                {
                    if ((i%2) != 0)
                    {
                        total = total + (Convert.ToInt32(array[i])*1);
                    }
                    else
                    {
                        var mult = Convert.ToInt32(array[i])*2;
                        if (mult > 9)
                            total = total + (mult - 9);
                        else
                            total = total + mult;
                    }
                }
                // ReSharper disable once PossibleLossOfFraction
                double decena = total/10;
                decena = Math.Floor(decena);
                decena = (decena + 1)*10;
                int final = ((int)decena - total);

                return (final == 10 && digito == 0) || (final == digito);
            }
            return false;
        }
    }
}
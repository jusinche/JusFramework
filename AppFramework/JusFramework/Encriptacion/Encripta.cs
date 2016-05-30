using System;
using System.Security.Cryptography;
using System.Text;
using JusFramework.Excepciones;

namespace JusFramework.Encriptacion
{
    public class Encripta : IEncripta
    {
        protected string Key { get; set; }



        public Encripta(string cadEncripta)
        {
            Key = cadEncripta;
        }
        

        public string EncryptKey(string cadenaEncriptar)
        {
            string cadena = StrInverir(cadenaEncriptar);
            //arreglo de bytes donde guardaremos la llave
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] arregloACifrar =
                Encoding.UTF8.GetBytes(cadena);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            var hashmd5 =
                new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            byte[] keyArray = hashmd5.ComputeHash(
                Encoding.UTF8.GetBytes(Key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            var tdes =
                new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
                tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] arrayResultado =
                cTransform.TransformFinalBlock(arregloACifrar,
                                               0, arregloACifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(arrayResultado,
                                          0, arrayResultado.Length);

        }

        public string DecryptKey(string clave)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] arrayADescifrar =
                Convert.FromBase64String(clave);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
                new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(
                Encoding.UTF8.GetBytes(Key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform =
                tdes.CreateDecryptor();

            byte[] resultArray =
                cTransform.TransformFinalBlock(arrayADescifrar,
                                               0, arrayADescifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return StrInverir(Encoding.UTF8.GetString(resultArray));
        }

        public string HashKey(string cadena)
        {
            if (cadena.Length==0)
            {
                throw new JusException("No se puede encriptar una cadena vacia");
            }
            cadena = Key+cadena+cadena[cadena.Length - 1] + cadena[0] + StrInverir(cadena);
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(cadena);
            byte[] hash=md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        protected string StrInverir(string cadena)
        {
            var cadenaAux = string.Empty;
            for (var i = cadena.Length-1; i >=0 ; i--)
            {
                cadenaAux = cadenaAux+cadena[i];
            }
            return cadenaAux;
        }

    }
}

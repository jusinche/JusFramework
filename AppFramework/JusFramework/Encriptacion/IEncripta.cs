namespace JusFramework.Encriptacion
{
    public interface IEncripta
    {
        string EncryptKey(string cadenaEncriptar);
        string DecryptKey(string cadenaDesencriptar);

        string HashKey(string cadenaHash);
    }
}

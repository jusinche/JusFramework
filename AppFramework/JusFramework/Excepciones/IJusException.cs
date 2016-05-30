using System;
using System.Runtime.InteropServices;

namespace JusFramework.Excepciones
{
    public interface IJusException : _Exception
    {
        string MsjAmigable { get;}
        void ExManager(Exception ex);
    }
}

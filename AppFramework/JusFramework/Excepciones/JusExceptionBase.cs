using System;

namespace JusFramework.Excepciones
{
    public abstract class JusExceptionBase : Exception, IJusException
    {
        public JusExceptionBase(string msjAmigable, string mensaje)
            : base(mensaje)
        {
            _msjAmigable = msjAmigable;
        }

        private string _msjAmigable;
        public string MsjAmigable { get { return _msjAmigable; }}

        protected void SetMensajeAmigable(string msj)
        {
            _msjAmigable = msj;
        }

        public abstract void ExManager(Exception ex);
    }
}
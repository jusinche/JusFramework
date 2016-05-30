using System;

namespace JusFrontal.UI.ManejadorExcepciones
{
    public interface IManejadorMensajes
    {
        string GetMensaje(Exception ex);
    }
    public class ManejadorMensajes : IManejadorMensajes
    {
        public string GetMensaje(Exception ex)
        {
            return "Ocurrio un error inesperado intentelo mas tarde";
        }
    }
}

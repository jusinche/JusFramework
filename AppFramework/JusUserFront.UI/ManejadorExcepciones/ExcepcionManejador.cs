using System;
using JusUserFront.UI.ManejadorExcepciones;
using Microsoft.Practices.ServiceLocation;

namespace JusUserFront.UI.ManejadorExcepciones
{
    public interface IExcepcionManejador
    {
        ExceptionManejadorResult Manejador(Exception ex);
    }

    public class ExcepcionManejador: IExcepcionManejador
    {

        private readonly IManejadorMensajes _manejadorMensajes = ServiceLocator.Current.GetInstance<IManejadorMensajes>();
        public ExceptionManejadorResult Manejador(Exception ex)
        {
            var result = new ExceptionManejadorResult();
            LogToElmah(ex);
            result.Message= _manejadorMensajes.GetMensaje(ex);
            result.Code = ex.HResult;
            return result;
        }

        public static void LogToElmah(Exception ex)
        {
           /* if (HttpContext.Current != null)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            else
            {
                ErrorLog errorLog = ErrorLog.GetDefault(null);
                errorLog.ApplicationName = ConfigurationManager.AppSettings[ConstantesFramework.AplicacionNombre];
                errorLog.Log(new Error(ex));
            }*/
        }
    }

    

        

        
}

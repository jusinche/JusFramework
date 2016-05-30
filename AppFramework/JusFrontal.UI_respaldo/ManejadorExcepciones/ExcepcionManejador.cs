using System;
using System.Configuration;
using System.Web;
using Elmah;
using JusFramework;
using Microsoft.Practices.ServiceLocation;

namespace JusFrontal.UI.ManejadorExcepciones
{
    public interface IExcepcionManejador
    {
        JusFrontal.UI.ManejadorExcepciones.ExceptionManejadorResult Manejador(Exception ex);
    }

    public class ExcepcionManejador: IExcepcionManejador
    {

        private readonly JusFrontal.UI.ManejadorExcepciones.IManejadorMensajes _manejadorMensajes = ServiceLocator.Current.GetInstance<JusFrontal.UI.ManejadorExcepciones.IManejadorMensajes>();
        public JusFrontal.UI.ManejadorExcepciones.ExceptionManejadorResult Manejador(Exception ex)
        {
            var result = new JusFrontal.UI.ManejadorExcepciones.ExceptionManejadorResult();
            LogToElmah(ex);
            result.Message= _manejadorMensajes.GetMensaje(ex);
            result.Code = ex.HResult;
            return result;
        }

        public static void LogToElmah(Exception ex)
        {
            if (HttpContext.Current != null)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            else
            {
                ErrorLog errorLog = ErrorLog.GetDefault(null);
                errorLog.ApplicationName = ConfigurationManager.AppSettings[ConstantesFramework.AplicacionNombre];
                errorLog.Log(new Error(ex));
            }
        }
    }

    

        

        
}

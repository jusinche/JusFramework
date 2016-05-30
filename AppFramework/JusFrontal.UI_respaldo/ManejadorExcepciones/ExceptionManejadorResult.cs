namespace JusFrontal.UI.ManejadorExcepciones
{
    public class ExceptionManejadorResult
    {
        /// <summary>
        /// Mensaje amigable al usuario
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Codigo interno, para categorizar el resultado
        /// </summary>
        public int Code { get; set; }

    }
}

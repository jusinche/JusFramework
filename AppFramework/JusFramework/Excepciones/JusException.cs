using System;

namespace JusFramework.Excepciones
{
    public class JusException: JusExceptionBase
    {
        public override void ExManager(Exception ex)
        {
            
        }

        public JusException(string msjAmigable) : base(msjAmigable, msjAmigable)
        {
        }

        public JusException(string msjAmigable, string mensaje, Exception ex)
            : base(msjAmigable, msjAmigable)
        {
           
        }
    }
}

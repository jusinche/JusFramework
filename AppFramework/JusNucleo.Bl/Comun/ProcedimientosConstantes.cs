namespace JusNucleo.Bl.Comun
{
    public class ProcedimientosConstantes
    {
        //Persona
        private const string PkgNegPersona = "PKG_NEG_PERSONA.";
        public const string PrcPersonaIns = PkgNegPersona + "PRC_PERSONA_INS";
        public const string PrcPersonaAct = PkgNegPersona + "PRC_PERSONA_ACT";
        public const string PrcPersonaObt = PkgNegPersona + "PRC_PERSONA_OBT";
        public const string PrcPersonaDel = PkgNegPersona + "PRC_PERSONA_DEL";
        //fin persona

        //Cuenta de usuario
        private const string PkgCuenta = "PKG_SEG_CUENTA.";
        public const string PrcCuentaIns = PkgCuenta + "PRC_CUENTA_INS";
        public const string PrcCuentaAct = PkgCuenta + "PRC_CUENTA_ACT";
        public const string PrcCuentaObt = PkgCuenta + "PRC_CUENTA_OBT";
        public const string PrcCuentaDel = PkgCuenta + "PRC_CUENTA_DEL";
        public const string PrcCuentaObtCant = PkgCuenta + "PRC_CUENTA_OBT_CANT";
        
        
        //Fin cuenta de usuario

        //Cuenta de usuario
        private const string PkgCorreo = "PKG_NEG_CORREO.";
        public const string PrcCorreoIns = PkgCorreo + "PRC_CORREO_INS";
        public const string PrcCorreoAct = PkgCorreo + "PRC_CORREO_ACT";
        public const string PrcCorreoObt = PkgCorreo + "PRC_CORREO_OBT";
        public const string PrcCorreoDel = PkgCorreo + "PRC_CORREO_DEL";
        public const string PrcCorreoCant = PkgCorreo + "PRC_CORREO_CANT";

        #region Catalogo 

        private const string PkgCatalogo = "PKG_ADM_CATALOGO.";
        public const string PrcItemCataloObt = PkgCatalogo+"PRC_ITEM_CATALOGO_OBT";

        #endregion Catalogo

        #region Seguidad
        private const string PKG_SEG_SEGURIDAD="PKG_SEG_SEGURIDAD.";
        public const string PRC_FUNCIONALIDAD_MENU_OBT=PKG_SEG_SEGURIDAD+"PRC_FUNCIONALIDAD_MENU_OBT";
        #endregion

    }
}

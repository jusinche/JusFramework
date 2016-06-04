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

        private const string PkgCuenta = "PKG_NEG_PERSONA.";
        public const string PrcCuentaIns = PkgCuenta + "PRC_CUENTA_INS";
        public const string PrcCuentaAct = PkgCuenta + "PRC_CUENTA_ACT";
        public const string PrcCuentaObt = PkgCuenta + "PRC_CUENTA_OBT";
        public const string PrcCuentaDel = PkgCuenta + "PRC_CUENTA_DEL";
        public const string PrcCuentaObtCant = PkgCuenta + "PRC_CUENTA_OBT_CANT";

        
        //Fin cuenta de usuario

        #region Catalogo 

        private const string PkgCatalogo = "PKG_ADM_CATALOGO.";
        public const string PrcItemCataloObt = PkgCatalogo+"PRC_ITEM_CATALOGO_OBT";

        #endregion Catalogo

    }
}

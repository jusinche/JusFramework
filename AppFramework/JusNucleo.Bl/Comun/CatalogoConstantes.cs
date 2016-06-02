using System.Globalization;
using System.IdentityModel.Metadata;

namespace JusNucleo.Bl.Comun
{
    public class CatalogoConstantes
    {
        #region persona

        #region Tipo Identificacion

        public const string CatIdentificacionTipo = "IDENTIFICACIONTIPO";

        public const string IdentificacionCedula = "CEDULA";
        public const string IdentificacionRuc = "RUC";
        public const string IdentificacionPasaporte = "PASAPORTE";
        public const string IdentificacionExcepcion = "EXCEPCION";

        #endregion Tipo Identificacion

        #region Genero

        public const string CatGenero = "GENERO";

        public const string GeneroMasculino = "MASCULINO";
        public const string GeneroFemenino = "FEMENINO";

        #endregion Genero

        #region Estado civil

        public const string CatEstadoCivil = "ESTADOCIVIL";

        public const string EstadoCivilSoltero = "SOLTERO";
        public const string EstadoCivilCasado = "CASADO";
        public const string EstadoCivilDivorciado = "DIVORCIADO";
        public const string EstadoCivilUnionlibre = "UNIONLIBRE";
        public const string EstadoCivilViudo = "VIUDO";



        #endregion Estado civil

        #region Tipo persona

        public const string CatPersonaTipo = "PERSONATIPO";
        public const string PersonaNatural = "PERNATURAL";
        public const string PersonaJuridica = "PERJURIDICA";


        #endregion Tipo persona

        #endregion persona

    }
}

using System;
using System.Data;
using JusFramework.Bl;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNaturalList :JusReadOnlyListBase<PersonaNaturalList, PersonaNaturalListInfo>
    {

        #region Factory Methods

        //public static PersonaNaturalList Get(PersonaNaturalCriteria criteria)
        //{
        //    return DataPortal.Fetch<PersonaNaturalList>(criteria);
        //}

        private PersonaNaturalList()
        { /* require use of factory methods */ }

        #endregion

        protected override string NombreProcedimiento
        {
            get { return ProcedimientosConstantes.PrcPersonaObt; }
        }

        protected void AddParameterCriteria(PersonaNaturalCriteria criteria)
        {
            Db.AddParameterWithValue(Comando, "ec_identificacion", DbType.String, criteria.Identificacion);
            Db.AddParameterWithValue(Comando, "ec_primer_nombre", DbType.String, criteria.PrimerNombre);
            Db.AddParameterWithValue(Comando, "ec_segundo_nombre", DbType.String, criteria.SegundoNombre);
            Db.AddParameterWithValue(Comando, "ec_primer_apellido", DbType.String, criteria.PrimerApellido);
            Db.AddParameterWithValue(Comando, "ec_segundo_apellido", DbType.String, criteria.SegundoApellido);

            Db.AddParameter(Comando, "sq_resultado", DbType.Object, ParameterDirection.Output);
        }

    }

    [Serializable]
    public class PersonaNaturalListInfo : JusReadOnlyBase<PersonaNaturalListInfo>
    {
        #region Business Methods

        public int TipoIdentificacionId { get; private set; }
        public string TipoIdentificacion { get; private set; }
        public string Identificacion { get; private set; }
        public string PrimerNombre { get; private set; }
        public string SegundoNombre { get; private set; }
        public string PrimerApellido { get; private set; }
        public string SegundoApellido { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public int GeneroId { get; private set; }
        public string Genero { get; private set; }
        public int EstadoCivilId { get; private set; }
        public string EstadoCivil { get; private set; }


        #endregion


        #region Factory Methods



        private PersonaNaturalListInfo()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(IDataReader data)
        {
            Id = Convert.ToInt32(data["per_id"]);
            TipoIdentificacionId = Convert.ToInt32(data["per_tipo_identificacion"]);
            TipoIdentificacion = data["tipo_identificacion"].ToString();
            Identificacion = data["per_identificacion"].ToString();
            PrimerNombre = data["PER_PRIMER_NOMBRE"].ToString();
            SegundoNombre = data["PER_SEGUNDO_NOMBRE"].ToString();
            PrimerApellido = data["PER_PRIMER_APELLIDO"].ToString();
            SegundoApellido = data["PER_SEGUNDO_APELLIDO"].ToString();
            GeneroId = Convert.ToInt32(data["PER_GENERO"]);
            Genero = data["genero"].ToString();
            FechaNacimiento = Convert.ToDateTime(data["PER_FECHA_NACIMIENTO"]);
            EstadoCivilId = Convert.ToInt32(data["PER_ESTADO_CIVIL"]);
            EstadoCivil = data["estado_civil"].ToString();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JusFramework.Bl;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNaturalList :JusReadOnlyListBase<PersonaNaturalList, PersonaNaturalInfo>
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

        protected override Type[] RootClass
        {
            get { return new []{typeof(PersonaNatural)};}
        }

        protected override List<PersonaNaturalInfo> OrdenarList(List<PersonaNaturalInfo> lista)
        {
            return lista.OrderBy(x => x.PrimerApellido).ThenBy(x => x.SegundoApellido).ThenBy(x => x.PrimerNombre).ToList();
        }


        protected void AddParameterCriteria(PersonaNaturalCriteria criteria)
        {
            Db.AddParameterWithValue(Comando, "ec_identificacion", DbType.String, criteria.Identificacion);
            Db.AddParameterWithValue(Comando, "ec_primer_nombre", DbType.String, criteria.PrimerNombre);
            Db.AddParameterWithValue(Comando, "ec_segundo_nombre", DbType.String, criteria.SegundoNombre);
            Db.AddParameterWithValue(Comando, "ec_primer_apellido", DbType.String, criteria.PrimerApellido);
            Db.AddParameterWithValue(Comando, "ec_segundo_apellido", DbType.String, criteria.SegundoApellido);
        }

    }

    [Serializable]
    public class PersonaNaturalInfo : JusReadOnlyBase<PersonaNaturalInfo>
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



        private PersonaNaturalInfo()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        protected void Child_Fetch(IDataReader data)
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

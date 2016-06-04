using System;
using System.Data;
using System.Linq;
using Csla;
using JusFramework.Bl;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNatural : JusBusinessBaseERoot<PersonaNatural>
    {
        #region Business Methods

        // TODO: add your own fields, properties and methods

        // example with private backing field
        /*public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, RelationshipTypes.PrivateField);
        private int _Id = IdProperty.DefaultValue;
        public int Id
        {
            get { return GetProperty(IdProperty, _Id); }
            set { SetProperty(IdProperty, ref _Id, value); }
        }
        */

        // example with managed backing field
        /*
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
         */

        public static readonly PropertyInfo<int> TipoIdentificacionProperty = RegisterProperty<int>(p => p.TipoIdentificacion);
        public int TipoIdentificacion
        {
            get { return GetProperty(TipoIdentificacionProperty); }
            set { SetProperty(TipoIdentificacionProperty, value); }
        }

        public static readonly PropertyInfo<string> IdenticacionProperty = RegisterProperty<string>(p => p.Identificacion);
        public string Identificacion
        {
            get { return GetProperty(IdenticacionProperty); }
            set { SetProperty(IdenticacionProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerNombreProperty = RegisterProperty<string>(p => p.PrimerNombre);
        public string PrimerNombre
        {
            get { return GetProperty(PrimerNombreProperty); }
            set { SetProperty(PrimerNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoNombreProperty = RegisterProperty<string>(p => p.SegundoNombre);
        public string SegundoNombre
        {
            get { return GetProperty(SegundoNombreProperty); }
            set { SetProperty(SegundoNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerApellidoProperty = RegisterProperty<string>(p => p.PrimerApellido);
        public string PrimerApellido
        {
            get { return GetProperty(PrimerApellidoProperty); }
            set { SetProperty(PrimerApellidoProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoApellidoProperty = RegisterProperty<string>(p => p.SegundoApellido);
        public string SegundoApellido
        {
            get { return GetProperty(SegundoApellidoProperty); }
            set { SetProperty(SegundoApellidoProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> FechaNacimientoProperty = RegisterProperty<DateTime>(p => p.FechaNacimiento);
        public DateTime FechaNacimiento
        {
            get { return GetProperty(FechaNacimientoProperty); }
            set { SetProperty(FechaNacimientoProperty, value); }
        }

        public static readonly PropertyInfo<int> GeneroProperty = RegisterProperty<int>(p => p.Genero);
        public int Genero
        {
            get { return GetProperty(GeneroProperty); }
            set { SetProperty(GeneroProperty, value); }
        }

        public static readonly PropertyInfo<int> EstadoCivilProperty = RegisterProperty<int>(p => p.EstadoCivil);
        public int EstadoCivil
        {
            get { return GetProperty(EstadoCivilProperty); }
            set { SetProperty(EstadoCivilProperty, value); }
        }

        public int TipoPersona
        {
            get { return CatalogoItemList.Get(CatalogoConstantes.CatPersonaTipo).First(x => x.Codigo == CatalogoConstantes.PersonaNatural).Id; }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

       

        #endregion

        #region Factory Methods

        

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<PersonaNatural>(id);
        }

        private PersonaNatural()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }

        

        protected override string ObtenerSp
        {
            get { return ProcedimientosConstantes.PrcPersonaObt; }
        }

        protected override string ActualizarSp
        {
            get { return ProcedimientosConstantes.PrcPersonaAct; }
        }

        protected override string InsertarSp
        {
            get { return ProcedimientosConstantes.PrcPersonaIns; }
        }

        protected override string BorrarSp
        {
            get { return ProcedimientosConstantes.PrcPersonaDel; }
        }

        
       

        protected override void AddCommonParameters()
        {
            Db.AddParameterWithValue(Comando, "ec_primer_nombre", DbType.String, PrimerNombre);
            Db.AddParameterWithValue(Comando, "ec_segundo_nombre", DbType.String, SegundoNombre);
            Db.AddParameterWithValue(Comando, "ec_primer_apellido", DbType.String, PrimerApellido);
            Db.AddParameterWithValue(Comando, "ec_segundo_apellido", DbType.String, SegundoApellido);
            Db.AddParameterWithValue(Comando, "ef_fecha_nac", DbType.DateTime, FechaNacimiento);
            Db.AddParameterWithValue(Comando, "en_genero", DbType.Int32, Genero);
            Db.AddParameterWithValue(Comando, "en_estado_civil", DbType.Int16, EstadoCivil);
        }



        protected override void AddInsertParameters()
        {
            Db.AddParameterWithValue(Comando, "en_tipo_identificacion", DbType.Int32, TipoIdentificacion);
            Db.AddParameterWithValue(Comando, "ec_identificacion", DbType.String, Identificacion);
            Db.AddParameterWithValue(Comando, "en_tipo_persona", DbType.Int16, TipoPersona);
            
            base.AddInsertParameters();

        }


        protected override void Fetch(IDataReader dr)
        {
            TipoIdentificacion = Convert.ToInt32(dr["per_tipo_identificacion"]);
            Identificacion = dr["per_identificacion"].ToString();
            PrimerNombre = dr["PER_PRIMER_NOMBRE"].ToString();
            SegundoNombre = dr["PER_SEGUNDO_NOMBRE"].ToString();
            PrimerApellido = dr["PER_PRIMER_APELLIDO"].ToString();
            SegundoApellido = dr["PER_SEGUNDO_APELLIDO"].ToString();
            Genero = Convert.ToInt32(dr["PER_GENERO"]);
            FechaNacimiento = Convert.ToDateTime(dr["PER_FECHA_NACIMIENTO"]);
            EstadoCivil = Convert.ToInt32(dr["PER_ESTADO_CIVIL"]);
        }

        

        #endregion
    }
}

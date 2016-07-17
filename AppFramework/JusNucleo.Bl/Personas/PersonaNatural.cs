using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Csla;
using JusFramework.Algorithms;
using JusFramework.Bl;
using JusFramework.Bl.Rules;
using JusFramework.Bl.ValidacionDatos;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNatural : JusBusinessBaseERoot<PersonaNatural>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> TipoIdentificacionProperty = RegisterProperty<int>(p => p.TipoIdentificacion);
        [CampoObligatorio]
        [Display(Name = "Tipo Identificación")]
        public int TipoIdentificacion
        {
            get { return GetProperty(TipoIdentificacionProperty); }
            set { SetProperty(TipoIdentificacionProperty, value); }
        }

        public static readonly PropertyInfo<string> IdenticacionProperty = RegisterProperty<string>(p => p.Identificacion);
        [CampoObligatorio]
        [Display(Name = "Identificación")]
        public string Identificacion
        {
            get { return GetProperty(IdenticacionProperty); }
            set { SetProperty(IdenticacionProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerNombreProperty = RegisterProperty<string>(p => p.PrimerNombre);
        [CampoObligatorio]
        [PersonaNombre]
        [Display(Name = "Primer Nombre")]
        //[Mayusculas]
        public string PrimerNombre
        {
            get { return GetProperty(PrimerNombreProperty); }
            set { SetProperty(PrimerNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoNombreProperty = RegisterProperty<string>(p => p.SegundoNombre);
        [PersonaNombre]
        //[Mayusculas]
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre
        {
            get { return GetProperty(SegundoNombreProperty); }
            set { SetProperty(SegundoNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerApellidoProperty = RegisterProperty<string>(p => p.PrimerApellido);
        [PersonaNombre]
        //[Mayusculas]
        [Display(Name = "Primer Apellido")]
        [CampoObligatorio]
        public string PrimerApellido
        {
            get { return GetProperty(PrimerApellidoProperty); }
            set { SetProperty(PrimerApellidoProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoApellidoProperty = RegisterProperty<string>(p => p.SegundoApellido);
         [PersonaNombre]
         //[Mayusculas]
         [Display(Name = "Segundo Apellido")]
        public string SegundoApellido
        {
            get { return GetProperty(SegundoApellidoProperty); }
            set { SetProperty(SegundoApellidoProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> FechaNacimientoProperty = RegisterProperty<DateTime>(p => p.FechaNacimiento);
        [CampoObligatorio]
        [Display(Name = "Fecha Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento
        {
            get { return GetProperty(FechaNacimientoProperty); }
            set { SetProperty(FechaNacimientoProperty, value); }
        }

        public static readonly PropertyInfo<int> GeneroProperty = RegisterProperty<int>(p => p.Genero);
        [CampoObligatorio]
        [Display(Name = "Género")]
        public int Genero
        {
            get { return GetProperty(GeneroProperty); }
            set { SetProperty(GeneroProperty, value); }
        }

        public static readonly PropertyInfo<int> EstadoCivilProperty = RegisterProperty<int>(p => p.EstadoCivil);
        [CampoObligatorio]
        [Display(Name = "Estado Civil")]
        public int EstadoCivil
        {
            get { return GetProperty(EstadoCivilProperty); }
            set { SetProperty(EstadoCivilProperty, value); }
        }

        public int TipoPersona
        {
            get { return CatalogoItemList.Get(CatalogoConstantes.CatPersonaTipo).GetItem(CatalogoConstantes.PersonaNatural).Id; }
        }



        private static PropertyInfo<PersonaCorreos> CorreosProperty = RegisterProperty<PersonaCorreos>(o => o.Correos);
        /// <summary>
        /// Lista de los Elementos de Asignacion para la beca
        /// </summary>
        [Display(Name="Correos Electrónicos")]
        public PersonaCorreos Correos
        {
            get
            {
                if (!(FieldManager.FieldExists(CorreosProperty)))
                //if (GetProperty(CorreosProperty) == null)
                {
                    if (IsNew)
                        LoadProperty(CorreosProperty,
                                     PersonaCorreos.New());
                    else
                        LoadProperty(CorreosProperty,
                                     PersonaCorreos.Get(Id));
                }
                return GetProperty(CorreosProperty);
            }
        }
        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
           base.AddBusinessRules();
           //Regla para verificar q la lista de correo no esta vacia
           BusinessRules.AddRule(new ListaConElementosRule(CorreosProperty));
           BusinessRules.AddRule(new ValidateRule<PersonaNatural>(VerificarIdentificacion, IdenticacionProperty,TipoIdentificacionProperty));
           BusinessRules.AddRule(new ValidateRule<PersonaNatural>(IdentificacionDuplicada, IdenticacionProperty));
        }

        private bool IdentificacionDuplicada(PersonaNatural obj, RuleContextArg args)
        {
            if (string.IsNullOrEmpty(obj.Identificacion))
            {
                return true;
            }
            string msj;
            if (CodigoDuplicadoCmd.Exists(obj.Id, obj.Identificacion, ProcedimientosConstantes.PrcCorreoCant, out msj))
            {
                return true;
            }
            args.Description = msj;
            return false;
        }

        private bool VerificarIdentificacion(PersonaNatural obj, RuleContextArg args)
        {
            args.Description = "Número de Identificación es Invalido";
            if (obj.TipoIdentificacion==0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(obj.Identificacion))
            {
                return true;
            }
            var tipos = CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo);
            var cedula = tipos.GetItem(CatalogoConstantes.IdentificacionCedula);
            if (obj.TipoIdentificacion==cedula.Id)
            {
                return IdentificacionValidation.VerificarCedula(obj.Identificacion);
            }
            var ruc = tipos.GetItem(CatalogoConstantes.IdentificacionRuc);
            if (obj.TipoIdentificacion == ruc.Id)
            {
                return IdentificacionValidation.VerificarRuc(obj.Identificacion);
            }
            return true;
        }


        protected override void OnChildChanged(Csla.Core.ChildChangedEventArgs e)
        {
            base.OnChildChanged(e);
            if (e.ChildObject is PersonaCorreos)
            {
                //Recheck our rules.
                BusinessRules.CheckRules(CorreosProperty);
                //OnPropertyChanged(CorreosProperty);
            }
           

        }

        #endregion

        #region Factory Methods

         private PersonaNatural()
        { /* require use of factory methods */ }
        

       

        //private PersonaNatural()
        //{ /* Require use of factory methods */ }

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

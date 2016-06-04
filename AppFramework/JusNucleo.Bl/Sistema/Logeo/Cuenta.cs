using System;
using System.Data;
using Csla;
using JusFramework.Bl;
using JusFramework.Enumeradores;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Sistema.Logeo
{
    [Serializable]
    public class Cuenta : JusBusinessBaseERoot<Cuenta> 
    {
        #region Business Methods

        

        public static readonly PropertyInfo<int?> PersonaIdProperty = RegisterProperty<int?>(p => p.PersonaId, RelationshipTypes.PrivateField);
        private int? _personaId = PersonaIdProperty.DefaultValue;
        public int? PersonaId
        {
            get { return GetProperty(PersonaIdProperty, _personaId); }
            set { SetProperty(PersonaIdProperty, ref _personaId, value); }
        }

        public static readonly PropertyInfo<DateTime> UltimoAccesoProperty = RegisterProperty<DateTime>(p => p.UltimoAcceso);
        public DateTime UltimoAcceso
        {
            get { return GetProperty(UltimoAccesoProperty); }
            set { SetProperty(UltimoAccesoProperty, value); }
        }

        public static readonly PropertyInfo<int> AccesosProperty = RegisterProperty<int>(p => p.Accesos, RelationshipTypes.PrivateField);
        private int _accesos = AccesosProperty.DefaultValue;
        public int Accesos
        {
            get { return GetProperty(AccesosProperty, _accesos); }
            set { SetProperty(AccesosProperty, ref _accesos, value); }
        }

        public static readonly PropertyInfo<string> LoginProperty = RegisterProperty<string>(p => p.Login);
        public string Login
        {
            get { return GetProperty(LoginProperty); }
            set { SetProperty(LoginProperty, value); }
        }


        public static readonly PropertyInfo<int> EstadoProperty = RegisterProperty<int>(p => p.Estado, RelationshipTypes.PrivateField);
        private int _estadoId = EstadoProperty.DefaultValue;
        public int Estado
        {
            get { return GetProperty(EstadoProperty, _estadoId); }
            set { SetProperty(EstadoProperty, ref _estadoId, value); }
        }

        public static readonly PropertyInfo<bool> EnLineaProperty = RegisterProperty<bool>(p => p.EnLinea);
        public bool EnLinea
        {
            get { return GetProperty(EnLineaProperty); }
            set { SetProperty(EnLineaProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> CambioClaveProperty = RegisterProperty<DateTime>(p => p.CambioClave);
        public DateTime CambioClave
        {
            get { return GetProperty(CambioClaveProperty); }
            set { SetProperty(CambioClaveProperty, value); }
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

        

        public static Cuenta Get(string usuario)
        {
            return DataPortal.Fetch<Cuenta>(usuario);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<Cuenta>(id);
        }

        private Cuenta()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected void AddParameterCriteria(string criteria)
        {
            Db.AddParameterWithValue(Comando, "ec_usuario", DbType.String, criteria);
        }


        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }


        protected override string ObtenerSp
        {
            get { return ProcedimientosConstantes.PrcCuentaObt; }
        }

        protected override string ActualizarSp
        {
            get { return ProcedimientosConstantes.PrcCuentaAct; }
        }

        protected override string InsertarSp
        {
            get { throw new NotImplementedException(); }
        }

        protected override string BorrarSp
        {
            get { throw new NotImplementedException(); }
        }


        protected override void Fetch(IDataReader dr)
        {
            
            var perId=dr["PER_ID"];
            if (perId != null && perId!=DBNull.Value)
            {
                PersonaId = Convert.ToInt32(perId);
            }
            UltimoAcceso=Convert.ToDateTime(dr["CUE_ULTIMO_ACCESO"]);
            Login=dr["CUE_LOGIN"].ToString();
            Accesos = Convert.ToInt32(dr["CUE_ACCESOS"]);
            Estado=Convert.ToInt32(dr["CUE_ESTADO"]);
            EnLinea = (dr["CUE_EN_LINEA"].ToString()) == Boleano.S.ToString();
            CambioClave=Convert.ToDateTime(dr["CUE_CAMBIO_CLAVE"]);
        }

        protected override void AddCommonParameters()
        {
            Db.AddParameterWithValue(Comando, "en_per_id", DbType.Int32, PersonaId);
            Db.AddParameterWithValue(Comando, "ef_cue_ultimo_acceso", DbType.Date, UltimoAcceso);
            Db.AddParameterWithValue(Comando, "en_cue_estado", DbType.Int32, Estado);
            Db.AddParameterWithValue(Comando, "en_cue_accesos", DbType.Int32, Accesos);
            Db.AddParameterWithValue(Comando, "ec_cue_en_linea", DbType.String, EnLinea ? Boleano.S : Boleano.N);
            Db.AddParameterWithValue(Comando, "ec_login", DbType.String, Login);

        }

        

        #endregion
       
    }
}

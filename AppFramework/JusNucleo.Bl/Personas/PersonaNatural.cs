using System;
using System.Data;
using Csla;
using JusFramework.Bl;

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

        public static readonly PropertyInfo<int> TipoIdenticacionProperty = RegisterProperty<int>(p => p.TipoIdentificacion, RelationshipTypes.PrivateField);
        private int _tipoIdentificacion = TipoIdenticacionProperty.DefaultValue;
        public int TipoIdentificacion
        {
            get { return GetProperty(TipoIdenticacionProperty, _tipoIdentificacion); }
            set { SetProperty(TipoIdenticacionProperty, ref _tipoIdentificacion, value); }
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

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
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

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        protected override string ObtenerSp
        {
            get { throw new NotImplementedException(); }
        }

        protected override string ActualizarSp
        {
            get { throw new NotImplementedException(); }
        }

        protected override string InsertarSp
        {
            get { throw new NotImplementedException(); }
        }

        protected override string BorrarSp
        {
            get { throw new NotImplementedException(); }
        }

        protected override void AddObjPost(IDataReader data)
        {
            throw new NotImplementedException();
        }

        protected override void AddCommonParameters()
        {
            throw new NotImplementedException();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // TODO: insert values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: update values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.TipoIdentificacion);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion
    }
}

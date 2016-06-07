using System;
using System.Collections.Generic;
using System.Data;
using Csla;
using JusFramework.Bl;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaCorreos : JusBusinessListBase<PersonaCorreos, PersonaCorreo>
    {
        #region Factory Methods

        //internal static PersonaCorreos NewEditableChildList()
        //{
        //    return DataPortal.CreateChild<PersonaCorreos>();
        //}

        //internal static PersonaCorreos GetEditableChildList(
        //  object childData)
        //{
        //    return DataPortal.FetchChild<PersonaCorreos>(childData);
        //}

        private PersonaCorreos()
        { }

        #endregion

        #region Data Access

        protected void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            foreach (var child in (IList<object>)childData)
                Add(PersonaCorreo.Get(child));
            RaiseListChangedEvents = true;
        }

        #endregion
    }

    [Serializable]
    public class PersonaCorreo : JusBusinessBaseEChild<PersonaCorreo>
    {
        #region Business Methods

        
        // example with managed backing field
        public static readonly PropertyInfo<string> CorreoProperty = RegisterProperty<string>(p => p.Correo);
        public string Correo
        {
            get { return GetProperty(CorreoProperty); }
            set { SetProperty(CorreoProperty, value); }
        }

        #endregion

        #region Business Rules

      
        #endregion

        #region Factory Methods

        //internal static PersonaCorreo NewEditableChild()
        //{
        //    return DataPortal.CreateChild<PersonaCorreo>();
        //}

        //internal static PersonaCorreo GetEditableChild(object childData)
        //{
        //    return DataPortal.FetchChild<PersonaCorreo>(childData);
        //}

        private PersonaCorreo()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        //protected override void Child_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //    base.Child_Create();
        //}
        /*

        private void Child_Fetch(object childData)
        {
            // TODO: load values
        }

        private void Child_Insert(object parent)
        {
            // TODO: insert values
        }

        private void Child_Update(object parent)
        {
            // TODO: update values
        }

        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }
        */
        #endregion

        protected override string ObtenerSp
        {
            get { return ProcedimientosConstantes.PrcCorreoObt; }
        }

        protected override string ActualizarSp
        {
            get { return ProcedimientosConstantes.PrcCorreoAct; }
        }

        protected override string InsertarSp
        {
            get { return ProcedimientosConstantes.PrcCorreoIns; }
        }

        protected override string BorrarSp
        {
            get { return ProcedimientosConstantes.PrcCorreoDel; }
        }

        protected override void AddCommonParameters()
        {
            Db.AddParameterWithValue(Comando, "ec_correo", DbType.String, Correo);
        }

        protected override void Fetch(IDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}
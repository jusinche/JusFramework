using System;
using System.Collections.Generic;
using System.Data;
using Csla;
using JusFramework.Bl;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaIdentificaciones :
      BusinessListBase<PersonaIdentificaciones, PersonaIdentificacion>
    {
        #region Factory Methods

        internal static PersonaIdentificaciones NewEditableChildList()
        {
            return DataPortal.CreateChild<PersonaIdentificaciones>();
        }

        internal static PersonaIdentificaciones GetEditableChildList(
          object childData)
        {
            return DataPortal.FetchChild<PersonaIdentificaciones>(childData);
        }

        private PersonaIdentificaciones()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            foreach (var child in (IList<object>)childData)
                this.Add(PersonaIdentificacion.Get(child));
            RaiseListChangedEvents = true;
        }

        #endregion
    }

    [Serializable]
    public class PersonaIdentificacion : JusBusinessBaseEChild<PersonaIdentificacion>
    {
        #region Business Methods

        // TODO: add your own fields, properties and methods

        
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


        protected override void AddCommonParameters()
        {
            throw new NotImplementedException();
        }

        protected override void Fetch(IDataReader dr)
        {
            throw new NotImplementedException();
        }

        // example with managed backing field
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        

        private PersonaIdentificacion()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.Child_Create();
        }

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

        #endregion
    }
}

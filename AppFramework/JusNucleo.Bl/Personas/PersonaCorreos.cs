using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Csla;
using JusFramework.Bl;
using JusFramework.Bl.ValidacionDatos;
using JusNucleo.Bl.Comun;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaCorreos : JusBusinessListBase<PersonaCorreos, PersonaCorreo>
    {
        #region Factory Methods
       

        //private PersonaCorreos(){ }

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

        protected override string NombreProcedimiento {
            get { return ProcedimientosConstantes.PrcCorreoObt; }
        }
    }

    [Serializable]
    public class PersonaCorreo : JusBusinessBaseEChild<PersonaCorreo>
    {
        #region Business Methods

        public int CorreoId {
            get { return Id; }
        }

        // example with managed backing field
        public static readonly PropertyInfo<string> CorreoProperty = RegisterProperty<string>(p => p.Correo);
        [Required]
        [Email]
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

        //private PersonaCorreo()
        //{ /* Require use of factory methods */ }

        #endregion

        #region Data Access

       
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
            Correo = dr["cor_correo"].ToString();
        }
    }
}
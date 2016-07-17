using System;
using System.ComponentModel;
using Csla;
using JusFramework.Bl;
using JusFramework.Bl.ValidacionDatos;
using JusFramework.Cache;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNaturalCriteria : JusCriteriaBase<PersonaNaturalCriteria, PersonaNaturalList, PersonaNaturalInfo>, ICacheable 
    {
        #region Business Methods

        private PersonaNaturalCriteria()
        {
        }

        public static readonly PropertyInfo<string> IdentificacionProperty =
            RegisterProperty<string>(p => p.Identificacion);

        [DisplayName("Identificación")]
        public string Identificacion
        {
            get { return GetProperty(IdentificacionProperty).ToUpper(); }
            set { SetProperty(IdentificacionProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerNombreProperty = RegisterProperty<string>(p => p.PrimerNombre);

        [PersonaNombre]
        [DisplayName("Primer Nombre")]
        public string PrimerNombre
        {
            get { return GetProperty(PrimerNombreProperty).ToUpper(); }
            set { SetProperty(PrimerNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoNombreProperty =
            RegisterProperty<string>(p => p.SegundoNombre);

        [PersonaNombre]
        [DisplayName("Segundo Nombre")]
        public string SegundoNombre
        {
            get { return GetProperty(SegundoNombreProperty).ToUpper(); }
            set { SetProperty(SegundoNombreProperty, value); }
        }


        public static readonly PropertyInfo<string> PrimerApellidoProperty =
            RegisterProperty<string>(p => p.PrimerApellido);

        [PersonaNombre]
        [DisplayName("Primer Apellido")]
        public string PrimerApellido
        {
            get { return GetProperty(PrimerApellidoProperty).ToUpper(); }
            set { SetProperty(PrimerApellidoProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoApellidoProperty =
            RegisterProperty<string>(p => p.SegundoApellido);

        [PersonaNombre]
        [DisplayName("Segundo Apellido")]
        public string SegundoApellido
        {
            get { return GetProperty(SegundoApellidoProperty).ToUpper(); }
            set { SetProperty(SegundoApellidoProperty, value); }
        }

        

        


        #endregion

        

        public override string GetKey()
        {
            return string.Format("{0}_{1}_{2}_{3}_{4}_{5}", GetType(), Identificacion,PrimerNombre, SegundoNombre, PrimerApellido,SegundoApellido);
        }

        
    }
}
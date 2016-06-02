﻿using System;
using Csla;
using JusFramework.Bl;
using JusFramework.Cache;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNaturalCriteria : JusCriteriaBase<PersonaNaturalCriteria>, ICacheable
    {
        #region Business Methods



        public static readonly PropertyInfo<string> IdentificacionProperty =
            RegisterProperty<string>(p => p.Identificacion);

        public string Identificacion
        {
            get { return GetProperty(IdentificacionProperty); }
            set { SetProperty(IdentificacionProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerNombreProperty = RegisterProperty<string>(p => p.PrimerNombre);

        public string PrimerNombre
        {
            get { return GetProperty(PrimerNombreProperty); }
            set { SetProperty(PrimerNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoNombreProperty =
            RegisterProperty<string>(p => p.SegundoNombre);

        public string SegundoNombre
        {
            get { return GetProperty(SegundoNombreProperty); }
            set { SetProperty(SegundoNombreProperty, value); }
        }

        public static readonly PropertyInfo<string> PrimerApellidoProperty =
            RegisterProperty<string>(p => p.PrimerApellido);

        public string PrimerApellido
        {
            get { return GetProperty(PrimerApellidoProperty); }
            set { SetProperty(PrimerApellidoProperty, value); }
        }

        public static readonly PropertyInfo<string> SegundoApellidoProperty =
            RegisterProperty<string>(p => p.SegundoApellido);

        public string SegundoApellido
        {
            get { return GetProperty(SegundoApellidoProperty); }
            set { SetProperty(SegundoApellidoProperty, value); }
        }


        #endregion

        private PersonaNaturalCriteria()
        {
            /* Require use of factory methods */
        }
        
        public string GetKey()
        {
            return string.Format("{0}_{1}_{2}_{3}_{4}_{5}", GetType(), Identificacion,PrimerNombre, SegundoNombre, PrimerApellido,SegundoApellido);
            return null;
        }
    }
}
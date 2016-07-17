using System;
using System.Collections.Generic;
using Csla;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusCriteriaBase<T, TP, TS> : BusinessBase<T>
        where T : JusCriteriaBase<T, TP,TS>
        where TP : JusReadOnlyListBase<TP, TS>
        where TS : JusReadOnlyBase<TS>
    {

        #region Factory Methods

        public static T New()
        {
            return DataPortal.Create<T>();
        }

        protected virtual int TamanioLista
        {
            get { return 20; }
        }
        

        public int TotalPaginas
        {
            get
            {
                int paginas = TotalRegistros / TamanioLista;
                if (TotalRegistros % TamanioLista != 0)
                {
                    paginas++;
                }
                return paginas;
            }
        }

        protected JusReadOnlyListBase<TP, TS> _lista;

        public List<TS> Lista
        {
            get
            {
                var lista = new List<TS>();
                if (_lista != null)
                {
                    var hasta = Hasta;
                    for (int i = Desde; i <= hasta; i++)
                    {
                        lista.Add(_lista[i-1]);
                    }
                }
                return lista;
            }
        }

        public int Desde {
            get
            {
                if (_lista == null)
                {
                    return 0;
                }
                return ((_numeroPagina-1)* TamanioLista)+1;
            }
        }

        public int Hasta
        {
            get
            {

                var hasta = (_numeroPagina*TamanioLista);
                if (_lista==null)
                {
                    return 0;
                }
                if (hasta>_lista.Count)
                {
                    hasta = _lista.Count;
                }
                return hasta;
            }
        }

        public int TotalRegistros
        {
            get
            {
                if (_lista==null)
                {
                    return 0;
                }
                return _lista.Count;
            }
        }

        private int _numeroPagina=1;
        public int NumeroPagina {
            get
            {
                if (_numeroPagina == 0) _numeroPagina++;
                if (_numeroPagina>TotalPaginas)
                {
                    _numeroPagina = 1;
                }
                 return _numeroPagina; }
            set { _numeroPagina = value; }
        }

        public string Mensaje {
            get
            {
                if (_lista==null)
                {
                    return String.Empty;
                }
                return String.Format("REGISTRO {0} HASTA {1} DE {2}", Desde, Hasta, _lista.TotalRegistros);
            }
        }

        public void Buscar()
        {
            _lista = JusReadOnlyListBase<TP, TS>.Get(this);
        }

        public abstract string GetKey();

        #endregion
    }

}

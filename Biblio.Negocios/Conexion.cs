using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblio.Datos;

namespace Biblio.Negocios
{
    internal class Conexion
    {
        private static BiblioDuoc2_BDEntities1 _biblioD;

        public static BiblioDuoc2_BDEntities1 BiblioD
        {
            get
            {
                if (_biblioD == null)
                {
                    _biblioD = new BiblioDuoc2_BDEntities1();
                }
                return _biblioD;
            }
            set { _biblioD = value; }
        }
    }
}

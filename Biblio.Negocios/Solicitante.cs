using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Negocios
{
    public class Solicitante
    {
        private string _codigoLibro;

        public string CodigoLibro
        {
            get { return _codigoLibro; }
            set
            {
                if (value.Length >= 4)
                {
                    _codigoLibro = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un código mayor o igual a 4 digitos.");
                }
            }
        }

        private string _rut;

        public string Rut
        {
            get { return _rut; }
            set
            {
                if (value.Length > 0)
                {
                    _rut = value;
                }
                else
                {
                    throw new ArgumentException("El rut no puede quedar vacío.");
                }
            }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (value.Length > 0)
                {
                    _nombre = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un nombre válido.");
                }
            }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set
            {
                if (value.Length > 0)
                {
                    _apellido = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un apellido válido.");
                }
            }
        }

        private TipoSolicitante _tipo;

        public TipoSolicitante Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


        private DateTime _fechaPr;

        public DateTime FechaPr
        {
            get { return _fechaPr; }
            set { _fechaPr = value; }
        }

        public Solicitante()
        {
            _codigoLibro = string.Empty;
            _rut = string.Empty;
            _nombre = string.Empty;
            _apellido = string.Empty;
            _tipo = TipoSolicitante.Alumno;
            _fechaPr = DateTime.Now;
        }

        public Solicitante(string codigoLibro, string rut, string nombre, string apellido, string tipo, DateTime fechaPrestamo)
        {
            _codigoLibro = codigoLibro;
            _rut = rut;
            _nombre = nombre;
            _apellido = apellido;
            TipoSolicitante ts;
            Enum.TryParse(tipo, out ts);
            _tipo = ts;
            _fechaPr = fechaPrestamo;
        }

        public bool Create()
        {
            try
            {
                Datos.Solicitante sol = new Datos.Solicitante()
                {
                    codigoLibro = this.CodigoLibro,
                    rut = this.Rut,
                    nombre = this.Nombre,
                    apellido = this.Apellido,
                    tipo = _tipo.ToString(),
                    fechaPrestamo = FechaPr

                };
                Conexion.BiblioD.Solicitante.Add(sol);
                Conexion.BiblioD.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Read()
        {
            try
            {
                Datos.Solicitante sol = (from auxsol in Conexion.BiblioD.Solicitante //auxsol es alias de solicitante
                                   where auxsol.codigoLibro == this.CodigoLibro
                                   select auxsol).First();

                this.CodigoLibro = sol.codigoLibro;
                this.Rut = sol.rut;
                this.Nombre = sol.nombre;
                this.Apellido = sol.apellido;
                TipoSolicitante ts;
                Enum.TryParse(sol.tipo, out ts);
                this._tipo = ts;
                this.FechaPr = sol.fechaPrestamo;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                Datos.Solicitante sol = Conexion.BiblioD.Solicitante.First(p => p.codigoLibro == CodigoLibro);
                {
                    sol.codigoLibro = CodigoLibro;
                    sol.rut = Rut;
                    sol.nombre = Nombre;
                    sol.apellido = Apellido;
                    sol.tipo = _tipo.ToString();
                    sol.fechaPrestamo = FechaPr;

                };

                Conexion.BiblioD.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete()
        {

            try
            {
                Datos.Solicitante sol = (from auxsol in Conexion.BiblioD.Solicitante
                                   where auxsol.codigoLibro == this.CodigoLibro
                                   select auxsol).First();
                Conexion.BiblioD.Solicitante.Remove(sol);
                Conexion.BiblioD.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}

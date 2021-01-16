using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Negocios
{
    public class Usuario
    {

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

        private TipoUsuario _tipoUs;

        public TipoUsuario TipoUs
        {
            get { return _tipoUs; }
            set { _tipoUs = value; }
        }

        private string _nombreUs;

        public string NombreUs
        {
            get { return _nombreUs; }
            set
            {
                if (value.Length > 0)
                {
                    _nombreUs = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un nombre de usuario válido.");
                }
            }
        }

        private string _contrasenia;

        public string Contrasenia
        {
            get { return _contrasenia; }
            set
            {
                if (value.Length > 0)
                {
                    _contrasenia = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese una contraseña válida.");
                }
            }
        }


        public Usuario()
        {
            _rut = string.Empty;
            _nombre = string.Empty;
            _apellido = string.Empty;
            _tipoUs = TipoUsuario.Administrador;
            _nombreUs = string.Empty;
            _contrasenia = string.Empty;

        }

        public Usuario(string rut, string nombre, string apellido, string tipo, string nombreUsuario, string contrasenia)
        {
            _rut = rut;
            _nombre = nombre;
            _apellido = apellido;
            TipoUsuario tu;
            Enum.TryParse(tipo, out tu);
            _tipoUs = tu;
            _nombreUs = nombreUsuario;
            _contrasenia = contrasenia;
        }

        public bool Create()
        {
            try
            {
                Datos.Usuario usu = new Datos.Usuario()
                {
                    rut = this.Rut,
                    nombre = this.Nombre,
                    apellido = this.Apellido,
                    tipo = _tipoUs.ToString(),
                    nombreUsuario = this.NombreUs,
                    contrasenia = this.Contrasenia

                };
                Conexion.BiblioD.Usuario.Add(usu);
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
                Datos.Usuario usu = (from auxusu in Conexion.BiblioD.Usuario //auxusu es alias de usuario
                                         where auxusu.rut == this.Rut
                                         select auxusu).First();

                this.Rut = usu.rut;
                this.Nombre = usu.nombre;
                this.Apellido = usu.apellido;
                TipoUsuario tu;
                Enum.TryParse(usu.tipo, out tu);
                this._tipoUs = tu;
                this.NombreUs = usu.nombreUsuario;
                this.Contrasenia = usu.contrasenia;

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
                Datos.Usuario usu = Conexion.BiblioD.Usuario.First(p => p.rut == Rut);
                {
                    usu.rut = Rut;
                    usu.nombre = Nombre;
                    usu.apellido = Apellido;
                    usu.tipo = _tipoUs.ToString();
                    usu.nombreUsuario = NombreUs;
                    usu.contrasenia = Contrasenia;

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
                Datos.Usuario usu = (from auxusu in Conexion.BiblioD.Usuario
                                         where auxusu.rut == this.Rut
                                         select auxusu).First();
                Conexion.BiblioD.Usuario.Remove(usu);
                Conexion.BiblioD.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool Read2()
        {
            try
            {
                Datos.Usuario usu = (from auxusu in Conexion.BiblioD.Usuario //auxusu es alias de usuario
                                     where auxusu.nombreUsuario == this.NombreUs
                                     select auxusu).First();

                this.Rut = usu.rut;
                this.Nombre = usu.nombre;
                this.Apellido = usu.apellido;
                TipoUsuario tu;
                Enum.TryParse(usu.tipo, out tu);
                this._tipoUs = tu;
                this.NombreUs = usu.nombreUsuario;
                this.Contrasenia = usu.contrasenia;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

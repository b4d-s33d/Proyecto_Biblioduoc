using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Negocios
{
    public class Libro
    {
        private string _codigo;

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (value.Length >= 4)
                {
                    _codigo = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un código mayor o igual a 4 digitos.");
                }
            }
        }

        private string _titulo;

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (value.Length > 0)
                {
                    _titulo = value;
                }
                else
                {
                    throw new ArgumentException("El título del libro no puede quedar vacío.");
                }
            }
        }

        private string _autor;

        public string Autor
        {
            get { return _autor; }
            set
            {
                if (value.Length > 0)
                {
                    _autor = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un nombre de autor válido.");
                }
            }
        }

        private int _anioEd;

        public int AnioEd
        {
            get { return _anioEd; }
            set
            {
                if (value >= 1900)
                {
                    _anioEd = value;
                }
                else
                {
                    throw new ArgumentException("Ingrese un año mayor o igual a 1900.");
                }
            }
        }

        private Genero _genero;

        public Genero Genero
        {
            get { return _genero; }
            set { _genero = value; }
        }

        private string _editorial;

        public string Editorial
        {
            get { return _editorial; }
            set
            {
                if (value.Length > 0)
                {
                    _editorial = value;
                }
                else
                {
                    throw new ArgumentException("La editorial del libro no puede quedar vacía.");
                }
            }
        }

        private DateTime _fechaIn;

        public DateTime FechaIn
        {
            get { return _fechaIn; }
            set { _fechaIn = value; }
        }

        public Libro()
        {
            _codigo = string.Empty;
            _titulo = string.Empty;
            _autor = string.Empty;
            _anioEd = 0;
            _genero = Genero.Novela;
            _editorial = string.Empty;
            _fechaIn = DateTime.Now;

        }

        public Libro(string codigo, string titulo, string autor, int anioEdicion, string genero, string editorial, DateTime fechaIngreso)
        {
            _codigo = codigo;
            _titulo = titulo;
            _autor = autor;
            _anioEd = anioEdicion;
            Genero gg;
            Enum.TryParse(genero, out gg);
            _genero = gg;
            _editorial = editorial;
            _fechaIn = fechaIngreso;
        }

        public bool Create()
        {
            try
            {
                Datos.Libro lib = new Datos.Libro()
                {
                    codigo = this.Codigo,
                    titulo = this.Titulo,
                    autor = this.Autor,
                    anioEdicion = this.AnioEd,
                    genero = _genero.ToString(),
                    editorial = this.Editorial,
                    fechaIngreso = FechaIn

                };
                Conexion.BiblioD.Libro.Add(lib);
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
                Datos.Libro lib = (from auxlib in Conexion.BiblioD.Libro //auxlib es alias de libro
                                    where auxlib.codigo == this.Codigo
                                    select auxlib).First();

                this.Codigo = lib.codigo;
                this.Titulo = lib.titulo;
                this.Autor = lib.autor;
                this.AnioEd = lib.anioEdicion;
                Genero gg;
                Enum.TryParse(lib.genero, out gg);
                this._genero = gg;
                this.Editorial = lib.editorial;
                this.FechaIn = lib.fechaIngreso;

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
                Datos.Libro lib = Conexion.BiblioD.Libro.First(p => p.codigo == Codigo);
                {
                    lib.codigo = Codigo;
                    lib.titulo = Titulo;
                    lib.autor = Autor;
                    lib.anioEdicion = AnioEd;
                    lib.genero = _genero.ToString();
                    lib.editorial = Editorial;
                    lib.fechaIngreso = FechaIn;

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
                Datos.Libro lib = (from auxlib in Conexion.BiblioD.Libro
                                    where auxlib.codigo == this.Codigo
                                    select auxlib).First();
                Conexion.BiblioD.Libro.Remove(lib);
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

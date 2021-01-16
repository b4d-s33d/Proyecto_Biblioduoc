using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblio.Datos;

namespace Biblio.Negocios
{
    public class Manejadora
    {
        public List<Libro> ListarLibro()
        {
            List<Libro> listaLibros = new List<Libro>();
            foreach (Datos.Libro librito in Conexion.BiblioD.Libro)
            {
                Libro nuevoLibro = new Libro(librito.codigo, librito.titulo, librito.autor,
                                                      librito.anioEdicion, librito.genero,
                                                      librito.editorial, librito.fechaIngreso);

                listaLibros.Add(nuevoLibro);
            }
            return listaLibros;
        }

        public List<Solicitante> ListarSolicitante()
        {
            List<Solicitante> listasolicitantes = new List<Solicitante>();
            foreach (Datos.Solicitante solicitantito in Conexion.BiblioD.Solicitante)
            {
                Solicitante nuevoSolicitante = new Solicitante(solicitantito.codigoLibro, solicitantito.rut, solicitantito.nombre,
                                                      solicitantito.apellido, solicitantito.tipo,
                                                      solicitantito.fechaPrestamo);

                listasolicitantes.Add(nuevoSolicitante);
            }
            return listasolicitantes;
        }

        public List<Usuario> ListarUsuario()
        {
            List<Usuario> listausuarios = new List<Usuario>();
            foreach (Datos.Usuario usuaricito in Conexion.BiblioD.Usuario)
            {
                Usuario nuevoUsuario = new Usuario(usuaricito.rut, usuaricito.nombre, usuaricito.apellido,
                                                      usuaricito.tipo, usuaricito.nombreUsuario,
                                                      usuaricito.contrasenia);

                listausuarios.Add(nuevoUsuario);
            }
            return listausuarios;
        }
    }
}

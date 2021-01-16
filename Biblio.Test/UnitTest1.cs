using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biblio.Negocios;

namespace Biblio.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLeer()
        {
            string nom_aux = "Las partículas elementales";
            string nom_libro = string.Empty;

            Negocios.Libro libT = new Negocios.Libro()
            {
                Codigo = "111111"
            };
            libT.Read();
            nom_libro = libT.Titulo;
            Assert.AreEqual(nom_aux, nom_libro);


            string nom_aux2 = "Wladimir";
            string nom_solicitante = string.Empty;

            Negocios.Solicitante solT = new Negocios.Solicitante()
            {
                CodigoLibro = "111111"
            };
            solT.Read();
            nom_solicitante = solT.Nombre;
            Assert.AreEqual(nom_aux2, nom_solicitante);


            string nom_aux3 = "Wladimir";
            string nom_usuario = string.Empty;

            Negocios.Usuario usuT = new Negocios.Usuario()
            {
                Rut = "17053976-1"
            };
            usuT.Read();
            nom_usuario = usuT.Nombre;
            Assert.AreEqual(nom_aux3, nom_usuario);
        }
    }
}

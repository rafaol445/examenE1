using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_PlaceMyBet.Models
{
    public class Usuario
    {
        #region Atributos

        public string email;
        public string nombre;
        public string apellidos;
        public int edad;
        public bool admin;

        #endregion

        #region Constructores
        public Usuario(string email, string nombre, string apellidos, int edad, bool admin)
        {
            this.email = email;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.edad = edad;
            this.admin = admin;
        }

        public Usuario()
        {
        }

        #endregion
    }
}
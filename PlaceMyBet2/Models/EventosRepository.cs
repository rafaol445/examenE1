using Api_PlaceMyBet.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_PlaceMyBet.Models
{
    public class EventosRepository
    {
        #region Metodos

        internal List<Evento> Retrive()
        {

            List<Evento> listaEventos = new List<Evento>();
            Evento evento = new Evento();
            

            string consulta = string.Format("SELECT * FROM `eventos`");

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);


            DataBaseRepository.AbrirConexion();
            try
            {
                MySqlDataReader resultado = comand.ExecuteReader();

                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        evento = new Evento(resultado.GetInt32(0), resultado.GetString(1), resultado.GetString(2), resultado.GetDateTime(3));
                        listaEventos.Add(evento);
                    }
                }

                DataBaseRepository.CerrarConexion();


            }
            catch (Exception)
            {

                
            }
            

            

            return listaEventos;
        }
        internal List<EventoDto> RetriveDTO()
        {

            List<EventoDto> listaEventos = new List<EventoDto>();
            
            EventoDto evento = new EventoDto();


            string consulta = string.Format("SELECT `local`, `visitante`, `fecha` FROM `eventos`");

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);


            DataBaseRepository.AbrirConexion();
            try
            {
                MySqlDataReader resultado = comand.ExecuteReader();

                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        evento = new EventoDto(resultado.GetString(0), resultado.GetString(1), resultado.GetDateTime(2));
                        listaEventos.Add(evento);
                    }
                }

                DataBaseRepository.CerrarConexion();


            }
            catch (Exception)
            {


            }




            return listaEventos;
        }


        

        



        #endregion


    }
}
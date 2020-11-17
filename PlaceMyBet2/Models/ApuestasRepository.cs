using Api_PlaceMyBet.Controllers;
using MySql.Data.MySqlClient;
using PlaceMyBet2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Web;

namespace Api_PlaceMyBet.Models
{
    public class ApuestasRepository
    {

        #region Metodos

        internal List<Apuesta> Retrive() {


            List<Apuesta> listaApuestas = new List<Apuesta>();
            Apuesta apuesta = new Apuesta();

            string consulta = string.Format("SELECT * FROM `apuestas`");

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);


            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    apuesta = new Apuesta(resultado.GetInt32(0), resultado.GetString(1), resultado.GetDouble(2), resultado.GetDouble(3), resultado.GetDateTime(4), resultado.GetInt32(5), resultado.GetString(6));
                    listaApuestas.Add(apuesta);
                }
            }

            DataBaseRepository.CerrarConexion();

            return listaApuestas;


        }
        internal List<ApuestaDTO> RetriveDTO()
        {


            List<ApuestaDTO> listaApuestas = new List<ApuestaDTO>();
            ApuestaDTO apuesta = new ApuestaDTO();


            string consulta = string.Format("SELECT `Tipo_Cuota`, `cuota`, `Dinero_Apostado`, `fecha`, `Mercado_idMercado`, `Usuario_Email` FROM `apuestas`");

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);


            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {

                    apuesta = new ApuestaDTO(resultado.GetString(0), resultado.GetDouble(1), resultado.GetDouble(2), resultado.GetDateTime(3), resultado.GetInt32(4), resultado.GetString(5));
                    listaApuestas.Add(apuesta);
                }
            }

            DataBaseRepository.CerrarConexion();

            return listaApuestas;


        }

        internal void HacerApuesta(Apuesta apuesta)
        {
            MercadosRepository mercado = new MercadosRepository();            

                mercado.ActualizarMercado(apuesta.IdMercado, apuesta.DineroApostado, apuesta.TipoCuota);

            Mercado mercado2 = new Mercado();
            mercado2 = mercado.ObtenerMercado(apuesta.idMercado);

            if (apuesta.tipoCuota == "over")
            {
                string consulta = string.Format("INSERT INTO `apuestas` (`idApuesta`, `Tipo_Cuota`, `cuota`, `Dinero_Apostado`, `fecha`, `Mercado_idMercado`, `Usuario_Email`) VALUES (NULL, '{0}','{1}','{2}','{3}','{4}','{5}');", apuesta.TipoCuota, mercado2.cuotaOver, apuesta.DineroApostado, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), apuesta.IdMercado, apuesta.UsuarioEmail);

                MySqlConnection conexion = DataBaseRepository.Conexion;
                MySqlCommand comand = new MySqlCommand(consulta, conexion);

                DataBaseRepository.AbrirConexion();

                int retorno = comand.ExecuteNonQuery();

                if (retorno > 0)
                {
                    Debug.WriteLine("apuesta insertada");
                }                
                else { Debug.WriteLine("no se a realizado la apuesta"); }
            }
            else if (apuesta.tipoCuota == "under")
            {

                string consulta = string.Format("INSERT INTO `apuestas` (`idApuesta`, `Tipo_Cuota`, `cuota`, `Dinero_Apostado`, `fecha`, `Mercado_idMercado`, `Usuario_Email`) VALUES (NULL, '{0}','{1}','{2}','{3}','{4}','{5}');", apuesta.TipoCuota, mercado2.cuotaUnder, apuesta.DineroApostado, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), apuesta.IdMercado, apuesta.UsuarioEmail);

                MySqlConnection conexion = DataBaseRepository.Conexion;
                MySqlCommand comand = new MySqlCommand(consulta, conexion);

                DataBaseRepository.AbrirConexion();

                int retorno = comand.ExecuteNonQuery();

                if (retorno > 0)
                {
                    Debug.WriteLine("apuesta insertada");
                }
                else { Debug.WriteLine("no se a realizado la apuesta"); }
                
            } 

            DataBaseRepository.CerrarConexion();

        }

        internal void ActualizarApuesta(double cuota, Apuesta apuesta)
        {

            string consulta = string.Format("UPDATE `apuestas` SET `cuota` = '{0}' WHERE `apuestas`.`idApuesta` = {1} AND `apuestas`.`Mercado_idMercado` = {2} AND `apuestas`.`Usuario_Email` = '{3}'; ", cuota, apuesta.idApuesta, apuesta.idMercado ,apuesta.usuarioEmail);

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            DataBaseRepository.AbrirConexion();

            int retorno = comand.ExecuteNonQuery();

            if (retorno > 0)
            {

                Debug.WriteLine("consulta actualizada correctamente");
                
            }
            else { Debug.WriteLine("apuesta no realizada"); }



            DataBaseRepository.CerrarConexion();



        }

        
        internal List<ApuestaArgumentos> ApuestaPorEmailMercado(int idMercado, string email)
        {

            ApuestaArgumentos apuesta = new ApuestaArgumentos();
            List<ApuestaArgumentos> listaApuestas = new List<ApuestaArgumentos>();

            MercadosRepository.metodoComas();

            string consulta = string.Format("SELECT `mercados`.`EVENTOS_idEvento`, `apuestas`.`Tipo_Cuota`, `apuestas`.`cuota`, `apuestas`.`Dinero_Apostado` FROM `apuestas` LEFT JOIN `mercados` ON `apuestas`.`Mercado_idMercado` = `mercados`.`idMercado` ; ", email, idMercado);                

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    apuesta = new ApuestaArgumentos(resultado.GetDouble(0), resultado.GetString(1), resultado.GetDouble(2), resultado.GetDouble(3));
                    listaApuestas.Add(apuesta);
                }
            }
            DataBaseRepository.CerrarConexion();

            return listaApuestas;
        }

        internal List<ApuestasPorTipoMercado> ApuestaPorEmailTipoMercado(double tipoMercado, string email)
        {

            ApuestasPorTipoMercado apuesta = new ApuestasPorTipoMercado();
            List<ApuestasPorTipoMercado> listaApuestas = new List<ApuestasPorTipoMercado>();

            MercadosRepository.metodoComas();

            string consulta = string.Format("SELECT `apuestas`.`Tipo_Cuota`, `apuestas`.`cuota`, `apuestas`.`Dinero_Apostado`, `mercados`.`EVENTOS_idEvento` FROM `apuestas` LEFT JOIN `mercados` ON `apuestas`.`Mercado_idMercado` = `mercados`.`idMercado` WHERE `mercados`.`Tipo_Over_Under` = '{0}' AND `apuestas`.`Usuario_Email` = '{1}'  ; ", tipoMercado, email);
            
            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    apuesta = new ApuestasPorTipoMercado(resultado.GetString(0), resultado.GetDouble(1), resultado.GetDouble(2), resultado.GetInt32(3));
                    listaApuestas.Add(apuesta);
                }

            }
            DataBaseRepository.CerrarConexion();

            return listaApuestas;
        }

        public ApuestaExamen apuestasPorEquipo(string equipo)
        {

            string consulta = string.Format("SELECT `eventos`.`local`, `eventos`.`visitante`, `apuestas`.`Dinero_Apostado` FROM `eventos`, `apuestas` WHERE `eventos`.`local` = '{0}' OR `eventos`.`visitante` = '{1}'; ",equipo,equipo);

            List<ApuestaExamen> listaApuestas = new List<ApuestaExamen>();
            ApuestaExamen apuesta = new ApuestaExamen();
            double contadorDinero = 0;
            string equipoMostrar = null;

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    if (resultado.GetString(0) == equipo)
                    {
                        apuesta = new ApuestaExamen(resultado.GetString(1), (contadorDinero+= resultado.GetDouble(2)));
                        
                        equipoMostrar = resultado.GetString(1);
                        listaApuestas.Add(apuesta);
                        

                    }
                    else {
                        apuesta = new ApuestaExamen(resultado.GetString(0), contadorDinero+=resultado.GetDouble(2));
                        equipoMostrar = resultado.GetString(0);
                        listaApuestas.Add(apuesta);

                    }
                    contadorDinero += resultado.GetDouble(2);
                    
                }

            }
            DataBaseRepository.CerrarConexion();
            

            return apuesta = new ApuestaExamen(equipoMostrar,contadorDinero);


            

        }


        #endregion

    }
}
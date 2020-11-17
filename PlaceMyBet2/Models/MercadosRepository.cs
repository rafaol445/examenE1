using Api_PlaceMyBet.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Diagnostics;

namespace Api_PlaceMyBet.Models
{
    public class MercadosRepository
    {

        #region Metodos

        internal List<Mercado> Retrive()
        {

            List<Mercado> listaMercados = new List<Mercado>();
            Mercado mercado = new Mercado();

            string consulta = string.Format("SELECT * FROM `mercados`");


            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);


            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    mercado = new Mercado(resultado.GetInt32(0), resultado.GetDouble(1), resultado.GetDouble(2), resultado.GetDouble(3), resultado.GetDouble(4), resultado.GetDouble(5), resultado.GetInt32(6));
                    listaMercados.Add(mercado);
                }
            }

            DataBaseRepository.CerrarConexion();

            return listaMercados;






        }

        internal List<MercadoDTO> RetriveDTO()
        {

            List<MercadoDTO> listaMercados = new List<MercadoDTO>();
            MercadoDTO mercado = new MercadoDTO();

            string consulta = string.Format("SELECT `Tipo_Over_Under`, `Cuota_Over`, `Cuota_Under` FROM `mercados`");


            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);


            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    mercado = new MercadoDTO(resultado.GetDouble(0), resultado.GetDouble(1), resultado.GetDouble(2));
                    listaMercados.Add(mercado);
                }
            }

            DataBaseRepository.CerrarConexion();

            return listaMercados;
        }

        internal void ActualizarMercado(int mercado, double cantidadApostada, string overUnder)
        {
            const double comision = 0.95;
            overUnder = overUnder.ToLower();
            double probabilidadOver;
            double probabilidadUnder;
            double cuotaOver = 0;
            double cuotaUnder = 0;

            Mercado mercadoDatos = ObtenerMercado(mercado);

            if (overUnder == "over")
            {
                double dineroOver = mercadoDatos.dineroOver + cantidadApostada;

                probabilidadOver = calcularProbabilidad(dineroOver, mercadoDatos.dineroUnder);
                probabilidadUnder = calcularProbabilidad(mercadoDatos.dineroUnder, dineroOver);                

                cuotaOver = calcularCuota(probabilidadOver, comision);
                cuotaUnder = calcularCuota(probabilidadUnder, comision);

                metodoComas();

                string consultaUpdateOver = string.Format("UPDATE `mercados` SET `Cuota_Over` = '{0}', `Cuota_Under` = '{1}', `Dinero_Over` = '{2}', `Dinero_Under` = '{3}' WHERE `mercados`.`idMercado` = {4}; ", cuotaOver, cuotaUnder, dineroOver, mercadoDatos.dineroUnder, mercado);

                MySqlConnection conexion = DataBaseRepository.Conexion;
                MySqlCommand comand = new MySqlCommand(consultaUpdateOver, conexion);

                DataBaseRepository.AbrirConexion();

                comand.ExecuteNonQuery();

            }
            else if (overUnder == "under")
            {
                double dineroUnder = mercadoDatos.dineroUnder + cantidadApostada;

                probabilidadOver = calcularProbabilidad(mercadoDatos.dineroOver, dineroUnder);
                probabilidadUnder = calcularProbabilidad(dineroUnder, mercadoDatos.dineroOver);

                cuotaOver = calcularCuota(probabilidadOver, comision);
                cuotaUnder = calcularCuota(probabilidadUnder, comision);

                

                CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");
                culInfo.NumberFormat.NumberDecimalSeparator = ".";
                culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
                culInfo.NumberFormat.PercentDecimalSeparator = ".";
                culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;


                string consultaUpdateOver = string.Format("UPDATE `mercados` SET `Cuota_Over` = '{0}', `Cuota_Under` = '{1}', `Dinero_Over` = '{2}', `Dinero_Under` = '{3}' WHERE `mercados`.`idMercado` = {4}; ", cuotaOver, cuotaUnder, mercadoDatos.dineroOver, dineroUnder, mercado);

                MySqlConnection conexion = DataBaseRepository.Conexion;
                MySqlCommand comand = new MySqlCommand(consultaUpdateOver, conexion);

                DataBaseRepository.AbrirConexion();

                comand.ExecuteNonQuery();



            }

            DataBaseRepository.CerrarConexion();
        }

        // metodo ejercicio 2
        internal void InsertarMercado(int idEvento, double tipoMercado)
        {
            metodoComas();
            double probabilidadOver = calcularProbabilidad(100, 100);
            double probabilidadUnder = calcularProbabilidad(100,100);
            double cuotaOver = calcularCuota(probabilidadOver, 0.95);
            double cuotaUnder = calcularCuota(probabilidadUnder, 0.95);
            metodoComas();

            string consulta = string.Format("INSERT INTO `mercados` (`idMercado`, `Tipo_Over_Under`, `Cuota_Over`, `Cuota_Under`, `Dinero_Over`, `Dinero_Under`, `EVENTOS_idEvento`) VALUES (NULL, '{0}', '{1}', '{2}', '100', '100', '{3}'); ", tipoMercado, cuotaOver, cuotaUnder,idEvento);
            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            

            DataBaseRepository.AbrirConexion();

            int retorno = comand.ExecuteNonQuery();

            if (retorno > 0)
            {
                Debug.WriteLine("mercado insertado");
            }
            else { Debug.WriteLine("mercado no insertado"); }
            DataBaseRepository.CerrarConexion();
        }


        internal Mercado ObtenerMercado(int idMercado) {



            string consulta = string.Format("SELECT * FROM `mercados` WHERE idMercado = {0};", idMercado);

            Mercado mercadoDatos = new Mercado();

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    mercadoDatos = new Mercado(resultado.GetInt32(0), resultado.GetDouble(1), resultado.GetDouble(2), resultado.GetDouble(3), resultado.GetDouble(4), resultado.GetDouble(5), resultado.GetInt32(6));
                }

            }
            DataBaseRepository.CerrarConexion();

            return mercadoDatos;




        }

        internal double calcularProbabilidad(double probabilidad1, double probabilidad2)
        {
            return probabilidad1 / (probabilidad1 + probabilidad2);
        }

        internal double calcularCuota(double probabilidad, double comision) {

            return Math.Round((1 / probabilidad) * comision, 2, MidpointRounding.AwayFromZero);
        }

        internal Mercado MercadosPorEvento(int idEvento, double tipo)
        {
            metodoComas();

            string consulta = string.Format("SELECT * FROM `mercados` WHERE `Tipo_Over_Under` = '{0}' AND `EVENTOS_idEvento` = '{1}' ", tipo, idEvento);

            Mercado mercadoDatos = new Mercado();

            MySqlConnection conexion = DataBaseRepository.Conexion;
            MySqlCommand comand = new MySqlCommand(consulta, conexion);

            DataBaseRepository.AbrirConexion();

            MySqlDataReader resultado = comand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    mercadoDatos = new Mercado(resultado.GetInt32(0), resultado.GetDouble(1), resultado.GetDouble(2), resultado.GetDouble(3), resultado.GetDouble(4), resultado.GetDouble(5), resultado.GetInt32(6));
                }

            }
            DataBaseRepository.CerrarConexion();

            return mercadoDatos;
        }

        public static void metodoComas()
        {
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");
            culInfo.NumberFormat.NumberDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;
        }

        #endregion








    }
}
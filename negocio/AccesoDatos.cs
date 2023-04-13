﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion; //variable para conexion a bd
        private SqlCommand comando; // variable para el query
        private SqlDataReader lector; // variable para leer de la bd

        public SqlDataReader Lector //esto me permite leer el lector desde afuera de la clase(solo lect)
        {
            get { return lector; }
        }

        public AccesoDatos ()
        {
            conexion = new SqlConnection ("server=DESKTOP-U06P0EN\\SQLEXPRESS; database=DISCOS_DB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta) 
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion ()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();      
        }
    }
}

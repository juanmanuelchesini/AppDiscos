using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DiscosNegocio
    {
        public List<Discos> Listar()
        {
			List<Discos> lista = new List<Discos>();
			AccesoDatos datos = new AccesoDatos(); //creo un obj datos que tiene conexion, comando y lector
			
			try
			{
				datos.setearConsulta("select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion Tipo, T.Descripcion Formato, D.IdEstilo, D.IdTipoEdicion, D.Id from DISCOS D, ESTILOS E, TIPOSEDICION T where e.Id = D.IdEstilo and T.Id = D.IdTipoEdicion");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Discos aux = new Discos();
					aux.Id = (int)datos.Lector["Id"];
					aux.Titulo = (string)datos.Lector["Titulo"];
					aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
					aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
					//if(!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("UrlImagenTapa"))))
					//	aux.UrlImagen = (string)datos.Lector["UrlImagenTapa"];
					if(!(datos.Lector["UrlImagenTapa"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagenTapa"];
                    aux.Tipo = new Estilos();
					aux.Tipo.Id = (int)datos.Lector["IdEstilo"];
					aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
					aux.Formato = new TiposEdicion();
					aux.Formato.Id = (int)datos.Lector["IdTipoEdicion"];
					aux.Formato.Formato = (string)datos.Lector["Formato"];

					lista.Add(aux);
				}

					return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{

				datos.cerrarConexion();
			}
			
        }

        public void agregar(Discos nuevo)
        {
			AccesoDatos datos = new AccesoDatos();
			try
			{

				datos.setearConsulta("insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, IdEstilo, IdTipoEdicion) values ('" + nuevo.Titulo + "', '"+ nuevo.FechaLanzamiento.ToString("dd/MM/yy") +"', " + nuevo.CantidadCanciones + ", '" + nuevo.UrlImagen +"', @idEstilo, @idTipoEdicion) ");
				datos.setearParametro("@idEstilo", nuevo.Tipo.Id);
				datos.setearParametro("@idTipoEdicion", nuevo.Formato.Id);
				datos.ejecutarAccion();
				Listar();
				

			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally 
			{
				datos.cerrarConexion();
			}
			
        }
        public void modificar(Discos disco)
        {
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fechaLanzamiento, CantidadCanciones = @cantidadCanciones, UrlImagenTapa = @urlImagenTapa, IdEstilo = @idEstilo, IdTipoEdicion = @idTipoEdicion where Id = @id");
				datos.setearParametro("@titulo", disco.Titulo);
                datos.setearParametro("@fechalanzamiento", disco.FechaLanzamiento);
                datos.setearParametro("@cantidadCanciones", disco.CantidadCanciones);
                datos.setearParametro("@urlImagenTapa", disco.UrlImagen);
                datos.setearParametro("@idEstilo", disco.Tipo.Id);
                datos.setearParametro("@idTipoEdicion", disco.Formato.Id);
                datos.setearParametro("@id", disco.Id);

				datos.ejecutarAccion();
            }
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
			}

        }
        public void eliminar(int id)
        {
				AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("delete from DISCOS where id = @id");
				datos.setearParametro("@id", id);
				datos.ejecutarAccion();
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }

		
    }


}

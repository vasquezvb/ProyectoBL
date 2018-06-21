using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClienteDAL
    {
        public string RutCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdSexo { get; set; }
        public int IdEstadoCivil { get; set; }


        public void Add(ClienteDAL cliente)
        {
            try
            {
                BeLifeContexto ctx = DB.Contexto;

                ctx.Cliente.Add(new Cliente()
                {
                    RutCliente = cliente.RutCliente,
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    FechaNacimiento = cliente.FechaNacimiento,
                    IdSexo = cliente.IdSexo,
                    IdEstadoCivil = cliente.IdEstadoCivil
            
                });
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar");
                Console.WriteLine("Detalle: " + ex.Message);
            }
            finally
            {

                Console.WriteLine("Cerrando la conexión...");
            }
        }

        public bool Edit(ClienteDAL cliente)
        {
            try
            {
                BeLifeContexto ctx = DB.Contexto;

                var c = ctx.Cliente.FirstOrDefault(x => x.RutCliente.Equals(cliente.RutCliente));

                c.Nombres = cliente.Nombres;
                c.Apellidos = cliente.Apellidos;
                c.FechaNacimiento = cliente.FechaNacimiento;
                c.IdSexo = cliente.IdSexo;
                c.IdSexo = cliente.IdEstadoCivil;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }


        public void Delete(ClienteDAL cliente)
        {
            //Delete CLientes belife

        }
        public void GetAll(ClienteDAL cliente)
        {
            //Listar todos los  CLientes belife


        }


		

    }
}

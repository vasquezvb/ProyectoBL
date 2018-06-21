using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
     public class ContratoDAL
    {
        public string Numero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaTermino { get; set; }
        public string RutCliente { get; set; }
        public string CodigoPlan { get; set; }
        public int IdTipoContrato { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public bool Vigente { get; set; }
        public bool DeclaracionSalud { get; set; }
        public float PrimaAnual { get; set; }
        public float PrimaMensual { get; set; }
        public string Observaciones { get; set; }

        public void Add(ContratoDAL contrato)
        {
            try
            {
                BeLifeContexto ctx = DB.Contexto;

                ctx.Contrato.Add(new Contrato()
                {
                    Numero = contrato.Numero,
                    FechaCreacion = contrato.FechaCreacion,
                    FechaTermino = contrato.FechaTermino,
                    RutCliente = contrato.RutCliente,
                    CodigoPlan = contrato.CodigoPlan,
                    IdTipoContrato = contrato.IdTipoContrato,
                    FechaInicioVigencia = contrato.FechaInicioVigencia,
                    FechaFinVigencia = contrato.FechaFinVigencia,
                    Vigente = contrato.Vigente,
                    DeclaracionSalud = contrato.DeclaracionSalud,
                    PrimaAnual = contrato.PrimaAnual,
                    PrimaMensual = contrato.PrimaMensual,
                    Observaciones = contrato.Observaciones,
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

        public bool Edit(ContratoDAL contrato)
        {
            try
            {
                BeLifeContexto ctx = DB.Contexto;

                var c = ctx.Contrato.FirstOrDefault(x => x.RutCliente.Equals(contrato.RutCliente));

                c.Numero = contrato.Numero;
                c.FechaCreacion = contrato.FechaCreacion;
                c.FechaTermino = contrato.FechaTermino;
                c.RutCliente = contrato.RutCliente;
                c.CodigoPlan = contrato.CodigoPlan;
                c.IdTipoContrato = contrato.IdTipoContrato;
                c.FechaInicioVigencia = contrato.FechaInicioVigencia;
                c.FechaFinVigencia = contrato.FechaFinVigencia;
                c.Vigente = contrato.Vigente;
                c.DeclaracionSalud = contrato.DeclaracionSalud;
                c.PrimaAnual = contrato.PrimaAnual;
                c.PrimaMensual = contrato.PrimaMensual; 
                c.Observaciones = contrato.Observaciones;
                ctx.SaveChanges();
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public void Delete(ContratoDAL contrato)
        {
            //Delete CLientes belife

        }
        public void GetAll(ContratoDAL contrato)
        {
            //Listar todos los  CLientes belife


        }
    }
}

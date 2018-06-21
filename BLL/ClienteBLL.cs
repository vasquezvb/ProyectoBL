using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
     public class ClienteBLL
    {
        public string RutCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public  DateTime FechaNacimiento  { get; set; }
        public int IdSexo  { get; set; }
        public int IdEstadoCivil { get; set; }


       


        //Agregar

        public void Agregar(string rutcliente, string nombres, string apellidos,
                            DateTime fechanacimiento,int idsexo,int idestadocivil)
        {
            ClienteDAL clienteDAL = new ClienteDAL();

            ClienteDAL cliente = new ClienteDAL();
            cliente.RutCliente = rutcliente;
            cliente.Nombres = nombres;
            cliente.Apellidos = apellidos;
            cliente.FechaNacimiento = fechanacimiento;
            cliente.IdSexo = idsexo;
            cliente.IdEstadoCivil = idestadocivil;

            clienteDAL.Add(cliente);
        }
    }
}

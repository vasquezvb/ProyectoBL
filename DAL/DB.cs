using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DB 
    {
        //private SqlConnection conexion;
        private static BeLifeContexto ctx = null;

        private DB() {}

        public static BeLifeContexto Contexto
        {
            get
            {
                if (ctx == null)
                    ctx = new BeLifeContexto();

                return ctx;
            }            
        }
    }
}

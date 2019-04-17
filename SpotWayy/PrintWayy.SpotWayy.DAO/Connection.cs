using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintWayy.SpotWayy.DAO
{
    class Connection:IDisposable
    {
        private readonly SqlConnection myConnection;

        //Construtor da classe para abrir a conexão com o SGBD          
        public Connection()
        {
            myConnection = new SqlConnection(ConfigurationManager.
                ConnectionStrings["SpotWayyManagerConfig"].ConnectionString);
            myConnection.Open();
        }

        //Executar query sem retorno
        public void ExecuteQry(string query)
        {
            var commandQry = new SqlCommand(query, myConnection);
            commandQry.ExecuteNonQuery();
        }

        //Executar query com retorno
        public SqlDataReader ExecuteSelect(string query)
        {
            var commandQry = new SqlCommand(query, myConnection);
            return commandQry.ExecuteReader();
        }

        //Implementção da interface Dispose para fechar a conexão
        public void Dispose()
        {
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
    }
}

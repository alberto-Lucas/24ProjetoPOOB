//Importar bibliotecas de banco
using System;
using System.Data;
using System.Data.SqlClient;


namespace ProjetoPOOB.Services
{
    public class DataBaseSqlServerService
    {
        //Método para conectar com o banco de dados
        private SqlConnection CriarConexao()
        {
            SqlConnection conexao =
                new SqlConnection();

            //Os dados para conectar no banco
            conexao.ConnectionString =
                "Data Source=.\\SQLExpress;" + //Servidor
                "Initial Catalog=ProjetoPOOA;" + //Nome do Banco
                "Integrated Security=SSPI;" + //Autenticação do Windows (usuario logado)
                "User Instance=false;"; //Usar o usuario da maquina

            //de String de comunicação para servidor Sql (com usuario e senha)
            /*
            conexao.ConnectionString =
                "Data Source=192.168.0.5;" + //Servidor
                "Initial Catalog=ProjetoPOOA;" + //Nome do Banco
                "Persist Security Info=False;" + //Autenticação por usuario e senha
                "User ID=usuario;" + //Usuario do login
                "Password=senha;"; //Senha do login
            */

            //Variações:
            //Servidor  local na rede
            //"Data Source=192.168.0.5;" 
            //Servidor online (1433 é a porta do banco de dados)
            //"Data Source=tcp:estagio.database.windows.net,1433;" 

            conexao.Open(); //Abrir a conexão com o banco
            return conexao;
        }

    }
}

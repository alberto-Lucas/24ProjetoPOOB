//Importar bibliotecas de banco
using System;
using System.ComponentModel.Design;
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
                "Initial Catalog=ProjetoPOOB;" + //Nome do Banco
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

        //Variavel privada global para armazenar
        //os parametros de comandos SQL
        private
            SqlParameterCollection sqlParameterCollection =
             new SqlCommand().Parameters;

        //Métod para limpar os parametros
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        //Método para adicionar parametros
        public void
            AdicionarParametros(
                string nomeParametro,
                object valorParametro)
        {
            sqlParameterCollection.Add(
                new SqlParameter(
                    nomeParametro, valorParametro));
        }

        //A partir daqui iremos começar as manipulações
        //INSERT, UPDATE e DELETE
        public int ExecutarManipulacao(
            CommandType commandType,
            string nomeStoredeOuTextoSql)
        {
            //Tenta executar o comando
            try
            {
                //Abro a conexão com o banco
                SqlConnection sqlConnection =
                    CriarConexao();

                //Criar a variavel do comando
                //que sera executado
                SqlCommand sqlCommand =
                    sqlConnection.CreateCommand();

                //Defini o tipo do comando
                //Insert, Update...
                sqlCommand.CommandType = commandType;

                //Definos o comando a ser executado
                sqlCommand.CommandText =
                    nomeStoredeOuTextoSql;

                //Carregar os parametros
                foreach (SqlParameter sqlParameter
                    in sqlParameterCollection)
                {
                    //Adicionar parametros ao comando
                    sqlCommand.Parameters.Add(
                        sqlParameter.ParameterName,
                        sqlParameter.Value);
                }

                //Executar o comando no banco de dados
                //coletar o retorno
                //0 - Não conseguiu executar
                //>0 - Registro afetados
                return sqlCommand.ExecuteNonQuery();

            }
            //Se der problema
            //Trata a msg para o usuario
            //Exception recebe o erro original
            catch (Exception ex)
            {
                //Crio a mensagem tratada
                throw new Exception(
                    "Houve uma falha ao executar " +
                    "o comando no banco de dados. " +
                    "\r\n" + //quebra de linha <br/> 
                    "Mensagem original: " +
                    ex.Message);
            }
        }

        //Método para executar consulta 
        //no banco de dados
        //Select retorna um tabela de dados
        public DataTable ExecutarConsulta(
            CommandType commandType,
            string nomeStoredeOuTextoSql)
        {
            try
            {
                //Abro a conexão com o banco
                SqlConnection sqlConnection =
                    CriarConexao();

                //Criar a variavel do comando
                //que sera executado
                SqlCommand sqlCommand =
                    sqlConnection.CreateCommand();

                //Defini o tipo do comando
                //Insert, Update...
                sqlCommand.CommandType = commandType;

                //Definos o comando a ser executado
                sqlCommand.CommandText =
                    nomeStoredeOuTextoSql;

                //Carregar os parametros
                foreach (SqlParameter sqlParameter
                    in sqlParameterCollection)
                {
                    //Adicionar parametros ao comando
                    sqlCommand.Parameters.Add(
                        sqlParameter.ParameterName,
                        sqlParameter.Value);
                }

                //O retorno da consulta é um DataTable
                //O C# precisa q esse DataTable seja
                //um objeto
                //Iremos utilizar o SqlDataAdapter
                //para converter o retorno do banco
                //em DataTable
                SqlDataAdapter sqlDataAdapter =
                    new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();

                //Nesse momento o comando SQL será
                //executado o retorno é convertido
                //o comando é executado dentro do
                //dataAdapter e o resulta sera 
                //colocado no dataTable
                //Fill = SELECT
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                //Crio a mensagem tratada
                throw new Exception(
                    "Houve uma falha ao executar " +
                    "a consulta no banco de dados. " +
                    "\r\n" + //quebra de linha <br/> 
                    "Mensagem original: " +
                    ex.Message);
            }
        }

        //Método consulta SCALAR
        //para retorna uma unica informação
        public object ExecutarConsultaScalar(
            CommandType commandType,
            string nomeStoredeOuTextoSql)
        {
            //Tenta executar o comando
            try
            {
                //Abro a conexão com o banco
                SqlConnection sqlConnection =
                    CriarConexao();

                //Criar a variavel do comando
                //que sera executado
                SqlCommand sqlCommand =
                    sqlConnection.CreateCommand();

                //Defini o tipo do comando
                //Insert, Update...
                sqlCommand.CommandType = commandType;

                //Definos o comando a ser executado
                sqlCommand.CommandText =
                    nomeStoredeOuTextoSql;

                //Carregar os parametros
                foreach (SqlParameter sqlParameter
                    in sqlParameterCollection)
                {
                    //Adicionar parametros ao comando
                    sqlCommand.Parameters.Add(
                        sqlParameter.ParameterName,
                        sqlParameter.Value);
                }

                //Executar o comando no banco de dados
                //coletar o retorno
                return sqlCommand.ExecuteScalar();

            }
            //Se der problema
            //Trata a msg para o usuario
            //Exception recebe o erro original
            catch (Exception ex)
            {
                //Crio a mensagem tratada
                throw new Exception(
                    "Houve uma falha ao executar " +
                    "a consulta scalar no banco de dados. " +
                    "\r\n" + //quebra de linha <br/> 
                    "Mensagem original: " +
                    ex.Message);
            }
        }
    }
}

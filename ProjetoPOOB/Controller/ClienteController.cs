using ProjetoPOOB.Services;
using ProjetoPOOB.Models;
using System.Data;
using System;

namespace ProjetoPOOB.Controller
{
    public class ClienteController
    {
        //Criar instancia global
        //para acessar a classe DataBase
        DataBaseSqlServerService dataBase = 
            new DataBaseSqlServerService();

        //Método Inserir
        //Irar inserir o cadastro de cliente
        //no banco de dados
        //Ira receber via parametros o
        //objeto Cliente
        public int Inserir(Cliente cliente)
        {
            //Iremos montar a nossa sql
            //Definir uma variavel com a sql completa
            string queryInserir =
                "INSERT INTO cliente " + 
                "(nome, rg, cpf, dt_nascimento, telefone) " +
                "VALUES " +
                "(@nome, @rg, @cpf, @dt_nascimento, @telefone)";

            //Vamos lidar com os parametros de banco de dados
            //Primeiro limpamos os parametros de banco
            dataBase.LimparParametros();

            //Adionar os valores do objeto em cada parametro
            //Termo: Mapeamento
            //MapObjectToSql
            //MapSqlToObject
            dataBase.AdicionarParametros("@nome",           cliente.Nome);
            dataBase.AdicionarParametros("@rg",             cliente.Rg);
            dataBase.AdicionarParametros("@cpf",            cliente.CPF);
            dataBase.AdicionarParametros("@dt_nascimento",  cliente.DtNascimento);
            dataBase.AdicionarParametros("@telefone",       cliente.Telefone);

            //Com a query montada
            //Parametro com seus devidos valores
            //Iremos executar o comando no banco
            dataBase.ExecutarManipulacao(
                CommandType.Text, queryInserir);

            //Precisamos retornar o id do registro
            //gerado pelo IDENTITY
            return
                Convert.ToInt32(
                    dataBase.ExecutarConsultaScalar(
                        CommandType.Text,
                        "SELECT MAX(id_cliente) FROM cliente"));
        }

        //Método alterar
        //Segue o mesmo principio do método Inserir
        public int Alterar(Cliente cliente)
        {
            string queryAlterar =
                "UPDATE cliente SET " +
                "nome = @nome, " +
                "rg = @rg, " +
                "cpf = @cpf," +
                "dt_nascimento = @dt_nascimento, " +
                "telefone = @telefone " +
                "WHERE id_cliente = @id_cliente";

            dataBase.LimparParametros();

            dataBase.AdicionarParametros("@nome", cliente.Nome);
            dataBase.AdicionarParametros("@rg", cliente.Rg);
            dataBase.AdicionarParametros("@cpf", cliente.CPF);
            dataBase.AdicionarParametros("@dt_nascimento", cliente.DtNascimento);
            dataBase.AdicionarParametros("@telefone", cliente.Telefone);
            dataBase.AdicionarParametros("@id_cliente", cliente.IdCliente);

            //Não é nescessario converter o retorno
            //pois o banco de dados ja retorna um int
            //com a quantidade de registros afetados
            return dataBase.ExecutarManipulacao(
                CommandType.Text, queryAlterar);
        }

        //Método Apagar
        //o Comando Apagar será com base
        //no ID do registro
        public int Apagar(int IdCliente)
        {
            string queryApagar =
                "DELETE FROM cliente " +
                "WHERE id_cliente = @id_cliente";

            dataBase.LimparParametros();
            dataBase.AdicionarParametros("@id_cliente", IdCliente);

            return dataBase.ExecutarManipulacao(
                CommandType.Text, queryApagar);
        }

        //Iremos para as Consultas ou SELECT
        //Método consultar filtrando pelo ID
        public Cliente ConsultarPorId(int IdCliente)
        {
            string queryConsulta =
                "SELECT * FROM cliente " +
                "WHERE id_cliente = @id_cliente";

            dataBase.LimparParametros();
            dataBase.AdicionarParametros("@id_cliente", IdCliente);

            //O retorno da consulta do banco de dados
            //sera populado na varaivel
            //dataTable
            DataTable dataTable =
                dataBase.ExecutarConsulta(
                    CommandType.Text, queryConsulta);

            //Validar a quantiade de linhas retornadas
            //Select por ID
            //Retorna apenas 1 registro ou nenhum
            if(dataTable.Rows.Count > 0)
            {
                //Mapeamento do banco para objeto
                //MapSqlToObject
                Cliente cliente = new Cliente();

                //Primeiro
                //Converter o retorno para o tipo do atributo
                //Segundo
                //Identificar a posição do registro
                //Como teremos 1 ou nenhuma
                //pegaremos sempre da primeira posiçao
                //Ou seja posição 0
                //Terceiro
                //Informamos qual campo sera convertido
                cliente.IdCliente =
                    Convert.ToInt32(
                        dataTable.Rows[0]["id_cliente"]);
                cliente.Nome =
                    Convert.ToString(
                        dataTable.Rows[0]["nome"]);
                cliente.Rg =
                    Convert.ToString(
                        dataTable.Rows[0]["rg"]);
                cliente.CPF =
                    Convert.ToChar(
                        dataTable.Rows[0]["cpf"]);
                cliente.DtNascimento =
                    Convert.ToDateTime(
                        dataTable.Rows[0]["dt_nascimento"]);
                cliente.Telefone =
                    Convert.ToString(
                        dataTable.Rows[0]["telefone"]);

                //Após mapeado o objeto
                //Retorno o objeto cliente mapeado
                return cliente;
            }
            else 
                //Se não tiver nenhum retorno
                //Meu objeto recebera nullo
                return null;
        }
    }
}

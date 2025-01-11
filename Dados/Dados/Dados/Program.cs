using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Dados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = "Data Source = LAPTOP-58T32C62;Initial Catalog=autoIPCA;Integrated Security=true";
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            bool autenticado = false;

            while (!autenticado)
            {
                Console.WriteLine("Login:");
                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                if (ValidarCredenciais(con, username, password))
                {
                    Console.WriteLine("Login bem-sucedido!");
                    autenticado = true;
                }
                else
                {
                    Console.WriteLine("Credenciais inválidas. Tente novamente.");
                }
            }

            int comando = -1;

            while (true)
            {
                Console.WriteLine("Comandos:\n" +
                    "0-Sair;\n" +
                    "1-Listar Clientes;\n" +
                    "2-Listar Vendedores;\n" +
                    "3-Listar Veiculos;\n" +
                    "4-Listar Alugueis de um cliente;\n"+
                    "5-Listar Multas de um cliente;\n"+
                    "6-Insere um cliente;\n" +
                    "7-Edita um cliente;\n" +
                    "8-Elimina um cliente;\n" +
                    "9-Inserir vendedor;\n" +
                    "10-Editar vendedor;\n" +
                    "11-Eliminar vendedor;\n" +
                    "12-Inserir Veiculo;\n" +
                    "13-Editar Veiculo;\n" +
                    "14-Eliminar Veiculo;\n"+
                    "Isira uma opção: "
                    );

                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out comando))
                {
                    // Sair
                    if (comando == 0)
                    {
                        break;
                    }
                    //Listar Clientes
                    else if (comando == 1)
                    {
                        ListarClientes(con);
                    }
                    //Listar Vendedores
                    else if (comando == 2)
                    {
                        ListarVendedores(con);
                    }
                    //Listar Carros
                    else if (comando == 3)
                    {
                        ListarCarros(con);
                    }
                    //Listar alugueis de um cliente
                    else if (comando == 4)
                    {
                        int clienteId;
                        Console.WriteLine("Escreva o ID do cliente: ");
                        int.TryParse(Console.ReadLine(), out clienteId);
                        ListarAlugueis(con, clienteId);
                    }
                    //Listar multas de um cliente
                    else if (comando == 5)
                    {
                        int clienteId;
                        Console.WriteLine("Escreva o ID do cliente: ");
                        int.TryParse(Console.ReadLine(), out clienteId);
                        ListarMultas(con, clienteId);
                    }
                    //Inserir cliente novo
                    else if (comando == 6)
                    {
                        int clienteId;
                        Console.WriteLine("Escreva o ID do cliente: ");
                        int.TryParse(Console.ReadLine(), out clienteId);

                        Console.WriteLine("Escreva o nome do cliente: ");
                        string nome = Console.ReadLine();

                        Console.WriteLine("Escreva o e-mail do cliente: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Escreva a data de nascimento do cliente(AAAA-MM-DD): ");
                        string dataNasc = Console.ReadLine();

                        int CPid;
                        Console.WriteLine("Escreva o código postal do cliente: ");
                        int.TryParse(Console.ReadLine(), out CPid);

                        Console.WriteLine("Escreva a localidade do cliente: ");
                        string localidade = Console.ReadLine();

                        int contactoId;
                        Console.WriteLine("Escreva o ID do contacto: ");
                        int.TryParse(Console.ReadLine(), out contactoId);

                        long Telefone;
                        Console.WriteLine("Escreva o telefone do cliente: ");
                        long.TryParse(Console.ReadLine(), out Telefone);

                        InserirCliente(con, clienteId, nome, email, dataNasc, CPid, localidade, contactoId, Telefone);
                    }
                    //Edita um cliente
                    else if (comando == 7)
                    {
                        int clienteId;
                        Console.WriteLine("Escreva o ID do cliente a editar: ");
                        int.TryParse(Console.ReadLine(), out clienteId);
                        EditaCliente(con, clienteId);
                    }
                    //Elimina um cliente
                    else if (comando == 8)
                    {
                        int clienteId;
                        Console.WriteLine("Escreva o ID do cliente a editar: ");
                        int.TryParse(Console.ReadLine(), out clienteId);
                        EliminaCliente(con, clienteId);
                    }
                    //Inserir vendedor novo
                    else if (comando == 9)
                    {
                        int VendedorId;
                        Console.WriteLine("Escreva o ID do vendedor: ");
                        int.TryParse(Console.ReadLine(), out VendedorId);

                        Console.WriteLine("Escreva o nome do vendedor: ");
                        string nome = Console.ReadLine();

                        Console.WriteLine("Escreva o e-mail do vendedor: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Escreva a data de nascimento do vendedor(AAAA-MM-DD): ");
                        string dataNasc = Console.ReadLine();

                        InserirVendedor(con, VendedorId, nome, email, dataNasc);
                    }
                    //Edita um vendedor
                    else if (comando == 10)
                    {
                        int VendedorID;
                        Console.WriteLine("Escreva o ID do vendedor a editar: ");
                        int.TryParse(Console.ReadLine(), out VendedorID);
                        EditaVendedor(con, VendedorID);
                    }
                    //Elimina um vendedor
                    else if (comando == 11)
                    {
                        int VendedorID;
                        Console.WriteLine("Escreva o ID do vendedor a eliminar: ");
                        int.TryParse(Console.ReadLine(), out VendedorID);
                        EliminaVendedor(con, VendedorID);
                    }
                    //Inserir veiculo novo
                    else if (comando == 12)
                    {
                        int VeiculoId;
                        Console.WriteLine("Escreva o ID do veiculo: ");
                        int.TryParse(Console.ReadLine(), out VeiculoId);

                        Console.WriteLine("Escreva a matricula do vendedor: ");
                        string Matricula = Console.ReadLine();

                        int Lugares;
                        Console.WriteLine("Escreva o nº de lugares do veiculo: ");
                        int.TryParse(Console.ReadLine(), out Lugares);

                        int Preco;
                        Console.WriteLine("Escreva o preço do veiculo: ");
                        int.TryParse(Console.ReadLine(), out Preco);

                        int Ano;
                        Console.WriteLine("Escreva o ano do veiculo: ");
                        int.TryParse(Console.ReadLine(), out Ano);

                        Console.WriteLine("Escreva a cor do veiculo: ");
                        string Cor = Console.ReadLine();

                        Console.WriteLine("Escreva a marca do veiculo: ");
                        int MarcaID = ProcuraMarca(con, Console.ReadLine());

                        Console.WriteLine("TIPOS DE VEICULO");
                        ListarTiposVeic(con);

                        int TipoVeic;
                        Console.WriteLine("Escreva o ID do tipo de veiculo: ");
                        int.TryParse(Console.ReadLine(), out TipoVeic);


                        InserirVeiculo(con, VeiculoId, Matricula, Lugares, Preco, Ano, Cor, MarcaID, TipoVeic);
                    }
                    //Edita um veiculo
                    else if (comando == 13)
                    {
                        int VeiculoID;
                        Console.WriteLine("Escreva o ID do veiculo a editar: ");
                        int.TryParse(Console.ReadLine(), out VeiculoID);
                        EditaVeiculo(con, VeiculoID);
                    }
                    //Elimina um Veiculo
                    else if (comando == 14)
                    {
                        int VeiculoID;
                        Console.WriteLine("Escreva o ID do veiculo a eliminar: ");
                        int.TryParse(Console.ReadLine(), out VeiculoID);
                        EliminaVeiculo(con, VeiculoID);
                    }
                    //Em caso de erro
                    else
                    {
                        Console.WriteLine("Comando inválido. Tente novamente.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número.\n");
                }

                comando = -1;
            }

            Console.ReadLine();
        }

        #region Vendedores

        /// <summary>
        /// Lista todos os Vendedores
        /// </summary>
        /// <param name="con"></param>
        public static void ListarVendedores(SqlConnection con)
        {
            string query = "SELECT * FROM Vendedor;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem vendedores registados.");
            }
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": " + reader.GetValue(1) + "; " + reader.GetValue(2) + "; " + reader.GetValue(3) + "; " + reader.GetValue(4) + "; ");
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Inserir novo vendedor
        /// </summary>
        /// <param name="con"></param>
        /// <param name="VendedorID"></param>
        /// <param name="Nome"></param>
        /// <param name="Email"></param>
        /// <param name="DataNasc"></param>
        public static void InserirVendedor(SqlConnection con, int VendedorID, string Nome, string Email, string DataNasc)
        {
            string query = $"INSERT INTO Vendedor (VendedorID, Nome, Email, NumeroDeVendas, DataNasc) VALUES({VendedorID}, '{Nome}', '{Email}', 0, '{DataNasc}');";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Vendedor inserido com sucesso!");

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Procura um vendedor por ID
        /// </summary>
        /// <param name="con"></param>
        /// <param name="VendedorID"></param>
        /// <returns></returns>
        public static bool ProcuraVendedor(SqlConnection con, int VendedorID)
        {
            string query = $"SELECT * FROM Vendedor WHERE VendedorID={VendedorID};";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem vendedores com esse ID.");
                Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
                return false;
            }
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": " + reader.GetValue(1) + "; " + reader.GetValue(2) + "; " + reader.GetValue(3) + "; " + reader.GetValue(4) + "; ");
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
            return true;
        }

        /// <summary>
        /// Edita um certo vendedor
        /// </summary>
        /// <param name="con"></param>
        /// <param name="VendedorID"></param>
        public static void EditaVendedor(SqlConnection con, int VendedorID)
        {
            if (!ProcuraVendedor(con, VendedorID))
                return;

            Console.WriteLine("Que dado deseja alterar?\n" +
                "0-Voltar;\n" +
                "1-Nome;\n" +
                "2-E-mail;\n" +
                "3-Data de Nascimento.");

            string entrada = Console.ReadLine();
            int opcao;
            string parametro = "";
            if (int.TryParse(entrada, out opcao))
            {
                if (opcao == 1)
                {
                    parametro = "Nome";
                }
                else if (opcao == 2)
                {
                    parametro = "Email";
                }
                else if (opcao == 3)
                {
                    parametro = "DataNasc";
                }
                else
                {
                    return;
                }
            }

            Console.WriteLine($"Insira o novo valor do parametro '{parametro}':");
            string valor = Console.ReadLine();

            string query = $"UPDATE Vendedor SET Vendedor.{parametro} = '{valor}' WHERE VendedorID = {VendedorID};";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Vendedor alterado com sucesso!");

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Elimina um vendedor
        /// </summary>
        /// <param name="con"></param>
        /// <param name="VendedorID"></param>
        public static void EliminaVendedor(SqlConnection con, int VendedorID)
        {
            ProcuraVendedor(con, VendedorID);

            Console.WriteLine("Deseja mesmo eliminar este Cliente?(y/n)");
            string opcao = Console.ReadLine().ToLower();

            if (opcao == "y")
            {
                string query = $"DELETE FROM ClienteVendedor WHERE VendedorID = {VendedorID};" +
                    $"DELETE FROM Aluguer WHERE VendedorID = {VendedorID};" +
                    $"DELETE FROM Contacto WHERE VendedorID = {VendedorID};" +
                    $"DELETE FROM Vendedor WHERE VendedorID = {VendedorID};";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Vendedor eliminado com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }

            Console.WriteLine("\n");
        }

        #endregion

        #region Veiculos
        
        /// <summary>
        /// Lista todos os Carros
        /// </summary>
        /// <param name="con"></param>
        public static void ListarCarros(SqlConnection con)
        {
            string query = "SELECT Veiculo.VeicID,Veiculo.Matricula,Veiculo.Lugares,Veiculo.Preco,Veiculo.Ano,Veiculo.Cor,Marca.Nome AS MarcaNome,TipoDeVeiculo.Tipo AS TipoVeiculo FROM Veiculo JOIN Marca ON Marca.MarcaID = Veiculo.MarcaID JOIN TipoDeVeiculo ON TipoDeVeiculo.TipoVeilID = Veiculo.TipoVeilID;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem nenhuns carros registados.");
            }

            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": Matricula: " + reader.GetValue(1) + "; Nº lugares: " + reader.GetValue(2) + "; Preço: " + reader.GetValue(3) + "; Ano: " + reader.GetValue(4) + "; Cor: " + reader.GetValue(5) + "; marca: " + reader.GetValue(6) + "; Tipo: " + reader.GetValue(7));
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
        }

        public static void InserirVeiculo(SqlConnection con, int VeicID, string Matricula, int Lugares, int Preco, int Ano, string Cor, int MarcaID, int TipoVeiculoID)
        {
            string query = $"INSERT INTO Veiculo (VeicID, Matricula, Lugares, Preco, Ano, Cor, MarcaID, TipoVeilID) VALUES({VeicID}, '{Matricula}', '{Lugares}', '{Preco}', '{Ano}', '{Cor}', '{MarcaID}', '{TipoVeiculoID}');";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Veiculo inserido com sucesso!");

            Console.WriteLine("\n");
        }

        public static bool ProcuraVeiculo(SqlConnection con, int VeiculoID)
        {
            string query = $"SELECT Veiculo.VeicID,Veiculo.Matricula,Veiculo.Lugares,Veiculo.Preco,Veiculo.Ano,Veiculo.Cor,Marca.Nome AS MarcaNome,TipoDeVeiculo.Tipo AS TipoVeiculo FROM Veiculo JOIN Marca ON Marca.MarcaID = Veiculo.MarcaID JOIN TipoDeVeiculo ON TipoDeVeiculo.TipoVeilID = Veiculo.TipoVeilID WHERE VeicID={VeiculoID};";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem veiculos com esse ID.");
                Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
                return false;
            }
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": " + reader.GetValue(1) + "; " + reader.GetValue(2) + "; " + reader.GetValue(3) + "; " + reader.GetValue(4) + "; " + reader.GetValue(5) + "; " + reader.GetValue(6) + "; " + reader.GetValue(7) + "; ");
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
            return true;
        }

        public static void EditaVeiculo(SqlConnection con, int VeiculoID)
        {
            if (!ProcuraVeiculo(con, VeiculoID))
                return;

            Console.WriteLine("Que dado deseja alterar?\n" +
                "0-Voltar;\n" +
                "1-Matricula;\n" +
                "2-Nº lugares;\n" +
                "3-Preço;\n" +
                "4-Ano;\n" +
                "5-Cor;\n" +
                "6-Marca;\n" +
                "7-Tipo Veiculo.");

            string entrada = Console.ReadLine();
            int opcao;
            string parametro = "";
            if (int.TryParse(entrada, out opcao))
            {
                if (opcao == 1)
                {
                    parametro = "Matricula";
                }
                else if (opcao == 2)
                {
                    parametro = "Lugares";
                }
                else if (opcao == 3)
                {
                    parametro = "Preco";
                }
                else if (opcao == 4)
                {
                    parametro = "Ano";
                }
                else if (opcao == 5)
                {
                    parametro = "Cor";
                }
                else if (opcao == 6)
                {
                    parametro = "MarcaID";
                }
                else if (opcao == 7)
                {
                    parametro = "TipoVeilID";
                }
                else
                {
                    return;
                }
            }

            string valor = "";

            if (parametro == "MarcaID")
            {
                Console.WriteLine($"Insira o nome da marca pretendida:");
                valor = Console.ReadLine();
                valor = Convert.ToString(ProcuraMarca(con, valor));
            }
            else if (parametro == "TipoVeilID")
            {
                ListarTiposVeic(con);

                Console.WriteLine($"Insira o ID do tipo de veiculo pretendido:");
                valor = Console.ReadLine();
            }
            else {
                Console.WriteLine($"Insira o novo valor do parametro '{parametro}':");
                valor = Console.ReadLine();
            }

            string query = $"UPDATE Veiculo SET Veiculo.{parametro} = '{valor}' WHERE VeicID = {VeiculoID};";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Veiculo alterado com sucesso!");

            Console.WriteLine("\n");
        }

        public static void EliminaVeiculo(SqlConnection con, int VeiculoID)
        {
            ProcuraCliente(con, VeiculoID);

            Console.WriteLine("Deseja mesmo eliminar este Veiculo?(y/n)");
            string opcao = Console.ReadLine().ToLower();

            if (opcao == "y")
            {
                string query = $"DELETE FROM Aluguer WHERE VeicID = {VeiculoID};" +
                    $"DELETE FROM Manutencao WHERE VeicID = {VeiculoID};" +
                    $"DELETE FROM Veiculo WHERE VeicID = {VeiculoID};";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Veiculo eliminado com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }

            Console.WriteLine("\n");
        }

        #endregion

        public static int ProcuraMarca(SqlConnection con, string marca)
        {
            string query = $"SELECT * FROM Marca WHERE Nome='{marca}';";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int marcaID = 0;

            if (!reader.HasRows)
            {
                // cria marca
                reader.Close();
                marcaID=InserirMarca(con, marca);
                return marcaID;
            }
            while (reader.Read())
            {
                marcaID = int.Parse(reader["MarcaID"].ToString());
                reader.Close();
                return marcaID;
            }
            return marcaID;
        }

        public static int InserirMarca(SqlConnection con, string Nome)
        {
            string query = $"SELECT MAX(MarcaID) AS MaiorID FROM Marca;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int marcaID = 1;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    marcaID = int.Parse(reader["MaiorID"].ToString());
                }
                reader.Close();
            }


            query = $"INSERT INTO Marca (MarcaID, Nome) VALUES({marcaID}, '{Nome}');";

            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Marca inserida com sucesso!");
            return marcaID;
        }

        public static void ListarTiposVeic(SqlConnection con)
        {
            string query = "SELECT * FROM TipoDeVeiculo;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem nenhum Tipo de Veiculo registados.");
            }

            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": Tipo: " + reader.GetValue(1));
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Lista todos os Carros
        /// </summary>
        /// <param name="con"></param>
        public static void ListarAlugueis(SqlConnection con, int clienteId)
        {
            string query = "SELECT Aluguer.AluguerID,Aluguer.Custo,Aluguer.Comissao,Aluguer.Data,Veiculo.Matricula FROM Aluguer JOIN Veiculo ON Aluguer.VeicID = Veiculo.VeicID WHERE Aluguer.ClientID = "+clienteId;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem alugueis associados a este cliente.");
            }
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": Custo: " + reader.GetValue(1) + "; Comissão: " + reader.GetValue(2) + "; Data: " + reader.GetValue(3) + "; Matricula: " + reader.GetValue(4));
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
        }

        public static void ListarMultas(SqlConnection con, int clienteId)
        {
            string query = "SELECT MultaID,Valor,StatusPag,DataMulta,TipoMulta.Tipo FROM Multa JOIN TipoMulta ON TipoMulta.TMultaID=Multa.MultaID WHERE Multa.ClienteID = " + clienteId;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem multas associadas a este cliente.");
            }

            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": Valor: " + reader.GetValue(1) + "; Status de pagamento: " + reader.GetValue(2) + "; Data da Multa: " + reader.GetValue(3) + "; Tipo de Multa: " + reader.GetValue(4));
            }

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            reader.Close();
            Console.WriteLine("\n");
        }
        
        #region Clientes
        public static void InserirCliente(SqlConnection con, int ClienteID, string Nome, string Email, string DataNasc, int CPid, string localidade, int contactoId, long Telefone)
        {
            string query = $"INSERT INTO Cliente (ClienteID, Nome, Email, DataNasc, NumeroDeAlugures, CPid) VALUES({ClienteID}, '{Nome}', '{Email}', '{DataNasc}', 0, {CPid});" +
                $"INSERT INTO Contacto (CID, Email, Telefone, TPID, ClientID, VendedorID) VALUES({contactoId}, '{Email}', {Telefone}, 1, {ClienteID}, NULL)";

            string aux = $"SELECT * FROM CP WHERE CP.CPid={CPid}";
            SqlCommand cmd = new SqlCommand(aux, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                query += $"INSERT INTO CP (CPid, Localidade) VALUES({CPid}, '{localidade}'";
            }

            reader.Close();

            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Cliente inserido com sucesso!");

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        /// <param name="con"></param>
        public static void ListarClientes(SqlConnection con)
        {
            string query = "SELECT * FROM Cliente;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

            if (!reader.HasRows)
            {
                Console.WriteLine("Não existem clientes registados.");
            }
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0) + ": " + reader.GetValue(1) + "; " + reader.GetValue(2) + "; " + reader.GetValue(3) + "; " + reader.GetValue(4) + "; " + reader.GetValue(5));
            }
            reader.Close();
            Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Procura um cliente pelo ID usando uma stored procedure
        /// </summary>
        /// <param name="con"></param>
        /// <param name="ClienteID"></param>
        public static void ProcuraCliente(SqlConnection con, int ClienteID)
        {
            string storedProcedure= "ProcurarCliente";
            using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Adicionar parâmetro
                cmd.Parameters.Add(new SqlParameter("@ClienteID", ClienteID));


                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n----------------------------------------------------------------------------------------\n");

                if (!reader.HasRows)
                {
                    Console.WriteLine("Não existe nenhum cliente com esse ID.");
                }
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetValue(0) + ": " + reader.GetValue(1) + "; " + reader.GetValue(2) + "; " + reader.GetValue(3) + "; " + reader.GetValue(4) + "; " + reader.GetValue(5));
                }
                reader.Close();
                Console.WriteLine("\n----------------------------------------------------------------------------------------\n");
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// Edita um parametro de um cliente
        /// </summary>
        /// <param name="con"></param>
        /// <param name="ClienteID"></param>
        public static void EditaCliente(SqlConnection con, int ClienteID)
        {
            ProcuraCliente(con, ClienteID);

            Console.WriteLine("Que dado deseja alterar?\n" +
                "0-Voltar;\n" +
                "1-Nome;\n" +
                "2-E-mail;\n" +
                "3-Data de Nascimento;\n" +
                "4-Código Postal.");

            string entrada = Console.ReadLine();
            int opcao;
            string parametro = "";
            if (int.TryParse(entrada, out opcao))
            {
                if (opcao == 1)
                {
                    parametro = "Nome";
                }
                else if (opcao == 2)
                {
                    parametro = "Email";
                }
                else if (opcao == 3)
                {
                    parametro = "DataNasc";
                }
                else if (opcao == 4)
                {
                    parametro = "CPid";
                }
                else
                {
                    return;
                }
            }

            Console.WriteLine($"Insira o novo valor do parametro '{parametro}':");
            string valor = Console.ReadLine();

            string query = $"UPDATE Cliente SET Cliente.{parametro} = '{valor}' WHERE ClienteID = {ClienteID};";
            SqlCommand cmd;

            //Sefor para alterar CP, verifica se já existe esse código
            if (parametro== "CPid")
            {
                string aux = $"SELECT * FROM CP WHERE CP.CPid={valor}";
                cmd = new SqlCommand(aux, con);
                SqlDataReader reader = cmd.ExecuteReader();
                //Se não existe é criado
                if (!reader.HasRows)
                {
                    reader.Close();
                    Console.WriteLine("Insira o nome da localidade:");
                    string localidade = Console.ReadLine();
                    aux = $"INSERT INTO CP (CPid, Localidade) VALUES({valor}, '{localidade}');";
                    cmd = new SqlCommand(aux, con);
                    cmd.ExecuteNonQuery();
                }
            }

            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Cliente alterado com sucesso!");

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Elimina 1 cliente
        /// </summary>
        /// <param name="con"></param>
        /// <param name="ClienteID"></param>
        public static void EliminaCliente(SqlConnection con, int ClienteID)
        {
            ProcuraCliente(con, ClienteID);

            Console.WriteLine("Deseja mesmo eliminar este Cliente?(y/n)");
            string opcao = Console.ReadLine().ToLower();

            if (opcao == "y")
            {
                string query = $"DELETE FROM Multa WHERE ClienteID = {ClienteID};" +
                    $"DELETE FROM Contacto WHERE ClientID = {ClienteID};" +
                    $"DELETE FROM Aluguer WHERE ClientID = {ClienteID};" +
                    $"DELETE FROM ClienteVendedor WHERE ClienteID = {ClienteID};" +
                    $"DELETE FROM Cliente WHERE ClienteID = {ClienteID};";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Cliente eliminado com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }

            Console.WriteLine("\n");
        }

        #endregion

        #region Login

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return "0x" + BitConverter.ToString(hashBytes).Replace("-", ""); // Converte para hexadecimal
            }
        }

        public static bool ValidarCredenciais(SqlConnection con, string username, string password)
        {
            string query = "SELECT PasswordHash FROM Utilizador WHERE Username = @Username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", username);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string storedHash = reader["PasswordHash"].ToString();
                reader.Close();

                // Compara o hash da palavra-passe fornecida com o armazenado
                return storedHash == HashPassword(password);
            }

            reader.Close();
            return false; // Nome de utilizador não encontrado
        }

        #endregion


    }
}

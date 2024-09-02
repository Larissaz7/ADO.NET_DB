using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjetoFinalBDD.Models;

namespace projetoFinalBDD.Data
{
    public class PessoaDataAccess
    {
        private readonly string _connectionString;

        public PessoaDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void AddPessoa(Pessoa pessoa)
        {
            using (var connection = GetConnection())
            {
                string query = "INSERT INTO Pessoas (Nome, Telefone) VALUES (@Nome, @Telefone)";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Nome", pessoa.Nome));
                    command.Parameters.Add(new SqlParameter("@Telefone", pessoa.Telefone));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePessoa(Pessoa pessoa)
        {
            using (var connection = GetConnection())
            {
                string query = "UPDATE Pessoas SET Nome = @Nome, Telefone = @Telefone WHERE Id = @Id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", pessoa.Id));
                    command.Parameters.Add(new SqlParameter("@Nome", pessoa.Nome));
                    command.Parameters.Add(new SqlParameter("@Telefone", pessoa.Telefone));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePessoa(int id)
        {
            using (var connection = GetConnection())
            {
                string query = "DELETE FROM Pessoas WHERE Id = @Id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Pessoa> GetAllPessoas()
        {
            var pessoas = new List<Pessoa>();
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM Pessoas";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pessoa = new Pessoa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                            };
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }
            return pessoas;
        }

        public Pessoa GetPessoaById(int id)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM Pessoas WHERE Id = @Id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pessoa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Pessoa> GetPessoasByCodigoENome(int id, string nome)
        {
            var pessoas = new List<Pessoa>();
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM Pessoas WHERE Id = @Id AND Nome LIKE '%' + @Nome + '%'";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.Parameters.Add(new SqlParameter("@Nome", nome));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pessoa = new Pessoa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                            };
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }
            return pessoas;
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjetoFinalBDD.Models;

namespace projetoFinalBDD.Data
{
    public class TelefoneDataAccess
    {
        private readonly string _connectionString;

        public TelefoneDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void AddTelefone(Telefone telefone)
        {
            using (var connection = GetConnection())
            {
                string query = "INSERT INTO Telefones (Numero, PessoaId) VALUES (@Numero, @PessoaId)";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Numero", telefone.Numero));
                    command.Parameters.Add(new SqlParameter("@PessoaId", telefone.PessoaId));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTelefone(Telefone telefone)
        {
            using (var connection = GetConnection())
            {
                string query = "UPDATE Telefones SET Numero = @Numero WHERE Id = @Id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", telefone.Id));
                    command.Parameters.Add(new SqlParameter("@Numero", telefone.Numero));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTelefone(int id)
        {
            using (var connection = GetConnection())
            {
                string query = "DELETE FROM Telefones WHERE Id = @Id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Telefone> GetTelefonesByPessoaId(int pessoaId)
        {
            var telefones = new List<Telefone>();
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM Telefones WHERE PessoaId = @PessoaId";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@PessoaId", pessoaId));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var telefone = new Telefone
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Numero = reader.GetString(reader.GetOrdinal("Numero")),
                                PessoaId = reader.GetInt32(reader.GetOrdinal("PessoaId"))
                            };
                            telefones.Add(telefone);
                        }
                    }
                }
            }
            return telefones;
        }

        public Telefone GetTelefoneByCodigoENumero(int id, string numero)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM Telefones WHERE Id = @Id AND Numero = @Numero";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.Parameters.Add(new SqlParameter("@Numero", numero));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Telefone
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Numero = reader.GetString(reader.GetOrdinal("Numero")),
                                PessoaId = reader.GetInt32(reader.GetOrdinal("PessoaId"))
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}

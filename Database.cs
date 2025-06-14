using System;
using System.Collections.Generic;
using Npgsql;

namespace Crud_Personne
{
    public class Database
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=CRUD-Boost";

        // Méthode pour tester la connexion
        public void Connect()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connexion réussie à PostgreSQL !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de connexion : " + ex.Message);
            }
        }

        // Méthode pour créer la table (si tu veux automatiser cela)
        public void CreatePersonTable()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    CREATE TABLE IF NOT EXISTS personnes (
                        id SERIAL PRIMARY KEY,
                        nom VARCHAR(100),
                        prenoms VARCHAR(100),
                        age INT,
                        numero_tel VARCHAR(20)
                    );
                ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Ajouter une personne
        public void AjouterPersonne(Personne p)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO personnes (nom, prenoms, age, numero_tel) VALUES (@nom, @prenoms, @age, @numero_tel)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nom", p.Nom);
                    cmd.Parameters.AddWithValue("prenoms", p.Prenoms);
                    cmd.Parameters.AddWithValue("age", p.Age);
                    cmd.Parameters.AddWithValue("numero_tel", p.NumeroTel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Récupérer toutes les personnes
        public List<Personne> GetPersonnes()
        {
            List<Personne> personnes = new List<Personne>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM personnes ORDER BY id DESC";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personnes.Add(new Personne
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            Prenoms = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            NumeroTel = reader.GetString(4)
                        });
                    }
                }
            }
            return personnes;
        }

        // Modifier une personne
        public void ModifierPersonne(Personne p)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE personnes SET nom = @nom, prenoms = @prenoms, age = @age, numero_tel = @numero_tel WHERE id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nom", p.Nom);
                    cmd.Parameters.AddWithValue("prenoms", p.Prenoms);
                    cmd.Parameters.AddWithValue("age", p.Age);
                    cmd.Parameters.AddWithValue("numero_tel", p.NumeroTel);
                    cmd.Parameters.AddWithValue("id", p.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Supprimer une personne
        public void SupprimerPersonne(int id)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM personnes WHERE id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

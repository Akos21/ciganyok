using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using TesztLap.Model;

namespace TesztLap.Data
{
    public class Database
    {
        private static readonly string filename = "../../Data/Users.sqlite";
        private readonly string ConnectionString = $"Data Source={filename};Version=3;";
        private SQLiteConnection _connection;

        public Database()
        {
            CreateFile();
            _connection = CreateConnection();
            SetupTable();
            //AddRow("Akos", "alma123", 0f);
        }

        private void CreateFile()
        {
            if (!File.Exists(filename))
            {
                SQLiteConnection.CreateFile(filename);
            }
        }

        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("../../Data/errors.txt"))
                {
                    DateTime time = DateTime.Now;
                    sw.WriteLine($"{time.Month}/{time.Day}/{time.Hour}:{time.Minute}:{time.Second} - {ex.Message}");
                }
            }

            return connection;
        }

        private void SetupTable()
        {
            string txt = "CREATE TABLE IF NOT EXISTS users (username VARCHAR(255), password VARCHAR(255), eredmeny double);";
            SQLiteCommand cmd = new SQLiteCommand(txt, _connection);

            cmd.ExecuteNonQuery();
        }

        public List<Felhasznalo> ReadTable()
        {
            string txt = "SELECT username, password, eredmeny FROM users;";
            SQLiteCommand cmd = new SQLiteCommand(txt, _connection);

            var reader = cmd.ExecuteReader();

            List<Felhasznalo> Users = new List<Felhasznalo>();

            while (reader.Read())
            {
                Users.Add(new Felhasznalo(reader));
            }

            return Users;
        }

        public void AddRow(string user, string pass)
        {
            string txt = $"INSERT INTO users (username, password, eredmeny) VALUES ('{user}', '{pass}', 0.0);";
            SQLiteCommand cmd = new SQLiteCommand(txt, _connection);

            cmd.ExecuteNonQuery();
        }

        public void AddRow(string user, string pass, float eredmeny)
        {
            string txt = $"INSERT INTO users (username, password, eredmeny) VALUES ('{user}', '{pass}', {eredmeny});";
            SQLiteCommand cmd = new SQLiteCommand(txt, _connection);

            cmd.ExecuteNonQuery();
        }

        public void EditEredmeny(string username, double eredmeny)
        {
            string txt = $"UPDATE users SET eredmeny = {eredmeny} WHERE username = '{username}';";
            SQLiteCommand cmd = new SQLiteCommand(txt, _connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteRow(string user)
        {
            string txt = $"DELETE FROM users WHERE username = '{user}';";
            SQLiteCommand cmd = new SQLiteCommand(txt, _connection);
            cmd.ExecuteNonQuery();
        }
    }
}

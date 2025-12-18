using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

namespace YüzmeAntrenmanıPlanlama
{
    public class DbManager
    {
        private string dbFileName = "yuzme_planlama.db";
        private string connectionString;

        public DbManager()
        {
            connectionString = $"Data Source={dbFileName};Version=3;";
            CheckAndCreateDatabase();
        }

        private void CheckAndCreateDatabase()
        {
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string sqlStudents = @"
                        CREATE TABLE IF NOT EXISTS Ogrenciler (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Ad TEXT NOT NULL,
                            Soyad TEXT NOT NULL,
                            GrupAdi TEXT NOT NULL,
                            Yas INTEGER DEFAULT 0,
                            Boy INTEGER DEFAULT 0,
                            Kilo INTEGER DEFAULT 0
                        )";

                    string sqlLogs = @"
                        CREATE TABLE IF NOT EXISTS AntrenmanLoglari (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            GrupAdi TEXT NOT NULL,
                            Tarih DATETIME NOT NULL,
                            Mesafe INTEGER NOT NULL,
                            Sure INTEGER NOT NULL,
                            Icerik TEXT 
                        )";

                    using (var cmd = new SQLiteCommand(sqlStudents, conn)) cmd.ExecuteNonQuery();
                    using (var cmd = new SQLiteCommand(sqlLogs, conn)) cmd.ExecuteNonQuery();
                }
            }
        }

        // --- ÖĞRENCİ İŞLEMLERİ ---
        public void AddStudent(string ad, string soyad, string grup)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Ogrenciler (Ad, Soyad, GrupAdi) VALUES (@Ad, @Soyad, @Grup)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Grup", grup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(string ad, string soyad, string grup)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Ogrenciler WHERE Ad=@Ad AND Soyad=@Soyad AND GrupAdi=@Grup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Grup", grup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllInGroup(string grup)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Ogrenciler WHERE GrupAdi=@Grup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Grup", grup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- GÜNCELLENMİŞ METOT: ARTIK GRUP ADINI DA DEĞİŞTİRİYOR ---
        public void UpdateStudentDetails(string ad, string soyad, string eskiGrup, string yeniGrup, int yas, int boy, int kilo)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                // Şartımız eski grup adı, güncellediğimiz ise yeni grup adı
                string sql = "UPDATE Ogrenciler SET GrupAdi=@YeniGrup, Yas=@Yas, Boy=@Boy, Kilo=@Kilo WHERE Ad=@Ad AND Soyad=@Soyad AND GrupAdi=@EskiGrup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@YeniGrup", yeniGrup);
                    cmd.Parameters.AddWithValue("@Yas", yas);
                    cmd.Parameters.AddWithValue("@Boy", boy);
                    cmd.Parameters.AddWithValue("@Kilo", kilo);
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@EskiGrup", eskiGrup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataRow GetStudentDetails(string ad, string soyad, string grup)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Yas, Boy, Kilo FROM Ogrenciler WHERE Ad=@Ad AND Soyad=@Soyad AND GrupAdi=@Grup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Grup", grup);

                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0) return dt.Rows[0];
                        return null;
                    }
                }
            }
        }

        public void DeleteTrainingLog(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM AntrenmanLoglari WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetFormattedStudentList()
        {
            List<string> list = new List<string>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Ad, Soyad, GrupAdi FROM Ogrenciler ORDER BY GrupAdi, Ad";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string line = $"{reader["Ad"]} {reader["Soyad"]} ({reader["GrupAdi"]})";
                        list.Add(line);
                    }
                }
            }
            return list;
        }

        public void AddTrainingLog(string grup, DateTime tarih, int mesafe, int sure, string jsonIcerik)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO AntrenmanLoglari (GrupAdi, Tarih, Mesafe, Sure, Icerik) VALUES (@Grup, @Tarih, @Mesafe, @Sure, @Icerik)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Grup", grup);
                    cmd.Parameters.AddWithValue("@Tarih", tarih);
                    cmd.Parameters.AddWithValue("@Mesafe", mesafe);
                    cmd.Parameters.AddWithValue("@Sure", sure);
                    cmd.Parameters.AddWithValue("@Icerik", jsonIcerik);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetTrainingHistoryByGroup(string grupAdi)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Tarih, Sure, Mesafe, Icerik FROM AntrenmanLoglari WHERE GrupAdi = @Grup ORDER BY Tarih DESC";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Grup", grupAdi);
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dt.Columns.Add("TarihFormatted", typeof(string));
                        dt.Columns.Add("OrtHiz", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime d = Convert.ToDateTime(row["Tarih"]);
                            row["TarihFormatted"] = d.ToString("dd.MM.yyyy HH:mm");
                            int dist = Convert.ToInt32(row["Mesafe"]);
                            int time = Convert.ToInt32(row["Sure"]);
                            if (dist > 0)
                            {
                                double pace = (double)time / (dist / 100.0);
                                row["OrtHiz"] = pace.ToString("0.0");
                            }
                            else row["OrtHiz"] = "-";
                        }
                        return dt;
                    }
                }
            }
        }
    }
}
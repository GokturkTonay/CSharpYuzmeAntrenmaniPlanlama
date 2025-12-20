using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite; // SQLite NuGet paketi gereklidir
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YüzmeAntrenmanıPlanlama
{
    public class DbManager
    {
        private string connectionString = "Data Source=YuzmeAntrenmanDB.db;Version=3;";

        public DbManager()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists("YuzmeAntrenmanDB.db"))
            {
                SQLiteConnection.CreateFile("YuzmeAntrenmanDB.db");
            }

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sqlOgrenciler = @"CREATE TABLE IF NOT EXISTS Ogrenciler (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Ad TEXT NOT NULL,
                                            Soyad TEXT NOT NULL,
                                            Grup TEXT NOT NULL,
                                            Yas INTEGER DEFAULT 0,
                                            Boy INTEGER DEFAULT 0,
                                            Kilo INTEGER DEFAULT 0
                                            );";
                using (var cmd = new SQLiteCommand(sqlOgrenciler, conn)) cmd.ExecuteNonQuery();

                string sqlAntrenmanlar = @"CREATE TABLE IF NOT EXISTS Antrenmanlar (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              Grup TEXT NOT NULL,
                                              Tarih DATETIME NOT NULL,
                                              Mesafe INTEGER NOT NULL,
                                              Sure INTEGER NOT NULL,
                                              Icerik TEXT NOT NULL
                                              );";
                using (var cmd = new SQLiteCommand(sqlAntrenmanlar, conn)) cmd.ExecuteNonQuery();

                // Grup Sıralaması Tablosu
                string sqlGroupOrder = @"CREATE TABLE IF NOT EXISTS GroupOrder (
                                            GrupName TEXT PRIMARY KEY,
                                            OrderIndex INTEGER NOT NULL
                                        );";
                using (var cmd = new SQLiteCommand(sqlGroupOrder, conn)) cmd.ExecuteNonQuery();

                EnsureGroupOrderExists("A Grubu", 1, conn);
                EnsureGroupOrderExists("B Grubu", 2, conn);
                EnsureGroupOrderExists("C Grubu", 3, conn);
            }
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        // ===================================================================
        // ÖĞRENCİ İŞLEMLERİ (Özel Sıralamalı)
        // ===================================================================
        public DataTable GetAllStudents()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    // Öğrencileri özel grup sırasına göre çek
                    string sql = @"
                        SELECT T1.Ad, T1.Soyad, T1.Grup 
                        FROM Ogrenciler AS T1
                        LEFT JOIN GroupOrder AS T2 ON T1.Grup = T2.GrupName
                        ORDER BY IFNULL(T2.OrderIndex, 9999) ASC, T1.Grup ASC, T1.Ad ASC";

                    using (var adapter = new SQLiteDataAdapter(sql, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci listesi alınırken hata: " + ex.Message);
            }
            return dt;
        }

        public void AddStudent(string ad, string soyad, string grup)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                EnsureGroupOrderExists(grup, 9999, conn);

                string sql = "INSERT INTO Ogrenciler (Ad, Soyad, Grup) VALUES (@ad, @soyad, @grup)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@grup", grup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(string ad, string soyad, string grup)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Ogrenciler WHERE Ad=@ad AND Soyad=@soyad AND Grup=@grup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@grup", grup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllInGroup(string grup)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Ogrenciler WHERE Grup=@grup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@grup", grup);
                    cmd.ExecuteNonQuery();
                }

                string sqlOrder = "DELETE FROM GroupOrder WHERE GrupName=@grup";
                using (var cmd = new SQLiteCommand(sqlOrder, conn))
                {
                    cmd.Parameters.AddWithValue("@grup", grup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataRow GetStudentDetails(string ad, string soyad, string grup)
        {
            DataTable dt = new DataTable();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Ogrenciler WHERE Ad=@ad AND Soyad=@soyad AND Grup=@grup LIMIT 1";
                using (var adapter = new SQLiteDataAdapter(sql, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@ad", ad);
                    adapter.SelectCommand.Parameters.AddWithValue("@soyad", soyad);
                    adapter.SelectCommand.Parameters.AddWithValue("@grup", grup);
                    adapter.Fill(dt);
                }
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public void UpdateStudentDetails(string eskiAd, string eskiSoyad, string eskiGrup, string yeniGrup, int yas, int boy, int kilo)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                if (eskiGrup != yeniGrup) EnsureGroupOrderExists(yeniGrup, 9999, conn);

                string sql = "UPDATE Ogrenciler SET Grup=@yGrup, Yas=@yas, Boy=@boy, Kilo=@kilo WHERE Ad=@eAd AND Soyad=@eSoyad AND Grup=@eGrup";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@yGrup", yeniGrup);
                    cmd.Parameters.AddWithValue("@yas", yas);
                    cmd.Parameters.AddWithValue("@boy", boy);
                    cmd.Parameters.AddWithValue("@kilo", kilo);
                    cmd.Parameters.AddWithValue("@eAd", eskiAd);
                    cmd.Parameters.AddWithValue("@eSoyad", eskiSoyad);
                    cmd.Parameters.AddWithValue("@eGrup", eskiGrup);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ===================================================================
        // GRUP SIRALAMA YÖNETİMİ
        // ===================================================================
        private void EnsureGroupOrderExists(string grupName, int defaultIndex, SQLiteConnection openConn)
        {
            string checkSql = "SELECT COUNT(*) FROM GroupOrder WHERE GrupName = @grup";
            using (var cmdCheck = new SQLiteCommand(checkSql, openConn))
            {
                cmdCheck.Parameters.AddWithValue("@grup", grupName);
                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                if (count == 0)
                {
                    string insertSql = "INSERT INTO GroupOrder (GrupName, OrderIndex) VALUES (@grup, @idx)";
                    using (var cmdInsert = new SQLiteCommand(insertSql, openConn))
                    {
                        cmdInsert.Parameters.AddWithValue("@grup", grupName);
                        cmdInsert.Parameters.AddWithValue("@idx", defaultIndex);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpdateGroupOrder(string movingGroup, string targetGroup, bool insertBefore)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        List<string> currentOrder = new List<string>();
                        string sqlSelect = "SELECT GrupName FROM GroupOrder ORDER BY OrderIndex ASC";
                        using (var cmd = new SQLiteCommand(sqlSelect, conn, transaction))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) currentOrder.Add(reader.GetString(0));
                        }

                        if (currentOrder.Contains(movingGroup) && currentOrder.Contains(targetGroup))
                        {
                            currentOrder.Remove(movingGroup);
                            int targetIndex = currentOrder.IndexOf(targetGroup);
                            if (insertBefore)
                                currentOrder.Insert(targetIndex, movingGroup);
                            else
                                currentOrder.Insert(targetIndex + 1, movingGroup);

                            string sqlUpdate = "UPDATE GroupOrder SET OrderIndex = @idx WHERE GrupName = @grup";
                            for (int i = 0; i < currentOrder.Count; i++)
                            {
                                using (var cmdUpdate = new SQLiteCommand(sqlUpdate, conn, transaction))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@idx", i + 1);
                                    cmdUpdate.Parameters.AddWithValue("@grup", currentOrder[i]);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ===================================================================
        // ANTRENMAN GEÇMİŞİ İŞLEMLERİ
        // ===================================================================
        public void AddTrainingLog(string grup, DateTime tarih, int mesafe, int sure, string icerikJson)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Antrenmanlar (Grup, Tarih, Mesafe, Sure, Icerik) VALUES (@grp, @trh, @msf, @sur, @icrk)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@grp", grup);
                    cmd.Parameters.AddWithValue("@trh", tarih);
                    cmd.Parameters.AddWithValue("@msf", mesafe);
                    cmd.Parameters.AddWithValue("@sur", sure);
                    cmd.Parameters.AddWithValue("@icrk", icerikJson);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetTrainingHistoryByGroup(string grup)
        {
            DataTable dt = new DataTable();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"SELECT Id, Tarih, strftime('%d.%m.%Y %H:%M', Tarih) as TarihFormatted, 
                               Mesafe, Sure, CAST(Mesafe AS REAL) / Sure AS OrtHiz, Icerik 
                               FROM Antrenmanlar WHERE Grup=@grp ORDER BY Tarih DESC";
                using (var adapter = new SQLiteDataAdapter(sql, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@grp", grup);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public void DeleteTrainingLog(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Antrenmanlar WHERE Id=@id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
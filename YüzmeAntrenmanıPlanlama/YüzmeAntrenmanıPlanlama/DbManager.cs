using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace YüzmeAntrenmanıPlanlama
{
    public class DbManager
    {
        private const string DbFileName = "YuzmeAppDb.sqlite";
        private readonly string _dbFullPath;
        private readonly string _connectionString;

        public DbManager()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _dbFullPath = Path.Combine(baseDirectory, DbFileName);
            // Pooling=True performans için önemlidir.
            _connectionString = $"Data Source={_dbFullPath};Version=3;Pooling=True;";
            InitializeDatabase();
        }

        // Veritabanı bağlantısı oluşturur.
        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        // Veritabanı dosyası ve tabloları yoksa oluşturur.
        private void InitializeDatabase()
        {
            try
            {
                if (!File.Exists(_dbFullPath))
                {
                    SQLiteConnection.CreateFile(_dbFullPath);
                }

                using (var connection = GetConnection())
                {
                    connection.Open();

                    // 1. Sporcular Tablosu
                    string sqlSporcular = @"
                        CREATE TABLE IF NOT EXISTS Sporcular (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Ad TEXT NOT NULL,
                            Soyad TEXT NOT NULL,
                            Grup TEXT NOT NULL
                        );";
                    using (var command = new SQLiteCommand(sqlSporcular, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // 2. Antrenman Geçmişi Tablosu
                    string sqlGecmis = @"
                        CREATE TABLE IF NOT EXISTS AntrenmanGecmisi (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Tarih TEXT NOT NULL,
                            Grup TEXT NOT NULL,
                            ToplamMesafe INTEGER NOT NULL,
                            ToplamSure INTEGER NOT NULL
                        );";
                    using (var command = new SQLiteCommand(sqlGecmis, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Veritabanı başlatma hatası: {ex.Message}", ex);
            }
        }

        // --- SPORCU İŞLEMLERİ ---

        // Veritabanındaki tüm sporcuları "Ad Soyad (Grup)" formatında listeler.
        public List<string> GetFormattedStudentList()
        {
            var list = new List<string>();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string sql = "SELECT Ad, Soyad, Grup FROM Sporcular ORDER BY Grup ASC, Ad ASC";
                    using (var command = new SQLiteCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ad = reader["Ad"] != DBNull.Value ? reader["Ad"].ToString() : "";
                            string soyad = reader["Soyad"] != DBNull.Value ? reader["Soyad"].ToString() : "";
                            string grup = reader["Grup"] != DBNull.Value ? reader["Grup"].ToString() : "";
                            list.Add($"{ad} {soyad} ({grup})");
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception($"Sporcu listesi hatası: {ex.Message}"); }
            return list;
        }

        // Yeni sporcu ekler.
        public void AddStudent(string ad, string soyad, string grup)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO Sporcular (Ad, Soyad, Grup) VALUES (@ad, @soyad, @grup)";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad.Trim());
                        command.Parameters.AddWithValue("@soyad", soyad.Trim());
                        command.Parameters.AddWithValue("@grup", grup.Trim());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw new Exception($"Sporcu ekleme hatası: {ex.Message}"); }
        }

        // Belirtilen sporcuyu siler.
        public void DeleteStudent(string ad, string soyad, string grup)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string sql = "DELETE FROM Sporcular WHERE Ad = @ad AND Soyad = @soyad AND Grup = @grup";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ad", ad.Trim());
                        command.Parameters.AddWithValue("@soyad", soyad.Trim());
                        command.Parameters.AddWithValue("@grup", grup.Trim());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw new Exception($"Sporcu silme hatası: {ex.Message}"); }
        }

        // Belirtilen gruptaki tüm sporcuları siler.
        public void DeleteAllInGroup(string grupAdi)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string sql = "DELETE FROM Sporcular WHERE Grup = @grup";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@grup", grupAdi.Trim());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw new Exception($"Grup silme hatası: {ex.Message}"); }
        }

        // Bir grupta öğrenci olup olmadığını kontrol eder.
        public bool IsGroupHasStudents(string grupAdi)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string sql = "SELECT COUNT(Id) FROM Sporcular WHERE Grup = @grup";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@grup", grupAdi.Trim());
                        object result = command.ExecuteScalar();
                        int count = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                        return count > 0;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Grup kontrol hatası: {ex.Message}"); return false; }
        }

        // --- ANTRENMAN GEÇMİŞİ İŞLEMLERİ ---

        // Oluşturulan antrenmanı veritabanına kaydeder.
        public void AddTrainingLog(string grup, DateTime tarih, int mesafe, int sure)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO AntrenmanGecmisi (Tarih, Grup, ToplamMesafe, ToplamSure) VALUES (@tarih, @grup, @mesafe, @sure)";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        // Tarihi YYYY-MM-DD HH:MM:SS formatında kaydediyoruz.
                        command.Parameters.AddWithValue("@tarih", tarih.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@grup", grup);
                        command.Parameters.AddWithValue("@mesafe", mesafe);
                        command.Parameters.AddWithValue("@sure", sure);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Antrenman kaydetme hatası: {ex.Message}");
            }
        }

        // Belirtilen gruba ait antrenman geçmişini DataGridView için uygun formatta döndürür.
        public DataTable GetTrainingHistoryByGroup(string grup)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    // Tarihi gün.ay.yıl formatına çevirir ve ortalama hızı hesaplar.
                    // Sıfıra bölünme hatasını önlemek için CASE kullanılır.
                    string sql = @"
                        SELECT 
                            strftime('%d.%m.%Y', Tarih) AS TarihFormatted, 
                            ToplamSure AS Sure, 
                            ToplamMesafe AS Mesafe,
                            CASE WHEN ToplamMesafe > 0 THEN 
                                round((CAST(ToplamSure AS REAL) / ToplamMesafe) * 100, 2) 
                            ELSE 0 END AS OrtHiz
                        FROM AntrenmanGecmisi 
                        WHERE Grup = @grup 
                        ORDER BY Tarih DESC";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@grup", grup);
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Geçmiş antrenmanlar çekilirken hata: {ex.Message}");
            }
            return dt;
        }
    }
}
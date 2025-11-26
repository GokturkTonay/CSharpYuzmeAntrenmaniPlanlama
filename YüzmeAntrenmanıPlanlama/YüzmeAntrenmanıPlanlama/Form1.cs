using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YüzmeAntrenmanıPlanlama
{
    public partial class Form1 : Form
    {
        // Buton gruplarını tutacak diziler
        private Button[] biyomotorikButtons = Array.Empty<Button>();
        private Button[] yuzmeStiliButtons = Array.Empty<Button>();

        public Form1()
        {
            InitializeComponent();

            // Butonların etkileşim ayarlarını yap (Seçilince renk değişimi vb.)
            ConfigureInteractiveButtons();

            // Antrenman Oluştur butonunun tıklama olayını bağla
            this.btnOlustur.Click += new EventHandler(this.BtnOlustur_Click);
        }

        // --- BUTON ETKİLEŞİM AYARLARI ---
        private void ConfigureInteractiveButtons()
        {
            // Grupları oluştur (Designer'daki adlarla uyumlu)
            this.biyomotorikButtons = new Button[] {
                    this.btnDayaniklilik,
                    this.btnSurat,
                    this.btnKuvvet,
                    this.btnEsneklik,
                    this.btnKoordinasyon
                };

            this.yuzmeStiliButtons = new Button[] {
                    this.btnSerbest,
                    this.btnSirtustu,
                    this.btnKurbagalama,
                    this.btnKelebek
                };

            // Her grup için ortak ayarlar ve event bağlama
            SetupGroupButtons(this.biyomotorikButtons);
            SetupGroupButtons(this.yuzmeStiliButtons);
        }

        private void SetupGroupButtons(Button[] group)
        {
            if (group == null) return;
            foreach (var btn in group)
            {
                if (btn == null) continue;

                // Temel görsel ayarlar (Designer'da yapılanları ezebilir)
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(4);

                // Tıklama olayını bağla (önce çıkarıp sonra ekleyerek çift bağlamayı önle)
                btn.Click -= GroupButton_Click;
                btn.Click += GroupButton_Click;
            }
        }

        // Bir grup butonuna tıklandığında çalışır
        private void GroupButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return;

            // Hangi gruba ait olduğunu bul ve o gruptaki seçimi güncelle
            if (Array.Exists(this.biyomotorikButtons, b => ReferenceEquals(b, clicked)))
            {
                SetSelectedButton(clicked, this.biyomotorikButtons);
                return;
            }

            if (Array.Exists(this.yuzmeStiliButtons, b => ReferenceEquals(b, clicked)))
            {
                SetSelectedButton(clicked, this.yuzmeStiliButtons);
                return;
            }
        }

        // Seçilen butonu vurgular, diğerlerinin vurgusunu kaldırır
        private void SetSelectedButton(Button clicked, Button[] group)
        {
            // Gruptaki tüm butonları varsayılan hale getir
            foreach (var b in group)
            {
                if (b == null) continue;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Color.Gray;
                b.BackColor = SystemColors.Control;
            }

            // Tıklanan butonu vurgula
            clicked.FlatAppearance.BorderSize = 2;
            clicked.FlatAppearance.BorderColor = Color.DodgerBlue;
            clicked.BackColor = Color.FromArgb(230, 245, 255); // Açık mavi arka plan
        }

        // --- MENÜ TIKLAMA OLAYLARI ---
        private void ProfilToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPageProfil;
        }

        private void AntrenmanOlusturToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPageAntrenmanOlustur;
        }

        // (Geçmiş antrenmanlar menü eventi kaldırıldı çünkü menüden silmiştik)


        // =================================================================================
        // --- ANTRENMAN OLUŞTURMA VE TABLO YÖNETİMİ (LİSTE GÖRÜNÜMÜ İÇİN) ---
        // =================================================================================

        // --- YARDIMCI METOTLAR ---

        // 1. Tip Satır: Koyu renkli bölüm başlığı ekler (Örn: "ISINMA")
        private void TabloyaBaslikEkle(string baslikMetni)
        {
            int rowIndex = dgvProgram.Rows.Add(baslikMetni.ToUpper()); // Büyük harfle ekle

            // Bu satırın stilini değiştir (Koyu gri arka plan, beyaz kalın yazı)
            DataGridViewRow row = dgvProgram.Rows[rowIndex];
            row.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60); // Koyu gri
            row.DefaultCellStyle.ForeColor = Color.White; // Beyaz yazı
            row.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold); // Kalın ve büyük font
            row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Ortala
            row.ReadOnly = true; // Başlıklar düzenlenemesin
            // Başlık ile içerik arasına biraz mesafe koymak için padding'i üstten artıralım
            row.DefaultCellStyle.Padding = new Padding(10, 15, 10, 10);
        }

        // 2. Tip Satır: Antrenman içeriğini tek satırda ekler
        private void TabloyaIcerikEkle(string aktivite, string detay)
        {
            // Aktivite ve detayı "•" işaretiyle birleştirip tek bir cümle yapıyoruz.
            string tamIcerikCümlesi = $"• {aktivite}: {detay}";

            int rowIndex = dgvProgram.Rows.Add(tamIcerikCümlesi);

            // İçerik satırlarının stilini belirle
            DataGridViewRow row = dgvProgram.Rows[rowIndex];
            row.DefaultCellStyle.BackColor = Color.White; // Beyaz arka plan
            row.DefaultCellStyle.ForeColor = Color.Black;
            row.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Sola yasla
        }

        // --- ANTRENMAN OLUŞTURMA MANTIĞI (BUTON TIKLAMASI) ---

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            int toplamMesafe = (int)nudToplamMesafe.Value;
            bool ekipmanVarMi = chkEkEkipman.Checked;

            if (toplamMesafe <= 0)
            {
                MessageBox.Show("Lütfen toplam mesafe giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NOT: Şu anki UI'da hangi butonun seçili olduğunu anlamak için bir mekanizma yok.
            // Bu yüzden senaryo gereği "Dayanıklılık" ve "Serbest" seçilmiş gibi varsayıyoruz.
            string secilenBiyomotor = "Dayanıklılık";
            List<string> secilenStiller = new List<string> { "Serbest" };

            // Tabloyu Temizle
            dgvProgram.Rows.Clear();

            // --- Program Akışı ---

            // 1. KARA ÇALIŞMASI
            HavuzaGirisOncesiKaraCalismasiEkle(secilenBiyomotor);

            // Mesafeleri Hesapla (Örnek dağılım)
            int isinmaMesafesi = (int)(toplamMesafe * 0.20);
            int sogumaMesafesi = (int)(toplamMesafe * 0.10);
            int anaSetMesafesi = toplamMesafe - isinmaMesafesi - sogumaMesafesi;

            // 2. HAVUZ ISINMA
            HavuzIsinmaEkle(isinmaMesafesi, secilenStiller, ekipmanVarMi);

            // 3. ANA ANTRENMAN
            AnaAntrenmanEkle(anaSetMesafesi, secilenBiyomotor, secilenStiller, ekipmanVarMi);

            // 4. SOĞUMA
            SogumaEkle(sogumaMesafesi, secilenStiller[0]);

            // İlk satırın seçili gelmesini engelle
            dgvProgram.ClearSelection();

            // --- DEĞİŞİKLİK BURADA: Mesaj kutusu kaldırıldı ---
            // MessageBox.Show("Antrenman programı başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- BÖLÜM METOTLARI (Yeni yapıya göre güncellendi) ---

        private void HavuzaGirisOncesiKaraCalismasiEkle(string biyomotorYetenek)
        {
            TabloyaBaslikEkle("KARA ÇALIŞMASI (BAŞLANGIÇ)");

            TabloyaIcerikEkle("Genel Esnetme", "5 dk dinamik kol ve bacak açma germe.");

            switch (biyomotorYetenek)
            {
                case "Dayanıklılık":
                    TabloyaIcerikEkle("Kardiyo Hazırlık", "3x30sn Jumping Jacks, 3x30sn High Knees (Yerinde koşu). Nabız yükseltme.");
                    break;
                case "Sürat":
                    TabloyaIcerikEkle("Patlayıcı Kuvvet", "3x10 Squat Jumps (Sıçrama), 3x15sn hızlı kol çevirme.");
                    break;
                // Diğer case'ler...
                default:
                    TabloyaIcerikEkle("Aktivasyon", "Temel ısınma hareketleri.");
                    break;
            }
            dgvProgram.Rows.Add(""); // Boşluk
        }

        private void HavuzIsinmaEkle(int mesafe, List<string> stiller, bool ekipman)
        {
            TabloyaBaslikEkle("HAVUZ ISINMA");

            string stilMetni = string.Join("/", stiller.ToArray());
            string ekipmanNotu = ekipman ? "(Palet/şnorkel opsiyonel)" : "";

            TabloyaIcerikEkle($"{mesafe}m Karışık Yüzme ({stilMetni})", $"Düşük tempo, tekniğe odaklanarak suya alışma. {ekipmanNotu}");

            if (mesafe >= 400)
            {
                TabloyaIcerikEkle("Teknik Drill Seti", "4 x 50m (25m sağ kol, 25m sol kol). Çekiş hissiyatı için.");
            }
            dgvProgram.Rows.Add(""); // Boşluk
        }

        private void AnaAntrenmanEkle(int mesafe, string biyomotor, List<string> stiller, bool ekipman)
        {
            string anaStil = stiller.Count > 0 ? stiller[0] : "Serbest";
            string ekipmanMetni = ekipman ? "Ekipman (Palet/El Paleti) ile." : "Ekipmansız.";

            TabloyaBaslikEkle($"ANA SET ({biyomotor.ToUpper()} ODAKLI)");

            switch (biyomotor)
            {
                case "Dayanıklılık":
                    int set1Mesafe = mesafe / 2;
                    int set2Mesafe = mesafe - set1Mesafe;
                    TabloyaIcerikEkle($"Aerobik Kapasite Seti 1", $"{set1Mesafe}m Kesintisiz yüzüş ({anaStil}) @%70 orta tempo. {ekipmanMetni}");
                    TabloyaIcerikEkle($"İnterval Seti 2", $"{Math.Max(1, set2Mesafe / 100)} x 100m ({anaStil}) @%80 efor. Her 100m bitişinde 20sn dinlenme.");
                    break;
                case "Sürat":
                    TabloyaIcerikEkle($"Sprint Seti", $"8 x 25m ({anaStil}) @%100 efor. Çıkış ve dönüş odaklı. Dinlenme: 1:30dk.");
                    TabloyaIcerikEkle($"Aktif Toparlanma", $"{mesafe - 200}m Çok yavaş yüzüş.");
                    break;
                // Diğer biyomotor yetenekler...
                default:
                    TabloyaIcerikEkle("Ana Seri", $"{mesafe}m boyunca {anaStil} ağırlıklı, orta tempo yüzüş.");
                    break;
            }
            dgvProgram.Rows.Add(""); // Boşluk
        }

        private void SogumaEkle(int mesafe, string stil)
        {
            TabloyaBaslikEkle("SOĞUMA VE BİTİRİŞ");

            if (mesafe > 0)
            {
                TabloyaIcerikEkle($"Yavaş Yüzme ({stil})", $"{mesafe}m çok yavaş tempo. Vücudu rahatlatma.");
            }
            TabloyaIcerikEkle("Statik Esnetme (Kara)", "Havuzdan çıktıktan sonra 5-10 dk ana kas gruplarını esnetme.");
        }
    }
}
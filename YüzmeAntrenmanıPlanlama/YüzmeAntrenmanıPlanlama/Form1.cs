using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace YüzmeAntrenmanıPlanlama
{
    public partial class Form1 : Form
    {
        private Button[] biyomotorikButtons = Array.Empty<Button>();
        private Button[] yuzmeStiliButtons = Array.Empty<Button>();
        private DbManager dbManager = new DbManager();
        private List<string> currentDbList = new List<string>();

        // Başlangıçta tanımlı gruplar
        private readonly string[] initialGroups = { "A Grubu", "B Grubu", "C Grubu" };

        public Form1()
        {
            InitializeComponent();

            // Olay Bağlamaları
            this.cmbGrup.SelectedIndexChanged += new EventHandler(this.cmbGrup_SelectedIndexChanged);
            this.btnOlustur.Click += new EventHandler(this.BtnOlustur_Click);
            this.btnEkleOgrenci.Click += new EventHandler(this.BtnEkleOgrenci_Click);
            this.btnSilOgrenci.Click += new EventHandler(this.BtnSilOgrenci_Click);
            this.btnSilGrup.Click += new EventHandler(this.BtnSilGrup_Click);

            ConfigureInteractiveButtons();

            // Grupları ComboBox'lara yükle
            foreach (string grup in initialGroups)
            {
                this.cmbGrup.Items.Add(grup);
                this.cmbAntrenmanGrup.Items.Add(grup); // Antrenman sekmesindeki combo'yu da doldur
            }
            this.cmbGrup.Items.Add("Yeni Grup Oluştur...");

            // Başlangıçta ilk grubu seç (eğer varsa)
            if (this.cmbGrup.Items.Count > 0) { this.cmbGrup.SelectedIndex = 0; }
            if (this.cmbAntrenmanGrup.Items.Count > 0) { this.cmbAntrenmanGrup.SelectedIndex = 0; }
        }

        // =================================================================================
        // --- EKSİK OLAN VE HATAYI ÇÖZEN MENÜ KODLARI BURAYA EKLENDİ ---
        // =================================================================================

        // Üst menüdeki "Profil"e tıklayınca çalışır
        private void ProfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageProfil;
        }

        // Üst menüdeki "Antrenman Oluştur"a tıklayınca çalışır
        private void AntrenmanOlusturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageAntrenmanOlustur;
        }

        // =================================================================================
        // --- PROFİL SEKMESİ İŞLEMLERİ ---
        // =================================================================================

        private void OgrenciListesiniGuncelle()
        {
            string selectedGroup = this.cmbGrup.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedGroup) || selectedGroup == "Yeni Grup Oluştur...")
            {
                this.lstOgrenciListesi.Items.Clear();
                return;
            }
            currentDbList = dbManager.GetFormattedStudentList();
            this.lstOgrenciListesi.BeginUpdate();
            this.lstOgrenciListesi.Items.Clear();
            foreach (string ogrenciFormatli in currentDbList)
            {
                if (ogrenciFormatli.Contains($"({selectedGroup})"))
                {
                    this.lstOgrenciListesi.Items.Add(ogrenciFormatli);
                }
            }
            this.lstOgrenciListesi.EndUpdate();
        }

        // Grup ComboBox'ı değiştiğinde çalışır (Hem öğrenci listesini hem geçmişi günceller)
        private void cmbGrup_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selectedGroup = this.cmbGrup.SelectedItem?.ToString();
            bool showInput = selectedGroup == "Yeni Grup Oluştur...";
            this.lblGrupInput.Visible = showInput;
            this.txtGrupInput.Visible = showInput;

            if (!showInput && !string.IsNullOrEmpty(selectedGroup))
            {
                // 1. Öğrenci listesini güncelle
                OgrenciListesiniGuncelle();

                // 2. YENİ: Geçmiş antrenmanları güncelle
                try
                {
                    DataTable dtHistory = dbManager.GetTrainingHistoryByGroup(selectedGroup);
                    // DataGridView'in otomatik sütun oluşturmasını kapatmıştık (Designer'da).
                    // Veritabanından gelen kolon isimleri ile Designer'daki HeaderText'leri eşleştiriyoruz.
                    dgvGecmisAntrenmanlarProfil.DataSource = dtHistory;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Geçmiş antrenmanlar yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // "Yeni Grup Oluştur" seçiliyse veya grup yoksa geçmiş tablosunu temizle
                dgvGecmisAntrenmanlarProfil.DataSource = null;
                lstOgrenciListesi.Items.Clear();
            }
        }

        private void BtnEkleOgrenci_Click(object sender, EventArgs e)
        {
            string ad = cmbAd.Text.Trim();
            string soyad = cmbSoyad.Text.Trim();
            string secilenGrup = cmbGrup.SelectedItem?.ToString();
            string yeniGrupAdi = txtGrupInput.Text.Trim();
            string hedefGrup = "";

            if (secilenGrup == "Yeni Grup Oluştur...")
            {
                if (string.IsNullOrEmpty(yeniGrupAdi)) { MessageBox.Show("Lütfen yeni grup için bir ad giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                hedefGrup = yeniGrupAdi;
                if (!cmbGrup.Items.Contains(hedefGrup))
                {
                    cmbGrup.Items.Insert(cmbGrup.Items.Count - 1, hedefGrup);
                    cmbAntrenmanGrup.Items.Add(hedefGrup); // Diğer combo'ya da ekle
                }
            }
            else { hedefGrup = secilenGrup; }

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(hedefGrup)) { MessageBox.Show("Lütfen Ad, Soyad ve Grup bilgilerini eksiksiz giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try { dbManager.AddStudent(ad, soyad, hedefGrup); }
            catch (Exception ex) { MessageBox.Show("Veritabanı hatası: " + ex.Message); return; }

            if (secilenGrup == "Yeni Grup Oluştur...") { cmbGrup.SelectedItem = hedefGrup; txtGrupInput.Clear(); }
            else { OgrenciListesiniGuncelle(); }
            cmbAd.Text = ""; cmbSoyad.Text = "";
        }

        private void BtnSilOgrenci_Click(object sender, EventArgs e)
        {
            if (lstOgrenciListesi.SelectedIndex == -1) return;
            string selectedString = lstOgrenciListesi.SelectedItem.ToString();
            DialogResult result = MessageBox.Show($"'{selectedString}' silinecek. Emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int groupStartIndex = selectedString.LastIndexOf('(');
                    string grupPart = selectedString.Substring(groupStartIndex + 1).Replace(")", "");
                    string namePart = selectedString.Substring(0, groupStartIndex).Trim();
                    var nameParts = namePart.Split(' ');
                    string soyad = nameParts.Last();
                    string ad = string.Join(" ", nameParts.Take(nameParts.Length - 1));
                    dbManager.DeleteStudent(ad, soyad, grupPart);
                    OgrenciListesiniGuncelle();
                }
                catch (Exception ex) { MessageBox.Show("Silme işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void BtnSilGrup_Click(object sender, EventArgs e)
        {
            string selectedGroup = this.cmbGrup.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedGroup) || selectedGroup == "Yeni Grup Oluştur...") { MessageBox.Show("Lütfen geçerli bir grup seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            bool gruptaOgrenciVar = dbManager.IsGroupHasStudents(selectedGroup);
            DialogResult result;
            if (gruptaOgrenciVar)
            {
                result = MessageBox.Show($"'{selectedGroup}' grubunda öğrenciler var. Grubu silerseniz içindeki TÜM ÖĞRENCİLER DE SİLİNECEK!\n\nDevam etmek istiyor musunuz?", "Kritik Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            else
            {
                result = MessageBox.Show($"'{selectedGroup}' grubunu silmek istediğinize emin misiniz? (Grup boş)", "Grup Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes)
            {
                dbManager.DeleteAllInGroup(selectedGroup);
                this.cmbGrup.Items.Remove(selectedGroup);
                this.cmbAntrenmanGrup.Items.Remove(selectedGroup); // Diğer combo'dan da sil
                GrupSilmeSonrasiArayuzGuncelle();
                MessageBox.Show("Grup silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GrupSilmeSonrasiArayuzGuncelle()
        {
            if (this.cmbGrup.Items.Count > 1) this.cmbGrup.SelectedIndex = 0;
            else { this.cmbGrup.SelectedIndex = -1; OgrenciListesiniGuncelle(); dgvGecmisAntrenmanlarProfil.DataSource = null; }

            if (this.cmbAntrenmanGrup.Items.Count > 0) this.cmbAntrenmanGrup.SelectedIndex = 0;
            else this.cmbAntrenmanGrup.SelectedIndex = -1;
        }

        // =================================================================================
        // --- DİĞER ARAYÜZ ETKİLEŞİMLERİ ---
        // =================================================================================

        private void ConfigureInteractiveButtons()
        {
            this.biyomotorikButtons = new Button[] { this.btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon };
            this.yuzmeStiliButtons = new Button[] { this.btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek };
            SetupGroupButtons(this.biyomotorikButtons);
            SetupGroupButtons(this.yuzmeStiliButtons);
        }

        private void SetupGroupButtons(Button[] group)
        {
            if (group == null) return;
            foreach (var btn in group)
            {
                if (btn == null) continue;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(4);
                btn.Click -= GroupButton_Click;
                btn.Click += GroupButton_Click;
            }
        }

        private void GroupButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return;
            if (Array.Exists(this.biyomotorikButtons, b => ReferenceEquals(b, clicked))) { SetSelectedButton(clicked, this.biyomotorikButtons); return; }
            if (Array.Exists(this.yuzmeStiliButtons, b => ReferenceEquals(b, clicked))) { SetSelectedButton(clicked, this.yuzmeStiliButtons); return; }
        }

        private void SetSelectedButton(Button clicked, Button[] group)
        {
            foreach (var b in group)
            {
                if (b == null) continue;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Color.Gray;
                b.BackColor = SystemColors.Control;
            }
            clicked.FlatAppearance.BorderSize = 2;
            clicked.FlatAppearance.BorderColor = Color.DodgerBlue;
            clicked.BackColor = Color.FromArgb(230, 245, 255);
        }

        // =================================================================================
        // --- ANTRENMAN OLUŞTURMA VE KAYDETME MANTIĞI ---
        // =================================================================================

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            // 1. Validasyonlar
            string secilenGrup = cmbAntrenmanGrup.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(secilenGrup))
            {
                MessageBox.Show("Lütfen antrenman için bir 'Hedef Grup' seçiniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int toplamMesafe = (int)nudToplamMesafe.Value;
            int toplamSure = (int)nudToplamSure.Value;
            bool ekipmanVarMi = chkEkEkipman.Checked;

            if (toplamMesafe <= 0 || toplamSure <= 0)
            {
                MessageBox.Show("Lütfen toplam mesafe ve süre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Antrenman Programını Oluştur (Mevcut Mantık)
            string secilenBiyomotor = GetSelectedButtonText(biyomotorikButtons) ?? "Dayanıklılık";
            List<string> secilenStiller = GetSelectedButtonText(yuzmeStiliButtons) != null ? new List<string> { GetSelectedButtonText(yuzmeStiliButtons) } : new List<string> { "Serbest" };

            dgvProgram.Rows.Clear();
            HavuzaGirisOncesiKaraCalismasiEkle(secilenBiyomotor);
            int isinmaMesafesi = (int)(toplamMesafe * 0.20);
            int sogumaMesafesi = (int)(toplamMesafe * 0.10);
            int anaSetMesafesi = toplamMesafe - isinmaMesafesi - sogumaMesafesi;
            HavuzIsinmaEkle(isinmaMesafesi, secilenStiller, ekipmanVarMi);
            AnaAntrenmanEkle(anaSetMesafesi, secilenBiyomotor, secilenStiller, ekipmanVarMi);
            SogumaEkle(sogumaMesafesi, secilenStiller[0]);
            dgvProgram.ClearSelection();

            // 3. YENİ: Antrenmanı Veritabanına Kaydet
            try
            {
                dbManager.AddTrainingLog(secilenGrup, DateTime.Now, toplamMesafe, toplamSure);
                MessageBox.Show($"Antrenman programı oluşturuldu ve '{secilenGrup}' geçmişine kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Eğer Profil sekmesinde aynı grup seçiliyse, oradaki listeyi de anlık güncelle
                if (cmbGrup.SelectedItem?.ToString() == secilenGrup)
                {
                    cmbGrup_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Antrenman kaydedilirken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yardımcı: Seçili butonun metnini bulur
        private string? GetSelectedButtonText(Button[] group)
        {
            foreach (var btn in group)
            {
                if (btn.BackColor == Color.FromArgb(230, 245, 255)) return btn.Text;
            }
            return null;
        }

        // --- YARDIMCI TABLO METOTLARI (Mevcut) ---
        private void TabloyaBaslikEkle(string baslikMetni)
        {
            int rowIndex = dgvProgram.Rows.Add(baslikMetni.ToUpper());
            DataGridViewRow row = dgvProgram.Rows[rowIndex];
            row.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            row.DefaultCellStyle.ForeColor = Color.White;
            row.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            row.ReadOnly = true;
        }

        private void TabloyaIcerikEkle(string aktivite, string detay)
        {
            string tamIcerikCümlesi = $"• {aktivite}: {detay}";
            int rowIndex = dgvProgram.Rows.Add(tamIcerikCümlesi);
            DataGridViewRow row = dgvProgram.Rows[rowIndex];
            row.DefaultCellStyle.BackColor = Color.White;
            row.DefaultCellStyle.ForeColor = Color.Black;
            row.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        // --- BÖLÜM METOTLARI (Mevcut) ---
        private void HavuzaGirisOncesiKaraCalismasiEkle(string biyomotorYetenek)
        {
            TabloyaBaslikEkle("KARA ÇALIŞMASI (BAŞLANGIÇ)");
            TabloyaIcerikEkle("Genel Esnetme", "5 dk dinamik kol ve bacak açma germe.");
            switch (biyomotorYetenek)
            {
                case "Dayanıklılık": TabloyaIcerikEkle("Kardiyo Hazırlık", "3x30sn Jumping Jacks, 3x30sn High Knees."); break;
                case "Sürat": TabloyaIcerikEkle("Patlayıcı Kuvvet", "3x10 Squat Jumps, 3x15sn hızlı kol çevirme."); break;
                default: TabloyaIcerikEkle("Aktivasyon", "Temel ısınma hareketleri."); break;
            }
            dgvProgram.Rows.Add("");
        }

        private void HavuzIsinmaEkle(int mesafe, List<string> stiller, bool ekipman)
        {
            TabloyaBaslikEkle("HAVUZ ISINMA");
            string stilMetni = string.Join("/", stiller.ToArray());
            string ekipmanNotu = ekipman ? "(Palet/şnorkel opsiyonel)" : "";
            TabloyaIcerikEkle($"{mesafe}m Karışık Yüzme ({stilMetni})", $"Düşük tempo, tekniğe odaklanarak suya alışma. {ekipmanNotu}");
            if (mesafe >= 400) TabloyaIcerikEkle("Teknik Drill Seti", "4 x 50m (25m sağ kol, 25m sol kol). Çekiş hissiyatı için.");
            dgvProgram.Rows.Add("");
        }

        private void AnaAntrenmanEkle(int mesafe, string biyomotor, List<string> stiller, bool ekipman)
        {
            string anaStil = stiller.Count > 0 ? stiller[0] : "Serbest";
            string ekipmanMetni = ekipman ? "Ekipman (Palet/El Paleti) ile." : "Ekipmansız.";
            TabloyaBaslikEkle($"ANA SET ({biyomotor.ToUpper()} ODAKLI)");
            switch (biyomotor)
            {
                case "Dayanıklılık":
                    int set1Mesafe = mesafe / 2; int set2Mesafe = mesafe - set1Mesafe;
                    TabloyaIcerikEkle($"Aerobik Kapasite Seti 1", $"{set1Mesafe}m Kesintisiz yüzüş ({anaStil}) @%70 orta tempo. {ekipmanMetni}");
                    TabloyaIcerikEkle($"İnterval Seti 2", $"{Math.Max(1, set2Mesafe / 100)} x 100m ({anaStil}) @%80 efor. 20sn dinlenme.");
                    break;
                case "Sürat":
                    TabloyaIcerikEkle($"Sprint Seti", $"8 x 25m ({anaStil}) @%100 efor. Çıkış ve dönüş odaklı. Dinlenme: 1:30dk.");
                    TabloyaIcerikEkle($"Aktif Toparlanma", $"{mesafe - 200}m Çok yavaş yüzüş.");
                    break;
                default: TabloyaIcerikEkle("Ana Seri", $"{mesafe}m boyunca {anaStil} ağırlıklı, orta tempo yüzüş."); break;
            }
            dgvProgram.Rows.Add("");
        }

        private void SogumaEkle(int mesafe, string stil)
        {
            TabloyaBaslikEkle("SOĞUMA VE BİTİRİŞ");
            if (mesafe > 0) TabloyaIcerikEkle($"Yavaş Yüzme ({stil})", $"{mesafe}m çok yavaş tempo. Vücudu rahatlatma.");
            TabloyaIcerikEkle("Statik Esnetme (Kara)", "Havuzdan çıktıktan sonra 5-10 dk ana kas gruplarını esnetme.");
        }
    }
}
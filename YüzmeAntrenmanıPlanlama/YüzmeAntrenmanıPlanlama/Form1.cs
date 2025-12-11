using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace YüzmeAntrenmanıPlanlama
{
    public partial class Form1 : Form
    {
        private Button[] biyomotorikButtons = new Button[0];
        private Button[] yuzmeStiliButtons = new Button[0];
        private DbManager dbManager = new DbManager();
        private List<string> currentDbList = new List<string>();
        private readonly string[] initialGroups = { "A Grubu", "B Grubu", "C Grubu" };

        public Form1()
        {
            InitializeComponent();

            this.cmbGrup.SelectedIndexChanged += new EventHandler(this.cmbGrup_SelectedIndexChanged);
            this.btnOlustur.Click += new EventHandler(this.BtnOlustur_Click);
            this.btnEkleOgrenci.Click += new EventHandler(this.BtnEkleOgrenci_Click);
            this.btnSilOgrenci.Click += new EventHandler(this.BtnSilOgrenci_Click);
            this.btnSilGrup.Click += new EventHandler(this.BtnSilGrup_Click);

            // Designer hatası alırsan bu satırları silebilirsin:
            this.profilToolStripMenuItem.Click += new EventHandler(this.ProfilToolStripMenuItem_Click);
            this.antrenmanOlusturToolStripMenuItem.Click += new EventHandler(this.AntrenmanOlusturToolStripMenuItem_Click);

            ConfigureInteractiveButtons();

            foreach (string grup in initialGroups)
            {
                this.cmbGrup.Items.Add(grup);
                this.cmbAntrenmanGrup.Items.Add(grup);
            }
            this.cmbGrup.Items.Add("Yeni Grup Oluştur...");

            if (this.cmbGrup.Items.Count > 0) { this.cmbGrup.SelectedIndex = 0; }
            if (this.cmbAntrenmanGrup.Items.Count > 0) { this.cmbAntrenmanGrup.SelectedIndex = 0; }
        }

        // --- MENÜ GEÇİŞLERİ ---
        private void ProfilToolStripMenuItem_Click(object sender, EventArgs e) { tabControl1.SelectedTab = tabPageProfil; }
        private void AntrenmanOlusturToolStripMenuItem_Click(object sender, EventArgs e) { tabControl1.SelectedTab = tabPageAntrenmanOlustur; }

        // --- YARDIMCI METOTLAR ---
        private void OgrenciListesiniGuncelle()
        {
            string selectedGroup = "";
            if (this.cmbGrup.SelectedItem != null) selectedGroup = this.cmbGrup.SelectedItem.ToString();

            if (string.IsNullOrEmpty(selectedGroup) || selectedGroup == "Yeni Grup Oluştur...") { this.lstOgrenciListesi.Items.Clear(); return; }

            currentDbList = dbManager.GetFormattedStudentList();
            this.lstOgrenciListesi.BeginUpdate();
            this.lstOgrenciListesi.Items.Clear();
            foreach (string ogrenciFormatli in currentDbList)
            {
                if (ogrenciFormatli.Contains("(" + selectedGroup + ")"))
                    this.lstOgrenciListesi.Items.Add(ogrenciFormatli);
            }
            this.lstOgrenciListesi.EndUpdate();
        }

        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGroup = "";
            if (this.cmbGrup.SelectedItem != null) selectedGroup = this.cmbGrup.SelectedItem.ToString();

            bool showInput = selectedGroup == "Yeni Grup Oluştur...";
            this.lblGrupInput.Visible = showInput; this.txtGrupInput.Visible = showInput;

            if (!showInput && !string.IsNullOrEmpty(selectedGroup))
            {
                OgrenciListesiniGuncelle();
                try
                {
                    DataTable dtHistory = dbManager.GetTrainingHistoryByGroup(selectedGroup);
                    dgvGecmisAntrenmanlarProfil.DataSource = dtHistory;
                }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
            else
            {
                dgvGecmisAntrenmanlarProfil.DataSource = null;
                lstOgrenciListesi.Items.Clear();
            }
        }

        private void BtnEkleOgrenci_Click(object sender, EventArgs e)
        {
            string ad = cmbAd.Text.Trim(); string soyad = cmbSoyad.Text.Trim();
            string secilenGrup = "";
            if (cmbGrup.SelectedItem != null) secilenGrup = cmbGrup.SelectedItem.ToString();
            string yeniGrupAdi = txtGrupInput.Text.Trim(); string hedefGrup = "";

            if (secilenGrup == "Yeni Grup Oluştur...")
            {
                if (string.IsNullOrEmpty(yeniGrupAdi)) { MessageBox.Show("Grup adı girin."); return; }
                hedefGrup = yeniGrupAdi;
                if (!cmbGrup.Items.Contains(hedefGrup)) { cmbGrup.Items.Insert(cmbGrup.Items.Count - 1, hedefGrup); cmbAntrenmanGrup.Items.Add(hedefGrup); }
            }
            else { hedefGrup = secilenGrup; }

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad)) { MessageBox.Show("Ad Soyad girin."); return; }
            try { dbManager.AddStudent(ad, soyad, hedefGrup); } catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            if (secilenGrup == "Yeni Grup Oluştur...") { cmbGrup.SelectedItem = hedefGrup; txtGrupInput.Clear(); } else { OgrenciListesiniGuncelle(); }
            cmbAd.Text = ""; cmbSoyad.Text = "";
        }

        private void BtnSilOgrenci_Click(object sender, EventArgs e)
        {
            if (lstOgrenciListesi.SelectedIndex == -1) return;
            string selectedString = lstOgrenciListesi.SelectedItem.ToString();
            if (MessageBox.Show("Silinsin mi?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { int idx = selectedString.LastIndexOf('('); string grp = selectedString.Substring(idx + 1).Replace(")", ""); string name = selectedString.Substring(0, idx).Trim(); var parts = name.Split(' '); string sn = parts.Last(); string n = string.Join(" ", parts.Take(parts.Length - 1)); dbManager.DeleteStudent(n, sn, grp); OgrenciListesiniGuncelle(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void BtnSilGrup_Click(object sender, EventArgs e)
        {
            string g = "";
            if (this.cmbGrup.SelectedItem != null) g = this.cmbGrup.SelectedItem.ToString();
            if (string.IsNullOrEmpty(g) || g == "Yeni Grup Oluştur...") return;
            if (MessageBox.Show("Grup silinsin mi?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes) { dbManager.DeleteAllInGroup(g); this.cmbGrup.Items.Remove(g); this.cmbAntrenmanGrup.Items.Remove(g); if (this.cmbGrup.Items.Count > 1) this.cmbGrup.SelectedIndex = 0; else this.cmbGrup.SelectedIndex = -1; if (this.cmbAntrenmanGrup.Items.Count > 0) this.cmbAntrenmanGrup.SelectedIndex = 0; else this.cmbAntrenmanGrup.SelectedIndex = -1; }
        }

        private void ConfigureInteractiveButtons() { this.biyomotorikButtons = new Button[] { this.btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon }; this.yuzmeStiliButtons = new Button[] { this.btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek }; SetupGroupButtons(this.biyomotorikButtons); SetupGroupButtons(this.yuzmeStiliButtons); }
        private void SetupGroupButtons(Button[] group) { if (group == null) return; foreach (var btn in group) { if (btn == null) continue; btn.Click -= GroupButton_Click; btn.Click += GroupButton_Click; } }
        private void GroupButton_Click(object sender, EventArgs e) { Button b = sender as Button; if (b == null) return; if (Array.Exists(this.biyomotorikButtons, x => ReferenceEquals(x, b))) SetSelectedButton(b, this.biyomotorikButtons); if (Array.Exists(this.yuzmeStiliButtons, x => ReferenceEquals(x, b))) SetSelectedButton(b, this.yuzmeStiliButtons); }
        private void SetSelectedButton(Button c, Button[] g) { foreach (var b in g) { b.FlatAppearance.BorderColor = Color.Gray; b.BackColor = SystemColors.Control; } c.FlatAppearance.BorderColor = Color.DodgerBlue; c.BackColor = Color.FromArgb(230, 245, 255); }
        private string GetSelectedButtonText(Button[] g) { foreach (var b in g) { if (b.BackColor == Color.FromArgb(230, 245, 255)) return b.Text; } return null; }

        private void TabloyaIcerikEkle(string aktivite, string detay) { int i = dgvProgram.Rows.Add(aktivite + ": " + detay); dgvProgram.Rows[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True; }

        // =================================================================================
        // --- YAPAY ZEKA İLE ANTRENMAN OLUŞTURMA ---
        // =================================================================================

        private async void BtnOlustur_Click(object sender, EventArgs e)
        {
            string secilenGrup = "";
            if (cmbAntrenmanGrup.SelectedItem != null) secilenGrup = cmbAntrenmanGrup.SelectedItem.ToString();

            if (string.IsNullOrEmpty(secilenGrup)) { MessageBox.Show("Grup seçin."); return; }
            int toplamMesafe = (int)nudToplamMesafe.Value;
            int toplamSure = (int)nudToplamSure.Value;
            bool ekipmanVarMi = chkEkEkipman.Checked;
            if (toplamMesafe <= 0 || toplamSure <= 0) { MessageBox.Show("Mesafe ve süre girin."); return; }

            string secilenBiyomotor = GetSelectedButtonText(biyomotorikButtons);
            if (secilenBiyomotor == null) secilenBiyomotor = "Dayanıklılık";

            string secilenStil = GetSelectedButtonText(yuzmeStiliButtons);
            if (secilenStil == null) secilenStil = "Karışık";

            string ekipmanDurumu = ekipmanVarMi ? "Palet, şnorkel var." : "Ekipman yok.";

            string eskiButonMetni = btnOlustur.Text;
            btnOlustur.Text = "Hazırlanıyor...";
            btnOlustur.Enabled = false;
            dgvProgram.Rows.Clear();

            try
            {
                // Prompt içinde JSON formatını özellikle belirtiyoruz
                string prompt = "Sen bir yüzme antrenörüsün. Aşağıdaki bilgilere göre antrenman yaz.\n" +
                    "Hedef: " + secilenGrup + ", Odak: " + secilenBiyomotor + ", Stil: " + secilenStil + ", Mesafe: " + toplamMesafe + "m, Ekipman: " + ekipmanDurumu + ".\n" +
                    "Çıktıyı SADECE şu JSON formatında ver (başka metin yazma):\n" +
                    "[ { \"bolum\": \"ISINMA\", \"aktivite\": \"...\", \"detay\": \"...\" }, ... ]";

                GeminiManager aiManager = new GeminiManager();

                // İsteği gönder
                string jsonCevap = await aiManager.AntrenmanProgramiIste(prompt);

                // Hata kontrolü
                if (jsonCevap.StartsWith("HATA:"))
                {
                    MessageBox.Show(jsonCevap, "Yapay Zeka Bağlantı Sorunu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // JSON Temizliği
                jsonCevap = jsonCevap.Replace("```json", "").Replace("```", "").Trim();

                // Deserialize
                var antrenmanListesi = JsonConvert.DeserializeObject<List<AntrenmanSatiri>>(jsonCevap);

                foreach (var satir in antrenmanListesi)
                {
                    TabloyaIcerikEkle(satir.bolum + " - " + satir.aktivite, satir.detay);
                }

                dbManager.AddTrainingLog(secilenGrup, DateTime.Now, toplamMesafe, toplamSure);

                string currentSelected = "";
                if (cmbGrup.SelectedItem != null) currentSelected = cmbGrup.SelectedItem.ToString();

                if (currentSelected == secilenGrup) cmbGrup_SelectedIndexChanged(null, null);

                MessageBox.Show("Program hazır!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnOlustur.Text = eskiButonMetni;
                btnOlustur.Enabled = true;
            }
        }
    }

    public class AntrenmanSatiri
    {
        public string bolum { get; set; }
        public string aktivite { get; set; }
        public string detay { get; set; }
    }
}
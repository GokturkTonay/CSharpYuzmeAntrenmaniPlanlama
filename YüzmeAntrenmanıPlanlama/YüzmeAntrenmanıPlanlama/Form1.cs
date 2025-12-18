using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;

namespace YüzmeAntrenmanıPlanlama
{
    public class AntrenmanSatiri
    {
        [JsonProperty("bolum")] public string Bolum { get; set; }
        [JsonProperty("aktivite")] public string Aktivite { get; set; }
        [JsonProperty("detay")] public string Detay { get; set; }
    }

    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private DbManager dbManager = new DbManager();
        private readonly string[] initialGroups = { "A Grubu", "B Grubu", "C Grubu" };
        private System.ComponentModel.BindingList<AntrenmanSatiri> programListesi;

        private string seciliOgrenciAd = "";
        private string seciliOgrenciSoyad = "";
        private string seciliOgrenciEskiGrup = "";

        public Form1()
        {
            InitializeComponent();
            programListesi = new System.ComponentModel.BindingList<AntrenmanSatiri>();
            gcProgram.DataSource = programListesi;

            InitializeGroups();
            SetupEvents();

            if (cmbGrup.Properties.Items.Count > 0)
            {
                cmbGrup.SelectedIndex = 0;
                cmbGrup_SelectedIndexChanged(null, null);
            }
        }

        private void SetupEvents()
        {
            this.cmbGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGrup.SelectedIndexChanged += new EventHandler(this.cmbGrup_SelectedIndexChanged);
            this.cmbGrup.SelectedIndexChanged += (s, e) => txtGrupInput.Visible = cmbGrup.Text == "Yeni Grup Oluştur...";

            this.btnOlustur.Click += new EventHandler(this.BtnOlustur_Click);
            this.btnEkleOgrenci.Click += new EventHandler(this.BtnEkleOgrenci_Click);
            this.btnSilOgrenci.Click += new EventHandler(this.BtnSilOgrenci_Click);
            this.btnSilGrup.Click += new EventHandler(this.BtnSilGrup_Click);
            this.btnPdfIndir.Click += (s, e) => ExportGrid(gcProgram, "Antrenman");
            this.btnPDFOlarakIndirProfil.Click += (s, e) => ExportGrid(gcGecmis, "Gecmis");
            this.btnSeciliyiIndir.Click += new EventHandler(this.BtnSeciliyiIndir_Click);
            this.btnGecmisSil.Click += new EventHandler(this.BtnGecmisSil_Click);
            this.gvGecmis.DoubleClick += GvGecmis_DoubleClick;

            this.btnDetayKaydet.Click += BtnDetayKaydet_Click;
            this.btnDetayKapat.Click += (s, e) => flyoutOgrenciDetay.HidePopup();
        }

        private void InitializeGroups()
        {
            foreach (string grup in initialGroups)
            {
                if (!this.cmbGrup.Properties.Items.Contains(grup)) this.cmbGrup.Properties.Items.Add(grup);
                if (!this.cmbAntrenmanGrup.Properties.Items.Contains(grup)) this.cmbAntrenmanGrup.Properties.Items.Add(grup);
            }
            this.cmbGrup.Properties.Items.Add("Yeni Grup Oluştur...");
            this.cmbGrup.SelectedIndex = 0;
            this.cmbAntrenmanGrup.SelectedIndex = 0;
        }

        private void OgrenciListesiniGuncelle()
        {
            aceMainGroup.Elements.Clear();
            string selectedGroup = cmbGrup.Text;

            if (selectedGroup == "Yeni Grup Oluştur..." || string.IsNullOrEmpty(selectedGroup)) return;

            var list = dbManager.GetFormattedStudentList();

            accordionOgrenciler.ContextButtonClick -= AccordionOgrenciler_ContextButtonClick;
            accordionOgrenciler.ContextButtonClick += AccordionOgrenciler_ContextButtonClick;

            foreach (var item in list)
            {
                if (item.Contains($"({selectedGroup})"))
                {
                    AccordionControlElement el = new AccordionControlElement(ElementStyle.Item);
                    el.Text = item;

                    AccordionContextButton btnDetay = new AccordionContextButton();
                    btnDetay.Name = "btnDetay";

                    Bitmap bmp = new Bitmap(24, 24);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        // Oku beyaz yaptık ki koyu temada görünsün
                        g.DrawString("➤", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, 2, 2);
                    }
                    btnDetay.ImageOptions.Image = bmp;
                    btnDetay.ToolTip = "Detayları Gör";
                    btnDetay.Tag = el;

                    el.ContextButtons.Add(btnDetay);
                    aceMainGroup.Elements.Add(el);
                }
            }
        }

        // --- KAPA - AÇ MANTIĞI ---
        private async void AccordionOgrenciler_ContextButtonClick(object sender, ContextItemClickEventArgs e)
        {
            var btn = e.Item as AccordionContextButton;
            if (btn == null) return;
            AccordionControlElement el = btn.Tag as AccordionControlElement;
            if (el == null) return;

            if (flyoutOgrenciDetay.IsPopupOpen)
            {
                flyoutOgrenciDetay.HidePopup();
                await Task.Delay(300); // Kapanma animasyonu için bekle
            }

            string fullText = el.Text;
            int idx = fullText.LastIndexOf('(');
            if (idx == -1) return;

            string grp = fullText.Substring(idx + 1).Replace(")", "");
            string namePart = fullText.Substring(0, idx).Trim();
            var parts = namePart.Split(' ');
            if (parts.Length < 2) return;

            string soyad = parts.Last();
            string ad = string.Join(" ", parts.Take(parts.Length - 1));

            seciliOgrenciAd = ad;
            seciliOgrenciSoyad = soyad;
            seciliOgrenciEskiGrup = grp;

            // Grup Listesini Doldur
            cmbFlyoutGrup.Properties.Items.Clear();
            foreach (var item in cmbGrup.Properties.Items)
                if (item.ToString() != "Yeni Grup Oluştur...") cmbFlyoutGrup.Properties.Items.Add(item);
            cmbFlyoutGrup.SelectedItem = grp;

            DataRow details = dbManager.GetStudentDetails(ad, soyad, grp);
            if (details != null)
            {
                nudYas.Value = Convert.ToInt32(details["Yas"]);
                nudBoy.Value = Convert.ToInt32(details["Boy"]);
                nudKilo.Value = Convert.ToInt32(details["Kilo"]);
            }
            else
            {
                nudYas.Value = 0; nudBoy.Value = 0; nudKilo.Value = 0;
            }

            lblFlyoutBaslik.Text = $"{ad}\n{soyad}";
            flyoutOgrenciDetay.ShowPopup();
        }

        private void BtnDetayKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(seciliOgrenciAd)) return;
            string yeniGrup = cmbFlyoutGrup.Text;
            dbManager.UpdateStudentDetails(seciliOgrenciAd, seciliOgrenciSoyad, seciliOgrenciEskiGrup, yeniGrup, (int)nudYas.Value, (int)nudBoy.Value, (int)nudKilo.Value);
            flyoutOgrenciDetay.HidePopup();
            XtraMessageBox.Show("Bilgiler güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OgrenciListesiniGuncelle();
        }

        // --- DETAYLI PROMPT ALANI ---
        private async void BtnOlustur_Click(object sender, EventArgs e)
        {
            string grp = cmbAntrenmanGrup.Text;
            string stil = GetSecilenStiller();
            string odak = GetBiyomotor();
            string ekipman = chkEkipman.Checked ? "Palet, Şnorkel, Tahta kullanılabilir." : "Ekipman yok.";
            int sure = (int)nudToplamSure.Value;
            int mesafe = (int)nudToplamMesafe.Value;

            string eski = btnOlustur.Text;
            btnOlustur.Text = "Yapay Zeka Hazırlıyor...";
            btnOlustur.Enabled = false;

            try
            {
                // Geçmiş veriyi al
                string gecmisOzet = "Bu grup için ilk antrenman.";
                try
                {
                    if (dbManager.GetTrainingHistoryByGroup(grp).Rows.Count > 0)
                        gecmisOzet = "Bu grubun geçmiş antrenmanları var, seviyeyi koruyarak ilerle.";
                }
                catch { }

                // --- DETAYLI PROMPT ---
                string prompt = $@"
                Sen uzman bir yüzme antrenörüsün. Aşağıdaki kriterlere göre profesyonel bir yüzme antrenmanı planla.
                
                **GRUP BİLGİLERİ:**
                - Hedef Grup: {grp}
                - Antrenman Geçmişi: {gecmisOzet}
                
                **ANTRENMAN HEDEFLERİ:**
                - Odak Noktası (Biyomotorik): {odak.ToUpper()}
                - Yüzme Stili: {stil}
                - Toplam Mesafe Hedefi: ~{mesafe} metre
                - Toplam Süre: {sure} dakika
                - Ekipman Durumu: {ekipman}

                **İSTENEN ÇIKTI FORMATI:**
                Lütfen cevabını SADECE aşağıdaki JSON formatında ver. Başka hiçbir açıklama yazma.
                Antrenmanı mantıklı bölümlere ayır (Isınma, Ana Set, Teknik Driller, Soğuma vb.).
                Setleri detaylandır (Örn: '4x50m', '8x100m @1:45', '200m Karışık' gibi).

                {{
                  ""antrenman"": [
                    {{ ""bolum"": ""ISINMA"", ""aktivite"": ""200m Serbest + 200m Sırt"", ""detay"": ""Rahat tempo, uzun kulaç"" }},
                    {{ ""bolum"": ""ANA SET"", ""aktivite"": ""8x50m {stil} Sprint"", ""detay"": ""@1:00 Çıkış, Maksimum efor"" }},
                    {{ ""bolum"": ""SOĞUMA"", ""aktivite"": ""200m Gevşeme"", ""detay"": ""Çok yavaş"" }}
                  ]
                }}
                ";

                GeminiManager ai = new GeminiManager();
                string json = await ai.AntrenmanProgramiIste(prompt);

                // JSON Temizliği
                json = json.Replace("```json", "").Replace("```", "").Trim();
                if (json.StartsWith("json")) json = json.Substring(4).Trim();

                var list = JsonConvert.DeserializeObject<List<AntrenmanSatiri>>(json);
                // Eğer kök obje varsa ({"antrenman": [...]})
                if (list == null)
                {
                    var root = JsonConvert.DeserializeObject<Dictionary<string, List<AntrenmanSatiri>>>(json);
                    if (root != null && root.ContainsKey("antrenman")) list = root["antrenman"];
                }

                if (list != null)
                {
                    programListesi.Clear();
                    foreach (var i in list) programListesi.Add(i);

                    dbManager.AddTrainingLog(grp, DateTime.Now, mesafe, sure, json);
                    if (cmbGrup.Text == grp) cmbGrup_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnOlustur.Text = eski;
                btnOlustur.Enabled = true;
            }
        }

        // --- DİĞER FONKSİYONLAR ---
        private void BtnEkleOgrenci_Click(object sender, EventArgs e)
        {
            string a = txtAd.Text, s = txtSoyad.Text, g = cmbGrup.Text == "Yeni Grup Oluştur..." ? txtGrupInput.Text : cmbGrup.Text;
            if (!string.IsNullOrEmpty(a))
            {
                dbManager.AddStudent(a, s, g);
                if (!cmbGrup.Properties.Items.Contains(g)) { cmbGrup.Properties.Items.Insert(cmbGrup.Properties.Items.Count - 1, g); cmbAntrenmanGrup.Properties.Items.Add(g); }
                if (cmbGrup.Text == "Yeni Grup Oluştur...") cmbGrup.SelectedItem = g; else OgrenciListesiniGuncelle();
                txtAd.Text = ""; txtAd.EditValue = null; txtSoyad.Text = ""; txtSoyad.EditValue = null;
            }
        }

        private void BtnSilOgrenci_Click(object sender, EventArgs e)
        {
            var sel = accordionOgrenciler.SelectedElement;
            if (sel == null || sel.Style == ElementStyle.Group) { XtraMessageBox.Show("Seçim yapın."); return; }
            string t = sel.Text;
            if (XtraMessageBox.Show("Sil?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idx = t.LastIndexOf('('); string g = t.Substring(idx + 1).Replace(")", ""), n = t.Substring(0, idx).Trim(); var p = n.Split(' ');
                dbManager.DeleteStudent(string.Join(" ", p.Take(p.Length - 1)), p.Last(), g); OgrenciListesiniGuncelle();
            }
        }

        private string GetBiyomotor() { return btnDayaniklilik.Checked ? "Dayanıklılık" : (btnSurat.Checked ? "Sürat" : (btnKuvvet.Checked ? "Kuvvet" : (btnEsneklik.Checked ? "Esneklik" : "Koordinasyon"))); }
        private string GetSecilenStiller() { List<string> s = new List<string>(); if (btnSerbest.Checked) s.Add("Serbest"); if (btnSirtustu.Checked) s.Add("Sırtüstü"); if (btnKurbagalama.Checked) s.Add("Kurbağa"); if (btnKelebek.Checked) s.Add("Kelebek"); return s.Count > 0 ? string.Join(", ", s) : "Karışık"; }

        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string g = cmbGrup.Text;
            if (g != "Yeni Grup Oluştur...")
            {
                OgrenciListesiniGuncelle();
                try
                {
                    DataTable dt = dbManager.GetTrainingHistoryByGroup(g);
                    gcGecmis.DataSource = dt;
                    var v = gvGecmis;
                    if (v.Columns["Id"] != null) v.Columns["Id"].Visible = false;
                    if (v.Columns["Icerik"] != null) v.Columns["Icerik"].Visible = false;
                    if (v.Columns["Tarih"] != null) v.Columns["Tarih"].Visible = false;
                    if (v.Columns["TarihFormatted"] != null) { v.Columns["TarihFormatted"].Caption = "Tarih"; v.Columns["TarihFormatted"].VisibleIndex = 0; }
                }
                catch { }
            }
            else { gcGecmis.DataSource = null; aceMainGroup.Elements.Clear(); }
        }

        private void GvGecmis_DoubleClick(object sender, EventArgs e)
        {
            var v = sender as GridView; if (v.FocusedRowHandle < 0) return;
            string json = v.GetFocusedRowCellValue("Icerik")?.ToString();
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    var list = JsonConvert.DeserializeObject<List<AntrenmanSatiri>>(json);
                    if (list == null)
                    {
                        var root = JsonConvert.DeserializeObject<Dictionary<string, List<AntrenmanSatiri>>>(json);
                        if (root != null && root.ContainsKey("antrenman")) list = root["antrenman"];
                    }
                    if (list != null) { programListesi.Clear(); foreach (var i in list) programListesi.Add(i); tabControl1.SelectedTabPage = tabPageAntrenmanOlustur; }
                }
                catch { }
            }
        }

        private void ExportGrid(GridControl g, string n) { string p = $"{n}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"; if ((g.MainView as GridView).RowCount > 0) { g.ExportToPdf(p); System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { FileName = p, UseShellExecute = true }); } }
        private void BtnSilGrup_Click(object sender, EventArgs e) { string g = cmbGrup.Text; if (g != "Yeni Grup Oluştur..." && XtraMessageBox.Show("Grup sil?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes) { dbManager.DeleteAllInGroup(g); cmbGrup.Properties.Items.Remove(g); cmbAntrenmanGrup.Properties.Items.Remove(g); cmbGrup.SelectedIndex = 0; OgrenciListesiniGuncelle(); } }
        private void BtnGecmisSil_Click(object sender, EventArgs e) { int h = gvGecmis.FocusedRowHandle; if (h >= 0 && XtraMessageBox.Show("Sil?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes) { dbManager.DeleteTrainingLog(Convert.ToInt32(gvGecmis.GetRowCellValue(h, "Id"))); cmbGrup_SelectedIndexChanged(null, null); } }
        private void BtnSeciliyiIndir_Click(object sender, EventArgs e) { GvGecmis_DoubleClick(gvGecmis, null); ExportGrid(gcProgram, "Detay"); }
    }
}
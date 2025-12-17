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

namespace YüzmeAntrenmanıPlanlama
{
    // Veri Modeli (JSON ile uyumlu olması için Attribute ekledik)
    public class AntrenmanSatiri
    {
        [JsonProperty("bolum")]
        public string Bolum { get; set; }

        [JsonProperty("aktivite")]
        public string Aktivite { get; set; }

        [JsonProperty("detay")]
        public string Detay { get; set; }
    }

    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        // Yöneticiler
        private DbManager dbManager = new DbManager();
        private PdfManager pdfManager = new PdfManager();
        private readonly string[] initialGroups = { "A Grubu", "B Grubu", "C Grubu" };

        // Grid'e bağlayacağımız liste (BindingList ekranda anlık güncelleme sağlar)
        private System.ComponentModel.BindingList<AntrenmanSatiri> programListesi;

        public Form1()
        {
            InitializeComponent();

            // Grid Bağlantısı
            programListesi = new System.ComponentModel.BindingList<AntrenmanSatiri>();
            gcProgram.DataSource = programListesi;

            InitializeGroups();
            SetupEvents();
        }

        private void SetupEvents()
        {
            // Combobox Eventleri
            this.cmbGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGrup.SelectedIndexChanged += new EventHandler(this.cmbGrup_SelectedIndexChanged);

            // "Yeni Grup Oluştur..." seçilirse input alanını göster/gizle
            this.cmbGrup.SelectedIndexChanged += (s, e) => {
                bool isNew = cmbGrup.Text == "Yeni Grup Oluştur...";
                txtGrupInput.Visible = isNew;
            };

            // Buton Eventleri
            this.btnOlustur.Click += new EventHandler(this.BtnOlustur_Click);
            this.btnEkleOgrenci.Click += new EventHandler(this.BtnEkleOgrenci_Click);
            this.btnSilOgrenci.Click += new EventHandler(this.BtnSilOgrenci_Click);
            this.btnSilGrup.Click += new EventHandler(this.BtnSilGrup_Click);

            // PDF ve Silme İşlemleri
            this.btnPdfIndir.Click += (s, e) => ExportGrid(gcProgram, "Antrenman");
            this.btnPDFOlarakIndirProfil.Click += (s, e) => ExportGrid(gcGecmis, "Gecmis");
            this.btnSeciliyiIndir.Click += new EventHandler(this.BtnSeciliyiIndir_Click);
            this.btnGecmisSil.Click += new EventHandler(this.BtnGecmisSil_Click);

            // Geçmiş Grid Çift Tıklama
            this.gvGecmis.DoubleClick += GvGecmis_DoubleClick;
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

        // --- BUTTON MANTIĞI ---
        private string GetBiyomotor()
        {
            if (btnDayaniklilik.Checked) return "Dayanıklılık";
            if (btnSurat.Checked) return "Sürat";
            if (btnKuvvet.Checked) return "Kuvvet";
            if (btnEsneklik.Checked) return "Esneklik";
            if (btnKoordinasyon.Checked) return "Koordinasyon";
            return "Dayanıklılık"; // Varsayılan
        }

        private string GetSecilenStiller()
        {
            List<string> stiller = new List<string>();
            if (btnSerbest.Checked) stiller.Add("Serbest");
            if (btnSirtustu.Checked) stiller.Add("Sırtüstü");
            if (btnKurbagalama.Checked) stiller.Add("Kurbağa");
            if (btnKelebek.Checked) stiller.Add("Kelebek");

            return stiller.Count > 0 ? string.Join(", ", stiller) : "Karışık";
        }

        // --- ACCORDION (LİSTE) YÖNETİMİ ---
        private void OgrenciListesiniGuncelle()
        {
            aceMainGroup.Elements.Clear(); // Listeyi temizle
            string selectedGroup = cmbGrup.Text;

            if (selectedGroup == "Yeni Grup Oluştur..." || string.IsNullOrEmpty(selectedGroup)) return;

            var list = dbManager.GetFormattedStudentList();
            foreach (var item in list)
            {
                if (item.Contains($"({selectedGroup})"))
                {
                    // Accordion Elemanı Ekle
                    AccordionControlElement el = new AccordionControlElement(ElementStyle.Item);
                    el.Text = item; // Örn: "Ahmet Yılmaz (A Grubu)"
                    aceMainGroup.Elements.Add(el);
                }
            }
        }

        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGroup = cmbGrup.Text;
            if (selectedGroup != "Yeni Grup Oluştur...")
            {
                OgrenciListesiniGuncelle();
                try
                {
                    DataTable dt = dbManager.GetTrainingHistoryByGroup(selectedGroup);
                    gcGecmis.DataSource = dt;

                    // Grid Sütunlarını Gizle
                    var view = gvGecmis;
                    if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
                    if (view.Columns["Icerik"] != null) view.Columns["Icerik"].Visible = false;
                    if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Visible = false;

                    // TarihFormatted sütunu varsa başlığını düzelt
                    if (view.Columns["TarihFormatted"] != null)
                    {
                        view.Columns["TarihFormatted"].Caption = "Tarih";
                        view.Columns["TarihFormatted"].VisibleIndex = 0;
                    }
                }
                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
            else
            {
                gcGecmis.DataSource = null;
                aceMainGroup.Elements.Clear();
            }
        }

        // --- ANTRENMAN OLUŞTURMA ---
        private async void BtnOlustur_Click(object sender, EventArgs e)
        {
            string secilenGrup = cmbAntrenmanGrup.Text;
            int sure = (int)nudToplamSure.Value;
            int mesafe = (int)nudToplamMesafe.Value;
            string ekipman = chkEkipman.Checked ? "Var" : "Yok";

            string stil = GetSecilenStiller();
            string odak = GetBiyomotor();

            string eskiText = btnOlustur.Text;
            btnOlustur.Text = "Yapay Zeka Çalışıyor...";
            btnOlustur.Enabled = false;

            try
            {
                // Geçmiş Özetini Al
                string gecmisOzet = "İlk Antrenman";
                try
                {
                    DataTable dt = dbManager.GetTrainingHistoryByGroup(secilenGrup);
                    if (dt.Rows.Count > 0) gecmisOzet = "Önceki antrenmanlar mevcut.";
                }
                catch { }

                string prompt = $@"Sen uzman bir yüzme antrenörüsün.
                Grup: {secilenGrup} | Geçmiş: {gecmisOzet}
                Hedef: {odak} | Stil: {stil} | Mesafe: {mesafe}m | Ekipman: {ekipman}
                
                KURALLAR: 
                1. Setleri parçala (Örn: '4x50m Serbest' ayrı satır).
                2. Detay kısmına sadece teknik veri yaz (Tempo: A1, Dinlenme: 15sn).
                
                ÇIKTI (JSON):
                {{ ""antrenman"": [ {{ ""bolum"": ""ANA SET"", ""aktivite"": ""..."", ""detay"": ""..."" }} ] }}";

                GeminiManager aiManager = new GeminiManager();
                string jsonCevap = await aiManager.AntrenmanProgramiIste(prompt);

                if (jsonCevap.StartsWith("HATA:"))
                {
                    XtraMessageBox.Show(jsonCevap, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jsonCevap = jsonCevap.Replace("```json", "").Replace("```", "").Trim();

                List<AntrenmanSatiri> antrenmanListesi = null;

                try
                {
                    // 1. Doğrudan Liste mi?
                    antrenmanListesi = JsonConvert.DeserializeObject<List<AntrenmanSatiri>>(jsonCevap);
                }
                catch
                {
                    // 2. Obje içinde mi?
                    var rootObj = JsonConvert.DeserializeObject<Dictionary<string, List<AntrenmanSatiri>>>(jsonCevap);
                    if (rootObj != null && rootObj.Values.Count > 0) antrenmanListesi = rootObj.Values.First();
                }

                if (antrenmanListesi != null)
                {
                    programListesi.Clear(); // BindingList'i temizle
                    foreach (var satir in antrenmanListesi)
                    {
                        // BindingList'e ekle (Grid otomatik güncellenir)
                        programListesi.Add(satir);
                    }

                    dbManager.AddTrainingLog(secilenGrup, DateTime.Now, mesafe, sure, jsonCevap);

                    // Eğer şu an o grup seçiliyse geçmiş listesini yenile
                    if (cmbGrup.Text == secilenGrup) cmbGrup_SelectedIndexChanged(null, null);

                    XtraMessageBox.Show("Program oluşturuldu ve kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { XtraMessageBox.Show("Hata: " + ex.Message); }
            finally { btnOlustur.Text = eskiText; btnOlustur.Enabled = true; }
        }

        // --- GEÇMİŞ DETAY (ÇİFT TIKLAMA) ---
        private void GvGecmis_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle < 0) return;

            object val = view.GetFocusedRowCellValue("Icerik");
            if (val == null) return;
            string json = val.ToString();

            if (string.IsNullOrEmpty(json)) return;

            try
            {
                var list = JsonConvert.DeserializeObject<List<AntrenmanSatiri>>(json);
                if (list == null) // Format farklıysa dictionary dene
                {
                    var rootObj = JsonConvert.DeserializeObject<Dictionary<string, List<AntrenmanSatiri>>>(json);
                    if (rootObj != null && rootObj.Values.Count > 0) list = rootObj.Values.First();
                }

                if (list != null)
                {
                    programListesi.Clear();
                    foreach (var item in list) programListesi.Add(item);
                    tabControl1.SelectedTabPage = tabPageAntrenmanOlustur; // Sekmeyi değiştir
                }
            }
            catch { XtraMessageBox.Show("Geçmiş veri formatı okunamadı."); }
        }

        // --- PDF İŞLEMLERİ ---
        private void ExportGrid(GridControl grid, string name)
        {
            string path = $"{name}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";
            // Grid'in içinde veri var mı kontrol et
            if (grid.MainView is GridView view && view.RowCount > 0)
            {
                grid.ExportToPdf(path);
                if (XtraMessageBox.Show("PDF oluşturuldu. Açmak ister misiniz?", "Başarılı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { FileName = path, UseShellExecute = true });
            }
            else
            {
                XtraMessageBox.Show("Dışa aktarılacak veri yok.");
            }
        }

        // --- ÖĞRENCİ EKLEME/SİLME ---
        private void BtnEkleOgrenci_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            string grup = cmbGrup.Text == "Yeni Grup Oluştur..." ? txtGrupInput.Text : cmbGrup.Text;

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad))
            {
                XtraMessageBox.Show("Ad ve Soyad girmelisiniz.");
                return;
            }

            dbManager.AddStudent(ad, soyad, grup);

            // Eğer yeni grupsa listeye ekle
            if (!cmbGrup.Properties.Items.Contains(grup))
            {
                cmbGrup.Properties.Items.Insert(cmbGrup.Properties.Items.Count - 1, grup);
                cmbAntrenmanGrup.Properties.Items.Add(grup);
            }

            if (cmbGrup.Text == "Yeni Grup Oluştur...")
            {
                cmbGrup.SelectedItem = grup;
            }
            else
            {
                OgrenciListesiniGuncelle();
            }

            txtAd.Text = ""; txtSoyad.Text = "";
            XtraMessageBox.Show("Öğrenci eklendi.");
        }

        private void BtnSilOgrenci_Click(object sender, EventArgs e)
        {
            // Accordion'da seçili elemanı bul
            var selected = accordionOgrenciler.SelectedElement;
            if (selected == null || selected.Style == ElementStyle.Group)
            {
                XtraMessageBox.Show("Lütfen silinecek öğrenciyi listeden seçin.");
                return;
            }

            string fullText = selected.Text; // "Ahmet Yılmaz (A Grubu)"
            if (XtraMessageBox.Show($"{fullText} silinsin mi?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // İsmi parse et
                    int idx = fullText.LastIndexOf('(');
                    string grp = fullText.Substring(idx + 1).Replace(")", "");
                    string namePart = fullText.Substring(0, idx).Trim();
                    var parts = namePart.Split(' ');
                    string soyad = parts.Last();
                    string ad = string.Join(" ", parts.Take(parts.Length - 1));

                    dbManager.DeleteStudent(ad, soyad, grp);
                    OgrenciListesiniGuncelle();
                }
                catch { XtraMessageBox.Show("Silme işleminde hata oluştu."); }
            }
        }

        private void BtnSilGrup_Click(object sender, EventArgs e)
        {
            string g = cmbGrup.Text;
            if (g == "Yeni Grup Oluştur..." || string.IsNullOrEmpty(g)) return;

            if (XtraMessageBox.Show($"'{g}' grubu ve içindeki tüm öğrenciler silinsin mi?", "Kritik Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dbManager.DeleteAllInGroup(g);
                cmbGrup.Properties.Items.Remove(g);
                cmbAntrenmanGrup.Properties.Items.Remove(g);

                cmbGrup.SelectedIndex = 0;
                OgrenciListesiniGuncelle();
            }
        }

        // --- GEÇMİŞ KAYIT SİLME ---
        private void BtnGecmisSil_Click(object sender, EventArgs e)
        {
            // GridView'den Id'yi al
            int rowHandle = gvGecmis.FocusedRowHandle;
            if (rowHandle < 0) return;

            if (XtraMessageBox.Show("Seçili antrenman kaydı silinsin mi?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                object idVal = gvGecmis.GetRowCellValue(rowHandle, "Id");
                if (idVal != null)
                {
                    int id = Convert.ToInt32(idVal);
                    dbManager.DeleteTrainingLog(id);
                    cmbGrup_SelectedIndexChanged(null, null); // Listeyi yenile
                }
            }
        }

        private void BtnSeciliyiIndir_Click(object sender, EventArgs e)
        {
            // Önce seçiliyi yükle, sonra export et
            GvGecmis_DoubleClick(gvGecmis, null);
            ExportGrid(gcProgram, "SeciliAntrenmanDetay");
        }
    }
}
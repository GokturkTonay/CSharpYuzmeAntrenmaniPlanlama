using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Data.Filtering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // Sürükle-Bırak için değişkenler
        private GridHitInfo downHitInfo = null;

        public Form1()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

            programListesi = new System.ComponentModel.BindingList<AntrenmanSatiri>();
            gcProgram.DataSource = programListesi;

            InitializeGroups();
            SetupEvents();

            if (cmbGrup.Properties.Items.Count > 0)
            {
                cmbGrup.SelectedIndex = 0;
            }

            // Başlangıçta tüm öğrenci listesini grid'e yükle
            TumOgrencileriYukle();
        }

        private void SetupEvents()
        {
            this.cmbGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAntrenmanGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Yeni Grid Filtreleme Olayları
            this.txtGridAraCustom.EditValueChanged += (s, e) => ApplyGridFilters();
            this.cmbGridFiltreGrup.SelectedIndexChanged += (s, e) => ApplyGridFilters();

            // "Hizala" butonu olayı kaldırıldı.

            // Grid Sürükle-Bırak Olayları
            this.gcTumOgrenciler.MouseDown += GcTumOgrenciler_MouseDown;
            this.gcTumOgrenciler.MouseMove += GcTumOgrenciler_MouseMove;
            this.gcTumOgrenciler.DragOver += GcTumOgrenciler_DragOver;
            this.gcTumOgrenciler.DragDrop += GcTumOgrenciler_DragDrop;

            this.cmbGrup.SelectedIndexChanged += (s, e) => txtGrupInput.Visible = cmbGrup.Text == "Yeni Grup Oluştur...";

            this.btnOlustur.Click += BtnOlustur_Click;
            this.btnEkleOgrenci.Click += BtnEkleOgrenci_Click;
            this.btnSilOgrenci.Click += BtnSilOgrenci_Click;
            this.btnSilGrup.Click += BtnSilGrup_Click;

            this.btnPdfIndir.Click += (s, e) => ExportGrid(gcProgram, "Antrenman_Programi");
            this.btnPDFOlarakIndirProfil.Click += (s, e) => ExportGrid(gcGecmis, "Gecmis_Antrenmanlar");
            this.btnSeciliyiIndir.Click += BtnSeciliyiIndir_Click;

            this.btnGecmisSil.Click += BtnGecmisSil_Click;
            this.gvGecmis.DoubleClick += GvGecmis_DoubleClick;

            this.gvTumOgrenciler.DoubleClick += GvTumOgrenciler_DoubleClick;

            this.btnDetayKaydet.Click += BtnDetayKaydet_Click;
            this.btnDetayKapat.Click += (s, e) => flyoutOgrenciDetay.HidePopup();

            this.tabControlProfilNested.SelectedPageChanged += (s, e) => {
                if (e.Page == subPageGecmisAntrenmanlar && cmbGrup.Text != "Yeni Grup Oluştur...")
                    GecmisAntrenmanlariYukle(cmbGrup.Text);
            };
        }

        private void InitializeGroups()
        {
            this.cmbGridFiltreGrup.Properties.Items.Add("Tüm Gruplar");
            foreach (string grup in initialGroups)
            {
                if (!this.cmbGrup.Properties.Items.Contains(grup)) this.cmbGrup.Properties.Items.Add(grup);
                if (!this.cmbAntrenmanGrup.Properties.Items.Contains(grup)) this.cmbAntrenmanGrup.Properties.Items.Add(grup);
                if (!this.cmbGridFiltreGrup.Properties.Items.Contains(grup)) this.cmbGridFiltreGrup.Properties.Items.Add(grup);
            }
            this.cmbGrup.Properties.Items.Add("Yeni Grup Oluştur...");
            this.cmbGrup.SelectedIndex = 0;
            this.cmbAntrenmanGrup.SelectedIndex = 0;
            this.cmbGridFiltreGrup.SelectedIndex = 0;
        }

        // "Hizala" butonu metodu kaldırıldı.

        // --- GRİD FİLTRELEME MANTIĞI ---
        private void ApplyGridFilters()
        {
            string searchText = txtGridAraCustom.Text.Trim();
            string selectedGroup = cmbGridFiltreGrup.Text;

            // Filtreleme yapılırken gruplamayı kaldır
            colOgrGrup.GroupIndex = -1;

            GroupOperator finalFilter = new GroupOperator(GroupOperatorType.And);

            if (!string.IsNullOrEmpty(searchText))
            {
                CriteriaOperator searchCriteria = CriteriaOperator.Parse("Contains([Ad], ?) OR Contains([Soyad], ?)", searchText, searchText);
                finalFilter.Operands.Add(searchCriteria);
            }

            if (selectedGroup != "Tüm Gruplar" && !string.IsNullOrEmpty(selectedGroup))
            {
                CriteriaOperator groupCriteria = new BinaryOperator("Grup", selectedGroup, BinaryOperatorType.Equal);
                finalFilter.Operands.Add(groupCriteria);
            }

            gvTumOgrenciler.ActiveFilterCriteria = finalFilter;
        }

        // --- GRİD SÜRÜKLE-BIRAK MANTIĞI ---
        private void GcTumOgrenciler_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = gcTumOgrenciler.MainView as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.InRow && view.IsGroupRow(hitInfo.RowHandle))
            {
                downHitInfo = hitInfo;
            }
        }

        private void GcTumOgrenciler_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = gcTumOgrenciler.MainView as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(downHitInfo.RowHandle, DragDropEffects.Move);
                    downHitInfo = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void GcTumOgrenciler_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(int)))
            {
                GridView view = gcTumOgrenciler.MainView as GridView;
                GridHitInfo hitInfo = view.CalcHitInfo(gcTumOgrenciler.PointToClient(new Point(e.X, e.Y)));
                if (hitInfo.InRow && view.IsGroupRow(hitInfo.RowHandle) && hitInfo.RowHandle != (int)e.Data.GetData(typeof(int)))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void GcTumOgrenciler_DragDrop(object sender, DragEventArgs e)
        {
            GridView view = gcTumOgrenciler.MainView as GridView;
            Point dragPointScreen = new Point(e.X, e.Y);
            Point dragPointClient = gcTumOgrenciler.PointToClient(dragPointScreen);
            GridHitInfo hitInfo = view.CalcHitInfo(dragPointClient);

            int sourceRowHandle = (int)e.Data.GetData(typeof(int));
            int targetRowHandle = hitInfo.RowHandle;

            string movingGroup = view.GetGroupRowValue(sourceRowHandle).ToString();
            string targetGroup = view.GetGroupRowValue(targetRowHandle).ToString();

            GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
            GridRowInfo groupRowInfo = viewInfo.GetGridRowInfo(targetRowHandle);
            Rectangle targetRowBounds = groupRowInfo.Bounds;

            int relativeY = dragPointClient.Y - targetRowBounds.Y;
            bool insertBefore = relativeY < (targetRowBounds.Height / 2);

            try
            {
                dbManager.UpdateGroupOrder(movingGroup, targetGroup, insertBefore);
                TumOgrencileriYukle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Sıralama güncellenirken hata oluştu: " + ex.Message);
            }
        }

        // ===================================================================

        private void TumOgrencileriYukle()
        {
            DataTable dtOgrenciler = dbManager.GetAllStudents();
            gcTumOgrenciler.DataSource = dtOgrenciler;
            // Veri yüklendiğinde filtre varsa uygula, yoksa grupla
            if (string.IsNullOrEmpty(txtGridAraCustom.Text) && cmbGridFiltreGrup.Text == "Tüm Gruplar")
            {
                colOgrGrup.GroupIndex = 0;
                gvTumOgrenciler.ExpandAllGroups();
            }
            else
            {
                ApplyGridFilters();
            }
        }

        // --- ÖĞRENCİ EKLEME METODU (Hizala Mantığı Buraya Eklendi) ---
        private void BtnEkleOgrenci_Click(object sender, EventArgs e)
        {
            string a = txtAd.Text.Trim();
            string s = txtSoyad.Text.Trim();
            string g = cmbGrup.Text == "Yeni Grup Oluştur..." ? txtGrupInput.Text.Trim() : cmbGrup.Text;

            if (!string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(g))
            {
                dbManager.AddStudent(a, s, g);
                if (!cmbGrup.Properties.Items.Contains(g))
                {
                    cmbGrup.Properties.Items.Insert(cmbGrup.Properties.Items.Count - 1, g);
                    cmbAntrenmanGrup.Properties.Items.Add(g);
                    cmbGridFiltreGrup.Properties.Items.Add(g);
                }

                if (cmbGrup.Text == "Yeni Grup Oluştur...")
                    cmbGrup.SelectedItem = g;

                // --- HİZALA MANTIĞI BAŞLANGICI ---
                // 1. Filtreleri temizle
                txtGridAraCustom.Text = "";
                cmbGridFiltreGrup.SelectedIndex = 0; // "Tüm Gruplar"
                gvTumOgrenciler.ClearColumnsFilter();

                // 2. Veriyi yükle (TumOgrencileriYukle filtreler temizken otomatik gruplar)
                TumOgrencileriYukle();
                // --- HİZALA MANTIĞI BİTİŞİ ---

                txtAd.Text = ""; txtAd.EditValue = null;
                txtSoyad.Text = ""; txtSoyad.EditValue = null;
                txtGrupInput.Text = "";
                XtraMessageBox.Show("Öğrenci başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Lütfen Ad, Soyad ve Grup bilgilerini eksiksiz giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSilGrup_Click(object sender, EventArgs e)
        {
            string g = cmbGrup.Text;
            if (g == "Yeni Grup Oluştur..." || string.IsNullOrEmpty(g))
            {
                XtraMessageBox.Show("Lütfen silinecek geçerli bir grup seçin.");
                return;
            }

            if (XtraMessageBox.Show($"'{g}' grubunu ve içindeki TÜM ÖĞRENCİLERİ silmek istediğinize emin misiniz? Bu işlem geri alınamaz!", "Kritik Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dbManager.DeleteAllInGroup(g);
                cmbGrup.Properties.Items.Remove(g);
                cmbAntrenmanGrup.Properties.Items.Remove(g);
                cmbGridFiltreGrup.Properties.Items.Remove(g);

                cmbGrup.SelectedIndex = 0;
                TumOgrencileriYukle();
            }
        }

        private async void GvTumOgrenciler_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition));

            if (!hitInfo.InRow || view.IsGroupRow(hitInfo.RowHandle)) return;

            if (flyoutOgrenciDetay.IsPopupOpen)
            {
                flyoutOgrenciDetay.HidePopup();
                await Task.Delay(300);
            }

            string ad = view.GetRowCellValue(hitInfo.RowHandle, "Ad")?.ToString();
            string soyad = view.GetRowCellValue(hitInfo.RowHandle, "Soyad")?.ToString();
            string grp = view.GetRowCellValue(hitInfo.RowHandle, "Grup")?.ToString();

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad)) return;

            seciliOgrenciAd = ad;
            seciliOgrenciSoyad = soyad;
            seciliOgrenciEskiGrup = grp;

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
            TumOgrencileriYukle();
            if (seciliOgrenciEskiGrup != yeniGrup && cmbGrup.Properties.Items.Contains(yeniGrup))
            {
                cmbGrup.SelectedItem = yeniGrup;
            }
        }

        private async void BtnOlustur_Click(object sender, EventArgs e)
        {
            string grp = cmbAntrenmanGrup.Text;
            string stil = GetSecilenStiller();
            string odak = GetBiyomotor();
            string ekipman = chkEkipman.Checked ? "Palet, Şnorkel, Tahta kullanılabilir." : "Ekipman yok.";
            int sure = (int)nudToplamSure.Value;
            int mesafe = (int)nudToplamMesafe.Value;

            string eskiButtonMetni = btnOlustur.Text;
            btnOlustur.Text = "Yapay Zeka Hazırlıyor...";
            btnOlustur.Enabled = false;

            try
            {
                string gecmisOzet = "Bu grup için ilk antrenman.";
                try
                {
                    if (dbManager.GetTrainingHistoryByGroup(grp).Rows.Count > 0)
                        gecmisOzet = "Bu grubun geçmiş antrenmanları var, önceki tempoyu dikkate alarak seviyeyi koruyacak veya artıracak şekilde ilerle.";
                }
                catch { }

                string prompt = $@"
                 Sen uzman bir yüzme antrenörüsün. Aşağıdaki kriterlere göre profesyonel ve detaylı bir yüzme antrenmanı planla.
                 
                 **GRUP BİLGİLERİ:**
                 - Hedef Grup: {grp}
                 - Antrenman Geçmişi Durumu: {gecmisOzet}
                 
                 **ANTRENMAN HEDEFLERİ:**
                 - Odak Noktası (Biyomotorik Yetenek): {odak.ToUpper()}
                 - Yüzme Stilleri: {stil} (Eğer birden fazla stil seçildiyse, bunları antrenmanın ana setlerine mantıklı bir şekilde dağıt, kombine et veya dönüşümlü olarak kullan.)
                 - Toplam Mesafe Hedefi: Yaklaşık {mesafe} metre
                 - Toplam Süre: {sure} dakika
                 - Ekipman Durumu: {ekipman}

                 **İSTENEN ÇIKTI FORMATI:**
                 Lütfen cevabını SADECE aşağıdaki JSON dizisi formatında ver. Başka hiçbir açıklama, giriş veya bitiş cümlesi yazma.
                 Antrenmanı mantıklı bölümlere ayır (Isınma, Teknik Driller, Ana Set, İkinci Set, Soğuma vb.).
                 'aktivite' kısmında setleri detaylandır (Örn: '4x50m Serbest @1:00', '200m Karışık Drill', '8x25m Seçilen Stillerde Sprint' gibi).
                 'detay' kısmında tempo, nefes, odaklanılacak teknik nokta gibi bilgileri ver.

                 [
                   {{ ""bolum"": ""ISINMA"", ""aktivite"": ""Örnek aktivite"", ""detay"": ""Örnek detay"" }},
                   {{ ""bolum"": ""ANA SET"", ""aktivite"": ""Seçilen stillere uygun ana set içeriği"", ""detay"": ""Tempo ve efor bilgisi"" }}
                 ]
                 ";

                GeminiManager ai = new GeminiManager();
                string json = await ai.AntrenmanProgramiIste(prompt);

                json = json.Replace("```json", "").Replace("```", "").Trim();
                if (json.StartsWith("json")) json = json.Substring(4).Trim();

                var list = JsonConvert.DeserializeObject<List<AntrenmanSatiri>>(json);

                if (list == null)
                {
                    var root = JsonConvert.DeserializeObject<Dictionary<string, List<AntrenmanSatiri>>>(json);
                    if (root != null && root.ContainsKey("antrenman")) list = root["antrenman"];
                    if (list == null && root != null && root.Values.Count > 0) list = root.Values.First();
                }

                if (list != null && list.Count > 0)
                {
                    programListesi.Clear();
                    foreach (var i in list) programListesi.Add(i);

                    dbManager.AddTrainingLog(grp, DateTime.Now, mesafe, sure, json);

                    if (cmbAntrenmanGrup.Text == cmbGrup.Text)
                    {
                        GecmisAntrenmanlariYukle(grp);
                    }

                    XtraMessageBox.Show("Antrenman programı başarıyla oluşturuldu!", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Yapay zeka yanıtı uygun formatta değildi. Lütfen tekrar deneyin.\n\nGelen Yanıt:\n" + json.Substring(0, Math.Min(json.Length, 200)) + "...", "Format Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Antrenman oluşturulurken bir hata meydana geldi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnOlustur.Text = eskiButtonMetni;
                btnOlustur.Enabled = true;
            }
        }

        private void BtnSilOgrenci_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gvTumOgrenciler.FocusedRowHandle;
            if (focusedRowHandle < 0 || gvTumOgrenciler.IsGroupRow(focusedRowHandle))
            {
                XtraMessageBox.Show("Lütfen listeden silinecek öğrenciyi seçin.");
                return;
            }

            string ad = gvTumOgrenciler.GetRowCellValue(focusedRowHandle, "Ad")?.ToString();
            string soyad = gvTumOgrenciler.GetRowCellValue(focusedRowHandle, "Soyad")?.ToString();
            string grup = gvTumOgrenciler.GetRowCellValue(focusedRowHandle, "Grup")?.ToString();

            if (XtraMessageBox.Show($"'{ad} {soyad}' adlı öğrenciyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dbManager.DeleteStudent(ad, soyad, grup);
                TumOgrencileriYukle();
            }
        }

        private string GetBiyomotor() { return btnDayaniklilik.Checked ? "Dayanıklılık" : (btnSurat.Checked ? "Sürat" : (btnKuvvet.Checked ? "Kuvvet" : (btnEsneklik.Checked ? "Esneklik" : "Koordinasyon"))); }

        private string GetSecilenStiller()
        {
            List<string> s = new List<string>();
            if (btnSerbest.Checked) s.Add("Serbest");
            if (btnSirtustu.Checked) s.Add("Sırtüstü");
            if (btnKurbagalama.Checked) s.Add("Kurbağa");
            if (btnKelebek.Checked) s.Add("Kelebek");
            return s.Count > 0 ? string.Join(", ", s) : "Karışık";
        }

        private void GecmisAntrenmanlariYukle(string grupAdi)
        {
            try
            {
                DataTable dt = dbManager.GetTrainingHistoryByGroup(grupAdi);
                gcGecmis.DataSource = dt;
                var v = gvGecmis;
                if (v.Columns["Id"] != null) v.Columns["Id"].Visible = false;
                if (v.Columns["Icerik"] != null) v.Columns["Icerik"].Visible = false;
                if (v.Columns["Tarih"] != null) v.Columns["Tarih"].Visible = false;
                if (v.Columns["TarihFormatted"] != null) { v.Columns["TarihFormatted"].Caption = "Tarih"; v.Columns["TarihFormatted"].VisibleIndex = 0; }
            }
            catch { gcGecmis.DataSource = null; }
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
                    if (list != null)
                    {
                        programListesi.Clear();
                        foreach (var i in list) programListesi.Add(i);
                        tabControlAna.SelectedTabPage = tabPageAntrenmanOlustur;
                        XtraMessageBox.Show("Geçmiş antrenman programı yüklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { XtraMessageBox.Show("Antrenman yüklenirken hata: " + ex.Message); }
            }
        }

        private void ExportGrid(GridControl g, string n)
        {
            string fileName = $"{n}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";
            if ((g.MainView as GridView).RowCount > 0)
            {
                try
                {
                    g.ExportToPdf(fileName);
                    if (XtraMessageBox.Show($"PDF dosyası oluşturuldu:\n{fileName}\n\nDosyayı şimdi açmak ister misiniz?", "Başarılı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var psi = new System.Diagnostics.ProcessStartInfo() { FileName = fileName, UseShellExecute = true };
                        System.Diagnostics.Process.Start(psi);
                    }
                }
                catch (Exception ex) { XtraMessageBox.Show("PDF oluşturulurken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                XtraMessageBox.Show("Dışa aktarılacak veri bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnGecmisSil_Click(object sender, EventArgs e)
        {
            int h = gvGecmis.FocusedRowHandle;
            if (h < 0) { XtraMessageBox.Show("Lütfen silinecek geçmiş kaydını seçin."); return; }
            if (XtraMessageBox.Show("Bu antrenman kaydını kalıcı olarak silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(gvGecmis.GetRowCellValue(h, "Id"));
                dbManager.DeleteTrainingLog(id);
                GecmisAntrenmanlariYukle(cmbGrup.Text);
            }
        }

        private void BtnSeciliyiIndir_Click(object sender, EventArgs e)
        {
            GvGecmis_DoubleClick(gvGecmis, null);
            ExportGrid(gcProgram, "Secili_Gecmis_Antrenman");
        }
    }
}
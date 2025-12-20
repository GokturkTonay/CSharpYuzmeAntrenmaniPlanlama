using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars.Navigation;
using DevExpress.Utils;

namespace YüzmeAntrenmanıPlanlama
{
    partial class Form1
    {
        private IContainer components = null;

        // --- ANA ELEMANLAR ---
        private XtraTabControl tabControlAna;
        private XtraTabPage tabPageProfil;
        private XtraTabPage tabPageAntrenmanOlustur;

        // --- PROFİL SAYFASI İÇİNDEKİ İÇ SEKMELER ---
        private XtraTabControl tabControlProfilNested;
        private XtraTabPage subPageOgrenciSistemi;
        private XtraTabPage subPageGecmisAntrenmanlar;

        // ========================================================================
        // 1. ALT SAYFA: ÖĞRENCİ SİSTEMİ ELEMANLARI
        // ========================================================================
        private SplitterControl splitterOgrenci;
        private PanelControl panelOgrenciSol;
        private PanelControl panelOgrenciSag;

        // SOL TARAF (Inputlar ve Butonlar)
        private TableLayoutPanel tblOgrenciSolLayout;
        private PanelControl panelOgrenciInputs;
        private LabelControl lblOgrenciYonetimiBaslik;
        private TextEdit txtAd, txtSoyad, txtGrupInput;
        private ComboBoxEdit cmbGrup;
        private SimpleButton btnEkleOgrenci;
        private PanelControl panelOgrenciButtons;
        private SimpleButton btnSilOgrenci, btnSilGrup;

        // SAĞ TARAF (FİLTRE PANELİ VE GRİD)
        private TableLayoutPanel tblSagLayout;
        private PanelControl panelGridUstFiltre;
        private TextEdit txtGridAraCustom;
        // Hizala butonu kaldırıldı.
        private ComboBoxEdit cmbGridFiltreGrup;
        private LabelControl lblGridFiltreBaslik;
        private GridControl gcTumOgrenciler;
        private GridView gvTumOgrenciler;
        private GridColumn colOgrAd, colOgrSoyad, colOgrGrup;


        // ========================================================================
        // 2. ALT SAYFA: GEÇMİŞ ANTRENMANLAR ELEMANLARI
        // ========================================================================
        private TableLayoutPanel tblGecmisLayout;
        private LabelControl lblGecmisBaslik;
        private GridControl gcGecmis;
        private GridView gvGecmis;
        private GridColumn colTarih, colSure, colMesafe, colHiz;
        private PanelControl panelGecmisButtons;
        private SimpleButton btnSeciliyiIndir, btnPDFOlarakIndirProfil, btnGecmisSil;


        // --- ANTRENMAN SAYFASI ELEMANLARI ---
        private PanelControl panelAntrenmanLeft;
        private PanelControl panelAntrenmanRight;
        private SplitterControl splitterAntrenman;
        private TableLayoutPanel tblAntrenmanLeftLayout;
        private TableLayoutPanel tblAntrenmanRightLayout;

        private GroupControl grpBiyomotorik;
        private TableLayoutPanel tblBiyomotorikContent;
        private CheckButton btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon;

        private GroupControl grpYuzmeStili;
        private TableLayoutPanel tblYuzmeStiliContent;
        private CheckButton btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek;

        private GroupControl grpAntrenmanBilgileri;
        private TableLayoutPanel tblAntrenmanBilgileriContent;
        private LabelControl lblToplamSure, lblHaftada, lblToplamMesafe, lblAntrenmanGrupSec;
        private SpinEdit nudToplamSure, nudHaftada, nudToplamMesafe;
        private ComboBoxEdit cmbAntrenmanGrup;
        private CheckEdit chkEkipman;
        private SimpleButton btnOlustur;

        private LabelControl lblProgramBaslik;
        private GridControl gcProgram;
        private GridView gvProgram;
        private SimpleButton btnPdfIndir;

        // --- FLYOUT PANEL ---
        private FlyoutPanel flyoutOgrenciDetay;
        private PanelControl pnlFlyoutIcerik;
        private LabelControl lblFlyoutBaslik;

        // Detay Kontrolleri
        private LabelControl lblGrupDegistir;
        public ComboBoxEdit cmbFlyoutGrup;
        private LabelControl lblYas, lblBoy, lblKilo;
        public SpinEdit nudYas;
        public SpinEdit nudBoy;
        public SpinEdit nudKilo;
        private SimpleButton btnDetayKaydet, btnDetayKapat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new Size(1300, 760);
            this.Text = "Yüzme Antrenmanı Planlama Asistanı (VS2022 - .NET 8 - WXI)";
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- ANA TAB CONTROL ---
            this.tabControlAna = new XtraTabControl();
            this.tabControlAna.Dock = DockStyle.Fill;
            this.tabPageProfil = new XtraTabPage() { Text = "Profil ve Yönetim" };
            this.tabPageAntrenmanOlustur = new XtraTabPage() { Text = "Antrenman Oluştur" };
            this.tabControlAna.TabPages.Add(this.tabPageProfil);
            this.tabControlAna.TabPages.Add(this.tabPageAntrenmanOlustur);
            this.Controls.Add(this.tabControlAna);

            // ========================================================================
            // PROFİL SAYFASI İÇİNDEKİ NESTED (İÇ İÇE) TAB YAPISI
            // ========================================================================
            this.tabControlProfilNested = new XtraTabControl() { Dock = DockStyle.Fill };
            this.tabControlProfilNested.AppearancePage.Header.Font = new Font("Segoe UI", 10F);

            this.subPageOgrenciSistemi = new XtraTabPage() { Text = "Öğrenci Sistemi" };
            this.subPageGecmisAntrenmanlar = new XtraTabPage() { Text = "Geçmiş Antrenmanlar" };

            this.tabControlProfilNested.TabPages.Add(this.subPageOgrenciSistemi);
            this.tabControlProfilNested.TabPages.Add(this.subPageGecmisAntrenmanlar);
            this.tabPageProfil.Controls.Add(this.tabControlProfilNested);

            // ########################################################################
            // 1. ALT SAYFA: ÖĞRENCİ SİSTEMİ TASARIMI
            // ########################################################################

            this.panelOgrenciSol = new PanelControl() { Dock = DockStyle.Left, Width = 380, Padding = new Padding(10) };
            this.splitterOgrenci = new SplitterControl() { Dock = DockStyle.Left };
            this.panelOgrenciSag = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(10) };

            this.subPageOgrenciSistemi.Controls.Add(this.panelOgrenciSag);
            this.subPageOgrenciSistemi.Controls.Add(this.splitterOgrenci);
            this.subPageOgrenciSistemi.Controls.Add(this.panelOgrenciSol);

            // --- SOL TARAF (Inputlar ve Butonlar) ---
            this.tblOgrenciSolLayout = new TableLayoutPanel();
            this.tblOgrenciSolLayout.Dock = DockStyle.Fill;
            this.tblOgrenciSolLayout.ColumnCount = 1;
            this.tblOgrenciSolLayout.RowCount = 2;
            this.tblOgrenciSolLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tblOgrenciSolLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            this.panelOgrenciSol.Controls.Add(this.tblOgrenciSolLayout);

            // INPUT PANELİ
            this.panelOgrenciInputs = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(10) };
            this.lblOgrenciYonetimiBaslik = new LabelControl() { Text = "Öğrenci Ekle / Düzenle", Dock = DockStyle.Top, Height = 50, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblOgrenciYonetimiBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblOgrenciYonetimiBaslik.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            this.txtAd = new TextEdit() { Dock = DockStyle.Top, Height = 45 }; this.txtAd.Properties.NullValuePrompt = "Ad Giriniz";
            this.txtSoyad = new TextEdit() { Dock = DockStyle.Top, Height = 45 }; this.txtSoyad.Properties.NullValuePrompt = "Soyad Giriniz";
            this.cmbGrup = new ComboBoxEdit() { Dock = DockStyle.Top, Height = 45 }; this.cmbGrup.Properties.NullValuePrompt = "Grup Seçiniz";
            this.txtGrupInput = new TextEdit() { Dock = DockStyle.Top, Visible = false, Height = 45 }; this.txtGrupInput.Properties.NullValuePrompt = "Yeni Grup Adı";
            this.btnEkleOgrenci = new SimpleButton() { Text = "Kaydet", Dock = DockStyle.Top, Height = 55 };
            this.btnEkleOgrenci.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Control[] gaps = { new Control { Height = 20, Dock = DockStyle.Top }, new Control { Height = 20, Dock = DockStyle.Top }, new Control { Height = 20, Dock = DockStyle.Top }, new Control { Height = 30, Dock = DockStyle.Top } };
            this.panelOgrenciInputs.Controls.AddRange(new Control[] { btnEkleOgrenci, gaps[3], txtGrupInput, cmbGrup, gaps[2], txtSoyad, gaps[1], txtAd, gaps[0], lblOgrenciYonetimiBaslik });
            this.tblOgrenciSolLayout.Controls.Add(this.panelOgrenciInputs, 0, 0);

            // SİLME BUTONLARI PANELİ
            this.panelOgrenciButtons = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(10) };
            this.btnSilOgrenci = new SimpleButton() { Text = "Seçili Öğrenciyi Sil", Dock = DockStyle.Top, Height = 45 };
            this.btnSilGrup = new SimpleButton() { Text = "Seçili Grubu Sil (DİKKAT)", Dock = DockStyle.Bottom, Height = 45 };
            this.panelOgrenciButtons.Controls.AddRange(new Control[] { btnSilGrup, new Control { Height = 15, Dock = DockStyle.Top }, btnSilOgrenci });
            this.tblOgrenciSolLayout.Controls.Add(this.panelOgrenciButtons, 0, 1);

            // --- SAĞ TARAF (FİLTRE PANELİ VE GRİD) ---
            this.tblSagLayout = new TableLayoutPanel();
            this.tblSagLayout.Dock = DockStyle.Fill;
            this.tblSagLayout.ColumnCount = 1;
            this.tblSagLayout.RowCount = 2;
            // Üst Filtre Paneli Yüksekliği
            this.tblSagLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            // Grid (Kalan tüm alan)
            this.tblSagLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.panelOgrenciSag.Controls.Add(this.tblSagLayout);

            // 1. ÜST FİLTRE PANELİ (Arama, Combobox) - Hizala butonu kalktı, tekrar 2 sütun
            this.panelGridUstFiltre = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(5) };
            TableLayoutPanel tblFiltreIci = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            tblFiltreIci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Arama Kutusu
            tblFiltreIci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Grup Filtresi

            this.txtGridAraCustom = new TextEdit() { Dock = DockStyle.Fill, Height = 45 };
            this.txtGridAraCustom.Properties.NullValuePrompt = "İsim veya Soyisim Ara...";
            this.txtGridAraCustom.Properties.Appearance.Font = new Font("Segoe UI", 11F);

            // Hizala butonu tanımı kaldırıldı.

            this.lblGridFiltreBaslik = new LabelControl() { Text = "Grup Filtre:", Dock = DockStyle.Fill, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblGridFiltreBaslik.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            this.lblGridFiltreBaslik.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            this.lblGridFiltreBaslik.Padding = new Padding(0, 0, 10, 0);

            this.cmbGridFiltreGrup = new ComboBoxEdit() { Dock = DockStyle.Fill, Height = 45 };
            this.cmbGridFiltreGrup.Properties.NullValuePrompt = "Tüm Gruplar";
            this.cmbGridFiltreGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGridFiltreGrup.Properties.Appearance.Font = new Font("Segoe UI", 11F);

            // Grup Filtresi için küçük bir panel (Etiket + Combobox)
            TableLayoutPanel pnlGrupFiltre = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = new Padding(0) };
            pnlGrupFiltre.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            pnlGrupFiltre.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlGrupFiltre.Controls.Add(this.lblGridFiltreBaslik, 0, 0);
            pnlGrupFiltre.Controls.Add(this.cmbGridFiltreGrup, 1, 0);

            tblFiltreIci.Controls.Add(this.txtGridAraCustom, 0, 0);
            // Hizala butonu ekleme satırı kaldırıldı.
            tblFiltreIci.Controls.Add(pnlGrupFiltre, 1, 0); // Grup filtresi 2. sütuna

            this.panelGridUstFiltre.Controls.Add(tblFiltreIci);
            this.tblSagLayout.Controls.Add(this.panelGridUstFiltre, 0, 0);

            // 2. GRİD KONTROL
            this.gcTumOgrenciler = new GridControl() { Dock = DockStyle.Fill };
            this.gvTumOgrenciler = new GridView(this.gcTumOgrenciler);
            this.gcTumOgrenciler.MainView = this.gvTumOgrenciler;

            // Grid Ayarları
            this.gvTumOgrenciler.OptionsBehavior.Editable = false;
            this.gvTumOgrenciler.OptionsFind.AlwaysVisible = false; // Kendi filtre panelimiz var
            this.gvTumOgrenciler.OptionsView.ShowGroupPanel = false; // Grup paneli gizli
            this.gvTumOgrenciler.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTumOgrenciler.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            this.gvTumOgrenciler.OptionsView.ShowVerticalLines = DefaultBoolean.False;

            // Sütunlar
            this.colOgrAd = new GridColumn() { Caption = "Ad", FieldName = "Ad", Visible = true, VisibleIndex = 0 };
            this.colOgrSoyad = new GridColumn() { Caption = "Soyad", FieldName = "Soyad", Visible = true, VisibleIndex = 1 };
            this.colOgrGrup = new GridColumn() { Caption = "Grup", FieldName = "Grup", Visible = true, VisibleIndex = 2 };

            this.gvTumOgrenciler.Columns.AddRange(new GridColumn[] { colOgrAd, colOgrSoyad, colOgrGrup });
            this.tblSagLayout.Controls.Add(this.gcTumOgrenciler, 0, 1);


            // ########################################################################
            // 2. ALT SAYFA: GEÇMİŞ ANTRENMANLAR TASARIMI
            // ########################################################################
            PanelControl panelGecmisContainer = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(15) };
            this.subPageGecmisAntrenmanlar.Controls.Add(panelGecmisContainer);

            this.tblGecmisLayout = new TableLayoutPanel();
            this.tblGecmisLayout.Dock = DockStyle.Fill;
            this.tblGecmisLayout.ColumnCount = 1;
            this.tblGecmisLayout.RowCount = 3;
            this.tblGecmisLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tblGecmisLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tblGecmisLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelGecmisContainer.Controls.Add(this.tblGecmisLayout);

            this.lblGecmisBaslik = new LabelControl() { Text = "Kayıtlı Antrenman Geçmişi", Dock = DockStyle.Top, Height = 50, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblGecmisBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblGecmisBaslik.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            this.gcGecmis = new GridControl() { Dock = DockStyle.Fill };
            this.gvGecmis = new GridView(this.gcGecmis); this.gcGecmis.MainView = this.gvGecmis;
            this.gvGecmis.OptionsView.ShowGroupPanel = false;
            this.gvGecmis.OptionsBehavior.Editable = false;
            this.gvGecmis.OptionsView.ColumnAutoWidth = true;
            this.gvGecmis.OptionsView.EnableAppearanceEvenRow = true;
            this.gvGecmis.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            this.gvGecmis.OptionsView.ShowHorizontalLines = DefaultBoolean.True;

            this.colTarih = new GridColumn() { Caption = "Tarih", FieldName = "TarihFormatted", Visible = true, VisibleIndex = 0 };
            this.colSure = new GridColumn() { Caption = "Süre (dk)", FieldName = "Sure", Visible = true, VisibleIndex = 1 };
            this.colMesafe = new GridColumn() { Caption = "Mesafe (m)", FieldName = "Mesafe", Visible = true, VisibleIndex = 2 };
            this.colHiz = new GridColumn() { Caption = "Ort. Hız", FieldName = "OrtHiz", Visible = true, VisibleIndex = 3 };
            this.gvGecmis.Columns.AddRange(new GridColumn[] { colTarih, colSure, colMesafe, colHiz });

            this.panelGecmisButtons = new PanelControl() { Dock = DockStyle.Bottom, AutoSize = true, Padding = new Padding(0, 15, 0, 0) };
            this.btnSeciliyiIndir = new SimpleButton() { Text = "Seçiliyi İndir", Dock = DockStyle.Top, Height = 45 };
            this.btnPDFOlarakIndirProfil = new SimpleButton() { Text = "Listeyi PDF İndir", Dock = DockStyle.Top, Height = 45 };
            this.btnGecmisSil = new SimpleButton() { Text = "KAYDI SİL", Dock = DockStyle.Top, Height = 45 };
            this.panelGecmisButtons.Controls.AddRange(new Control[] { btnPDFOlarakIndirProfil, new Control { Height = 10, Dock = DockStyle.Top }, btnSeciliyiIndir, new Control { Height = 10, Dock = DockStyle.Top }, btnGecmisSil });

            this.tblGecmisLayout.Controls.Add(this.lblGecmisBaslik, 0, 0);
            this.tblGecmisLayout.Controls.Add(this.gcGecmis, 0, 1);
            this.tblGecmisLayout.Controls.Add(this.panelGecmisButtons, 0, 2);

            // ########################################################################
            // ANTRENMAN OLUŞTUR SAYFASI (Değişiklik Yok)
            // ########################################################################
            this.panelAntrenmanLeft = new PanelControl() { Dock = DockStyle.Left, Width = 600, Padding = new Padding(10) };
            this.splitterAntrenman = new SplitterControl() { Dock = DockStyle.Left };
            this.panelAntrenmanRight = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(10) };

            this.tabPageAntrenmanOlustur.Controls.Add(this.panelAntrenmanRight);
            this.tabPageAntrenmanOlustur.Controls.Add(this.splitterAntrenman);
            this.tabPageAntrenmanOlustur.Controls.Add(this.panelAntrenmanLeft);

            this.tblAntrenmanLeftLayout = new TableLayoutPanel();
            this.tblAntrenmanLeftLayout.Dock = DockStyle.Fill;
            this.tblAntrenmanLeftLayout.ColumnCount = 1; this.tblAntrenmanLeftLayout.RowCount = 3;
            this.tblAntrenmanLeftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            this.tblAntrenmanLeftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            this.tblAntrenmanLeftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.panelAntrenmanLeft.Controls.Add(this.tblAntrenmanLeftLayout);

            this.grpBiyomotorik = new GroupControl() { Text = "1. Biyomotorik Yetenek (Tek Seçim)", Dock = DockStyle.Fill };
            this.tblBiyomotorikContent = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 5, RowCount = 1, Padding = new Padding(5) };
            for (int i = 0; i < 5; i++) this.tblBiyomotorikContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            CheckButton CreateBtn(string t, int g) { var b = new CheckButton() { Text = t, Dock = DockStyle.Fill, Margin = new Padding(3), GroupIndex = g }; return b; }

            this.btnDayaniklilik = CreateBtn("Dayanıklılık", 1); this.btnSurat = CreateBtn("Sürat", 1); this.btnKuvvet = CreateBtn("Kuvvet", 1); this.btnEsneklik = CreateBtn("Esneklik", 1); this.btnKoordinasyon = CreateBtn("Koord.", 1);
            this.tblBiyomotorikContent.Controls.AddRange(new Control[] { btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon });
            this.grpBiyomotorik.Controls.Add(this.tblBiyomotorikContent);
            this.tblAntrenmanLeftLayout.Controls.Add(this.grpBiyomotorik, 0, 0);

            this.grpYuzmeStili = new GroupControl() { Text = "2. Yüzme Stili (Çoklu Seçim)", Dock = DockStyle.Fill };
            this.tblYuzmeStiliContent = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, Padding = new Padding(5) };
            for (int i = 0; i < 4; i++) this.tblYuzmeStiliContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            // GroupIndex = -1 (Çoklu seçim için)
            this.btnSerbest = CreateBtn("Serbest", -1);
            this.btnSirtustu = CreateBtn("Sırtüstü", -1);
            this.btnKurbagalama = CreateBtn("Kurbağa", -1);
            this.btnKelebek = CreateBtn("Kelebek", -1);

            this.tblYuzmeStiliContent.Controls.AddRange(new Control[] { btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek });
            this.grpYuzmeStili.Controls.Add(this.tblYuzmeStiliContent);
            this.tblAntrenmanLeftLayout.Controls.Add(this.grpYuzmeStili, 0, 1);

            this.grpAntrenmanBilgileri = new GroupControl() { Text = "3. Antrenman Detayları", Dock = DockStyle.Fill };
            this.tblAntrenmanBilgileriContent = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6, Padding = new Padding(15) };
            this.tblAntrenmanBilgileriContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); this.tblAntrenmanBilgileriContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            LabelControl CreateLbl(string t) { var l = new LabelControl() { Text = t, AutoSizeMode = LabelAutoSizeMode.None, Dock = DockStyle.Fill }; l.Appearance.TextOptions.VAlignment = VertAlignment.Center; return l; }
            this.lblToplamSure = CreateLbl("Süre (dk):"); this.nudToplamSure = new SpinEdit(); this.nudToplamSure.Properties.MaxValue = 300; this.nudToplamSure.Value = 60; this.nudToplamSure.Dock = DockStyle.Fill; this.nudToplamSure.Height = 40;
            this.lblHaftada = CreateLbl("Haftada:"); this.nudHaftada = new SpinEdit(); this.nudHaftada.Properties.MaxValue = 7; this.nudHaftada.Value = 3; this.nudHaftada.Dock = DockStyle.Fill; this.nudHaftada.Height = 40;
            this.lblToplamMesafe = CreateLbl("Mesafe (m):"); this.nudToplamMesafe = new SpinEdit(); this.nudToplamMesafe.Properties.MaxValue = 20000; this.nudToplamMesafe.Properties.Increment = 100; this.nudToplamMesafe.Value = 1500; this.nudToplamMesafe.Dock = DockStyle.Fill; this.nudToplamMesafe.Height = 40;
            this.lblAntrenmanGrupSec = CreateLbl("Grup:"); this.cmbAntrenmanGrup = new ComboBoxEdit() { Dock = DockStyle.Fill, Height = 40 };
            this.chkEkipman = new CheckEdit() { Text = "Ekipman Kullanılsın mı?", Dock = DockStyle.Fill };
            this.btnOlustur = new SimpleButton() { Text = "ANTRENMANI OLUŞTUR", Dock = DockStyle.Fill, Height = 60 };
            this.btnOlustur.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            this.tblAntrenmanBilgileriContent.Controls.Add(lblToplamSure, 0, 0); this.tblAntrenmanBilgileriContent.Controls.Add(nudToplamSure, 1, 0);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblHaftada, 0, 1); this.tblAntrenmanBilgileriContent.Controls.Add(nudHaftada, 1, 1);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblToplamMesafe, 0, 2); this.tblAntrenmanBilgileriContent.Controls.Add(nudToplamMesafe, 1, 2);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblAntrenmanGrupSec, 0, 3); this.tblAntrenmanBilgileriContent.Controls.Add(cmbAntrenmanGrup, 1, 3);
            this.tblAntrenmanBilgileriContent.Controls.Add(chkEkipman, 0, 4); this.tblAntrenmanBilgileriContent.SetColumnSpan(chkEkipman, 2);
            this.tblAntrenmanBilgileriContent.Controls.Add(btnOlustur, 0, 5); this.tblAntrenmanBilgileriContent.SetColumnSpan(btnOlustur, 2);
            for (int i = 0; i < 4; i++) this.tblAntrenmanBilgileriContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tblAntrenmanBilgileriContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); this.tblAntrenmanBilgileriContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileriContent);
            this.tblAntrenmanLeftLayout.Controls.Add(this.grpAntrenmanBilgileri, 0, 2);

            this.tblAntrenmanRightLayout = new TableLayoutPanel(); this.tblAntrenmanRightLayout.Dock = DockStyle.Fill; this.tblAntrenmanRightLayout.ColumnCount = 1; this.tblAntrenmanRightLayout.RowCount = 3;
            this.tblAntrenmanRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); this.tblAntrenmanRightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); this.tblAntrenmanRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.panelAntrenmanRight.Controls.Add(this.tblAntrenmanRightLayout);
            this.lblProgramBaslik = new LabelControl() { Text = "Antrenman Programı", Dock = DockStyle.Top, Height = 50 };
            this.lblProgramBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblProgramBaslik.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lblProgramBaslik.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            this.gcProgram = new GridControl() { Dock = DockStyle.Fill };
            this.gvProgram = new GridView(this.gcProgram); this.gcProgram.MainView = this.gvProgram; this.gvProgram.OptionsView.ShowGroupPanel = false;
            this.gvProgram.OptionsBehavior.Editable = true; this.gvProgram.OptionsView.ColumnAutoWidth = true;
            this.gvProgram.OptionsView.EnableAppearanceEvenRow = true;
            this.gvProgram.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            this.gvProgram.OptionsView.ShowHorizontalLines = DefaultBoolean.True;

            this.btnPdfIndir = new SimpleButton() { Text = "PDF Olarak Kaydet", Dock = DockStyle.Bottom, Height = 60 };
            this.btnPdfIndir.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            this.tblAntrenmanRightLayout.Controls.Add(this.lblProgramBaslik, 0, 0); this.tblAntrenmanRightLayout.Controls.Add(this.gcProgram, 0, 1); this.tblAntrenmanRightLayout.Controls.Add(this.btnPdfIndir, 0, 2);

            // ************************************************************************
            // FLYOUT PANEL (Artık "Tüm Öğrenciler" gridinin olduğu sağ panele bağlı)
            // ************************************************************************
            this.flyoutOgrenciDetay = new FlyoutPanel();
            this.flyoutOgrenciDetay.OwnerControl = this.panelOgrenciSag;
            this.flyoutOgrenciDetay.Location = new Point(0, 0);
            this.flyoutOgrenciDetay.Size = new Size(350, 600);
            this.flyoutOgrenciDetay.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Right;
            this.flyoutOgrenciDetay.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Slide;
            this.flyoutOgrenciDetay.Options.CloseOnOuterClick = true;

            this.pnlFlyoutIcerik = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(25) };
            this.flyoutOgrenciDetay.Controls.Add(this.pnlFlyoutIcerik);

            this.lblFlyoutBaslik = new LabelControl() { Text = "Öğrenci Detayı", Dock = DockStyle.Top, Height = 70, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblFlyoutBaslik.Appearance.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblFlyoutBaslik.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lblFlyoutBaslik.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            this.lblGrupDegistir = new LabelControl() { Text = "Grubu Değiştir:", Dock = DockStyle.Top, Height = 40 };
            this.lblGrupDegistir.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lblGrupDegistir.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
            this.lblGrupDegistir.Appearance.Font = new Font("Segoe UI", 11F);

            this.cmbFlyoutGrup = new ComboBoxEdit();
            this.cmbFlyoutGrup.Dock = DockStyle.Top;
            this.cmbFlyoutGrup.Height = 45;
            this.cmbFlyoutGrup.Properties.AutoHeight = false;
            this.cmbFlyoutGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFlyoutGrup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            SpinEdit CreateSpin(int max)
            {
                var s = new SpinEdit();
                s.Properties.MaxValue = max;
                s.Dock = DockStyle.Top;
                s.Properties.AutoHeight = false;
                s.Height = 45;
                s.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                return s;
            }

            LabelControl CreateLabel(string t)
            {
                var l = new LabelControl() { Text = t, Dock = DockStyle.Top, Height = 40 };
                l.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
                l.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                l.Appearance.Font = new Font("Segoe UI", 11F);
                return l;
            }

            this.lblYas = CreateLabel("Yaş:");
            this.nudYas = CreateSpin(100);
            this.lblBoy = CreateLabel("Boy (cm):");
            this.nudBoy = CreateSpin(250);
            this.lblKilo = CreateLabel("Kilo (kg):");
            this.nudKilo = CreateSpin(200);

            this.btnDetayKaydet = new SimpleButton() { Text = "KAYDET", Dock = DockStyle.Bottom, Height = 65 };
            this.btnDetayKaydet.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnDetayKapat = new SimpleButton() { Text = "Kapat", Dock = DockStyle.Top, Height = 45 };

            Control spacer1 = new Control { Height = 25, Dock = DockStyle.Top };
            Control spacer2 = new Control { Height = 25, Dock = DockStyle.Top };
            Control spacer3 = new Control { Height = 25, Dock = DockStyle.Top };
            Control spacer4 = new Control { Height = 25, Dock = DockStyle.Top };
            Control spacer5 = new Control { Height = 35, Dock = DockStyle.Top };

            this.pnlFlyoutIcerik.Controls.Add(this.btnDetayKaydet);
            this.pnlFlyoutIcerik.Controls.Add(spacer1);
            this.pnlFlyoutIcerik.Controls.Add(this.cmbFlyoutGrup);
            this.pnlFlyoutIcerik.Controls.Add(this.lblGrupDegistir);
            this.pnlFlyoutIcerik.Controls.Add(spacer2);
            this.pnlFlyoutIcerik.Controls.Add(this.nudKilo);
            this.pnlFlyoutIcerik.Controls.Add(this.lblKilo);
            this.pnlFlyoutIcerik.Controls.Add(spacer3);
            this.pnlFlyoutIcerik.Controls.Add(this.nudBoy);
            this.pnlFlyoutIcerik.Controls.Add(this.lblBoy);
            this.pnlFlyoutIcerik.Controls.Add(spacer4);
            this.pnlFlyoutIcerik.Controls.Add(this.nudYas);
            this.pnlFlyoutIcerik.Controls.Add(this.lblYas);
            this.pnlFlyoutIcerik.Controls.Add(spacer5);
            this.pnlFlyoutIcerik.Controls.Add(this.lblFlyoutBaslik);
            this.pnlFlyoutIcerik.Controls.Add(this.btnDetayKapat);

            this.Controls.Add(this.flyoutOgrenciDetay);
        }
    }
}
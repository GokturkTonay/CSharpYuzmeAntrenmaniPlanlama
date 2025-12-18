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
        private XtraTabControl tabControl1;
        private XtraTabPage tabPageProfil;
        private XtraTabPage tabPageAntrenmanOlustur;

        // --- PROFİL SAYFASI ---
        private PanelControl panelProfilLeft;
        private PanelControl panelProfilRight;
        private SplitterControl splitterProfil;

        // SOL TARAF
        private TableLayoutPanel tblProfilSolLayout;
        private PanelControl panelProfilInputs;
        private LabelControl lblOgrenciYonetimi;
        private TextEdit txtAd, txtSoyad, txtGrupInput;
        private ComboBoxEdit cmbGrup;
        private SimpleButton btnEkleOgrenci;

        private AccordionControl accordionOgrenciler;
        private AccordionControlElement aceMainGroup;

        private PanelControl panelProfilButtons;
        private SimpleButton btnSilOgrenci, btnSilGrup;

        // SAĞ TARAF
        private TableLayoutPanel tblProfilRightLayout;
        private LabelControl lblGecmisBaslik;
        private GridControl gcGecmis;
        private GridView gvGecmis;
        private GridColumn colTarih, colSure, colMesafe, colHiz;
        private PanelControl panelProfilRightButtons;
        private SimpleButton btnSeciliyiIndir, btnPDFOlarakIndirProfil, btnGecmisSil;

        // --- ANTRENMAN SAYFASI ---
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
        private LabelControl lblGrupDegistir; // YENİ
        public ComboBoxEdit cmbFlyoutGrup;    // YENİ
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
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1300, 760);
            this.Text = "Yüzme Antrenmanı Planlama Asistanı";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.tabControl1 = new XtraTabControl();
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabPageProfil = new XtraTabPage() { Text = "Profil ve Yönetim" };
            this.tabPageAntrenmanOlustur = new XtraTabPage() { Text = "Antrenman Oluştur" };
            this.tabControl1.TabPages.Add(this.tabPageProfil);
            this.tabControl1.TabPages.Add(this.tabPageAntrenmanOlustur);
            this.Controls.Add(this.tabControl1);

            // 1. PROFİL SAYFASI
            this.panelProfilLeft = new PanelControl() { Dock = DockStyle.Left, Width = 480, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };
            this.splitterProfil = new SplitterControl() { Dock = DockStyle.Left };
            this.panelProfilRight = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(10), BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };

            this.tabPageProfil.Controls.Add(this.panelProfilRight);
            this.tabPageProfil.Controls.Add(this.splitterProfil);
            this.tabPageProfil.Controls.Add(this.panelProfilLeft);

            // SOL TARAF
            this.tblProfilSolLayout = new TableLayoutPanel();
            this.tblProfilSolLayout.Dock = DockStyle.Fill;
            this.tblProfilSolLayout.ColumnCount = 1;
            this.tblProfilSolLayout.RowCount = 3;
            this.tblProfilSolLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tblProfilSolLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tblProfilSolLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            this.panelProfilLeft.Controls.Add(this.tblProfilSolLayout);

            this.panelProfilInputs = new PanelControl() { Dock = DockStyle.Fill, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder, Padding = new Padding(10), AutoSize = true };
            this.lblOgrenciYonetimi = new LabelControl() { Text = "Öğrenci Yönetimi", Dock = DockStyle.Top, Height = 40, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblOgrenciYonetimi.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            Font fontInput = new Font("Segoe UI", 11F);
            this.txtAd = new TextEdit() { Dock = DockStyle.Top, Height = 35 }; this.txtAd.Properties.NullText = "Ad Giriniz"; this.txtAd.Properties.Appearance.Font = fontInput;
            this.txtSoyad = new TextEdit() { Dock = DockStyle.Top }; this.txtSoyad.Properties.NullText = "Soyad Giriniz"; this.txtSoyad.Properties.Appearance.Font = fontInput;
            this.cmbGrup = new ComboBoxEdit() { Dock = DockStyle.Top }; this.cmbGrup.Properties.NullText = "Grup Seçiniz"; this.cmbGrup.Properties.Appearance.Font = fontInput;
            this.txtGrupInput = new TextEdit() { Dock = DockStyle.Top, Visible = false }; this.txtGrupInput.Properties.NullText = "Yeni Grup Adı"; this.txtGrupInput.Properties.Appearance.Font = fontInput;
            this.btnEkleOgrenci = new SimpleButton() { Text = "Ekle / Güncelle", Dock = DockStyle.Top, Height = 45 }; this.btnEkleOgrenci.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Control[] gaps = { new Control { Height = 10, Dock = DockStyle.Top }, new Control { Height = 10, Dock = DockStyle.Top }, new Control { Height = 10, Dock = DockStyle.Top }, new Control { Height = 10, Dock = DockStyle.Top } };
            this.panelProfilInputs.Controls.AddRange(new Control[] { btnEkleOgrenci, gaps[3], txtGrupInput, cmbGrup, gaps[2], txtSoyad, gaps[1], txtAd, gaps[0], lblOgrenciYonetimi });
            this.tblProfilSolLayout.Controls.Add(this.panelProfilInputs, 0, 0);

            this.accordionOgrenciler = new AccordionControl() { Dock = DockStyle.Fill };
            this.accordionOgrenciler.AllowItemSelection = true;
            this.aceMainGroup = new AccordionControlElement(ElementStyle.Group) { Text = "Öğrenci Listesi", Expanded = true };
            this.accordionOgrenciler.Elements.Add(this.aceMainGroup);
            this.tblProfilSolLayout.Controls.Add(this.accordionOgrenciler, 0, 1);

            this.panelProfilButtons = new PanelControl() { Dock = DockStyle.Fill, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder, Padding = new Padding(10) };
            this.btnSilOgrenci = new SimpleButton() { Text = "Seçiliyi Sil", Dock = DockStyle.Top, Height = 35 };
            this.btnSilGrup = new SimpleButton() { Text = "Grubu Sil", Dock = DockStyle.Bottom, Height = 35 }; this.btnSilGrup.Appearance.BackColor = Color.IndianRed;
            this.panelProfilButtons.Controls.AddRange(new Control[] { btnSilGrup, new Control { Height = 10, Dock = DockStyle.Top }, btnSilOgrenci });
            this.tblProfilSolLayout.Controls.Add(this.panelProfilButtons, 0, 2);

            // SAĞ TARAF
            this.tblProfilRightLayout = new TableLayoutPanel();
            this.tblProfilRightLayout.Dock = DockStyle.Fill;
            this.tblProfilRightLayout.ColumnCount = 1;
            this.tblProfilRightLayout.RowCount = 3;
            this.tblProfilRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tblProfilRightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tblProfilRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.panelProfilRight.Controls.Add(this.tblProfilRightLayout);

            this.lblGecmisBaslik = new LabelControl() { Text = "Geçmiş Antrenmanlar", Dock = DockStyle.Top, Height = 40, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblGecmisBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.gcGecmis = new GridControl() { Dock = DockStyle.Fill };
            this.gvGecmis = new GridView(this.gcGecmis); this.gcGecmis.MainView = this.gvGecmis; this.gvGecmis.OptionsView.ShowGroupPanel = false; this.gvGecmis.OptionsBehavior.Editable = false; this.gvGecmis.OptionsView.ColumnAutoWidth = true;
            this.colTarih = new GridColumn() { Caption = "Tarih", FieldName = "TarihFormatted", Visible = true, VisibleIndex = 0 };
            this.colSure = new GridColumn() { Caption = "Süre (dk)", FieldName = "Sure", Visible = true, VisibleIndex = 1 };
            this.colMesafe = new GridColumn() { Caption = "Mesafe (m)", FieldName = "Mesafe", Visible = true, VisibleIndex = 2 };
            this.colHiz = new GridColumn() { Caption = "Ort. Hız", FieldName = "OrtHiz", Visible = true, VisibleIndex = 3 };
            this.gvGecmis.Columns.AddRange(new GridColumn[] { colTarih, colSure, colMesafe, colHiz });

            this.panelProfilRightButtons = new PanelControl() { Dock = DockStyle.Bottom, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder, AutoSize = true };
            this.btnSeciliyiIndir = new SimpleButton() { Text = "Seçiliyi İndir", Dock = DockStyle.Top, Height = 40 };
            this.btnPDFOlarakIndirProfil = new SimpleButton() { Text = "Listeyi PDF İndir", Dock = DockStyle.Top, Height = 40 };
            this.btnGecmisSil = new SimpleButton() { Text = "KAYDI SİL", Dock = DockStyle.Top, Height = 40 }; this.btnGecmisSil.Appearance.BackColor = Color.IndianRed;
            this.panelProfilRightButtons.Controls.AddRange(new Control[] { btnPDFOlarakIndirProfil, btnSeciliyiIndir, btnGecmisSil });

            this.tblProfilRightLayout.Controls.Add(this.lblGecmisBaslik, 0, 0);
            this.tblProfilRightLayout.Controls.Add(this.gcGecmis, 0, 1);
            this.tblProfilRightLayout.Controls.Add(this.panelProfilRightButtons, 0, 2);

            // 2. ANTRENMAN SAYFASI
            this.panelAntrenmanLeft = new PanelControl() { Dock = DockStyle.Left, Width = 600, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };
            this.splitterAntrenman = new SplitterControl() { Dock = DockStyle.Left };
            this.panelAntrenmanRight = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(10), BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };

            this.tabPageAntrenmanOlustur.Controls.Add(this.panelAntrenmanRight);
            this.tabPageAntrenmanOlustur.Controls.Add(this.splitterAntrenman);
            this.tabPageAntrenmanOlustur.Controls.Add(this.panelAntrenmanLeft);

            this.tblAntrenmanLeftLayout = new TableLayoutPanel();
            this.tblAntrenmanLeftLayout.Dock = DockStyle.Fill;
            this.tblAntrenmanLeftLayout.ColumnCount = 1; this.tblAntrenmanLeftLayout.RowCount = 3;
            this.tblAntrenmanLeftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            this.tblAntrenmanLeftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            this.tblAntrenmanLeftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.panelAntrenmanLeft.Controls.Add(this.tblAntrenmanLeftLayout);

            this.grpBiyomotorik = new GroupControl() { Text = "1. Biyomotorik Yetenek", Dock = DockStyle.Fill };
            this.tblBiyomotorikContent = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 5, RowCount = 1 };
            for (int i = 0; i < 5; i++) this.tblBiyomotorikContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            CheckButton CreateBtn(string t, int g) { var b = new CheckButton() { Text = t, Dock = DockStyle.Fill, Margin = new Padding(2), GroupIndex = g }; b.Appearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold); return b; }
            this.btnDayaniklilik = CreateBtn("Dayanıklılık", 1); this.btnSurat = CreateBtn("Sürat", 1); this.btnKuvvet = CreateBtn("Kuvvet", 1); this.btnEsneklik = CreateBtn("Esneklik", 1); this.btnKoordinasyon = CreateBtn("Koord.", 1);
            this.tblBiyomotorikContent.Controls.AddRange(new Control[] { btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon });
            this.grpBiyomotorik.Controls.Add(this.tblBiyomotorikContent);
            this.tblAntrenmanLeftLayout.Controls.Add(this.grpBiyomotorik, 0, 0);

            this.grpYuzmeStili = new GroupControl() { Text = "2. Yüzme Stili", Dock = DockStyle.Fill };
            this.tblYuzmeStiliContent = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1 };
            for (int i = 0; i < 4; i++) this.tblYuzmeStiliContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.btnSerbest = CreateBtn("Serbest", 0); this.btnSirtustu = CreateBtn("Sırtüstü", 0); this.btnKurbagalama = CreateBtn("Kurbağa", 0); this.btnKelebek = CreateBtn("Kelebek", 0);
            this.tblYuzmeStiliContent.Controls.AddRange(new Control[] { btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek });
            this.grpYuzmeStili.Controls.Add(this.tblYuzmeStiliContent);
            this.tblAntrenmanLeftLayout.Controls.Add(this.grpYuzmeStili, 0, 1);

            this.grpAntrenmanBilgileri = new GroupControl() { Text = "3. Antrenman Detayları", Dock = DockStyle.Fill };
            this.tblAntrenmanBilgileriContent = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6, Padding = new Padding(10) };
            this.tblAntrenmanBilgileriContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); this.tblAntrenmanBilgileriContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            LabelControl CreateLbl(string t) { var l = new LabelControl() { Text = t, AutoSizeMode = LabelAutoSizeMode.None, Dock = DockStyle.Fill }; l.Appearance.TextOptions.VAlignment = VertAlignment.Center; l.Appearance.Font = new Font("Segoe UI", 10F); return l; }
            this.lblToplamSure = CreateLbl("Süre (dk):"); this.nudToplamSure = new SpinEdit(); this.nudToplamSure.Properties.MaxValue = 300; this.nudToplamSure.Value = 60; this.nudToplamSure.Dock = DockStyle.Fill;
            this.lblHaftada = CreateLbl("Haftada:"); this.nudHaftada = new SpinEdit(); this.nudHaftada.Properties.MaxValue = 7; this.nudHaftada.Value = 3; this.nudHaftada.Dock = DockStyle.Fill;
            this.lblToplamMesafe = CreateLbl("Mesafe (m):"); this.nudToplamMesafe = new SpinEdit(); this.nudToplamMesafe.Properties.MaxValue = 20000; this.nudToplamMesafe.Properties.Increment = 100; this.nudToplamMesafe.Value = 1500; this.nudToplamMesafe.Dock = DockStyle.Fill;
            this.lblAntrenmanGrupSec = CreateLbl("Grup:"); this.cmbAntrenmanGrup = new ComboBoxEdit() { Dock = DockStyle.Fill };
            this.chkEkipman = new CheckEdit() { Text = "Ekipman Kullanılsın mı?", Dock = DockStyle.Fill };
            this.btnOlustur = new SimpleButton() { Text = "ANTRENMANI OLUŞTUR", Dock = DockStyle.Fill, Height = 50 }; this.btnOlustur.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblToplamSure, 0, 0); this.tblAntrenmanBilgileriContent.Controls.Add(nudToplamSure, 1, 0);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblHaftada, 0, 1); this.tblAntrenmanBilgileriContent.Controls.Add(nudHaftada, 1, 1);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblToplamMesafe, 0, 2); this.tblAntrenmanBilgileriContent.Controls.Add(nudToplamMesafe, 1, 2);
            this.tblAntrenmanBilgileriContent.Controls.Add(lblAntrenmanGrupSec, 0, 3); this.tblAntrenmanBilgileriContent.Controls.Add(cmbAntrenmanGrup, 1, 3);
            this.tblAntrenmanBilgileriContent.Controls.Add(chkEkipman, 0, 4); this.tblAntrenmanBilgileriContent.SetColumnSpan(chkEkipman, 2);
            this.tblAntrenmanBilgileriContent.Controls.Add(btnOlustur, 0, 5); this.tblAntrenmanBilgileriContent.SetColumnSpan(btnOlustur, 2);
            for (int i = 0; i < 4; i++) this.tblAntrenmanBilgileriContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tblAntrenmanBilgileriContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); this.tblAntrenmanBilgileriContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileriContent);
            this.tblAntrenmanLeftLayout.Controls.Add(this.grpAntrenmanBilgileri, 0, 2);

            this.tblAntrenmanRightLayout = new TableLayoutPanel(); this.tblAntrenmanRightLayout.Dock = DockStyle.Fill; this.tblAntrenmanRightLayout.ColumnCount = 1; this.tblAntrenmanRightLayout.RowCount = 3;
            this.tblAntrenmanRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); this.tblAntrenmanRightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); this.tblAntrenmanRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.panelAntrenmanRight.Controls.Add(this.tblAntrenmanRightLayout);
            this.lblProgramBaslik = new LabelControl() { Text = "Antrenman Programı", Dock = DockStyle.Top, Height = 40 }; this.lblProgramBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold); this.lblProgramBaslik.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.gcProgram = new GridControl() { Dock = DockStyle.Fill };
            this.gvProgram = new GridView(this.gcProgram); this.gcProgram.MainView = this.gvProgram; this.gvProgram.OptionsView.ShowGroupPanel = false; this.gvProgram.OptionsBehavior.Editable = true; this.gvProgram.OptionsView.ColumnAutoWidth = true;
            this.btnPdfIndir = new SimpleButton() { Text = "PDF Olarak Kaydet", Dock = DockStyle.Bottom, Height = 50 };
            this.tblAntrenmanRightLayout.Controls.Add(this.lblProgramBaslik, 0, 0); this.tblAntrenmanRightLayout.Controls.Add(this.gcProgram, 0, 1); this.tblAntrenmanRightLayout.Controls.Add(this.btnPdfIndir, 0, 2);

            // ************************************************************************
            // FLYOUT PANEL (SAĞ PANELİN SOLUNA YASLI)
            // ************************************************************************
            this.flyoutOgrenciDetay = new FlyoutPanel();

            // --- SAHİP KONTROL: SAĞ PANEL (Gridin olduğu yer) ---
            this.flyoutOgrenciDetay.OwnerControl = this.panelProfilRight;

            this.flyoutOgrenciDetay.Location = new Point(0, 0);
            this.flyoutOgrenciDetay.Size = new Size(350, 600);

            // --- AÇILMA YÖNÜ: SAĞ PANELİN SOLUNDAN ---
            this.flyoutOgrenciDetay.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Left;
            this.flyoutOgrenciDetay.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Slide;
            this.flyoutOgrenciDetay.Options.CloseOnOuterClick = true;

            // Arkaplan OPAK
            this.flyoutOgrenciDetay.Appearance.BackColor = Color.FromArgb(45, 45, 48);
            this.flyoutOgrenciDetay.Appearance.Options.UseBackColor = true;

            this.pnlFlyoutIcerik = new PanelControl() { Dock = DockStyle.Fill, Padding = new Padding(20), BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };
            this.pnlFlyoutIcerik.Appearance.BackColor = Color.FromArgb(45, 45, 48);
            this.pnlFlyoutIcerik.Appearance.Options.UseBackColor = true;

            this.flyoutOgrenciDetay.Controls.Add(this.pnlFlyoutIcerik);

            this.lblFlyoutBaslik = new LabelControl() { Text = "Öğrenci Detayı", Dock = DockStyle.Top, Height = 60, AutoSizeMode = LabelAutoSizeMode.None };
            this.lblFlyoutBaslik.Appearance.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblFlyoutBaslik.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lblFlyoutBaslik.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            // --- YENİ EKLENEN: GRUP DEĞİŞTİRME ---
            this.lblGrupDegistir = new LabelControl() { Text = "Grubu Değiştir:", Dock = DockStyle.Top, Height = 30 };
            this.lblGrupDegistir.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblGrupDegistir.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lblGrupDegistir.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;

            this.cmbFlyoutGrup = new ComboBoxEdit();
            this.cmbFlyoutGrup.Dock = DockStyle.Top;
            this.cmbFlyoutGrup.Height = 40;
            this.cmbFlyoutGrup.Properties.AutoHeight = false;
            this.cmbFlyoutGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFlyoutGrup.Properties.Appearance.Font = new Font("Segoe UI", 14F);
            this.cmbFlyoutGrup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            SpinEdit CreateSpin(int max)
            {
                var s = new SpinEdit();
                s.Properties.MaxValue = max;
                s.Dock = DockStyle.Top;
                s.Properties.AutoHeight = false;
                s.Height = 40;
                s.Properties.Appearance.Font = new Font("Segoe UI", 14F);
                s.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                return s;
            }

            LabelControl CreateLabel(string t)
            {
                var l = new LabelControl() { Text = t, Dock = DockStyle.Top, Height = 30 };
                l.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
                l.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                l.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                return l;
            }

            this.lblYas = CreateLabel("Yaş:");
            this.nudYas = CreateSpin(100);

            this.lblBoy = CreateLabel("Boy (cm):");
            this.nudBoy = CreateSpin(250);

            this.lblKilo = CreateLabel("Kilo (kg):");
            this.nudKilo = CreateSpin(200);

            this.btnDetayKaydet = new SimpleButton() { Text = "KAYDET", Dock = DockStyle.Bottom, Height = 55 };
            this.btnDetayKaydet.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            this.btnDetayKapat = new SimpleButton() { Text = "Kapat", Dock = DockStyle.Top, Height = 35 };

            Control spacer1 = new Control { Height = 20, Dock = DockStyle.Top };
            Control spacer2 = new Control { Height = 20, Dock = DockStyle.Top };
            Control spacer3 = new Control { Height = 20, Dock = DockStyle.Top };
            Control spacer4 = new Control { Height = 20, Dock = DockStyle.Top };
            Control spacer5 = new Control { Height = 30, Dock = DockStyle.Top };

            this.pnlFlyoutIcerik.Controls.Add(this.btnDetayKaydet); // Bottom

            // --- TERS SIRA ---
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
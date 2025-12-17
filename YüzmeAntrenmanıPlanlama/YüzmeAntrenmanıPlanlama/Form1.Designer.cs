using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns; // Sütunlar için gerekli kütüphane
using DevExpress.XtraBars.Navigation;
using DevExpress.Utils;

namespace YüzmeAntrenmanıPlanlama
{
    partial class Form1
    {
        private IContainer components = null;

        // --- Ana Yapı ---
        private XtraTabControl tabControl1;
        private XtraTabPage tabPageProfil;
        private XtraTabPage tabPageAntrenmanOlustur;

        // --- Antrenman Oluşturma Sayfası ---
        private PanelControl leftContainer;
        private PanelControl panelSag;
        private SplitterControl splitterAntrenman;

        private GroupControl grpBiyomotorik;
        private TableLayoutPanel tblBiyomotorik;
        private CheckButton btnDayaniklilik;
        private CheckButton btnSurat;
        private CheckButton btnKuvvet;
        private CheckButton btnEsneklik;
        private CheckButton btnKoordinasyon;

        private GroupControl grpYuzmeStili;
        private TableLayoutPanel tblYuzmeStili;
        private CheckButton btnSerbest;
        private CheckButton btnSirtustu;
        private CheckButton btnKurbagalama;
        private CheckButton btnKelebek;

        private GroupControl grpAntrenmanBilgileri;
        private TableLayoutPanel tblAntrenmanBilgileri;
        private LabelControl lblToplamSure;
        private SpinEdit nudToplamSure;
        private LabelControl lblHaftada;
        private SpinEdit nudHaftada;
        private LabelControl lblToplamMesafe;
        private SpinEdit nudToplamMesafe;
        private LabelControl lblAntrenmanGrupSec;
        private ComboBoxEdit cmbAntrenmanGrup;
        private CheckEdit chkEkipman;
        private SimpleButton btnOlustur;

        private LabelControl lblProgramBaslik;
        private GridControl gcProgram;
        private GridView gvProgram;
        private SimpleButton btnPdfIndir;

        // --- Profil Sayfası ---
        private PanelControl panelProfilLeft;
        private PanelControl panelProfilRight;
        private SplitterControl splitterProfil;

        private PanelControl panelProfilInputs;
        private LabelControl lblOgrenciYonetimi;
        private TextEdit txtAd;
        private TextEdit txtSoyad;
        private ComboBoxEdit cmbGrup;
        private TextEdit txtGrupInput;
        private SimpleButton btnEkleOgrenci;

        private AccordionControl accordionOgrenciler;
        private AccordionControlElement aceMainGroup;

        private PanelControl panelProfilButtons;
        private SimpleButton btnSilOgrenci;
        private SimpleButton btnSilGrup;

        // Profil Sağ
        private LabelControl lblGecmisBaslik;
        private GridControl gcGecmis;
        private GridView gvGecmis;

        // --- BURASI DÜZELTİLDİ: Sütun Tanımlamaları ---
        private GridColumn colTarih;
        private GridColumn colSure;
        private GridColumn colMesafe;
        private GridColumn colHiz;

        private SimpleButton btnSeciliyiIndir;
        private SimpleButton btnPDFOlarakIndirProfil;
        private SimpleButton btnGecmisSil;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();

            // FORM
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 650);
            this.Text = "Yüzme Antrenmanı Planlama Asistanı";
            this.StartPosition = FormStartPosition.CenterScreen;

            // TABS
            this.tabControl1 = new XtraTabControl();
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabPageProfil = new XtraTabPage() { Text = "Profil ve Yönetim" };
            this.tabPageAntrenmanOlustur = new XtraTabPage() { Text = "Antrenman Oluştur" };
            this.tabControl1.TabPages.Add(this.tabPageProfil);
            this.tabControl1.TabPages.Add(this.tabPageAntrenmanOlustur);
            this.Controls.Add(this.tabControl1);

            // ========================================================================
            // SAYFA 2: ANTRENMAN OLUŞTURMA (AYNI KALDI)
            // ========================================================================

            this.leftContainer = new PanelControl();
            this.leftContainer.Dock = DockStyle.Left;
            this.leftContainer.Width = 580;
            this.leftContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            this.splitterAntrenman = new SplitterControl();
            this.splitterAntrenman.Dock = DockStyle.Left;

            this.panelSag = new PanelControl();
            this.panelSag.Dock = DockStyle.Fill;
            this.panelSag.Padding = new Padding(10);
            this.panelSag.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            this.tabPageAntrenmanOlustur.Controls.Add(this.panelSag);
            this.tabPageAntrenmanOlustur.Controls.Add(this.splitterAntrenman);
            this.tabPageAntrenmanOlustur.Controls.Add(this.leftContainer);

            // Sol Panel İçerik
            this.grpBiyomotorik = new GroupControl();
            this.grpBiyomotorik.Text = "1. Biyomotorik Yetenek";
            this.grpBiyomotorik.Dock = DockStyle.Top;
            this.grpBiyomotorik.Height = 130;

            this.tblBiyomotorik = new TableLayoutPanel();
            this.tblBiyomotorik.Dock = DockStyle.Fill;
            this.tblBiyomotorik.ColumnCount = 5;
            this.tblBiyomotorik.RowCount = 1;
            for (int i = 0; i < 5; i++) this.tblBiyomotorik.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            CheckButton CreateBtn(string t, int groupIndex)
            {
                var b = new CheckButton() { Text = t, Dock = DockStyle.Fill, Margin = new Padding(3), GroupIndex = groupIndex };
                b.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                return b;
            }

            this.btnDayaniklilik = CreateBtn("Dayanıklılık", 1); this.btnDayaniklilik.Checked = true;
            this.btnSurat = CreateBtn("Sürat", 1);
            this.btnKuvvet = CreateBtn("Kuvvet", 1);
            this.btnEsneklik = CreateBtn("Esneklik", 1);
            this.btnKoordinasyon = CreateBtn("Koordinasyon", 1);
            this.tblBiyomotorik.Controls.AddRange(new Control[] { btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon });
            this.grpBiyomotorik.Controls.Add(this.tblBiyomotorik);

            this.grpYuzmeStili = new GroupControl();
            this.grpYuzmeStili.Text = "2. Yüzme Stili";
            this.grpYuzmeStili.Dock = DockStyle.Top;
            this.grpYuzmeStili.Height = 130;
            this.tblYuzmeStili = new TableLayoutPanel();
            this.tblYuzmeStili.Dock = DockStyle.Fill;
            this.tblYuzmeStili.ColumnCount = 4;
            this.tblYuzmeStili.RowCount = 1;
            for (int i = 0; i < 4; i++) this.tblYuzmeStili.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.btnSerbest = CreateBtn("Serbest", 0);
            this.btnSirtustu = CreateBtn("Sırtüstü", 0);
            this.btnKurbagalama = CreateBtn("Kurbağa", 0);
            this.btnKelebek = CreateBtn("Kelebek", 0);
            this.tblYuzmeStili.Controls.AddRange(new Control[] { btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek });
            this.grpYuzmeStili.Controls.Add(this.tblYuzmeStili);

            this.grpAntrenmanBilgileri = new GroupControl();
            this.grpAntrenmanBilgileri.Text = "3. Antrenman Detayları";
            this.grpAntrenmanBilgileri.Dock = DockStyle.Fill;
            this.tblAntrenmanBilgileri = new TableLayoutPanel();
            this.tblAntrenmanBilgileri.Dock = DockStyle.Fill;
            this.tblAntrenmanBilgileri.ColumnCount = 2;
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            this.tblAntrenmanBilgileri.Padding = new Padding(15);
            this.tblAntrenmanBilgileri.RowCount = 6;
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));

            LabelControl CreateLbl(string t)
            {
                var lbl = new LabelControl() { Text = t, AutoSizeMode = LabelAutoSizeMode.None, Dock = DockStyle.Fill };
                lbl.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                lbl.Appearance.Font = new Font("Segoe UI", 10F);
                return lbl;
            }

            this.lblToplamSure = CreateLbl("Süre (dk):");
            this.nudToplamSure = new SpinEdit(); this.nudToplamSure.Properties.MaxValue = 300; this.nudToplamSure.Value = 60; this.nudToplamSure.Dock = DockStyle.Fill;
            this.lblHaftada = CreateLbl("Haftada:");
            this.nudHaftada = new SpinEdit(); this.nudHaftada.Properties.MaxValue = 7; this.nudHaftada.Value = 3; this.nudHaftada.Dock = DockStyle.Fill;
            this.lblToplamMesafe = CreateLbl("Mesafe (m):");
            this.nudToplamMesafe = new SpinEdit(); this.nudToplamMesafe.Properties.MaxValue = 20000; this.nudToplamMesafe.Properties.Increment = 100; this.nudToplamMesafe.Value = 1500; this.nudToplamMesafe.Dock = DockStyle.Fill;
            this.lblAntrenmanGrupSec = CreateLbl("Grup:");
            this.cmbAntrenmanGrup = new ComboBoxEdit(); this.cmbAntrenmanGrup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor; this.cmbAntrenmanGrup.Dock = DockStyle.Fill;
            this.chkEkipman = new CheckEdit() { Text = "Ekipman Kullanılsın mı?" }; this.chkEkipman.Dock = DockStyle.Fill;
            this.btnOlustur = new SimpleButton() { Text = "ANTRENMANI OLUŞTUR", Dock = DockStyle.Fill };
            this.btnOlustur.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            this.tblAntrenmanBilgileri.Controls.Add(lblToplamSure, 0, 0); this.tblAntrenmanBilgileri.Controls.Add(nudToplamSure, 1, 0);
            this.tblAntrenmanBilgileri.Controls.Add(lblHaftada, 0, 1); this.tblAntrenmanBilgileri.Controls.Add(nudHaftada, 1, 1);
            this.tblAntrenmanBilgileri.Controls.Add(lblToplamMesafe, 0, 2); this.tblAntrenmanBilgileri.Controls.Add(nudToplamMesafe, 1, 2);
            this.tblAntrenmanBilgileri.Controls.Add(lblAntrenmanGrupSec, 0, 3); this.tblAntrenmanBilgileri.Controls.Add(cmbAntrenmanGrup, 1, 3);
            this.tblAntrenmanBilgileri.Controls.Add(chkEkipman, 0, 4); this.tblAntrenmanBilgileri.SetColumnSpan(chkEkipman, 2);
            this.tblAntrenmanBilgileri.Controls.Add(btnOlustur, 0, 5); this.tblAntrenmanBilgileri.SetColumnSpan(btnOlustur, 2);

            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileri);
            this.leftContainer.Controls.Add(this.grpAntrenmanBilgileri);
            this.leftContainer.Controls.Add(this.grpYuzmeStili);
            this.leftContainer.Controls.Add(this.grpBiyomotorik);

            this.lblProgramBaslik = new LabelControl() { Text = "Antrenman Programı", Dock = DockStyle.Top, Height = 40 };
            this.lblProgramBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblProgramBaslik.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            this.gcProgram = new GridControl();
            this.gcProgram.Dock = DockStyle.Fill;
            this.gvProgram = new GridView(this.gcProgram);
            this.gcProgram.MainView = this.gvProgram;
            this.gvProgram.OptionsView.ShowGroupPanel = false;
            this.gvProgram.OptionsBehavior.Editable = true;
            this.gvProgram.OptionsView.ColumnAutoWidth = true;

            this.btnPdfIndir = new SimpleButton() { Text = "PDF Olarak Kaydet", Dock = DockStyle.Bottom, Height = 50 };

            this.panelSag.Controls.Add(this.gcProgram);
            this.panelSag.Controls.Add(this.btnPdfIndir);
            this.panelSag.Controls.Add(this.lblProgramBaslik);


            // ========================================================================
            // SAYFA 1: PROFIL (GRID FIX VE YERLEŞİM)
            // ========================================================================

            // 1. SOL PANEL
            this.panelProfilLeft = new PanelControl();
            this.panelProfilLeft.Dock = DockStyle.Left;
            this.panelProfilLeft.Width = 480;
            this.panelProfilLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            // 2. AYIRICI
            this.splitterProfil = new SplitterControl();
            this.splitterProfil.Dock = DockStyle.Left;

            // 3. SAĞ PANEL
            this.panelProfilRight = new PanelControl();
            this.panelProfilRight.Dock = DockStyle.Fill;
            this.panelProfilRight.Padding = new Padding(10);
            this.panelProfilRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            // EKLEME SIRASI
            this.tabPageProfil.Controls.Add(this.panelProfilRight); // En arkada
            this.tabPageProfil.Controls.Add(this.splitterProfil);   // Ortada
            this.tabPageProfil.Controls.Add(this.panelProfilLeft);  // En önde

            // --- PROFİL SOL İÇERİK ---
            this.panelProfilInputs = new PanelControl();
            this.panelProfilInputs.Dock = DockStyle.Top;
            this.panelProfilInputs.Height = 350;
            this.panelProfilInputs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProfilInputs.Padding = new Padding(10);

            this.lblOgrenciYonetimi = new LabelControl() { Text = "Öğrenci Yönetimi", Dock = DockStyle.Top };
            this.lblOgrenciYonetimi.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblOgrenciYonetimi.AutoSizeMode = LabelAutoSizeMode.None;
            this.lblOgrenciYonetimi.Height = 50;

            Font inputFont = new Font("Segoe UI", 11F);
            this.txtAd = new TextEdit() { Dock = DockStyle.Top };
            this.txtAd.Properties.NullText = "Ad Giriniz";
            this.txtAd.Properties.Appearance.Font = inputFont;
            this.txtAd.Height = 40;

            this.txtSoyad = new TextEdit() { Dock = DockStyle.Top };
            this.txtSoyad.Properties.NullText = "Soyad Giriniz";
            this.txtSoyad.Properties.Appearance.Font = inputFont;

            this.cmbGrup = new ComboBoxEdit() { Dock = DockStyle.Top };
            this.cmbGrup.Properties.NullText = "Grup Seçiniz";
            this.cmbGrup.Properties.Appearance.Font = inputFont;

            this.txtGrupInput = new TextEdit() { Dock = DockStyle.Top, Visible = false };
            this.txtGrupInput.Properties.NullText = "Yeni Grup Adı Girin";
            this.txtGrupInput.Properties.Appearance.Font = inputFont;

            this.btnEkleOgrenci = new SimpleButton() { Text = "Ekle / Güncelle", Dock = DockStyle.Top, Height = 50 };
            this.btnEkleOgrenci.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Control gap1 = new Control { Height = 15, Dock = DockStyle.Top };
            Control gap2 = new Control { Height = 15, Dock = DockStyle.Top };
            Control gap3 = new Control { Height = 15, Dock = DockStyle.Top };
            Control gap4 = new Control { Height = 15, Dock = DockStyle.Top };

            this.panelProfilInputs.Controls.Add(this.btnEkleOgrenci);
            this.panelProfilInputs.Controls.Add(gap4);
            this.panelProfilInputs.Controls.Add(this.txtGrupInput);
            this.panelProfilInputs.Controls.Add(this.cmbGrup);
            this.panelProfilInputs.Controls.Add(gap3);
            this.panelProfilInputs.Controls.Add(this.txtSoyad);
            this.panelProfilInputs.Controls.Add(gap2);
            this.panelProfilInputs.Controls.Add(this.txtAd);
            this.panelProfilInputs.Controls.Add(gap1);
            this.panelProfilInputs.Controls.Add(this.lblOgrenciYonetimi);

            this.panelProfilButtons = new PanelControl();
            this.panelProfilButtons.Dock = DockStyle.Bottom;
            this.panelProfilButtons.Height = 120;
            this.panelProfilButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProfilButtons.Padding = new Padding(10);

            this.btnSilOgrenci = new SimpleButton() { Text = "Seçili Öğrenciyi Sil", Dock = DockStyle.Top, Height = 40 };
            this.btnSilGrup = new SimpleButton() { Text = "Grubu Sil", Dock = DockStyle.Bottom, Height = 40 };
            this.btnSilGrup.Appearance.BackColor = Color.IndianRed;

            Control gapBtn = new Control { Height = 10, Dock = DockStyle.Top };

            this.panelProfilButtons.Controls.Add(this.btnSilGrup);
            this.panelProfilButtons.Controls.Add(gapBtn);
            this.panelProfilButtons.Controls.Add(this.btnSilOgrenci);

            this.accordionOgrenciler = new AccordionControl();
            this.accordionOgrenciler.Dock = DockStyle.Fill;
            this.aceMainGroup = new AccordionControlElement(ElementStyle.Group);
            this.aceMainGroup.Text = "Öğrenci Listesi";
            this.aceMainGroup.Expanded = true;
            this.accordionOgrenciler.Elements.Add(this.aceMainGroup);

            this.panelProfilLeft.Controls.Add(this.accordionOgrenciler); // Fill
            this.panelProfilLeft.Controls.Add(this.panelProfilInputs);   // Top
            this.panelProfilLeft.Controls.Add(this.panelProfilButtons);  // Bottom

            this.panelProfilInputs.BringToFront();
            this.panelProfilButtons.BringToFront();

            // --- PROFİL SAĞ İÇERİK (GRID BAŞLIKLARI EKLENDİ) ---
            this.lblGecmisBaslik = new LabelControl() { Text = "Geçmiş Antrenmanlar", Dock = DockStyle.Top, Height = 40 };
            this.lblGecmisBaslik.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblGecmisBaslik.AutoSizeMode = LabelAutoSizeMode.None;

            this.gcGecmis = new GridControl();
            this.gcGecmis.Dock = DockStyle.Fill;
            this.gvGecmis = new GridView(this.gcGecmis);
            this.gcGecmis.MainView = this.gvGecmis;
            this.gvGecmis.OptionsView.ShowGroupPanel = false;
            this.gvGecmis.OptionsBehavior.Editable = false;
            this.gvGecmis.OptionsView.ColumnAutoWidth = true;

            // --- BURASI EKLENDİ: GRID SÜTUNLARI ELLE OLUŞTURULDU ---
            this.colTarih = new GridColumn() { Caption = "Tarih", FieldName = "TarihFormatted", Visible = true, VisibleIndex = 0 };
            this.colSure = new GridColumn() { Caption = "Süre (dk)", FieldName = "Sure", Visible = true, VisibleIndex = 1 };
            this.colMesafe = new GridColumn() { Caption = "Mesafe (m)", FieldName = "Mesafe", Visible = true, VisibleIndex = 2 };
            this.colHiz = new GridColumn() { Caption = "Ort. Hız", FieldName = "OrtHiz", Visible = true, VisibleIndex = 3 };

            this.gvGecmis.Columns.AddRange(new GridColumn[] { colTarih, colSure, colMesafe, colHiz });

            this.btnSeciliyiIndir = new SimpleButton() { Text = "Seçiliyi İndir", Dock = DockStyle.Bottom, Height = 45 };
            this.btnPDFOlarakIndirProfil = new SimpleButton() { Text = "Listeyi PDF İndir", Dock = DockStyle.Bottom, Height = 45 };
            this.btnGecmisSil = new SimpleButton() { Text = "KAYDI SİL", Dock = DockStyle.Bottom, Height = 45 };
            this.btnGecmisSil.Appearance.BackColor = Color.IndianRed;

            this.panelProfilRight.Controls.Add(this.gcGecmis);
            this.panelProfilRight.Controls.Add(this.btnGecmisSil);
            this.panelProfilRight.Controls.Add(this.btnSeciliyiIndir);
            this.panelProfilRight.Controls.Add(this.btnPDFOlarakIndirProfil);
            this.panelProfilRight.Controls.Add(this.lblGecmisBaslik);
        }
    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YüzmeAntrenmanıPlanlama
{
    partial class Form1
    {
        private IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem profilToolStripMenuItem;
        private ToolStripMenuItem antrenmanOlusturToolStripMenuItem;
        private ToolStripMenuItem gecmisAntrenmanlarToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPageAntrenmanOlustur;
        private TabPage tabPageProfil;
        private TabPage tabPageGecmisAntrenmanlar;

        private Panel leftContainer;
        private GroupBox grpBiyomotorik;
        private TableLayoutPanel tblBiyomotorik;
        private Button btnDayaniklilik;
        private Button btnSurat;
        private Button btnKuvvet;
        private Button btnEsneklik;
        private Button btnKoordinasyon;

        private GroupBox grpYuzmeStili;
        private TableLayoutPanel tblYuzmeStili;
        private Button btnSerbest;
        private Button btnSirtustu;
        private Button btnKurbagalama;
        private Button btnKelebek;

        private GroupBox grpAntrenmanBilgileri;
        private TableLayoutPanel tblAntrenmanBilgileri;
        private Label lblToplamSure;
        private Label lblHaftada;
        private Label lblToplamMesafe;
        private NumericUpDown nudToplamSure;
        private NumericUpDown nudHaftada;
        private NumericUpDown nudToplamMesafe;
        private CheckBox chkEkEkipman;
        private Button btnOlustur;

        private Panel panelSag;
        private Label lblProgramBaslik;
        private Button btnPdfIndir;
        private DataGridView dgvProgram;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();

            // Menu ve TabControl bileşenleri
            this.menuStrip1 = new MenuStrip();
            this.profilToolStripMenuItem = new ToolStripMenuItem();
            this.antrenmanOlusturToolStripMenuItem = new ToolStripMenuItem();
            this.gecmisAntrenmanlarToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl1 = new TabControl(); // TabControl tanımlaması
            this.tabPageProfil = new TabPage();
            this.tabPageAntrenmanOlustur = new TabPage();
            this.tabPageGecmisAntrenmanlar = new TabPage();

            // Sol panel bileşenleri
            this.leftContainer = new Panel();
            this.grpBiyomotorik = new GroupBox();
            this.tblBiyomotorik = new TableLayoutPanel();
            this.btnDayaniklilik = new Button();
            this.btnSurat = new Button();
            this.btnKuvvet = new Button();
            this.btnEsneklik = new Button();
            this.btnKoordinasyon = new Button();

            this.grpYuzmeStili = new GroupBox();
            this.tblYuzmeStili = new TableLayoutPanel();
            this.btnSerbest = new Button();
            this.btnSirtustu = new Button();
            this.btnKurbagalama = new Button();
            this.btnKelebek = new Button();

            this.grpAntrenmanBilgileri = new GroupBox();
            this.tblAntrenmanBilgileri = new TableLayoutPanel();
            this.lblToplamSure = new Label();
            this.lblHaftada = new Label();
            this.lblToplamMesafe = new Label();
            this.nudToplamSure = new NumericUpDown();
            this.nudHaftada = new NumericUpDown();
            this.nudToplamMesafe = new NumericUpDown();
            this.chkEkEkipman = new CheckBox();
            this.btnOlustur = new Button();

            // Sağ panel bileşenleri
            this.panelSag = new Panel();
            this.lblProgramBaslik = new Label();
            this.btnPdfIndir = new Button();
            this.dgvProgram = new DataGridView();

            // PerformLayout ve SuspendLayout kullanımı (Form ve Ana Kontroller için)
            this.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageAntrenmanOlustur.SuspendLayout(); // Sadece AntrenmanOluştur tab'ının içeriğini ekleyeceğimiz için burayı ekliyoruz.
            this.leftContainer.SuspendLayout();
            this.grpBiyomotorik.SuspendLayout();
            this.tblBiyomotorik.SuspendLayout();
            this.grpYuzmeStili.SuspendLayout();
            this.tblYuzmeStili.SuspendLayout();
            this.grpAntrenmanBilgileri.SuspendLayout();
            this.tblAntrenmanBilgileri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToplamSure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHaftada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToplamMesafe)).BeginInit();
            this.panelSag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgram)).BeginInit();
            // ---

            // -------------------- MENUSTRIP --------------------
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.profilToolStripMenuItem,
                this.antrenmanOlusturToolStripMenuItem,
                this.gecmisAntrenmanlarToolStripMenuItem
            });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // Menu items
            this.profilToolStripMenuItem.Name = "profilToolStripMenuItem";
            this.profilToolStripMenuItem.Size = new Size(58, 24);
            this.profilToolStripMenuItem.Text = "Profil";
            this.profilToolStripMenuItem.Click += new EventHandler(this.ProfilToolStripMenuItem_Click);

            this.antrenmanOlusturToolStripMenuItem.Name = "antrenmanOlusturToolStripMenuItem";
            this.antrenmanOlusturToolStripMenuItem.Size = new Size(145, 24);
            this.antrenmanOlusturToolStripMenuItem.Text = "Antrenman Oluştur";
            this.antrenmanOlusturToolStripMenuItem.Click += new EventHandler(this.AntrenmanOlusturToolStripMenuItem_Click);

            this.gecmisAntrenmanlarToolStripMenuItem.Name = "gecmisAntrenmanlarToolStripMenuItem";
            this.gecmisAntrenmanlarToolStripMenuItem.Size = new Size(164, 24);
            this.gecmisAntrenmanlarToolStripMenuItem.Text = "Geçmiş Antrenmanlar";
            this.gecmisAntrenmanlarToolStripMenuItem.Click += new EventHandler(this.GecmisAntrenmanlarToolStripMenuItem_Click);

            // -------------------- TABCONTROL --------------------
            this.tabControl1.Controls.Add(this.tabPageProfil);
            this.tabControl1.Controls.Add(this.tabPageAntrenmanOlustur);
            this.tabControl1.Controls.Add(this.tabPageGecmisAntrenmanlar);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, this.menuStrip1.Height); // MenuStrip'in hemen altından başla
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(1200, 700 - this.menuStrip1.Height); // Toplam yüksekliği menuStrip'ten düş
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Appearance = TabAppearance.FlatButtons; // Sekmeleri buton gibi göster (ama gizleyeceğiz)
            this.tabControl1.ItemSize = new Size(0, 1); // Sekme başlıklarının yüksekliğini neredeyse sıfıra düşür
            this.tabControl1.SizeMode = TabSizeMode.Fixed; // Sekme boyutunu sabitle

            // Tab pages - tüm özellikler eklendi
            // Bu kısımda Location, Padding, Size ayarları TabControl'ün kendi boyutlandırmasına bırakılabilir,
            // çünkü ItemSize ve Dock.Fill sayesinde TabPage'ler otomatik olarak yerleşecektir.
            this.tabPageProfil.Name = "tabPageProfil";
            this.tabPageProfil.Text = "Profil";
            this.tabPageProfil.UseVisualStyleBackColor = true;

            this.tabPageAntrenmanOlustur.Name = "tabPageAntrenmanOlustur";
            this.tabPageAntrenmanOlustur.Text = "Antrenman Oluştur";
            this.tabPageAntrenmanOlustur.UseVisualStyleBackColor = true;

            this.tabPageGecmisAntrenmanlar.Name = "tabPageGecmisAntrenmanlar";
            this.tabPageGecmisAntrenmanlar.Text = "Geçmiş Antrenmanlar";
            this.tabPageGecmisAntrenmanlar.UseVisualStyleBackColor = true;

            // -------------------- SOL PANEL (tabPageAntrenmanOlustur içinde) --------------------
            // leftContainer'ı tabPageAntrenmanOlustur'un soluna dock et ve genişliğini ayarla
            int leftPanelWidth = 460; // Genişliği biraz daha düşürdük
            int groupHeight = 135; // GroupBox'ların yüksekliği
            int antrenmanBilgileriGroupHeight = 210; // Antrenman Bilgileri GroupBox'ının yüksekliği

            this.leftContainer.Dock = DockStyle.Left;
            this.leftContainer.Width = leftPanelWidth;
            this.leftContainer.Padding = new Padding(8);
            // Controls.Add sırası önemlidir: En alttaki kontrolü önce ekle (DockStyle.Top için)
            this.leftContainer.Controls.Add(this.grpAntrenmanBilgileri);
            this.leftContainer.Controls.Add(this.grpYuzmeStili);
            this.leftContainer.Controls.Add(this.grpBiyomotorik);


            // Biyomotorik bölümü
            this.grpBiyomotorik.Text = "Biyomotorik Yetenek";
            this.grpBiyomotorik.Dock = DockStyle.Top;
            this.grpBiyomotorik.Height = groupHeight;
            this.grpBiyomotorik.Padding = new Padding(8);
            this.grpBiyomotorik.Controls.Add(this.tblBiyomotorik);

            // Biyomotorik tablo
            this.tblBiyomotorik.Dock = DockStyle.Fill;
            this.tblBiyomotorik.ColumnCount = 5;
            for (int i = 0; i < 5; i++)
                this.tblBiyomotorik.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.tblBiyomotorik.RowCount = 1;
            this.tblBiyomotorik.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var btnFont = new Font("Segoe UI", 9F, FontStyle.Regular); // Buton fontunu biraz daha küçülttük

            // Biyomotorik butonları
            Button[] biyomotorikButtons = { btnDayaniklilik, btnSurat, btnKuvvet, btnEsneklik, btnKoordinasyon };
            string[] biyomotorikTexts = { "Dayanıklılık", "Sürat", "Kuvvet", "Esneklik", "Koordinasyon" };

            for (int i = 0; i < biyomotorikButtons.Length; i++)
            {
                var btn = biyomotorikButtons[i];
                btn.Text = biyomotorikTexts[i];
                btn.Dock = DockStyle.Fill;
                btn.Font = btnFont;
                btn.AutoEllipsis = true; // Yazı sığmazsa ... koyar
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.MinimumSize = new Size(70, 40); // Minimum boyutu daha da küçülttük
                btn.TextAlign = ContentAlignment.MiddleCenter;
                this.tblBiyomotorik.Controls.Add(btn, i, 0);
            }

            // Yüzme stili bölümü
            this.grpYuzmeStili.Text = "Yüzme Stili";
            this.grpYuzmeStili.Dock = DockStyle.Top;
            this.grpYuzmeStili.Height = groupHeight;
            this.grpYuzmeStili.Padding = new Padding(8);
            this.grpYuzmeStili.Controls.Add(this.tblYuzmeStili);

            // Yüzme stili tablo
            this.tblYuzmeStili.Dock = DockStyle.Fill;
            this.tblYuzmeStili.ColumnCount = 4;
            for (int i = 0; i < 4; i++)
                this.tblYuzmeStili.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tblYuzmeStili.RowCount = 1;
            this.tblYuzmeStili.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Yüzme stili butonları
            Button[] yuzmeStiliButtons = { btnSerbest, btnSirtustu, btnKurbagalama, btnKelebek };
            string[] yuzmeStiliTexts = { "Serbest", "Sırtüstü", "Kurbağalama", "Kelebek" };

            for (int i = 0; i < yuzmeStiliButtons.Length; i++)
            {
                var btn = yuzmeStiliButtons[i];
                btn.Text = yuzmeStiliTexts[i];
                btn.Dock = DockStyle.Fill;
                btn.Font = btnFont;
                btn.AutoEllipsis = true;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.MinimumSize = new Size(70, 40);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                this.tblYuzmeStili.Controls.Add(btn, i, 0);
            }

            // Antrenman bilgileri bölümü
            this.grpAntrenmanBilgileri.Text = "Antrenman Bilgileri";
            this.grpAntrenmanBilgileri.Dock = DockStyle.Top;
            this.grpAntrenmanBilgileri.Height = antrenmanBilgileriGroupHeight;
            this.grpAntrenmanBilgileri.Padding = new Padding(8);
            this.grpAntrenmanBilgileri.Controls.Add(this.tblAntrenmanBilgileri);

            // Antrenman bilgileri tablo
            this.tblAntrenmanBilgileri.Dock = DockStyle.Fill;
            this.tblAntrenmanBilgileri.ColumnCount = 2;
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.tblAntrenmanBilgileri.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            this.tblAntrenmanBilgileri.RowCount = 4;
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            this.tblAntrenmanBilgileri.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tblAntrenmanBilgileri.Padding = new Padding(4);

            // Label ve NumericUpDown ayarları
            this.lblToplamSure.Text = "Toplam Süre (dk):";
            this.lblToplamSure.Anchor = AnchorStyles.Left;
            this.lblToplamSure.Font = btnFont;

            this.nudToplamSure.Minimum = 0;
            this.nudToplamSure.Maximum = 10000;
            this.nudToplamSure.Dock = DockStyle.Fill;
            this.nudToplamSure.Font = btnFont;

            this.lblHaftada.Text = "Haftada Kaç Gün:";
            this.lblHaftada.Anchor = AnchorStyles.Left;
            this.lblHaftada.Font = btnFont;

            this.nudHaftada.Minimum = 0;
            this.nudHaftada.Maximum = 7;
            this.nudHaftada.Dock = DockStyle.Fill;
            this.nudHaftada.Font = btnFont;

            this.lblToplamMesafe.Text = "Toplam Mesafe (m):";
            this.lblToplamMesafe.Anchor = AnchorStyles.Left;
            this.lblToplamMesafe.Font = btnFont;

            this.nudToplamMesafe.Minimum = 0;
            this.nudToplamMesafe.Maximum = 100000;
            this.nudToplamMesafe.Dock = DockStyle.Fill;
            this.nudToplamMesafe.Font = btnFont;

            this.chkEkEkipman.Text = "Ek Ekipman Kullanılsın mı?";
            this.chkEkEkipman.Anchor = AnchorStyles.Left;
            this.chkEkEkipman.Font = btnFont;

            this.btnOlustur.Text = "Antrenman Oluştur";
            this.btnOlustur.AutoSize = false;
            this.btnOlustur.Width = 150; // Genişliği daha da düşürdük
            this.btnOlustur.Height = 40;
            this.btnOlustur.Anchor = AnchorStyles.Right;
            this.btnOlustur.Font = new Font("Segoe UI", 9F, FontStyle.Bold); // Buton fontunu da küçült
            this.btnOlustur.FlatStyle = FlatStyle.Flat;
            this.btnOlustur.FlatAppearance.BorderSize = 1;
            this.btnOlustur.FlatAppearance.BorderColor = Color.Gray;

            // Antrenman bilgileri kontrolleri yerleştirme
            this.tblAntrenmanBilgileri.Controls.Add(this.lblToplamSure, 0, 0);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudToplamSure, 1, 0);
            this.tblAntrenmanBilgileri.Controls.Add(this.lblHaftada, 0, 1);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudHaftada, 1, 1);
            this.tblAntrenmanBilgileri.Controls.Add(this.lblToplamMesafe, 0, 2);
            this.tblAntrenmanBilgileri.Controls.Add(this.nudToplamMesafe, 1, 2);
            this.tblAntrenmanBilgileri.Controls.Add(this.chkEkEkipman, 0, 3);
            this.tblAntrenmanBilgileri.SetColumnSpan(this.chkEkEkipman, 1);
            this.tblAntrenmanBilgileri.Controls.Add(this.btnOlustur, 1, 3);
            this.tblAntrenmanBilgileri.SetColumnSpan(this.btnOlustur, 1);
            this.tblAntrenmanBilgileri.SetRowSpan(this.btnOlustur, 1);

            // -------------------- SAĞ PANEL (tabPageAntrenmanOlustur içinde) --------------------
            this.panelSag.Dock = DockStyle.Fill; // Kalan tüm alanı kaplasın
            this.panelSag.Padding = new Padding(12);

            // Kontrollerin eklenme sırası önemli:
            // 1. Label (en üstte)
            // 2. Buton (label'ın altında)
            // 3. DataGridView (kalan tüm alanı kaplasın)
            this.panelSag.Controls.Add(this.dgvProgram);    // DGV'yi en son ekliyoruz ki Dock.Fill çalışsın
            this.panelSag.Controls.Add(this.btnPdfIndir);   // PDF butonu DGV'den önce, başlığın altında
            this.panelSag.Controls.Add(this.lblProgramBaslik); // Başlık en üstte


            this.lblProgramBaslik.Text = "Oluşturulan Antrenman Programı";
            this.lblProgramBaslik.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblProgramBaslik.Dock = DockStyle.Top;
            this.lblProgramBaslik.Height = 32;

            this.btnPdfIndir.Text = "PDF Olarak İndir";
            this.btnPdfIndir.Dock = DockStyle.Top;
            this.btnPdfIndir.Height = 36;
            this.btnPdfIndir.Font = btnFont; // btnFont daha önce tanımlanmış olmalı
            this.btnPdfIndir.FlatStyle = FlatStyle.Flat;
            this.btnPdfIndir.Margin = new Padding(0, 8, 0, 8); // Üstten ve alttan boşluk bırakır

            this.dgvProgram.Dock = DockStyle.Fill; // Bu, kalan tüm alanı kaplamasını sağlar
            this.dgvProgram.AllowUserToAddRows = false;
            this.dgvProgram.AllowUserToDeleteRows = false;
            this.dgvProgram.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProgram.RowHeadersVisible = false;
            this.dgvProgram.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgram.Font = btnFont; // btnFont daha önce tanımlanmış olmalı

            var colBolum = new DataGridViewTextBoxColumn() { Name = "colBolum", HeaderText = "Bölüm", DataPropertyName = "Bolum" };
            var colAktivite = new DataGridViewTextBoxColumn() { Name = "colAktivite", HeaderText = "Aktivite", DataPropertyName = "Aktivite" };
            var colDetay = new DataGridViewTextBoxColumn() { Name = "colDetay", HeaderText = "Detay", DataPropertyName = "Detay" };
            this.dgvProgram.Columns.AddRange(new DataGridViewColumn[] { colBolum, colAktivite, colDetay });

            // Antrenman oluştur sayfasına panelleri ekleme
            this.tabPageAntrenmanOlustur.Controls.Add(this.panelSag);
            this.tabPageAntrenmanOlustur.Controls.Add(this.leftContainer);


            // -------------------- FORM AYARLARI --------------------
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 700);
            this.Controls.Add(this.tabControl1); // TabControl en son eklenmeli ki MenuStrip'i kaplamasın
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Yüzme Antrenmanı Planlama";

            // ResumeLayout
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false); // TabControl'ün ResumeLayout'u tüm tab sayfalarını etkiler
            this.tabPageAntrenmanOlustur.ResumeLayout(false);
            this.leftContainer.ResumeLayout(false);
            this.grpBiyomotorik.ResumeLayout(false);
            this.tblBiyomotorik.ResumeLayout(false);
            this.grpBiyomotorik.PerformLayout();
            this.grpYuzmeStili.ResumeLayout(false);
            this.tblYuzmeStili.ResumeLayout(false);
            this.grpYuzmeStili.PerformLayout();
            this.grpAntrenmanBilgileri.ResumeLayout(false);
            this.tblAntrenmanBilgileri.ResumeLayout(false);
            this.grpAntrenmanBilgileri.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToplamSure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHaftada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToplamMesafe)).EndInit();
            this.panelSag.ResumeLayout(false);
            this.panelSag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
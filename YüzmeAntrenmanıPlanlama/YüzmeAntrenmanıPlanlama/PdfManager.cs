using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace YüzmeAntrenmanıPlanlama
{
    public class PdfManager
    {
        // DataGridView'deki veriyi PDF'e aktaran fonksiyon
        public bool ExportDataGridViewToPdf(DataGridView dgv, string baslik, string dosyaAdi)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("PDF'e aktarılacak veri yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Dosyası|*.pdf";
            sfd.FileName = dosyaAdi + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // PDF Dokümanı Oluştur (A4 Boyutu)
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(sfd.FileName, FileMode.Create));

                    pdfDoc.Open();

                    // Türkçe karakter desteği için font ayarı (Windows fontlarından Arial'i kullanıyoruz)
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font titleFont = new Font(bf, 18, Font.BOLD);
                    Font cellFont = new Font(bf, 10, Font.NORMAL);

                    // Başlık Ekle
                    Paragraph title = new Paragraph(baslik, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    pdfDoc.Add(title);

                    // Tablo Oluştur
                    PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    // Başlıkları Ekle (Header)
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        if (column.Visible) // Sadece görünenleri ekle
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, cellFont));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5;
                            pdfTable.AddCell(cell);
                        }
                    }

                    // Satırları Ekle (Rows)
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (dgv.Columns[cell.ColumnIndex].Visible)
                            {
                                string cellValue = cell.Value?.ToString() ?? "";
                                PdfPCell pdfCell = new PdfPCell(new Phrase(cellValue, cellFont));
                                pdfCell.Padding = 5;
                                pdfTable.AddCell(pdfCell);
                            }
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();

                    MessageBox.Show("PDF başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }
    }
}
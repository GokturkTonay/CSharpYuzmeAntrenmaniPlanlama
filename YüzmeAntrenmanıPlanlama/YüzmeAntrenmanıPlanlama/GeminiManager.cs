using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YüzmeAntrenmanıPlanlama
{
    public class GeminiManager
    {
        // -------------------------------------------------------------------------
        // BURAYA GROQ SİTESİNDEN ALDIĞIN 'gsk_' İLE BAŞLAYAN ŞİFREYİ YAPIŞTIR
        // -------------------------------------------------------------------------
        private const string ApiKey = "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";

        // Groq API Adresi (OpenAI uyumlu format kullanır)
        private const string ApiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public async Task<string> AntrenmanProgramiIste(string prompt)
        {
            if (string.IsNullOrEmpty(ApiKey) || ApiKey.Contains("BURAYA"))
            {
                return "HATA: Groq API Anahtarı girilmemiş! Lütfen GeminiManager.cs dosyasını düzenleyin.";
            }

            using (var client = new HttpClient())
            {
                // Yetkilendirme Başlığı
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

                // Groq İstek Gövdesi
                // Model olarak "llama3-8b-8192" kullanıyoruz. Çok hızlıdır.
                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.5 // 0'a yakın olması daha tutarlı JSON verir
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(ApiUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"HATA: Groq Bağlantı Hatası ({response.StatusCode}).\nDetay: {responseString}";
                    }

                    try
                    {
                        JObject jsonResponse = JObject.Parse(responseString);

                        // Groq/OpenAI cevap formatı: choices -> 0 -> message -> content
                        var text = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();

                        if (!string.IsNullOrEmpty(text))
                        {
                            // Yapay zeka bazen "İşte JSON:" diye lafa başlar, bunları temizleyelim.
                            text = text.Replace("```json", "").Replace("```", "").Trim();

                            // Bazen en sonda açıklama yapabilir, sadece [ ile ] arasını almaya çalışalım
                            int firstBracket = text.IndexOf('[');
                            int lastBracket = text.LastIndexOf(']');

                            if (firstBracket != -1 && lastBracket != -1)
                            {
                                text = text.Substring(firstBracket, lastBracket - firstBracket + 1);
                            }

                            return text;
                        }

                        return "HATA: Groq boş cevap döndürdü.";
                    }
                    catch (Exception ex)
                    {
                        return $"HATA: Cevap okunamadı. Veri: {responseString}\nHata: {ex.Message}";
                    }
                }
                catch (Exception ex)
                {
                    return $"HATA: İnternet bağlantı sorunu: {ex.Message}";
                }
            }
        }
    }
}
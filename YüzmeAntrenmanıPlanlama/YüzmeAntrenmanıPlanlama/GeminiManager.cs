using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YüzmeAntrenmanıPlanlama;

public class GeminiManager
{
    // API ANAHTARINI BURAYA YAPIŞTIR (Boşluksuz!)
    private const string ApiKey = "";

    // Denenecek Modeller Listesi (Sırasıyla dener)
    private readonly string[] _models = {
        "gemini-2.0-flash-exp"
    };

    public async Task<string> AntrenmanProgramiIste(string prompt)
    {
        if (string.IsNullOrEmpty(ApiKey) || ApiKey.Contains("BURAYA"))
            return "HATA: API Anahtarı girilmemiş! GeminiManager.cs dosyasını düzeltin.";

        using var client = new HttpClient();

        // Her modeli sırayla dene
        foreach (var model in _models)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={ApiKey}";

            var requestBody = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Eğer başarılıysa cevabı döndür ve çık
                if (response.IsSuccessStatusCode)
                {
                    return CevabiTemizle(responseString);
                }

                // 429 (Kota Dolu) hatasıysa diğer modele geçme, beklemesi lazım.
                if ((int)response.StatusCode == 429)
                {
                    return "HATA: Çok hızlı istek gönderildi (Kota Dolu). Lütfen 1 dakika bekleyip tekrar deneyin.";
                }

                // 404 (Model Bulunamadı) hatasıysa bir sonraki modeli dene (Döngü devam eder)
                // Diğer hatalar için de döngüye devam etsin.
            }
            catch
            {
                // Bağlantı hatası varsa diğer modele geç
            }
        }

        return "HATA: Hiçbir yapay zeka modeline ulaşılamadı. API Anahtarınızı veya internet bağlantınızı kontrol edin.";
    }

    private string CevabiTemizle(string jsonVeri)
    {
        try
        {
            var jsonResponse = JObject.Parse(jsonVeri);
            var text = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

            if (!string.IsNullOrEmpty(text))
            {
                return text.Replace("```json", "").Replace("```", "").Trim();
            }
            return "HATA: Yapay zeka boş cevap döndürdü.";
        }
        catch (Exception ex)
        {
            return $"HATA: Cevap okunamadı. {ex.Message}";
        }
    }
}
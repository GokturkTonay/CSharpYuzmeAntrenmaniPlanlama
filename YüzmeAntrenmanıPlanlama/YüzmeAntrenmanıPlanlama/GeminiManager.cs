using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // JSON verisini parçalamak için

namespace YüzmeAntrenmanıPlanlama
{
    public class GeminiManager
    {
        // BURAYA ALDIĞIN 'AIza...' İLE BAŞLAYAN ANAHTARI YAPIŞTIR
        private const string ApiKey = "AIzaSyDhXD9FfkNzwBK92FPuFpNz3s3bsGATM9I";

        private const string ApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";

        public async Task<string> AntrenmanProgramiIste(string prompt)
        {
            using (var client = new HttpClient())
            {
                // Google'ın istediği JSON formatını hazırlıyoruz
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    }
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // İsteği gönderiyoruz
                var response = await client.PostAsync($"{ApiUrl}?key={ApiKey}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Cevabı okuyoruz
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Gelen karmaşık JSON içinden sadece metni ayıklıyoruz
                    JObject jsonResponse = JObject.Parse(responseString);
                    string gelenCevap = jsonResponse["candidates"][0]["content"]["parts"][0]["text"].ToString();

                    return gelenCevap;
                }
                else
                {
                    return "Hata: Yapay zeka servisine ulaşılamadı. " + response.StatusCode;
                }
            }
        }
    }
}
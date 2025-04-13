using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string SatuanSuhu { get; set; }
    public int BatasHariDemam { get; set; }
    public string PesanDitolak { get; set; }
    public string PesanDiterima { get; set; }

    public CovidConfig()
    {
        SatuanSuhu = "celcius";
        BatasHariDemam = 14;
        PesanDitolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        PesanDiterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

        string pathFileConfig = Path.Combine(AppContext.BaseDirectory, "covid_config.json");

        if (File.Exists(pathFileConfig))
        {
            try
            {
                string isiJson = File.ReadAllText(pathFileConfig);
                using (JsonDocument doc = JsonDocument.Parse(isiJson))
                {
                    JsonElement root = doc.RootElement;

                    if (root.TryGetProperty("satuan_suhu", out JsonElement satuan))
                        SatuanSuhu = satuan.GetString();

                    if (root.TryGetProperty("batas_hari_deman", out JsonElement batas))
                        BatasHariDemam = batas.GetInt32();

                    if (root.TryGetProperty("pesan_ditolak", out JsonElement ditolak))
                        PesanDitolak = ditolak.GetString();

                    if (root.TryGetProperty("pesan_diterima", out JsonElement diterima))
                        PesanDiterima = diterima.GetString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal membaca file config: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("File konfigurasi tidak ditemukan. Menggunakan konfigurasi default.");
        }
    }


    public void UbahSatuan()
    {
        if (SatuanSuhu.ToLower() == "celcius")
        {
            SatuanSuhu = "fahrenheit";
        }
        else
        {
            SatuanSuhu = "celcius";
        }
        Console.WriteLine($"Satuan suhu diubah menjadi: {SatuanSuhu}");
    }
}

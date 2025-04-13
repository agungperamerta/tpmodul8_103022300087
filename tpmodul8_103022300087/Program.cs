using System;

class Program
{
    public static void Main(string[] args)
    {
        CovidConfig config = new CovidConfig();

        Console.WriteLine($"Satuan suhu saat ini: {config.SatuanSuhu}");
        config.UbahSatuan();

        Console.WriteLine("\n=== Skrining Covid Gedung ===");
        Console.WriteLine($"(Satuan suhu yang digunakan: {config.SatuanSuhu})\n");

        Console.Write($"Berapa suhu badan anda saat ini? Dalam satuan {config.SatuanSuhu}: ");
        string inputSuhu = Console.ReadLine();
        double suhu;

        while (!double.TryParse(inputSuhu, out suhu))
        {
            Console.Write("Input tidak valid. Masukkan angka suhu yang benar: ");
            inputSuhu = Console.ReadLine();
        }

        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir mengalami gejala demam? ");
        string inputHari = Console.ReadLine();
        int hariDemam;

        while (!int.TryParse(inputHari, out hariDemam))
        {
            Console.Write("Input tidak valid. Masukkan angka hari yang benar: ");
            inputHari = Console.ReadLine();
        }

        bool suhuNormal = false;
        if (config.SatuanSuhu.ToLower() == "celcius")
        {
            suhuNormal = suhu >= 36.5 && suhu <= 37.5;
        }
        else if (config.SatuanSuhu.ToLower() == "fahrenheit")
        {
            suhuNormal = suhu >= 97.7 && suhu <= 99.5;
        }

        bool hariAman = hariDemam < config.BatasHariDemam;

        Console.WriteLine("\n== Hasil Evaluasi ==");
        if (suhuNormal && hariAman)
        {
            Console.WriteLine(config.PesanDiterima);
        }
        else
        {
            Console.WriteLine(config.PesanDitolak);
        }
    }
}

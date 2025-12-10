using System;
using System.Collections.Generic;
using System.Reflection;

// Custom attribute
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class DeveloperInfoAttribute : Attribute
{
    public string Author { get; }
    public string Version { get; }
    public DeveloperInfoAttribute(string author, string version)
    {
        Author = author;
        Version = version;
    }

    public override string ToString() => $"Yazar={Author}, Sürüm={Version}";
}

public struct Ogrenci
{
    public int Id;
    public string Ad;
    public double NotOrt;

    public Ogrenci(int id, string ad, double notOrt)
    {
        Id = id;
        Ad = ad;
        NotOrt = notOrt;
    }

    public override string ToString() => $"Id={Id}, Ad={Ad}, NotOrt={NotOrt:F2}";
}

[DeveloperInfo("Enes Baysal", "1.0")]
public class OrnekSinif
{
    [DeveloperInfo("Enes", "1.0")]
    public void MetotA() { }

    [DeveloperInfo("Enes", "1.1")]
    public void MetotB() { }

    public void MetotC() { }
}

class Program
{
    // Obsolete methods
    [Obsolete("YeniMethoduKullan - sadece uyarı", false)]
    public static void EskiMetotUyarisi()
    {
        Console.WriteLine("EskiMetotUyarisi çağrıldı.");
    }

    // This would cause a compile-time error if left enabled.
    [Obsolete("Bu metot kaldırıldı - hata", true)]
    public static void EskiMetotHatasi()
    {
        Console.WriteLine("EskiMetotHatasi çağrıldı.");
    }

    static void AttributeRaporu()
    {
        Console.WriteLine("=== ATTRIBUTE RAPORU ===");
        Type t = typeof(OrnekSinif);
        Console.WriteLine($"Sınıf: {t.FullName}");
        foreach (var m in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            Console.WriteLine($"- Metot: {m.Name}");
            foreach (var attr in m.GetCustomAttributes())
            {
                Console.WriteLine($"    - Öznitelik: {attr.GetType().Name} => {attr}");
            }
        }
    }

    static void StructOrnegi()
    {
        Console.WriteLine("=== STRUCT ÖRNEĞİ ===");
        var o1 = new Ogrenci(1, "Ali", 3.5);
        var o2 = o1; // value type kopyalanır
        o2.Ad = "Veli";
        var o3 = new Ogrenci(2, "Ayşe", 3.9);
        var liste = new List<Ogrenci> { o1, o2, o3 };
        Console.WriteLine("Öğrenci listesi:");
        foreach (var o in liste) Console.WriteLine(o);
        Console.WriteLine("DİKKAT: o2'nin adı değişse bile o1 etkilenmez (value type davranışı).");
    }

    static void HataYonetimiOrnegi()
    {
        Console.WriteLine("=== HATA YÖNETİMİ ÖRNEĞİ ===");
        try
        {
            Console.Write("Bir tam sayı bölen giriniz: ");
            string input = Console.ReadLine();
            int bolen = int.Parse(input ?? throw new FormatException());
            int sonuc = 10 / bolen;
            Console.WriteLine($"10 / {bolen} = {sonuc}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Sıfıra Bölme Hatası yakalandı: " + ex.Message);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Format Hatası yakalandı: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally bloğu çalıştı (her zaman çalışır).");
        }
    }

    static void Main()
    {
        StructOrnegi();

        HataYonetimiOrnegi();

        Console.WriteLine();
        Console.WriteLine("Obsolete metot çağrılıyor (EskiMetotUyarisi):");
        EskiMetotUyarisi();

        // Hata veren obsolete metot çağrılmıyor
        // EskiMetotHatasi();

        Console.WriteLine();
        AttributeRaporu();

        Console.WriteLine();
        Console.WriteLine("Çıkmak için Enter'a basın...");
        Console.ReadLine();
    }
}

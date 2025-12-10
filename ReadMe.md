# Proje Raporu 

## İçerikler
- Console uygulaması: Struct, Exception Handling, Obsolete attribute, Custom attribute ve Reflection raporu.
- Web API: Model validasyon, Middleware, Action/Exception filter, Metadata haritası endpoint.

## Önemli Noktalar
- Struct gösterimi: Student struct'u örneklenip listeye eklenerek değer tipi davranışı gösterilmiştir.
- Hata yönetimi: try/catch/finally kullanımı, DivideByZeroException ve FormatException ayrı ayrı yakalanmıştır.
- Obsolete attribute: Bir metod uyarı (isError=false) ile işaretlenmiş ve bir metod derleme hatası (isError=true) verecek şekilde işaretlenmiştir. Derlenebilirlik için error olan metod yorum satırına alınmıştır.
- Reflection endpoint: /api/system/attribute-map adresi Web API içindeki controller ve action'ları tarayıp üzerine konulan HTTP attribute'larını JSON olarak döner.


📌 Proje Açıklaması


🚀 Console Uygulaması Özeti

Console uygulaması şu konuları göstermektedir:

Struct (Ogrenci) → değer tipi davranışı
Exception Handling → try/catch/finally
Obsolete Attribute → warning ve error örnekleri
Custom Attribute → DeveloperInfoAttribute
Reflection → sınıf/metot üzerindeki attribute bilgilerini çıkarma

🧩 Web API Özeti
✨ API Özellikleri:

Ürün CRUD endpointleri
Model validation
İstek/yanıt loglayan custom middleware
Action süresini ölçen Action Filter
Hataları JSON formatlayarak dönen Exception Filter
Reflection ile metadata dönen /api/system/attribute-map endpoint’i

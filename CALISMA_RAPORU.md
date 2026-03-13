# CALISMA_RAPORU.md

## 1. Proje Özeti

Bu çalışmada şirket bazlı ürün yönetimi sağlayan basit bir **Akıllı Depo (Smart Warehouse)** uygulaması geliştirilmiştir.
Sistem üzerinden kullanıcılar bir şirkete ait ürünleri ekleyebilir, güncelleyebilir, silebilir ve arayabilir.

Backend tarafında RESTful bir Web API geliştirilmiş, frontend tarafında ise React kullanılarak kullanıcı arayüzü oluşturulmuştur.
Ürünler **CompanyId** üzerinden izole edilerek çoklu şirket desteği sağlanmıştır.

Uygulama aşağıdaki temel özellikleri içermektedir:

* Ürün ekleme
* Ürün güncelleme
* Ürün silme (soft delete)
* Şirket bazlı ürün listeleme
* Arama (search)
* Sayfalama (pagination)
* React tabanlı kullanıcı arayüzü

---

# 2. Kullanılan Teknolojiler

## Backend

* .NET Web API
* Entity Framework Core
* SQL Server
* C#

## Frontend

* React
* TypeScript
* Material UI
* Axios

## Diğer Araçlar

* Visual Studio / Visual Studio Code
* Git

---

# 3. Mimari Yaklaşım ve Kararlar

Katmanlar:

* **Controllers** → API endpointleri
* **Managers** → iş mantığı
* **Repositories** → veritabanı işlemleri
* **Entities** → veritabanı tabloları
* **DTOs** → veri transfer modelleri

Repository pattern kullanılarak veri erişim katmanı soyutlanmıştır.
Controller katmanı doğrudan repository yerine **Manager katmanı üzerinden işlem yapmaktadır**. Bu yaklaşım iş mantığının merkezi bir yerde yönetilmesini sağlar.

Veriler şirket bazlı olarak **CompanyId** alanı ile filtrelenmektedir. Böylece çoklu şirket kullanımına uygun bir yapı elde edilmiştir.

Ürün silme işlemlerinde veri kaybını önlemek amacıyla **soft delete (IsDeleted)** yaklaşımı uygulanmıştır.

---

# 4. Veritabanı Tasarımı

Temel olarak iki ana entity bulunmaktadır.

## Product

Ürün bilgilerini saklar.

Alanlar:

* Id
* CompanyId
* ProductName
* Barcode
* Category
* CreatedAt
* IsDeleted

## StockMovement

Ürünlerin depo içerisindeki hareketlerini takip etmek amacıyla tasarlanmıştır.

Alanlar:

* ProductId
* ShelfId
* Quantity
* MovementType
* MovementDate
* CompanyId

Bu yapı sayesinde ileride stok giriş ve çıkış işlemlerinin takip edilmesi mümkün olacaktır.

---

# 5. Karşılaşılan Sorunlar ve Çözüm Yolları

### 1. API bağlantı hataları

Frontend tarafında bazı API çağrılarında **500 hata kodu** alınmıştır. Sorunun backend tarafındaki veri sorgularından kaynaklandığı tespit edilmiştir ve sorgular düzeltilmiştir.

### 2. CompanyId bağlama

Geliştirme sırasında kullanıcı girişi ile bağlı olduğu şirket çekerek ürünleri eklemek istedim fakat bu proje sürecini uzatacağını düşündüğüm için frontend tarafında sabit companyId eklemek zorunda kaldım.

---

# 6. Yapay Zeka Kullanımı

Geliştirme sürecinde yapay zeka araçları aşağıdaki konularda destek amaçlı kullanılmıştır:

* Mimari yaklaşım hakkında öneriler
* React ve .NET tarafında bazı kod yapılandırmaları
* hata analizleri ve çözüm önerileri

Yapay zeka araçları yalnızca **destekleyici bir araç** olarak kullanılmış, proje mantığı ve uygulama geliştirme süreci tarafımdan gerçekleştirilmiştir.

---


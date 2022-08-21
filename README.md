### Protein ve Patika iş birliği ile yapılan Protein .Net Bootcamp'inin bitirme projesi olarak yaptığım uygulama.

- ##  Projemiz Kullanıcıların istedikleri ürünlere teklif verip yada direk satın aldıkları mini e Ticaret uygulaması.
### Üye İşlemleri
- Kullanıcı ilgili bilgileri doldurup kayıt olabiliyor.Ardından E mail ile bir Guid activasyon code gönderiliyor ve kişinin Activasyon işlemlerini tamamlanıyor.
- Kullanıcı Email ve Pasword ile sisteme giriş yapabiliyor. Ardından Kullanıcıya Hoş geldiniz maili gönderiliyor.
- Kullanıcı girdigi şifre kontrol ediliyor 3 kez yanlış girilen şifrelerde Hesap Bloke ediliyor. Ardındna kullanıcının mail adresine Guid ile code gönderilip bloke işlemini kaldırılmasını saglıyoruz.
- Kullanıcı istedigi zaman şifresini degiştirip yeni bir şifre alabiliyor.
- Kulanıcıları aktif, pasif veya veri tabanındaki tüm kayıtları listeliyoruz.
### Ürün işlemleri
- Kullanıcılar satıcakları ürünleri Kategori, Renk, Marka, Kullanım durumu gibi özelliklerle ekleyebiliyor.
- Kulanıcıcılar bir ürüne sadece 1 resim yükleyebliyor ve resim boyutu maksimum 400kb olabiliyor. 
- Kullanıcının ürünlerini veri tabanından çekip listeliyebilir.
- Ürünleri aktif, pasif veya tüm ürünleri listeliyoruz.
### Teklif işlemleri
- Kullanıcı teklif vericegi ürünün isOfferable alanı kontrol edilip teklif verebilmesini saglıyoruz.
- Kullanıcının yaptıgı teklifleri listeliyoruz.
- Belirtilen Ürüne yapılan teklifleri listeliyoruz.
- Kullanıcının ürünlerine gelen teklifleri listeliyoruz.
- Kullanıcı istedigi ürüne teklif vermeden direk satın alabiliyor. Satın aldıktan sonra bu ürüne yapılan teklifleri siliyoruz.
- Kullanıcı gelen teklife göre Onaylama işlemi gerçekleştiriyor. Ardındad teklifi onaylanan ürünün bilgilerini günceleyip bu ürüne yapılan teklifleri siliyoruz.
### Kategori işlemleri
- Belirtilen kategoriye göre ürünleri listeliyoruz.
- Kategorileri aktif, pasif veya tüm ürünleri listeliyoruz.
 <hr>
 
# Database Diagram
![Diagram](https://i.hizliresim.com/iw387vc.png)
 
 <hr>
 
 ## Projenin Kurulumu
 - Proje’yi çalıştırmak için PostgreSql'in bilgisayarımızda yüklü ve çalışıyor olması gerekmektedir. Daha Sonra FinalProject.Api katmanındakı **appsettings.json** dosyası
 içerisindeki sırasıyla kendi database ayaralarına göre değiştirmelisiniz.
 
 `
 ConnectionStrings": {
  "DefaultConnection": "User ID=(DatabaseUserBuraya(postgres));Password=(ŞifreBuraya);Host=(HostIp veya 'localhost');Port=(PortNumarası(5432));Database=MiniCommerceDb;Integrated Security=true;Pooling=true"
}, `
 

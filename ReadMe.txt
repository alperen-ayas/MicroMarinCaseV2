Selamlar

Projede hangi nesnenin crud edileceği kısmı recordType parametresi ile ele alınmaktadır.
Bu bilgiye göre strateji pattern ile ilgili strateji set edilip ilgili request nesnesinin oluşması
ve akabinde mediator pattern ile handlera yönlendirilmesi gerçekleşmektedir.
Normalde her operasyon için ayrı bir operasyon birimi açılması gerekse de bunun overengineering olacağından doalyı tek operation nesnesinde bunu yönettim.
Projede DDD prensiplerine göre geliştirme yapılmıştır. Bunun en net örneği customer classında bulunmaktadır.
Yine Customer Domain modeli için Domain Unit testler yazılmıştır. Aklıma gelen senaryolar sadece bunlardı. Umarım unuttuğum yoktur.
Ayrıca domain eventların bir örneği olması için; customer nesnesi yaratılırken herhangi sayıda bir order da iletildiyse
Bu kısımdaki işlemler CustomerOrdersCreateDomainEvent ile bir event oluşturulup ilgili handlerda yapılmıştır.
Bu ilgili eventin handlerı aynı scope içerisindeki context ile çalıştığı için bu bir sorun yaratmaz ve bunlar aynı transaction içinde yapılmaktadır.

NOT 1 : Database contexti scoped veya singleton dışında bir şekilde örneklenmemelidir. Yoksa yukarda belirtildiği gibi transaction ortak olmaz.
NOT 2 : Validasyon ve exception handling kısmını bir önceki projede yaptığım için burada yapmamış bulundum. Umarım anlayış ile karşılarsınız.


Elimden gelen en hızlı şekilde bitirmeye çalıştım. İyi çalışmalar dilerim.

Alperen Ayaş

00:29 01/10/2024
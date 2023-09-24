# rtech-crypto-coin-app

Proje altyapımızı hazırlamak için proje dizininde aşaıdaki komutları çalıştırmamız gerekecektir.

`docker network create rtech-network`

`docker-compose -f docker-compose.infrastructure.yml -f docker-compose.infrastructure.override.yml -d`

Daha sonra aşağıdaki komut ile web uygulaması dizinine geçilir.

`cd RTech.CryptoCoin.Web`

Ardından `dotnet run` komutu ile proje çalıştırılır.

Tarayıcıdan `https://localhost:7174/` adresine istek atılır.

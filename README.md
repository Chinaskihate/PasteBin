TODO:
- логгер в эластик нормальный https://learn.microsoft.com/ru-ru/dotnet/core/extensions/logging?tabs=command-line
- аутентификация
- создание топика
    - рефактор создание shorturl
        - отдельный сервис? что то должно генерировать урлы и помнить созданные, 1 к 1 между guid и урлом может не выйти
            - хэш? хз
            - base64 + обрезать строку
    - не проверять на каждом эдите юзера, проверка при джойне в группу
    - сохранение содержимого частых топиков в кэш, бэкграунд сервис обновляет их в с3 периодично
- ресурсы в ошибках, сообщениях и тд
- change topicmetadata to record
- логгирование ef ?
- шифрование паролей в appsettings/vault
- метрики
- по генерации урлов - сервис другой в бэкграунде создает рандомные гуиды, чекает что нет в бд такого урла. делает пачку (в пачке тоже не повторяется), эта пачка будет храниться в каком то кеше (редиска). основной сервис по надобности оттуда дергает урл, желательно самый старый.
- в сервисе генерации урлов сделать какое-то ограничение на количество урлов в кэше. при этом поставить задержку на апи чтоб не был пустым список урлов
- мб потом перевод на hangfire
- пофиксить регистрацию в GeneratorService (без синглтонов)


действия по развертке:
1) в докере
- выписать dotnet dev-cert в корневую папку проекта
- расписать что нужно в .env файле

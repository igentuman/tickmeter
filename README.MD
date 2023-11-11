# TickMeter - программа для измерения тикрейта, пинга и трафика в играх и приложениях.
[ENG version vvvvvv](#eng)


[https://discord.gg/ZuCn4UJ](https://discord.gg/ZuCn4UJ)

[https://www.youtube.com/watch?v=TxLZkME0mnY](https://www.youtube.com/watch?v=TxLZkME0mnY)


Если очень любопытно какой в игре тикрейт и пинг на серваках (PUBG, Dead by Daylight, Quake Champions и тд), то это очень простое и удобное решение, да еще и с открытым кодом. 

P.S. С VPN не дружит. VPNы делают так, что бы нельзя было трафик подслушивать.

Для работы требуется:

[https://www.microsoft.com/en-us/download/details.aspx?id=30653](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

[https://npcap.com/#download](https://npcap.com/#download)

[https://www.microsoft.com/ru-RU/download/details.aspx?id=40784](https://www.microsoft.com/ru-RU/download/details.aspx?id=40784)

Для более эффектиного маппинга процесса к сетевому пакету рекомендуется запускать от Администратора

За поддержку спасибо Andaletru, Susmix, Юрас, triton, Bad Koala и др.


## Актуальная версия 2.3

### Функционал
 - Мониторинг всех сетевых пакетов. Годно для анализа сетевой активности чего угодно
 - Измерение тикрейта в играх и приложениях - главнейшая фича
 - Измерение пинга и определение локации IP адреса - вторая по главности фича
 - Мониторинг исходящего/входящего трафика
 - Мониторинг потерь
 - Время подключения - довольно полезная фича
 - График тикрейта.
 - Экспорт тикрейта в CSV. Можно графики строить в экселе
 - Логгирование всех подключений с указанием IP, Location, Ping, avg Tickrate. Вы просили - я сделал
 - Настройка собственных профилей для отслеживания под любое приложение или игру. Настроил фильтр и мониторишь
 - Вывод данных в оверлее. Без оверлея мониторить не получится жеж
 - Волшебные графики тикрейта и тиктайма
 - Автоматический мониторинг активного окна (если включено в настройках)

## CHANGELOG

### v2.3
[https://github.com/igentuman/tickmeter/releases/tag/2.3](https://github.com/igentuman/tickmeter/releases/tag/2.3)

 - Статистика дропа пакетов

### v2.2
[https://github.com/igentuman/tickmeter/releases/tag/2.2](https://github.com/igentuman/tickmeter/releases/tag/2.2)

 - Исправление крашей RTSS
 - Улучшение автоматического треккинга
 - Исправления в работе с коллекциями данных и прочие улучшения

### v2.1
[https://github.com/igentuman/tickmeter/releases/tag/2.1](https://github.com/igentuman/tickmeter/releases/tag/2.1)

 - Атоматическое отслеживание активного окна
 - Обработки всяких исключений (улучшена стабильность)
 - Улучшения производительности
 - Более удачный маппинг пакетов к процессам с использованием ETW
 - Сворачивание в трей и настройка для запуска в трее

### v2.0
[https://github.com/igentuman/tickmeter/releases/tag/2.0](https://github.com/igentuman/tickmeter/releases/tag/2.0)


- Исправлен баг на странице статистики пакетов
- Добавлено автоопределение сетевой активности
- График пинга
- Новые опции настройки
- Фиксы багов


### v1.9


- Исправлены краши RTSS оверлея
- Настройка портов для пинга
- Автодетект запущеной сетевой игры
- Статистика по тикрейту (анонимная аналитика)
- Небольшие фиксы


### v1.8


- Фиксы стабильности графиков
- Функционал отправки данных

### v1.7


- Добавлены 2 встроенных профиля Islands of nyne и Quacke Champions
- Вывод графика тикрейта и тик тайм через RTSS
- Несколько багфиксов приводивших к крашам

### v1.6


- Окно мониторинга сетевых пакетов с поддержкой фильтрации
- Создание и использование игровых профилей для мониторинга в оверлее
- Вывод времени подключения
- Улучшен механизм пингования
- Скрытая настройка интервала пингования в settings.ini (по вашим просьбам)
- Цикл мониторинга пакетов вынесен на отдельный поток (мультипоток приди)
- Не слабо переписан код. В идеале, все должно стать лучше, но это не точно.
- Логгирование подключений в отдельном файле. Для статистики.

### v1.5


- Чтение/сохранение настроек
- Отдельное окно настроек
- Настройки цвета оверлея
- Помощник для RTSS и WinPCAP
- Автоматическая проверка обновлений
- Исправлена куча ошибок

### v1.4


- Переписан код
- Более стабильная и точная работа

### v1.3


- English translation
- Список подключений с IP

### v1.2 

- вывод графика тикрейта


### v1.1 

- добавлена поддержка вывода в RTSS
- учет трафика

----------------------

# ENG
# TickMeter - opensource tool for monitoring tickrate, ping and traffic in games and apps.

If you curious about ping and tickrate in games (PUBG, Dead by Daylight, Quake Champions, etc.), this is easy way to track this data. It has 2 built in game profiles for monitoring and easy way to create custom game profile to track any game.

P.S. It doesn't support VPN. And probably this won't change


Requires:

[https://www.microsoft.com/en-us/download/details.aspx?id=30653](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

[https://npcap.com/#download](https://npcap.com/#download)

[http://www.microsoft.com/en-us/download/details.aspx?id=14632](http://www.microsoft.com/en-us/download/details.aspx?id=14632)

Recommended to run as Administrator in order to get better packet mapping to process


### Functionality
 - All network packets monitoring window
 - Tickrate meater - major feature
 - Ping and IP location detection. Second majority feature
 - Track upload/download traffic
 - Drops monitoring
 - Connection time - useful sometimes
 - Tickrate chart
 - Export tickrate history to CSV - to build great charts in Excell
 - All game connections logging - you asked for this
 - Custom game profiles
 - Overlay output

### v2.3
[https://github.com/igentuman/tickmeter/releases/tag/2.3](https://github.com/igentuman/tickmeter/releases/tag/2.3)

 - Packet drops statistics


### v2.1
[https://github.com/igentuman/tickmeter/releases/tag/2.1](https://github.com/igentuman/tickmeter/releases/tag/2.1)

 - Automatic Tracking for active window
 - Stability improvements by handling some exceptions
 - Better perfomance
 - Better packet mapping to process logic with ETW
 - Minimize to tray and config option "Run minimized"

## CHANGELOG
### v2.0
[https://github.com/igentuman/tickmeter/releases/tag/2.0](https://github.com/igentuman/tickmeter/releases/tag/2.0)

- Packet Stats grid crash fixed
- Autodetection of active game network activity
- Ping Chart
- New settings
- Bugfixes

### v1.9


- RTSS crashes fix
- Ping ports settings
- Network game autodetect
- Tickrate charts
- Other fixes

### v1.8


- Charts stability fixes
- Data reports functionality

### v1.7


- Two built-in profules were added Islands of nyne and Quacke Champions
- Tickrate and ticktime charts via RTSS
- Crash avoinding bugfixes and improvements

### v1.6


- All network packets monitoring window + filtering
- Custom game profiles
- Connection time
- Ping functionality improvements
- ping_interval in settings.ini
- Dedicated PCAP thread
- GREAT code refactoring. All will work fine, i think...
- Connections loggin

### v1.5


- Settings read/wrinte settings.ini
- separate settings window
- Overlay color settings
- RTSS and WinPCAP helper
- Autoupdate checks
- Lot of bugfixes and improvements

### v1.4


- Refactoring
- Packet timestamp

### v1.3


- English translation
- adapter selector with IP

### v1.2 


- tickrate chart

### v1.1 

- RTSS improvements
- traffic

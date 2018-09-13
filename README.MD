# TickMeter - программа для измерения тикрейта, пинга и трафика в играх и приложениях.
[ENG version vvvvvv](#eng)

[![N|Solid](https://it-man.website/res/pics/ic-96.png)](https://it-man.website)[![N|Solid] (https://it-man.website/res/pics/yt.png)](https://www.youtube.com/watch?v=TxLZkME0mnY)  [![N|Solid](https://it-man.website/res/pics/ds.png)](https://discord.gg/ZuCn4UJ)

Если очень любопытно какой в игре тикрейт и пинг на серваках (PUBG, Dead by Daylight, Quake Champions и тд), то это очень простое и удобное решение, да еще и с открытым кодом. 

P.S. С VPN не дружит. Такие дела. Попробую когда-нибудь сделать.

## Актуальная версия 1.9

**Пубжисты, которые боятся, что их забанят читают** [это.](https://bitbucket.org/dvman8bit/tickmeter/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D1%8B%D0%B5%20%D1%80%D0%B8%D1%81%D0%BA%D0%B8%20%7C%20Possible%20risks)


### Функционал
 - Мониторинг всех сетевых пакетов. Годно для анализа сетевой активности чего угодно
 - Измерение тикрейта в играх и приложениях - главнейшая фича
 - Измерение пинга и определение локации IP адреса - вторая по главности фича
 - Мониторинг исходящего/входящего трафика
 - Время подключения - довольно полезная фича
 - График тикрейта. Пока только на формочке
 - Экспорт тикрейта в CSV. Можно графики строить в экселе
 - Логгирование всех подключений с указанием IP, Location, Ping, avg Tickrate. Вы просили - я сделал
 - Настройка собственных профилей для отслеживания под любое приложение или игру. Настроил фильтр и мониторишь
 - Вывод данных в оверлее. Без оверлея мониторить не получится жеж
 - Волшебные графики тикрейта и тиктайма
 - Отправка анонимных данных на сервер для анализа и статистики https://it-man.website/tickmeter/stats

## SCREENS
[![main window|N](https://it-man.website/tickmeter/screens/1-1.jpg =250x)](https://it-man.website/tickmeter/screens/1.jpg)   [![main window|N](https://it-man.website/tickmeter/screens/2-1.jpg =250x)](https://it-man.website/tickmeter/screens/2.jpg)
[![main window](https://it-man.website/tickmeter/screens/3-1.jpg =250x)](https://it-man.website/tickmeter/screens/3.jpg)   [![main window](https://it-man.website/tickmeter/screens/4-1.jpg =250x)](https://it-man.website/tickmeter/screens/4.jpg)

## CHANGELOG
### v1.9
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.9.zip

https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.9-installer.exe


- Исправлены краши RTSS оверлея
- Настройка портов для пинга
- Автодетект запущеной сетевой игры
- Статистика по тикрейту (анонимная аналитика)
- Небольшие фиксы


### v1.8
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.8.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.8-installer.exe

- Фиксы стабильности графиков
- Функционал отправки данных

### v1.7
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.7.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.7-installer.exe

- Добавлены 2 встроенных профиля Islands of nyne и Quacke Champions
- Вывод графика тикрейта и тик тайм через RTSS
- Несколько багфиксов приводивших к крашам

### v1.6
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.6.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.6-installer.exe

- Окно мониторинга сетевых пакетов с поддержкой фильтрации
- Создание и использование игровых профилей для мониторинга в оверлее
- Вывод времени подключения
- Улучшен механизм пингования
- Скрытая настройка интервала пингования в settings.ini (по вашим просьбам)
- Цикл мониторинга пакетов вынесен на отдельный поток (мультипоток приди)
- Не слабо переписан код. В идеале, все должно стать лучше, но это не точно.
- Логгирование подключений в отдельном файле. Для статистики.

### v1.5
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.5.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.5-installer.exe

- Чтение/сохранение настроек
- Отдельное окно настроек
- Настройки цвета оверлея
- Помощник для RTSS и WinPCAP
- Автоматическая проверка обновлений
- Исправлена куча ошибок

### v1.4
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.4.zip

- Переписан код
- Более стабильная и точная работа

### v1.3
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.3.zip

- English translation
- Список подключений с IP

### v1.2 
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.2.zip

- вывод графика тикрейта

### v1.1 
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.1.zip

- добавлена поддержка вывода в RTSS
- учет трафика

----------------------

# ENG
# TickMeter - opensource tool for monitoring tickrate, ping and traffic in games and apps.

If you curious about ping and tickrate in games (PUBG, Dead by Daylight, Quake Champions, etc.), this is easy way to track this data. It has 2 built in game profiles for monitoring and easy way to create custom game profile to track any game.

P.S. It doesn't support VPN atm. But, i hope, i will add it soon

**PUBG players who wondering if Battleye can ban them ** [read this at first.](https://bitbucket.org/dvman8bit/tickmeter/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D1%8B%D0%B5%20%D1%80%D0%B8%D1%81%D0%BA%D0%B8%20%7C%20Possible%20risks)


### Functionality
 - All network packets monitoring window
 - Tickrate meater - major feature
 - Ping and IP location detection. Second majority feature
 - Track upload/download traffic
 - Connection time - useful sometimes
 - Tickrate chart
 - Export tickrate history to CSV - to build great charts in Excell
 - All game connections logging - you asked for this
 - Custom game profiles
 - Overlay output


## CHANGELOG
### v1.9
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.9.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.9-installer.exe

- RTSS crashes fix
- Ping ports settings
- Network game autodetect
- Tickrate charts
- Other fixes

### v1.8
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.8.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.8-installer.exe

- Charts stability fixes
- Data reports functionality

### v1.7
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.7.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.7-installer.exe

- Two built-in profules were added Islands of nyne and Quacke Champions
- Tickrate and ticktime charts via RTSS
- Crash avoinding bugfixes and improvements

### v1.6
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.6.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.6-installer.exe

- All network packets monitoring window + filtering
- Custom game profiles
- Connection time
- Ping functionality improvements
- ping_interval in settings.ini
- Dedicated PCAP thread
- GREAT code refactoring. All will work fine, i think...
- Connections loggin

### v1.5
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.5.zip
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.5-installer.exe

- Settings read/wrinte settings.ini
- separate settings window
- Overlay color settings
- RTSS and WinPCAP helper
- Autoupdate checks
- Lot of bugfixes and improvements

### v1.4
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.4.zip

- Refactoring
- Packet timestamp

### v1.3
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.3.zip

- English translation
- adapter selector with IP

### v1.2 
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.2.zip

- tickrate chart

### v1.1 
https://bitbucket.org/dvman8bit/tickmeter/downloads/tickMeter-1.1.zip

- RTSS improvements
- traffic
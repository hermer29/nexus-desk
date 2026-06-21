# Nexus Desk — Roadmap

Линейный список задач для solo-разработки. Отмечай выполненное: `- [x]`.

---

## 0. Основа проекта

- [x] Создать Unity-проект (URP или Built-in — зафиксировать выбор) и пустую сцену `Boot`
- [x] Настроить структуру папок: `Art`, `Audio`, `Data`, `Prefabs/UI`, `Scripts`, `Scenes`, `Tests`
- [x] Подключить TextMeshPro и создать базовые UI-шрифты (Regular, Bold, Mono)
- [x] Создать UI Sprite Atlas и собрать в него фоны, кнопки, иконки, 9-slice
- [-] Задать цветовую палитру и UI Style Guide (ScriptableObject или static theme)
- [x] Создать сцену `MainDesk` и настроить EventSystem + Input System (если используешь новый Input)
- [ ] Сделать `GameBootstrap`: загрузка `MainDesk`, инициализация сервисов, без логики геймплея в MonoBehaviour «богов»

## 1. Архитектура UI

- [ ] Реализовать `ScreenStack` (push/pop/replace, один активный экран, overlay-слой)
- [ ] Разделить Canvas на слои: `Static`, `Dynamic`, `Overlay`, `Modal`
- [ ] Отключить `Raycast Target` у всех декоративных `Image` в базовых prefab'ах
- [ ] Включить `Pixel Perfect` / reference resolution и Safe Area для разных aspect ratio
- [ ] Сделать базовый prefab экрана: header, content, footer, loading/error states
- [ ] Реализовать `UIScreen` base class: `OnShow`, `OnHide`, `OnRefresh`, подписка/отписка от событий
- [ ] Добавить глобальный `UIEventBus` (или аналог) для слабой связи между экранами
- [ ] Реализовать MVVM-слой: `View` ↔ `ViewModel` ↔ `Model`, без прямых ссылок View на gameplay-системы

## 2. Общие виджеты

- [ ] Prefab кнопки: normal/hover/pressed/disabled + звук клика
- [ ] Prefab toggle / checkbox / radio group
- [ ] Prefab dropdown (TMP) с кастомным стилем
- [ ] Prefab slider + value label без аллокаций в Update
- [ ] Prefab progress bar (fill + optional pulse на критическом пороге)
- [ ] Prefab tooltip (delayed show, follow cursor, clamp в экран)
- [ ] Prefab toast notification (queue, auto-dismiss, pool-ready API)
- [ ] Prefab modal dialog (title, body, 1–3 кнопки, блокировка input под модалкой)
- [ ] Prefab tab bar с переключением панелей без пересоздания всего экрана
- [ ] Prefab loading spinner и fullscreen blocker

## 3. Command Deck (главный экран)

- [ ] Сверстать layout Command Deck: KPI-панель, зона алертов, быстрые протоколы, статус систем
- [ ] Создать `DashboardViewModel` и mock-данные KPI (энергия, связь, мораль, риск)
- [ ] Обновлять KPI через binding без полного refresh экрана
- [ ] Реализовать цветовые пороги KPI (ok / warning / critical) через theme tokens
- [ ] Добавить панель «Active Alerts» (последние N, клик → детали инцидента)
- [ ] Добавить панель «Quick Protocols» (кнопки с cooldown и confirm для опасных)
- [ ] Добавить часы смены / таймер волны инцидентов (один источник игрового времени)
- [ ] Профилировать Command Deck: зафиксировать baseline FPS, UI Rebuild, draw calls

## 4. Live Feed (лента событий)

- [ ] Создать модель `FeedEntry` (id, timestamp, severity, category, message, metadata)
- [ ] Создать `FeedService`: генерация mock-событий с настраиваемой частотой
- [ ] Реализовать виртуализированный `ScrollRect` (recycled items, только видимые строки)
- [ ] Сделать prefab `FeedItem` (иконка severity, время, текст, optional badge)
- [ ] Поддержать фильтры: severity, category, search (без пересоздания всего списка)
- [ ] Поддержать pause/resume ленты и «jump to latest»
- [ ] Довести ленту до 10 000 записей в модели при плавном скролле 60 FPS
- [ ] Убрать GC из обновления текста в ленте (кэш, StringBuilder, без `$""` в hot path)

## 5. Incident Board (канбан)

- [ ] Создать модель `Incident` (id, title, priority, sector, deadline, status, assignedProtocol)
- [ ] Создать `IncidentService`: spawn, escalate, resolve, fail
- [ ] Сверстать канбан: колонки Backlog / In Progress / Resolved / Failed
- [ ] Реализовать drag-and-drop карточек между колонками с валидацией правил
- [ ] Prefab `IncidentCard` (priority stripe, timer, sector tag, assignee slot)
- [ ] Анимация drag без LayoutGroup rebuild каждый кадр (cached sizes / manual layout)
- [ ] Сортировка внутри колонки (priority, deadline) без Instantiate/Destroy
- [ ] Клик по карточке открывает боковую панель деталей инцидента
- [ ] Связать изменения канбана с KPI и Live Feed (события в шину)

## 6. Crew Comms (чат экипажа)

- [ ] Создать модель `ChatMessage` (author, channel, text, replyTo, attachments, read state)
- [ ] Создать `CommsService`: каналы (General, Engineering, Medical, Security)
- [ ] Сверстать экран чата: список каналов, message list, input, send
- [ ] Виртуализировать список сообщений (отдельный scroll pool от Live Feed)
- [ ] Prefab `ChatBubble` (left/right, mention highlight, timestamp)
- [ ] Поддержать @mentions и кликабельные ссылки на инциденты/секторы
- [ ] Индикатор непрочитанных по каналам без полного пересканирования UI
- [ ] Mock-бот экипажа: ответы с задержкой, ветки reply

## 7. Sector Map (2D-карта)

- [ ] Создать модель `Sector` (id, name, status, risk, linkedIncidents)
- [ ] Сверстать карту: фон, сетка секторов, legend, minimap controls
- [ ] Prefab `SectorNode` (status color, icon, pulse on critical)
- [ ] Pan/zoom карты (clamp bounds, smooth zoom, без World Space камеры если не нужно)
- [ ] Object pool для иконок статуса и всплывающих маркеров на карте
- [ ] Клик по сектору: highlight + панель деталей + фильтр инцидентов по сектору
- [ ] Синхронизировать статусы секторов с IncidentService в реальном времени

## 8. Resource Ledger (таблицы ресурсов)

- [ ] Создать модель `ResourceRow` (name, current, cap, delta, trend)
- [ ] Сверстать таблицу: header, sortable columns, row highlight on deficit
- [ ] Виртуализировать строки таблицы при 500+ ресурсах
- [ ] Реализовать сортировку по колонке без пересоздания всех row prefab'ов
- [ ] Добавить фильтр «только дефицит» и поиск по имени
- [ ] Sparkline/trend (простой UI-график линией или bar) без тяжёлых chart-библиотек

## 9. Protocol Terminal

- [ ] Создать модель `Protocol` (command, description, cost, cooldown, success chance)
- [ ] Сверстать терминал: history, input line, autocomplete popup, output log
- [ ] Парсер команд (минимум: help, status, assign, resolve, boost, scan)
- [ ] Autocomplete список с виртуализацией и keyboard navigation
- [ ] История команд (↑/↓) без аллокаций на каждый ввод
- [ ] Ошибки и success feedback в output log (цвета, timestamps)
- [ ] Опасные команды требуют confirm modal из общего prefab'а

## 10. Audit Timeline

- [ ] Создать модель `AuditEvent` (actor, action, target, timestamp)
- [ ] Сверстать горизонтальный timeline с zoom in/out
- [ ] Виртуализировать события на таймлайне при большом горизонте
- [ ] Клик по событию: jump к инциденту/сообщению/сектору
- [ ] Фильтры по типу действия и временному окну

## 11. Игровой цикл (логика без 3D)

- [ ] Реализовать `ShiftController`: старт смены, длительность, пауза, конец смены
- [ ] Реализовать `WaveController`: нарастающая частота инцидентов по фазам
- [ ] Реализовать win/lose условия только через KPI и failed incidents
- [ ] Экран briefing перед сменой (цели, ограничения, tutorial toggles)
- [ ] Экран debrief после смены (статистика, timeline highlights, grade)
- [ ] Сохранение лучшего результата и разблокировки «сложных протоколов» (локально)

## 12. Object Pooling (сквозная задача)

- [ ] Сделать generic `UIPool<T>` для toast, alerts, feed items, chat bubbles, map markers
- [ ] Перевести burst-сценарий алертов на pool (30+ за 2 сек без spikes)
- [ ] Добавить warm-up пулов на загрузке сцены для критичных prefab'ов
- [ ] Метрики пула: in-use / available / peak (debug overlay)

## 13. Addressables и загрузка

- [ ] Настроить Addressables группы: Core UI, Screens, Audio
- [ ] Lazy load тяжёлых экранов (Audit Timeline, Sector Map) при первом открытии
- [ ] Unload UI-экранов при закрытии с освобождением неиспользуемых assets
- [ ] Boot flow: loading bar → preload core → MainDesk

## 14. Аудио и juice (всё ещё UI-first)

- [ ] SFX: click, alert, message, success, failure, terminal keystroke
- [ ] Адаптивная музыка по уровню critical KPI (слои, не 3D)
- [ ] Micro-animations: panel transitions через `CanvasGroup` + `RectTransform`
- [ ] Critical alert pulse на Command Deck без rebuild Static Canvas

## 15. Settings и доступность

- [ ] Экран Settings: UI scale, fullscreen/windowed, volume, reduce motion
- [ ] Reduce motion отключает тяжёлые анимации и pulse
- [ ] Hotkeys: переключение экранов, pause, focus terminal, dismiss modal
- [ ] Локализация-ready: все строки через keys (хотя бы EN + заготовка под RU)

## 16. Производительность (чеклисты-задачи)

- [ ] Зафиксировать perf budget документ (FPS, GC/frame, UI Rebuild, draw calls)
- [ ] Прогнать stress test: 10k feed + 200 incidents + active chat + map updates
- [ ] Убрать все `ContentSizeFitter` с hot path (или заменить на cached layout)
- [ ] Проверить каждый экран в Profiler: нет лишних `Canvas.SendWillRenderCanvases` rebuild
- [ ] Включить Frame Debugger и сократить draw calls до целевого бюджета
- [ ] Проверить отсутствие `Instantiate/Destroy` в Update у всех экранов
- [ ] EditMode тесты: virtual list indexing, pool get/release, sort comparator
- [ ] PlayMode тест: 60 секунд stress без роста памяти и без GC spikes

## 17. Данные и контент

- [ ] ScriptableObject `IncidentTemplate` (10–20 шаблонов)
- [ ] ScriptableObject `ProtocolDefinition` (15–25 команд/протоколов)
- [ ] ScriptableObject `CrewMember` (имя, роль, avatar icon)
- [ ] ScriptableObject `SectorDefinition` (layout позиции на карте)
- [ ] Минимальный набор narrative событий для 1 полной смены (30–45 мин)

## 18. Полировка и релиз-готовность

- [ ] Единый error handling UI (network/mock failures, corrupted save)
- [ ] Debug overlay (FPS, pools, event bus stats) только в dev build
- [ ] Проход по всем экранам на 16:9, 16:10, 21:9, 4:3
- [ ] Tutorial: 5 шагов onboarding прямо в Command Deck
- [ ] Main menu: New Shift / Continue / Settings / Quit
- [ ] Build pipeline: dev / release, dev без debug overlay
- [ ] README для проекта: как запустить, как профилировать UI, известные лимиты

---

**Как пользоваться:** выполнил задачу → отметь чекбокс → попроси проверить по номеру или названию.

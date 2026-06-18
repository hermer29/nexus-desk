# Nexus Desk

**Nexus Desk** — учебный Unity-проект, в котором почти весь геймплей построен на UI. Игрок выступает оператором кризисного штаба: удерживает систему от коллапса, переключаясь между панелями, лентами событий, канбаном инцидентов, чатом экипажа и терминалом протоколов.

Проект задуман как практикум по **производительному UI** в Unity: виртуализация списков, pooling, разделение Canvas, MVVM, профилирование и stress-тесты.

---

## Концепция

Сеттинг: колония / станция / город в режиме постоянных инцидентов. Нет 3D-персонажа, камеры и игрового мира — только интерфейсы, звук и (опционально) статичный фон.

### Игровой цикл

1. **Briefing** — цели смены, ограничения, краткий tutorial.
2. **Смена** — волны инцидентов, растущая нагрузка на UI.
3. **Реакция** — приоритизация на канбане, протоколы в терминале, решения на Command Deck.
4. **Последствия** — KPI, сообщения в чате, статусы на карте секторов.
5. **Debrief** — статистика, оценка смены, разблокировки.

Победа и поражение определяются только состоянием систем (KPI, failed incidents), без «боевой» механики.

---

## Экраны

| Экран | Назначение |
|-------|------------|
| **Command Deck** | Главный дашборд: KPI, алерты, быстрые протоколы |
| **Live Feed** | Поток событий в реальном времени (тысячи записей) |
| **Incident Board** | Канбан инцидентов с drag-and-drop |
| **Crew Comms** | Чат экипажа с каналами и @mentions |
| **Sector Map** | 2D-карта секторов со статусами и рисками |
| **Resource Ledger** | Таблицы ресурсов, сортировка, фильтры |
| **Protocol Terminal** | Ввод команд, autocomplete, история |
| **Audit Timeline** | Горизонтальный таймлайн действий с zoom |
| **Settings** | Масштаб UI, звук, доступность, hotkeys |

---

## Технический фокус

Проект намеренно включает задачи, критичные для быстрого UI:

- **Canvas layers** — Static / Dynamic / Overlay / Modal, минимум лишних rebuild
- **Virtualized lists** — Live Feed, чат, таблицы (10k+ элементов в модели)
- **Object pooling** — алерты, toasts, маркеры на карте
- **Layout без rebuild** — канбан, drag-and-drop, кэш размеров
- **TMP без GC** — форматирование строк вне hot path
- **Raycast optimization** — отключение лишних `Raycast Target`
- **Sprite atlases** — снижение draw calls
- **MVVM / event bus** — точечное обновление виджетов
- **Addressables** — lazy load тяжёлых экранов
- **Profiler budget** — измеримые цели по FPS, GC, UI Rebuild

### Целевые метрики (1080p, mid PC)

| Метрика | Цель |
|---------|------|
| FPS | 60 стабильно |
| UI Rebuild | < 2 ms/кадр в пике |
| GC | < 200 B/кадр в gameplay |
| Draw calls (UI) | < 80 на главном экране |
| RAM UI | < 150 MB после 30 мин сессии |

---

## Структура проекта (план)

```
Assets/
  _Project/
    Art/UI/              # Atlases, 9-slice, icons
    Audio/
    Data/                # ScriptableObjects
    Prefabs/UI/
      Canvases/
      Widgets/
      Screens/
      Pool/
    Scripts/
      Core/
      UI/
        Architecture/
        Virtualization/
        Pooling/
        Binding/
        Widgets/
      Systems/
    Scenes/
      Boot.unity
      MainDesk.unity
    Tests/
```

---

## Требования

- **Unity** 2022.3 LTS или новее (рекомендуется LTS)
- **TextMeshPro** (входит в Unity)
- Опционально: **Input System**, **Addressables**, **URP**

---

## Быстрый старт

1. Клонировать репозиторий.
2. Открыть проект в Unity Hub.
3. Дождаться импорта пакетов.
4. Открыть сцену `Assets/_Project/Scenes/Boot.unity` (после создания).
5. Play — загрузка `MainDesk`, старт смены с Command Deck.

> Проект в начальной стадии. Актуальный прогресс — в [ROADMAP.md](ROADMAP.md).

---

## Roadmap

Полный линейный список задач с чекбоксами: **[ROADMAP.md](ROADMAP.md)**

Выполняй задачи по порядку, отмечай `- [x]` и проси проверить конкретный пункт.

---

## Профилирование UI

1. **Window → Analysis → Profiler** — смотреть `UI`, `GC.Alloc`, `Canvas.SendWillRenderCanvases`.
2. **Frame Debugger** — draw calls и батчинг.
3. Stress-сценарий (после реализации): 10k feed + 200 incidents + активный чат + обновления карты.

Зафиксированный perf budget и чеклист — в разделе 16 roadmap.

---

## Лицензия

Учебный проект. Лицензию можно добавить позже.

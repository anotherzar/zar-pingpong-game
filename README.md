# Zar Ping Pong Game 🏓

Game Ping Pong klasik yang dikembangkan menggunakan **Unity Engine**. Dibuat untuk tugas mata kuliah Pemrograman Game 2D.

## ✨ Fitur Utama

- **Dynamic AI Opponent**:
  - **Difficulty Scaling**: Tingkat kesulitan AI bisa diatur dari 0.0 (Gampang) hingga 1.0 (Sangat Sulit).
  - **Noob Mode (LUCK Factor)**: Ada peluang 5% AI bakal mendadak "noob" (melambat drastis) dalam waktu singkat, memberikan kesempatan pemain untuk mencetak skor.
- **Scoring System**: Sistem perhitungan skor otomatis untuk Player dan AI.
- **Game State Management**: Dilengkapi dengan main menu, pause logic, dan game over screen.

## 🛠️ Teknologi yang Digunakan

- **Engine**: Unity 2022/2023+
- **Bahasa**: C# (Scripting)
- **Physics**: Rigidbody2D untuk pergerakan bola dan paddle.

## 🎮 Kontrol

- **W / Arrow Up**: Gerak ke Atas
- **S / Arrow Down**: Gerak ke Bawah

## 📁 Struktur Script Utama

- `Player.cs`: Logika pergerakan pemain.
- `OpponentAI.cs`: Logika AI dengan fitur *difficulty* dan *noob chance*.
- `PongBall.cs`: Logika pantulan dan kecepatan bola.
- `GameManager.cs`: Mengatur alur permainan (Start, Score, Game Over).
- `ScoreZone.cs`: Deteksi ketika bola masuk ke gawang.

---
Dibuat dengan ❤️ oleh **anotherzar**.

# Multiplayer Koşu Oyunu

Bu proje, Yalova Üniversitesi 2024 yılı Sanal Gerçeklik dersi kapsamında geliştirilen, Unity ve Mirror Network kullanılarak aynı ağ üzerinde multiplayer oynanabilen bir çocuk oyunudur. Proje, **Mert Kan** tarafından okul numarası **200101117** ile geliştirilmiştir.

## Proje Hakkında

Bu oyun, iki karakter (kedi ve köpek) arasından seçim yaparak oynanabilen basit bir engelli koşu oyunudur. Oyuncular, gelen engellerin üzerinden atlayarak puan kazanırlar. Her engelden başarıyla atlayan oyuncu puan kazanır, ancak bir engele çarparsa, başlangıçta 3 olan canlarından biri azalır.

Oyunun amacı, mümkün olduğunca uzun süre hayatta kalmak ve en yüksek puanı kazanmaktır. Oyun sonunda oyuncuya mevcut skor ve en yüksek skor gösterilir.

### Oyun Özellikleri
- **Multiplayer**: Aynı ağ üzerinden iki oyuncunun katılımıyla oynanabilir.
- **Karakterler**: İki karakter seçeneği - kedi ve köpek.
- **Engeller**: Oyun boyunca rastgele gelen engelleri aşmak için atlama yeteneği.
- **Can Sistemi**: Her oyuncunun başlangıçta 3 canı vardır, her engelle temas ettiğinde 1 can azalır.
- **Skor Sistemi**: Oyuncuların topladıkları puanlar ve oyun sonunda en yüksek skor gösterilir.

## Kullanılan Teknolojiler
- **Unity**: Oyun motoru olarak Unity kullanılmıştır.
- **Mirror Network**: Aynı ağ üzerinde multiplayer özellikleri sağlamak için Mirror Network kütüphanesi kullanılmıştır.
- **C#**: Oyun geliştirme ve mekaniklerin programlanması için C# dili kullanılmıştır.

## Kurulum ve Kullanım

1. **Unity Projesi**: Bu projeyi Unity'nin son sürümü ile açmanız gerekmektedir.
2. **Mirror Network**: Mirror Network eklentisinin yüklü olduğundan emin olun.
3. **Multiplayer Bağlantısı**: Oyun aynı ağ üzerindeki cihazlar arasında oynanabilir. Bir oyuncu sunucu olarak, diğer oyuncu istemci olarak bağlanmalıdır.

### Adım Adım Kurulum

1. Unity Hub üzerinden bu projeyi açın.
2. Projede Mirror eklentisinin kurulu ve doğru yapılandırılmış olduğundan emin olun.
3. Oyunu "Play" tuşuna basarak çalıştırın.
4. Bir oyuncu "Host" olarak başlasın, diğer oyuncu "Client" olarak bağlansın.
5. Oyuna başlayın ve karakterinizi seçerek oyunun keyfini çıkarın!


Bu proje Yalova Üniversitesi Sanal Gerçeklik dersi için geliştirilmiştir.



# Multiplayer Running Game

This project is a children's game developed for the Virtual Reality course at Yalova University in 2024. It was created using Unity and Mirror Network to allow multiplayer gameplay over the same network. The project was developed by **Mert Kan**, student ID **200101117**.

## About the Project

This is a simple obstacle running game where players choose between two characters (a cat or a dog). Players earn points by jumping over obstacles that appear during the game. Successfully jumping over an obstacle earns points, but hitting an obstacle causes the player to lose one of their three starting lives.

The objective of the game is to survive as long as possible and achieve the highest score. At the end of the game, the player's current score and the highest score are displayed.

### Game Features
- **Multiplayer**: Can be played by two players on the same network.
- **Characters**: Two character options - a cat and a dog.
- **Obstacles**: Random obstacles appear throughout the game, and players must jump to avoid them.
- **Life System**: Each player starts with 3 lives, and they lose 1 life when they hit an obstacle.
- **Score System**: Players' scores are tracked, and the current and highest scores are shown at the end of the game.

## Technologies Used
- **Unity**: The game was developed using the Unity game engine.
- **Mirror Network**: Mirror Network library was used to implement multiplayer functionality over the same network.
- **C#**: C# was used for programming game mechanics and logic.

## Installation and Usage

1. **Unity Project**: Open this project using the latest version of Unity.
2. **Mirror Network**: Ensure that the Mirror Network package is installed and properly configured.
3. **Multiplayer Connection**: The game can be played by two players on the same network. One player should act as the server (Host), and the other should connect as the client (Client).

### Step-by-Step Installation

1. Open the project via Unity Hub.
2. Ensure the Mirror plugin is installed and properly set up in the project.
3. Start the game by pressing the "Play" button.
4. One player should select "Host," while the other player connects as a "Client."
5. Enjoy the game by choosing your character and starting the obstacle challenge!



---

This project was developed for the Virtual Reality course at Yalova University.

# Meu Ambiente VR
**Web 3.0 | Residência em TIC 29**  
Aluno: Filipe Mazon | Prof.: Ana Beatriz

Projeto de VR desenvolvido com Unity 6 LTS (6000.3.14f1) e Meta XR SDK.
O jogador navega por um ambiente 3D com uma casa, coleta objetos pelo cenário e interage com um botão interativo.

---

## Estrutura do Projeto

```
MeuProjeto/
├── Assets/
│   ├── Scripts/
│   │   ├── GameManager.cs
│   │   ├── PlayerController.cs
│   │   ├── HUDView.cs
│   │   ├── ObjetoColetavelController.cs
│   │   ├── ObjetoColetavelView.cs
│   │   ├── BotaoPrincipalController.cs
│   │   └── BotaoPrincipalView.cs
│   ├── Editor/
│   │   ├── CasaBuilder.cs
│   │   ├── SceneBuilder.cs
│   │   ├── SceneAutoWire.cs
│   │   ├── SceneComponentWire.cs
│   │   └── FixCasa.cs
│   ├── Scenes/
│   │   └── scene1.unity
│   ├── Materials/
│   └── Prefabs/
├── Packages/
│   └── manifest.json
├── ProjectSettings/
└── .gitignore
```

---

## Arquitetura

O projeto usa uma separação MVC simples:

- **GameManager** — controla o estado global (pontuação, objetos coletados, eventos da cena)
- **PlayerController** — lida com input de teclado e detecta coleta por proximidade
- **Views** (`HUDView`, `ObjetoColetavelView`, `BotaoPrincipalView`) — cuidam só do visual
- **Controllers** (`ObjetoColetavelController`, `BotaoPrincipalController`) — lógica de cada objeto

---

## Requisitos

- Unity 6 (6000.3.14f1)
- Meta XR All-in-One SDK v60+
- TextMeshPro (já incluso no Unity)
- XR Plugin Management
- Android Build Support (para build no Quest)

---

## Como abrir o projeto

1. Abra o **Unity Hub** com o editor **6000.3.14f1** instalado
2. Clique em **Open** e selecione a pasta `MeuProjeto/`
3. Aguarde a importação dos pacotes (pode demorar na 1ª vez)
4. Abra a cena `Assets/scene1.unity`

### Instalar o Meta XR SDK

O `manifest.json` já inclui a dependência. Se não importar automaticamente:
1. **Window → Package Manager → "+" → Add package by name**
2. Nome: `com.meta.xr.sdk.all`

### Configurar XR Plugin Management

1. **Edit → Project Settings → XR Plugin Management**
2. Aba **PC**: marque **Oculus**
3. Aba **Android**: marque **Oculus**

### Build Settings para Android (Meta Quest)

1. **File → Build Settings → Android → Switch Platform**
2. **Player Settings → Other Settings:**
   - Minimum API Level: Android 10 (API 29)
   - Target API Level: Android 12 (API 31)
   - Texture Compression: ASTC
   - Scripting Backend: IL2CPP
   - Target Architectures: ARM64

---

## Hierarquia da cena

```
[--- MANAGEMENT ---]
  GameManager          → GameManager.cs
  EventSystem
  HUD_Canvas           → Canvas World Space + HUDView.cs
    Texto_Pontuacao
    Texto_Objetos
    Texto_Mensagem

[--- PLAYER ---]
  XROrigin             → PlayerController.cs  (tag: Player)

[--- ENVIRONMENT ---]
  Plane_Chao
  Directional Light
  Casa
    Paredes
    Chao_Interno

[--- INTERACTABLES ---]
  Objeto_Coletavel_01  → Chave   (10 pts)
  Objeto_Coletavel_02  → Cristal (20 pts)
  Objeto_Coletavel_03  → Moeda   (30 pts)
  Botao_Principal
```

### Conectar referências no Inspector

| GameObject | Campo | Arrastar |
|------------|-------|---------|
| `GameManager` | Hud View | `HUD_Canvas` |
| `GameManager` | Botao Principal | `Botao_Principal` |
| `GameManager` | Parede Entrada | `Parede_Frente_Topo` |
| `PlayerController` | Referencia Camera | `Main Camera` |
| `HUDView` | Texto Pontuacao | `Texto_Pontuacao` |
| `HUDView` | Texto Objetos | `Texto_Objetos` |
| `HUDView` | Texto Mensagem | `Texto_Mensagem` |

---

## Teste no Editor (sem headset)

| Ação | Controle |
|------|---------|
| Mover | WASD ou setas |
| Coletar objeto | Aproximar o player |
| Ativar botão | E ou clique do mouse |

---

## Build para Meta Quest

```
1. Conecte o Quest via USB com Modo Desenvolvedor ativo
2. File → Build Settings → Build and Run
```

---

## Scripts de Editor (pasta Tools/)

Esses scripts facilitaram a construção da cena, já que o desenvolvimento foi feito no Linux sem headset disponível:

- **Tools/Build Casa** — gera as paredes, teto e porta da casa via primitivos
- **Tools/Build Scene Structure** — cria a hierarquia base da cena
- **Tools/Auto-Wire Scene References** — conecta referências entre GameObjects automaticamente
- **Tools/Add Missing Components** — adiciona componentes faltando em cada GameObject
- **Tools/Fix Casa** — reaplica materiais e corrige posições da cena

---

*Web 3.0 | Residência em TIC 29 — 2026*

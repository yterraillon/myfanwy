# Myfanwy

Application .NET 10 modulaire servant de hub pour diverses fonctionnalités personnelles.

[![Build](https://github.com/yterraillon/checquy/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/yterraillon/checquy/actions/workflows/dotnet-build.yml)
[![Docker](https://github.com/yterraillon/checquy/actions/workflows/docker-publish.yml/badge.svg)](https://hub.docker.com/r/checquy/myfanwy)

## Modules

| Module | Description | Statut |
|--------|-------------|--------|
| **Thermo** | Affichage température, humidité et qualité de l'air (Eve Room) | ✅ Actif |
| **EnBref** | Résumés quotidiens d'actualités via OpenAI | ✅ Actif |
| **ComicGrabber** | Gestion et récupération de comics | ✅ Actif |
| **MealPicker** | Sélection et planification de repas | ✅ Actif |
| **MuscleRoutine** | Gestion de routines d'entraînement | ✅ Actif |
| **CineRoulette** | Sélection aléatoire de films | ✅ Actif |
| **Nanny** | Gestion de babysitting | ✅ Actif |
| **Turneu** | Gestion de tournois | ✅ Actif |

## Quick Start

### Prérequis

- .NET 10.0 SDK
- Docker (optionnel)

### Développement local

```bash
# Cloner le repo
git clone https://github.com/yterraillon/checquy.git
cd checquy/myfanwy

# Configurer les secrets
cd Web
dotnet user-secrets set "EnBrefConnectionString" "<connection-string>"
dotnet user-secrets set "OpenAiApiKey" "<api-key>"
dotnet user-secrets set "NtfyToken" "<token>"

# Build et run
cd ..
dotnet restore myfanwy.slnx
dotnet run --project Web
```

L'application sera disponible sur `https://localhost:5001`.

### Docker

```bash
cd Web
docker build -t myfanwy:local .
docker run -p 8080:8080 myfanwy:local
```

## Architecture

```
myfanwy/
├── Api/                    # Contrôleurs REST
├── BuildingBlocks/         # Code partagé
│   ├── Application/        # Abstractions (IRepository, etc.)
│   └── Infrastructure/     # Implémentations
├── Modules/                # Modules fonctionnels
│   ├── */Application/      # Logique métier
│   └── */Infrastructure/   # Accès données
├── Web/                    # UI Blazor SSR
├── Database/               # SQLite (Myfanwy.db)
└── tests/                  # Tests unitaires et E2E
```

### Technologies

- **Framework** : .NET 10, Blazor SSR
- **CSS** : Tailwind CSS, Bootstrap
- **ORM** : Entity Framework Core
- **CQRS** : MediatR
- **Scheduling** : Quartz.NET
- **Charts** : Blazor Bootstrap

## Modules en détail

### Thermo

Affiche les données environnementales collectées par un Eve Room via iOS Shortcut.

- **Endpoint** : `GET /Thermo`
- **Alimentation** : [Shortcut iCloud](https://www.icloud.com/shortcuts/eb03143bd7fb4254a88d22027cb84f2f) (à planifier toutes les heures)

### EnBref

Génère des résumés quotidiens d'actualités avec OpenAI et les stocke sur Azure Blob Storage.

- **Endpoint** : `GET /EnBref`
- **Planification** : Automatique via Quartz.NET
- **Stockage** : Azure Blob Storage

## Tests

```bash
# Tests unitaires
dotnet test myfanwy.slnx

# Tests E2E (nécessite Bruno CLI)
cd tests/EndToEnd/EnBref
bru run --env production
```

## Documentation

- [Architecture détaillée](../docs/architecture/myfanwy-architecture.md)
- [Configuration & Secrets](../docs/development/configuration.md)
- [Guide de développement](../docs/development/getting-started.md)
- [Tests](../docs/development/testing.md)

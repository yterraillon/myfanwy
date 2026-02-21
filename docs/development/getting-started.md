# Guide de Démarrage

## Prérequis

| Outil | Version | Usage |
|-------|---------|-------|
| .NET SDK | 10.0+ | Runtime et build |
| Docker | Récent | Déploiement (optionnel) |
| Node.js | 18+ | Compilation Tailwind CSS (optionnel) |
| Bruno CLI | Dernier | Tests E2E (optionnel) |

## Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/yterraillon/checquy.git
cd checquy/myfanwy
```

### 2. Restaurer les dépendances

```bash
dotnet restore myfanwy.slnx
```

### 3. Configurer les secrets

Les secrets sont gérés via .NET Secret Manager en local (jamais committés dans Git) :

```bash
cd Web
dotnet user-secrets set "EnBrefConnectionString" "<azure-blob-connection-string>"
dotnet user-secrets set "OpenAiApiKey" "<openai-api-key>"
dotnet user-secrets set "NtfyToken" "<ntfy-token>"
cd ..
```

→ Voir [Configuration & Secrets](configuration.md) pour le détail de chaque clé.

### 4. Lancer l'application

```bash
dotnet run --project Web
```

Application disponible sur `https://localhost:5001`.

## Développement CSS (Tailwind)

Tailwind CSS est compilé automatiquement via `Tailwind.Extensions.AspNetCore` au lancement en mode développement. Aucune action manuelle requise.

## IDE

- **JetBrains Rider** — configuration `.idea/` incluse dans le dépôt
- **Visual Studio 2022** (17.12+) — support du format `.slnx` requis
- **VS Code** — avec l'extension C# Dev Kit

## Docker en local

```bash
# Build de l'image locale
docker build -t myfanwy:local Web/

# Lancer le conteneur
docker run -p 8080:8080 \
  -e EnBrefConnectionString="<connection>" \
  -e OpenAiApiKey="<key>" \
  -e NtfyToken="<token>" \
  myfanwy:local
```

Application disponible sur `http://localhost:8080`.

## Commandes courantes

```bash
# Build de la solution complète
dotnet build myfanwy.slnx

# Tests unitaires
dotnet test myfanwy.slnx

# Nettoyer les artefacts de build
dotnet clean myfanwy.slnx
```

→ Voir [Stratégie de tests](testing.md) pour les tests unitaires et E2E.

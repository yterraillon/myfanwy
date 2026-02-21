# Architecture Globale

## Vue d'ensemble

Myfanwy est une application Blazor SSR modulaire construite en Clean Architecture avec le pattern CQRS. Elle regroupe plusieurs modules fonctionnels autour d'une infrastructure partagée.

```mermaid
graph TB
    User["Utilisateur"] --> Web["Web\nBlazor SSR :5001"]
    External["Services externes\n(iOS Shortcut, RSS)"] --> API["API REST\n/api/*"]

    Web --> MediatR["MediatR\n(CQRS Bus)"]
    API --> MediatR

    MediatR --> Modules["Modules\nThermo · EnBref · ComicGrabber\nMealPicker · MuscleRoutine\nCineRoulette · Nanny · Turneu"]

    Modules --> EF["Entity Framework Core"]
    Modules --> Blob["Azure Blob Storage"]
    Modules --> OpenAI["OpenAI API"]
    Modules --> Ntfy["Ntfy\n(Notifications)"]

    EF --> SQLite[(SQLite\nMyfanwy.db)]
```

## Stack Technique

| Couche | Technologies |
|--------|-------------|
| **Frontend** | Blazor SSR, Tailwind CSS, Blazor.Bootstrap |
| **Backend** | .NET 10, ASP.NET Core |
| **CQRS** | MediatR |
| **ORM** | Entity Framework Core (SQLite) |
| **Scheduling** | Quartz.NET |
| **IA** | OpenAI API |
| **Stockage** | Azure Blob Storage |
| **Notifications** | Ntfy |
| **Conteneurisation** | Docker |

## Déploiement

En production, Myfanwy tourne derrière SWAG (reverse proxy) avec SSL automatique via Let's Encrypt pour le domaine `checquy.ovh`.

```mermaid
graph LR
    Internet --> SWAG["SWAG\nReverse Proxy\n+ Let's Encrypt"]

    subgraph appNet["appNet"]
        SWAG --> Myfanwy["Myfanwy :8080"]
        SWAG --> Homepage["Homepage :3000"]
        SWAG --> n8n["n8n :5678"]
        SWAG --> Jellyseerr["Jellyseerr :5055"]
    end

    subgraph mediaNet["mediaNet"]
        SWAG --> Jellyfin["Jellyfin :8096"]
        SWAG --> Arr["*arr Stack"]
        SWAG --> Navidrome["Navidrome :4533"]
    end
```

## CI/CD

| Workflow | Déclencheur | Action |
|----------|-------------|--------|
| `dotnet-build.yml` | Push / PR sur `main`, `feature/**` | Build + Tests unitaires |
| `docker-publish.yml` | Release GitHub | Build image Docker multi-stage → Docker Hub |
| `e2e-testing-recap-publication.yml` | Quotidien 16h30 UTC | Tests E2E Bruno → notification Discord si échec |

---

→ Voir [Patterns applicatifs](myfanwy-architecture.md) pour l'architecture interne de l'application.
→ Voir [Guide de démarrage](../development/getting-started.md) pour le setup local.

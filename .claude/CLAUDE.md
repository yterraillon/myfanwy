# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Checquy is a personal infrastructure project consisting of:
- **Myfanwy**: A modular .NET 10 Blazor SSR application with CQRS architecture
- **Docker Compose ecosystem**: 42+ self-hosted services
- Reverse proxy (SWAG) with Let's Encrypt SSL for `checquy.ovh`

## Common Commands

### Myfanwy Development
```bash
cd myfanwy
dotnet restore myfanwy.slnx
dotnet build myfanwy.slnx
dotnet run --project Web
dotnet test myfanwy.slnx
```

### Docker Services
```bash
# Start a service
cd docker-compose/<service>
docker compose up -d

# View logs
docker compose logs -f

# Build local Myfanwy image
docker build -t myfanwy:local myfanwy/Web/
```

### E2E Tests (Bruno CLI)
```bash
npm install -g @usebruno/cli
cd myfanwy/tests/EndToEnd/EnBref
bru run --env production
```

## Architecture

### Myfanwy Structure (Clean Architecture + CQRS)
```
myfanwy/
├── Web/                 # Blazor SSR entry point, Tailwind CSS
├── Api/                 # REST controllers
├── BuildingBlocks/
│   ├── Application/     # Abstractions (IRepository, INotificationService)
│   └── Infrastructure/  # EF Core, Azure Blob, Ntfy implementations
├── Modules/             # Domain modules (Thermo, EnBref, ComicGrabber, etc.)
├── Database/            # SQLite files (Myfanwy.db)
└── tests/               # Unit tests (xUnit) + E2E (Bruno)
```

### Key Patterns
- **CQRS with MediatR**: Commands/queries in each module's Application layer
- **Module registration**: Each module has a `DependencyInjection.cs` that registers services
- **Centralized NuGet**: `Directory.Packages.props` manages all package versions

### Tech Stack
- .NET 10, Blazor SSR, Tailwind CSS, Entity Framework Core (SQLite)
- Quartz.NET for scheduling, Blazor.Bootstrap for UI components
- External: OpenAI (news summarization), Azure Blob Storage, Ntfy (notifications)

## Docker Network Topology
```
Internet → SWAG (reverse proxy) → Services
           ├── appNet: Myfanwy, Homepage, n8n, Jellyseerr, etc.
           └── mediaNet: Jellyfin, *arr stack, Navidrome
```

## Configuration

### Secrets (via .NET Secret Manager locally, env vars in Docker)
- `EnBrefConnectionString`: Azure Blob connection
- `OpenAiApiKey`: OpenAI API key
- `NtfyToken`: Notification service token

### Docker Logging Standard
All compose files use:
```yaml
logging:
  driver: json-file
  options:
    max-size: 10m
    max-file: "3"
```

## CI/CD

- **dotnet-build.yml**: Builds and tests on push/PR to main and feature/** branches
- **docker-publish.yml**: On release, builds multi-stage Docker image (includes Tailwind processing), pushes to Docker Hub
- **e2e-testing-recap-publication.yml**: Daily E2E tests at 16:30 UTC, Discord notification on failure

## Documentation

Detailed documentation exists in `/docs/`:
- `architecture/overview.md` - System architecture
- `architecture/myfanwy-architecture.md` - Application patterns
- `deployment/docker-services.md` - All 42+ services with ports
- `development/getting-started.md` - Local setup guide
- `development/testing.md` - Testing strategy

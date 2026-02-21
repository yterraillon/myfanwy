# Module Thermo

Affichage des données environnementales (température, humidité, qualité de l'air) collectées par un capteur Eve Room via un iOS Shortcut.

## Flux de données

```mermaid
sequenceDiagram
    participant SC as iOS Shortcut
    participant API as API REST
    participant DB as SQLite
    participant Page as Page Blazor

    SC->>API: POST /api/thermo\n{temp, humidity, voc}
    API->>DB: INSERT mesure
    Note over SC: Planifié toutes les heures

    Page->>DB: SELECT dernières mesures
    DB-->>Page: Historique
    Page-->>Page: Affichage graphiques
```

## Endpoints

| Méthode | Route | Description |
|---------|-------|-------------|
| `GET` | `/Thermo` | Page de visualisation |
| `POST` | `/api/thermo` | Réception d'une mesure depuis iOS Shortcut |

## Configuration iOS Shortcut

[Shortcut iCloud](https://www.icloud.com/shortcuts/eb03143bd7fb4254a88d22027cb84f2f) — à planifier toutes les heures via l'automatisation iOS.

## Structure

```
Modules/Thermo/
├── Thermo.Application/
│   ├── Commands/RecordMeasurement/
│   ├── Queries/GetMeasurements/
│   └── DependencyInjection.cs
└── Thermo.Infrastructure/
    ├── ThermoRepository.cs
    └── DependencyInjection.cs
```

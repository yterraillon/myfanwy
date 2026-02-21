# Stratégie de Tests

## Tests Unitaires (xUnit)

Les tests unitaires sont dans `tests/UnitTests/`, organisés par module.

### Lancer les tests

```bash
# Tous les tests
dotnet test myfanwy.slnx

# Avec rapport de couverture
dotnet test myfanwy.slnx --collect:"XPlat Code Coverage"

# Filtrer par module
dotnet test --filter "Category=EnBref"
```

### Structure

```
tests/UnitTests/
└── EnBref.UnitTests/
    ├── EnBref.UnitTests.csproj
    └── ...
```

### Conventions

- Un projet de tests par module : `{Module}.UnitTests`
- Classes nommées `{ClassUnderTest}Tests`
- Méthodes nommées `{Method}_{Scenario}_{ExpectedResult}`

## Tests E2E (Bruno CLI)

Les tests E2E valident les endpoints REST en conditions réelles.

### Installation

```bash
npm install -g @usebruno/cli
```

### Lancer les tests

```bash
cd tests/EndToEnd/EnBref
bru run --env production
```

### Structure

```
tests/EndToEnd/
└── EnBref/
    ├── bruno.json
    ├── environments/
    │   └── production.bru
    └── *.bru              # Fichiers de requêtes Bruno
```

## CI/CD

| Workflow | Tests exécutés |
|----------|---------------|
| `dotnet-build.yml` | Tests unitaires à chaque push/PR sur `main` et `feature/**` |
| `e2e-testing-recap-publication.yml` | Tests E2E Bruno quotidiens à 16h30 UTC |

Notification Discord en cas d'échec des tests E2E.

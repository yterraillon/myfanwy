# Configuration & Secrets

## Secrets applicatifs

| Clé | Description | Module |
|-----|-------------|--------|
| `EnBrefConnectionString` | Azure Blob Storage connection string | EnBref |
| `OpenAiApiKey` | Clé API OpenAI | EnBref |
| `NtfyToken` | Token d'authentification Ntfy | Notifications |

## En local (Secret Manager)

```bash
cd Web
dotnet user-secrets set "EnBrefConnectionString" "<connection-string>"
dotnet user-secrets set "OpenAiApiKey" "sk-..."
dotnet user-secrets set "NtfyToken" "<token>"
```

Les secrets sont stockés dans `%APPDATA%\Microsoft\UserSecrets\` (Windows) ou `~/.microsoft/usersecrets/` (Linux/Mac) — jamais dans le dépôt Git.

## En production (Variables d'environnement Docker)

```yaml
# docker-compose.yml
services:
  myfanwy:
    image: checquy/myfanwy:latest
    environment:
      - EnBrefConnectionString=${ENBREF_CONNECTION_STRING}
      - OpenAiApiKey=${OPENAI_API_KEY}
      - NtfyToken=${NTFY_TOKEN}
```

## appsettings.json

Configuration non-sensible dans `Web/appsettings.json` :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Configuration spécifique au développement dans `Web/appsettings.Development.json`.

## Logging Docker

Standard utilisé par tous les services Docker Compose :

```yaml
logging:
  driver: json-file
  options:
    max-size: 10m
    max-file: "3"
```

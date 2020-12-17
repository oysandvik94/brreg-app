# brreg-app

Webapplikasjon bygget på .NET Framework, React.js og PostgreSQL.
Applikasjonen lagrer informasjon om en bedrift. Applikasjonen kan hente informasjon fra Brønnøysregisteret for å lagre nye bedrifter.

https://data.brreg.no/enhetsregisteret/api/docs/index.html

## Requirements

Kjøre via docker:

* Docker
* docker-compose

Uten app uten docker:

* .NET Framework 5.0
* Node.js V15.0.0

## Kjøreinstrukser

### Docker (anbefalt)

Naviger til docker-compose

Kjør:
`docker-compose up -p`

### Bygg selv

Pull DB:
`docker pull oysandvik94/brreg-db:latest`

Kjør DB:
`docker run -d --name oysandvik94/brreg-db -p 5555:5432 brreg-db`

Naviger til `webapp`

Kjør app:
`dotnet run --launch-profile Production`

## Generate Model files
From WebApp folder:

`dotnet ef dbcontext scaffold "Host=localhost;Database=$DB_NAME;Username=$DB_USERNAME;Password=$DB_PASSWORD;Port=$DB_PORT" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force --data-annotations --context WebAppDbContext --no-pluralize`

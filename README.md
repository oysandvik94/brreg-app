# brreg-app

Webapplikasjon bygget på .NET Framework, React.js og PostgreSQL.
Applikasjonen lagrer informasjon om en bedrift. Applikasjonen kan hente informasjon fra Brønnøysregisteret for å lagre nye bedrifter.

https://data.brreg.no/enhetsregisteret/api/docs/index.html

## Requirements

Docker

## Quick start

Naviger til docker-compose

Kjør:
`docker-compose up -p`

## Bygg selv
Krever .NET Framework 5.0

Pull DB:
`docker pull oysandvik94/brreg-db:latest`

Kjør DB:
`docker run -d --name brreg-db -p 5555:5432 brreg-postgres-db`

Naviger til `webapp`

Kjør app:
`dotnet run --launch-profile Production`

## Generate Model files
From WebApp folder:

`dotnet ef dbcontext scaffold "Host=localhost;Database=$DB_NAME;Username=$DB_USERNAME;Password=$DB_PASSWORD;Port=$DB_PORT" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force --data-annotations --context WebAppDbContext --no-pluralize`

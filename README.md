# brreg-app

Webapplikasjon bygget på .NET Framework, React.js og PostgreSQL.
Applikasjonen lagrer informasjon om en bedrift. Applikasjonen kan hente informasjon fra Brønnøysregisteret for å lagre nye bedrifter.

Informasjon hentet fra:

https://data.brreg.no/enhetsregisteret/api/docs/index.html

https://data.norge.no/dataservices/4dabc5ea-b57b-3b68-9e48-2ff993be3f6e

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
`docker-compose up`

Compose filen bygger imagene selv, dette kan ta noen minutter. 

Applikasjonen er tilgjengelig på:
http://localhost:8000

### Bygg selv

Pull DB:
`docker pull oysandvik94/brreg-db:latest`

Kjør DB:
`docker run -d --name brreg-db -p 5555:5432 oysandvik94/brreg-db`

Naviger til `webapp`

Kjør app:
`dotnet run --launch-profile Production`

Applikasjonen er tilgjengelig på:
https://localhost:5001

## Generate Model files
From WebApp folder:

`dotnet ef dbcontext scaffold "Host=localhost;Database=$DB_NAME;Username=$DB_USERNAME;Password=$DB_PASSWORD;Port=$DB_PORT" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force --data-annotations --context WebAppDbContext --no-pluralize`

Test

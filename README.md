# brreg-app

## How to run

`docker run -d --name brreg-db -p 5555:5432 brreg-postgres-db`

## Generate Model files
From WebApp folder:

`dotnet ef dbcontext scaffold "Host=localhost;Database=$DB_NAME;Username=$DB_USERNAME;Password=$DB_PASSWORD;Port=$DB_PORT" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force --data-annotations --context WebAppDbContext --no-pluralize`

FROM postgres 
ENV DB_USER postgres
ENV POSTGRES_PASSWORD postgres 
ENV POSTGRES_DB brregdb 
COPY init.sql /docker-entrypoint-initdb.d/

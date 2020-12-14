-- Config settings

SET datestyle TO DMY;

-- Set up initial tables

CREATE TABLE organizations (
    orgNr int PRIMARY KEY,
    orgName varchar(255),
    orgType varchar(255),
    businessAddress varchar(255),
    municipality varchar(255),
    postAddress varchar(255),
    webAddress varchar(255),
    registrationDate DATE,
    foundationDate DATE,
    ceo varchar(255),
    objective varchar(255),
    businessType varchar(255),
    sectorCode varchar(255),
    registeredNotes varchar(255),
    board varchar(255),
    boardLeader varchar(255),
    boardDeputy varchar(255),
    boardMembers varchar(255),
    signature varchar(255),
    auditor varchar(255),
    accountant varchar(255)
);

CREATE TABLE businessCodes (
    businessCode varchar(255) PRIMARY KEY,
    name varchar(255) UNIQUE NOT NULL
);

CREATE TABLE subOrganizations (
    subOrgNr int PRIMARY KEY,
    subOrgName varchar(255),
    locationAddress varchar(255),
    postAddress varchar(255),
    municipality varchar(255),
    registeredNotes varchar(255),
    parentOrgNr int,
    FOREIGN KEY (parentOrgNr)
        REFERENCES organizations (orgNr)
);

CREATE TABLE businessCode_org (
    businessCode varchar(255) REFERENCES businessCodes(businessCode) ON UPDATE CASCADE,
    orgNr int REFERENCES organizations(orgNr) ON UPDATE CASCADE,
    CONSTRAINT businessCode_org_pkey PRIMARY KEY (businessCode, orgNr)
);

CREATE TABLE businessCode_subOrg (
    businessCode varchar(255) REFERENCES businessCodes(businessCode) ON UPDATE CASCADE,
    subOrgNr int REFERENCES subOrganizations(subOrgNr) ON UPDATE CASCADE,
    CONSTRAINT businessCode_subOrg_pkey PRIMARY KEY (businessCode, subOrgNr)
);

-- Insert testdata 

INSERT INTO businessCodes VALUES (
    '93.120',
    'Idrettslag og -klubber'
);

INSERT INTO businessCodes VALUES (
    '68.209',
    'Utleie av egen eller leid fast eiendom ellers'
);

INSERT INTO organizations VALUES (
    871023832,
    'FK HAUGESUND',
    'Forening/lag/innretning',
    'Karmsundgata 169
5522 HAUGESUND',
    'HAUGESUND',
    null,
    'www.fkh.no',
    '20-02-1995',
    '28-10-1993',
    'John Harald Log',
    null,
    'Fotball',
    '7000 Ideelle organisasjoner',
    'Registrert i Merverdiavgiftsregisteret
Frivillig registrering i Merverdiavgiftsregisteret:
- Utleier av bygg eller anlegg
Registrert i NAV Aa-registeret
Registrert i Frivillighetsregisteret
Sist innsendte årsregnskap 2019',
    null,
    'Leiv Helge Kaldheim',
    'Tore Lillenes',
    'Lillian Medby Morisbak
Synnøve Haftorsen Brakstad
Nils Halvor Berge
Trygve Nygaard',
    'Daglig leder alene.
Styrets leder alene.',
    null,
    null
);

INSERT INTO organizations VALUES (
    945725729,
    'AMANDA STORSENTER AS',
    'Aksjeselskap',
    'Støperigata 1
0250 OSLO',
    'OSLO',
    'Postboks 1593 Vika
0118 OSLO',
    null,
    '19-02-1995',
    '07-10-1987',
    'Bjørn Tjaum',
    'Eie og forvalte fast eiendom, samt å drive handel,  tjenesteytende virksomhet og produksjon og alt hva  hermed står i forbindelse, herunder å delta i  andre foretagender som driver tilsvarende virksom-  het.',
    'Eie og forvalte fast eiendom, samt å drive handel,  tjenesteytende virksomhet og produksjon og alt hva  hermed står i forbindelse, herunder å delta i  andre foretagender som driver tilsvarende virksom-  het.',
    '2100 Private aksjeselskaper mv.',
    'Registrert i Foretaksregisteret
Registrert i Merverdiavgiftsregisteret
Frivillig registrering i Merverdiavgiftsregisteret:
- Utleier av bygg eller anlegg
Inngår i konsern
Sist innsendte årsregnskap 2019',
    null,
    'Nils Eivind Risvand',
    null,
    'Bjørn Kulseth Mjaaseth
Bjørn Tjaum',
    'Styrets leder alene eller to styremedlemmer i fellesskap.',
    null,
    'Godkjent revisjonsselskap
Organisasjonsnummer 980 211 282
DELOITTE AS
Dronning Eufemias gate 14
0191   OSLO'
);

INSERT INTO subOrganizations VALUES (
    972370134,
    'FK HAUGESUND',
    'Karmsundgata 169
5522  HAUGESUND',
    null,
    'HAUGESUND',
    'Registrert i NAV Aa-registeret',
    871023832
);

INSERT INTO subOrganizations VALUES (
    983314635,
    'AMANDA STORSENTER AS',
    'Støperigata 1
0250 OSLO',
    'Postboks 1593 Vika
0118 OSLO',
    'OSLO',
     null,
     945725729
);


INSERT INTO businessCode_org VALUES(
    '93.120',
    871023832
);

INSERT INTO businessCode_org VALUES(
    '68.209',
    945725729
);

INSERT INTO businessCode_subOrg VALUES(
    '93.120',
    972370134
);

INSERT INTO businessCode_subOrg VALUES(
   '68.209',
    983314635
);

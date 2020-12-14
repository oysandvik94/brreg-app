CREATE TABLE organizations (
    orgNr int PRIMARY KEY,
    name varchar(255),
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
    businessCode varchar(255),
    sectorCode varchar(255),
    registeredNotes varchar(255),
    board varchar(255),
    boardLeader varchar(255),
    boardMembers varchar(255),
    signature varchar(255),
    auditor varchar(255),
    accountant varchar(255),
    subOrganization int,
    FOREIGN KEY (subOrganization)
        REFERENCES subOrganizatios (orgNr),
    FOREIGN KEY (businessCode)
        REFERENCES businessCodes (businessCode)
);

CREATE TABLE organizations (
    orgNr int PRIMARY KEY,
    name varchar(255),
    address varchar(255),
    municipality varchar(255),
    businessCode varchar(255),
    registeredNotes varchar(255),
    FOREIGN KEY (businessCode)
        REFERENCES businessCodes (businessCode)
);

CREATE TABLE businessCodes (
    businessCode varchar(255) PRIMARY KEY,
    name varchar(255) UNIQUE NOT NULL,
    description varchar(255)
);

CREATE DATABASE [Life Events of Celebrities]
GO

USE [Life Events of Celebrities]
GO

CREATE TABLE Celebrities (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Nationality NVARCHAR(100) NULL,
    ReqPhotoPath NVARCHAR(500) NULL
);
GO

CREATE TABLE LifeEvents (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CelebrityId INT NOT NULL FOREIGN KEY references Celebrities(Id),
    EventDate DATE NULL,
    [Description] NVARCHAR(MAX) NULL,
    ReqPhotoPath NVARCHAR(500) NULL,
);
GO


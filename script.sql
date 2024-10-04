-- 1 Crear Base de datos

CREATE DATABASE HardcoreGamesRanking;

-- 2 DDL

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) NOT NULL,
    Username VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_Usuarios PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT CHK_UsernameLength CHECK (LEN(Username) >= 3),
    CONSTRAINT CHK_EmailFormat CHECK (Email LIKE '%_@__%.__%')
);

CREATE TABLE Companias (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(255) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL,
    UsuarioActualizacion INT NULL,
    CONSTRAINT PK_Companias PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Companias_Usuarios FOREIGN KEY (UsuarioActualizacion) REFERENCES Usuarios(Id),
    CONSTRAINT CHK_NombreLength CHECK (LEN(Nombre) >= 3)
);

CREATE TABLE Videojuegos (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(255) NOT NULL,
    IdCompania INT NOT NULL,
    AnioLanzamiento INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Puntaje DECIMAL(3,2) NOT NULL DEFAULT 0.00,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL,
    UsuarioActualizacion INT NULL,
    Activo BIT NOT NULL DEFAULT 1, 
    CONSTRAINT PK_Videojuegos PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Videojuegos_Companias FOREIGN KEY (IdCompania) REFERENCES Companias(Id),
    CONSTRAINT FK_Videojuegos_Usuarios FOREIGN KEY (UsuarioActualizacion) REFERENCES Usuarios(Id),
    CONSTRAINT CHK_Anio_Lanzamiento CHECK (AnioLanzamiento > 0),
    CONSTRAINT CHK_Precio CHECK (Precio > 0),
    CONSTRAINT CHK_Puntaje CHECK (Puntaje >= 0.00 AND Puntaje <= 5.00),
    CONSTRAINT CHK_Activo CHECK (Activo IN (0, 1))
);

CREATE TABLE Calificaciones (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    Nickname VARCHAR(30) NOT NULL,
    IdVideojuego INT NOT NULL,
    Puntuacion DECIMAL(3,2) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL,
    UsuarioActualizacion INT NULL,
    CONSTRAINT PK_Calificaciones PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Calificaciones_Videojuegos FOREIGN KEY (IdVideojuego) REFERENCES Videojuegos(Id),
    CONSTRAINT FK_Calificaciones_Usuarios FOREIGN KEY (UsuarioActualizacion) REFERENCES Usuarios(Id),
    CONSTRAINT CHK_Puntuacion CHECK (Puntuacion >= 0.00 AND Puntuacion <= 5.00),
    CONSTRAINT CHK_NickNameLength CHECK (LEN(Nickname) <= 30)
);

-- 3 DML

INSERT INTO Companias (Nombre) VALUES
('From Software'),
('StudioMDHR'),
('Konami'),
('Koei Tecmo'),
('Extremely OK Games'),
('Rare'),
('Team17'),
('Capcom'),
('Ska Studios'),
('Nintendo'),
('Direct2Drive');

INSERT INTO Videojuegos (Nombre, IdCompania, AnioLanzamiento, Precio) VALUES
('Dark Souls', 1, 2011, 39.99),
('Sekiro: Shadows Die Twice', 1, 2019, 59.99),
('Bloodborne', 1, 2015, 19.99),
('Demon''s Souls', 1, 2009, 39.99),
('Cuphead', 2, 2017, 19.99),
('Contra', 3, 1987, 6.99),
('Nioh', 4, 2017, 19.99),
('Celeste', 5, 2018, 7.99),
('Battletoads', 6, 1991, 5.99),
('Blasphemous', 7, 2019, 24.99),
('Teenage Mutant Ninja Turtles', 3, 1989, 12.99),
('Ninja Gaiden Black', 4, 2005, 25.99),
('Ghosts ''n Goblins', 8, 1985, 6.99),
('Salt and Sanctuary', 9, 2016, 17.99),
('Dark Souls III', 1, 2016, 59.99),
('Super Meat Boy', 10, 2010, 5.99),
('Dark Souls II', 1, 2014, 39.99),
('Hollow Knight', 7, 2017, 14.99),
('Super Mario Maker 2', 11, 2019, 59.99),
('Elden Ring', 1, 2022, 69.99);


-- 4 SP

CREATE PROCEDURE SpGenerarCalificacionesAleatorias
    @Cantidad INT = 0, 
    @CodigoError INT OUTPUT, 
    @MensajeError NVARCHAR(255) OUTPUT 
AS
BEGIN
    BEGIN TRY
        SET @CodigoError = 0;
        SET @MensajeError = NULL;

        IF @Cantidad <= 0
        BEGIN
            SET @CodigoError = 1;
            SET @MensajeError = 'El valor ingresado para la cantidad no es válido.';
            RETURN;
        END

        DECLARE @i INT = 0;
        DECLARE @Nickname VARCHAR(30);
        DECLARE @IdVideojuego INT;
        DECLARE @Puntuacion DECIMAL(3, 2);

        WHILE @i < @Cantidad
        BEGIN
            SET @Nickname = LEFT(NEWID(), 8) + LEFT(CONVERT(VARCHAR(10), ABS(CHECKSUM(NEWID()))), 5);

            SELECT TOP 1 @IdVideojuego = Id FROM Videojuegos ORDER BY NEWID();

            SET @Puntuacion = CAST((RAND(CHECKSUM(NEWID())) * 5.0) AS DECIMAL(3, 2));

            INSERT INTO Calificaciones (Nickname, IdVideojuego, Puntuacion)
            VALUES (@Nickname, @IdVideojuego, @Puntuacion);

            SET @i = @i + 1;
        END

        SET @CodigoError = 0;
        SET @MensajeError = NULL;
    END TRY
    BEGIN CATCH
        SET @CodigoError = ERROR_NUMBER();
        SET @MensajeError = ERROR_MESSAGE();
    END CATCH;
END;

-- 5 USE SP

DECLARE @CodigoError INT, @MensajeError NVARCHAR(255);

EXEC SpGenerarCalificacionesAleatorias
    @Cantidad = 1000000,
    @CodigoError = @CodigoError OUTPUT,
    @MensajeError = @MensajeError OUTPUT;

SELECT @CodigoError AS CodigoError, @MensajeError AS MensajeError;

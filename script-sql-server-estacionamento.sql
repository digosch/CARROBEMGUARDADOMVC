IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'EstacionamentoDB')
BEGIN
    CREATE DATABASE EstacionamentoDB;
END
GO

USE EstacionamentoDB;
GO

SET DATEFORMAT ymd;
GO

IF OBJECT_ID('dbo.RegistrosEstacionamento', 'U') IS NOT NULL DROP TABLE dbo.RegistrosEstacionamento;
GO
IF OBJECT_ID('dbo.TabelaPrecos', 'U') IS NOT NULL DROP TABLE dbo.TabelaPrecos;
GO

CREATE TABLE dbo.TabelaPrecos (
    Id                   INT IDENTITY(1,1)  NOT NULL,
    DataInicioVigencia   DATE               NOT NULL,
    DataFimVigencia      DATE               NOT NULL,
    ValorHoraInicial     DECIMAL(10,2)      NOT NULL,
    ValorHoraAdicional   DECIMAL(10,2)      NOT NULL,
    DataCriacao          DATETIME           NOT NULL
        CONSTRAINT DF_TabelaPrecos_DataCriacao DEFAULT (GETDATE()),
    CONSTRAINT PK_TabelaPrecos PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT CK_TabelaPrecos_Vigencia CHECK (DataFimVigencia >= DataInicioVigencia),
    CONSTRAINT CK_TabelaPrecos_Valores  CHECK (ValorHoraInicial > 0 AND ValorHoraAdicional > 0)
);
GO

CREATE TABLE dbo.RegistrosEstacionamento (
    Id                INT IDENTITY(1,1)  NOT NULL,
    Placa             VARCHAR(8)         NOT NULL,
    DataHoraEntrada   DATETIME           NOT NULL,
    DataHoraSaida     DATETIME           NULL,
    TabelaPrecoId     INT                NULL,
    ValorCobrado      DECIMAL(10,2)      NULL,
    CONSTRAINT PK_RegistrosEstacionamento PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_RegistrosEstacionamento_TabelaPrecos FOREIGN KEY (TabelaPrecoId) REFERENCES dbo.TabelaPrecos(Id),
    CONSTRAINT CK_RegistrosEstacionamento_Datas CHECK (DataHoraSaida IS NULL OR DataHoraSaida >= DataHoraEntrada)
);
GO

CREATE NONCLUSTERED INDEX IX_RegistrosEstacionamento_Placa ON dbo.RegistrosEstacionamento (Placa);
GO
CREATE NONCLUSTERED INDEX IX_RegistrosEstacionamento_DataHoraSaida ON dbo.RegistrosEstacionamento (DataHoraSaida);
GO

--EXEMPLO pra tabela de precos
--INSERT INTO dbo.TabelaPrecos (DataInicioVigencia, DataFimVigencia, ValorHoraInicial, ValorHoraAdicional)
--VALUES (CAST(GETDATE() AS DATE), DATEADD(YEAR, 1, CAST(GETDATE() AS DATE)), 5.00, 3.00);
--GO

--Para a Tabela de Estacionamento registrar via front end a data de entrada e saida da placa

SELECT * FROM dbo.TabelaPrecos;
SELECT * FROM dbo.RegistrosEstacionamento;
CREATE TABLE dbo.TEISES
(
	id int IDENTITY (1, 1) NOT NULL,
	viesbuciu_tinklas varchar (30) NOT NULL,
	viesbutis varchar (30) NULL,
	tipas int NOT NULL,
	fk_Klientaskliento_kodas int NULL,
	fk_Darbuotojasdarbuojo_kodas int NULL,
	[teisiu_statusas] BIT NOT NULL, 
    [priezastis] VARCHAR(50) NULL, 
    [data_iki] DATETIME NULL, 
    PRIMARY KEY(id),
	FOREIGN KEY(tipas) REFERENCES dbo.TEISIU_TIPAI (id),
	CONSTRAINT turi FOREIGN KEY(fk_Klientaskliento_kodas) REFERENCES dbo.KLIENTAI (kliento_kodas)
);
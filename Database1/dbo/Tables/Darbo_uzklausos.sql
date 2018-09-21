CREATE TABLE dbo.DARBO_UZKLAUSOS
(
	id int IDENTITY (1, 1) NOT NULL,
	slaptazodis varchar (100) NOT NULL,
	fk_Darbuotojasdarbuojo_kodas int NOT NULL,
	fk_Klientaskliento_kodas int NOT NULL,
	[pareigos] VARCHAR(12) NOT NULL, 
    PRIMARY KEY(id),
	CONSTRAINT sukuria FOREIGN KEY(fk_Darbuotojasdarbuojo_kodas) REFERENCES dbo.DARBUOTOJAI (darbuojo_kodas),
	CONSTRAINT patvirtina FOREIGN KEY(fk_Klientaskliento_kodas) REFERENCES dbo.KLIENTAI (kliento_kodas)
);
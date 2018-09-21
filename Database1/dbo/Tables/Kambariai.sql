CREATE TABLE dbo.KAMBARIAI
(
	id int IDENTITY (1, 1) NOT NULL,
	vietu_sk int NOT NULL,
	numeris varchar (20) NOT NULL,
	kaina float (5) NOT NULL,
	aprasymas varchar (200) NOT NULL,
	tipas int  NOT NULL,
	fk_Viesbutisid int NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(tipas) REFERENCES dbo.KAMBARIO_TIPAI (id),
	CONSTRAINT yra FOREIGN KEY(fk_Viesbutisid) REFERENCES dbo.VIESBUCIAI (id)
);
CREATE TABLE dbo.MOKEJIMAI
(
	id int IDENTITY (1, 1) NOT NULL,
	data datetime NOT NULL,
	atsiskaitymo_budas varchar (20) NOT NULL,
	suma varchar (10) NOT NULL,
	apmokejimo_data datetime NOT NULL,
	tipas int NOT NULL,
	busena int NOT NULL,
	fk_Rezervacijaid int NOT NULL,
	fk_Klientaskliento_kodas int NOT NULL,
	PRIMARY KEY(id),
	UNIQUE(fk_Rezervacijaid),
	FOREIGN KEY(tipas) REFERENCES dbo.MOKEJIMO_TIPAI (id),
	FOREIGN KEY(busena) REFERENCES dbo.MOKEJIMO_BUSENOS (id),
	CONSTRAINT priklauso FOREIGN KEY(fk_Rezervacijaid) REFERENCES dbo.REZERVACIJOS (id),
	CONSTRAINT apmoka FOREIGN KEY(fk_Klientaskliento_kodas) REFERENCES dbo.KLIENTAI (kliento_kodas)
);
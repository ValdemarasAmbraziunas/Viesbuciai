CREATE TABLE dbo.REZERVACIJOS
(
	id int IDENTITY (1, 1) NOT NULL,
	rezervacijos_pradzia datetime NOT NULL,
	rezervacijos_pabaiga datetime NOT NULL,
	rezervacijos_atlikimo_data datetime NOT NULL,
	busena int NOT NULL,
	fk_Klientaskliento_kodas int NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT sukuria2 FOREIGN KEY(fk_Klientaskliento_kodas) REFERENCES dbo.KLIENTAI (kliento_kodas)
);
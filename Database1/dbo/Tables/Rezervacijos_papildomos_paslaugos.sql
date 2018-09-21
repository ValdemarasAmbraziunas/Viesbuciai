CREATE TABLE dbo.REZERVACIJOS_PAPILDOMOS_PASLAUGOS
(
	fk_Rezervacijaid int NOT NULL,
	fk_Papildoma_paslaugaid int NOT NULL,
	PRIMARY KEY(fk_Rezervacijaid, fk_Papildoma_paslaugaid),
	CONSTRAINT priklauso2 FOREIGN KEY(fk_Rezervacijaid) REFERENCES dbo.REZERVACIJOS (id)
);
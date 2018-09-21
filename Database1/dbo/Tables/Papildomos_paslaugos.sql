CREATE TABLE dbo.PAPILDOMOS_PASLAUGOS
(
	id int IDENTITY (1, 1) NOT NULL,
	aprasymas varchar (250) NOT NULL,
	kaina float (5) NOT NULL,
	fk_Viesbutisid int NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT teikia FOREIGN KEY(fk_Viesbutisid) REFERENCES dbo.VIESBUCIAI (id)
);
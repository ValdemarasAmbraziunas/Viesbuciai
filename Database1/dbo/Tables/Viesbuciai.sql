CREATE TABLE dbo.VIESBUCIAI
(
	id int IDENTITY (1, 1) NOT NULL,
	pavadinimas varchar (30) NOT NULL,
	viesbuciu_tinklas varchar (30) NOT NULL,
	zvaigzduciu_sk int NOT NULL,
	miestas varchar (20) NOT NULL,
	adresas varchar (20) NOT NULL,
	aprasymas varchar (200) NOT NULL,
	fk_savininkas int NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT priklaussso FOREIGN KEY(fk_savininkas) REFERENCES dbo.DARBUOTOJAI (darbuojo_kodas)
);
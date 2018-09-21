CREATE TABLE dbo.DARBUOTOJAI
(
	darbuojo_kodas int IDENTITY (1, 1) NOT NULL,
	vardas varchar (20) NOT NULL,
	pavarde varchar (20) NOT NULL,
	adresas varchar (50) NOT NULL,
	telefonas varchar (10) NOT NULL,
	lytis varchar (10) NOT NULL,
	darbo_pradzios_laikas datetime NOT NULL,
	slaptazodis varchar (100) NOT NULL,
	darbuotojo_tipas int  NOT NULL,
	fk_Viesbutisid int NULL,
	[el_pastas] VARCHAR(40) NOT NULL, 
    PRIMARY KEY(darbuojo_kodas),
	FOREIGN KEY(darbuotojo_tipas) REFERENCES dbo.DARBUOTOJU_TIPAI (id),
	CONSTRAINT dirba FOREIGN KEY(fk_Viesbutisid) REFERENCES dbo.VIESBUCIAI (id)
);
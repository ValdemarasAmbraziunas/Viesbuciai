CREATE TABLE dbo.KLIENTAI
(
	kliento_kodas int IDENTITY(1,1) NOT NULL,
	vardas varchar (20) NOT NULL,
	pavarde varchar (20) NOT NULL,
	el_pastas varchar (40) NOT NULL,
	lytis varchar (10) NOT NULL,
	telefonas varchar (13) NOT NULL,
	adresas varchar (40) NOT NULL,
	sukurimo_data DATETIME NOT NULL,
	slaptazodis varchar (100) NOT NULL,
	UNIQUE(el_pastas),
	PRIMARY KEY(kliento_kodas)
);

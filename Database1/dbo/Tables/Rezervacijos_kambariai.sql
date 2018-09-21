CREATE TABLE dbo.REZERVACIJOS_KAMBARIAI
(
	fk_Kambarysid int NOT NULL,
	fk_Rezervacijaid int NOT NULL,
	PRIMARY KEY(fk_Kambarysid, fk_Rezervacijaid),
	CONSTRAINT priklauso3 FOREIGN KEY(fk_Kambarysid) REFERENCES dbo.KAMBARIAI (id)
);

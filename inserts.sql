USE [MojoDb];
GO

BEGIN TRANSACTION;
GO

-----------------------------------------------------------
-- 0. CORRECTION INDEX DOCUMENTS (UNIQUE ? NON-UNIQUE)
-----------------------------------------------------------
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Documents_ContratId' AND is_unique = 1)
BEGIN
    DROP INDEX [IX_Documents_ContratId] ON [dbo].[Documents];
    PRINT 'Index UNIQUE supprimé';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Documents_ContratId')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Documents_ContratId] 
    ON [dbo].[Documents] ([ContratId] ASC);
    PRINT 'Index NON-UNIQUE créé';
END
GO

-----------------------------------------------------------
-- 1. [__EFMigrationsHistory]
-----------------------------------------------------------
IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20250101_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES 
    ('20250101_Init', '8.0.0');
END
GO

-----------------------------------------------------------
-- 2. [Organisations]
-----------------------------------------------------------
SET IDENTITY_INSERT [Organisations] ON;

INSERT INTO [Organisations] ([Id], [Name], [Code], [Address], [ContactEmail], [IsActif], [IdContact], [LogoUrl], [EmailAutorise], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 'Mojo Corporate', 'MOJO01', 'Paris', 'admin@mojo.com', 1, 'U01', 'https://mojovelo.be/wp-content/uploads/2020/03/cropped-mojo-logo-sans-rien-3.jpg', '@mojo.com', GETDATE(), GETDATE(), 'System', 'System'),
(2, 'Velocité Lyon', 'VELO02', 'Lyon', 'contact@velocite.com', 1, 'U02', 'https://www.webador.fr/blog/wp-content/uploads/2023/09/image-4-1024x576.png', '@velocite.com', GETDATE(), GETDATE(), 'System', 'System'),
(3, 'EcoBike Bordeaux', 'ECO03', 'Bordeaux', 'info@ecobike.com', 1, 'U03', 'https://img.pikbest.com/png-images/20241029/ride-your-dreams_11024247.png!w700wp', '@ecobike.com', GETDATE(), GETDATE(), 'System', 'System'),
(4, 'Lille Cycles', 'LIL04', 'Lille', 'lille@cycles.com', 1, 'U04', 'https://www.webador.fr/blog/wp-content/uploads/2023/09/Evian_Logo.png', '@cycles.com', GETDATE(), GETDATE(), 'System', 'System'),
(5, 'Nantes Mobilité', 'NAN05', 'Nantes', 'nantes@mob.com', 1, 'U05', 'https://mir-s3-cdn-cf.behance.net/project_modules/1400_webp/a8247e180299449.65089787f051f.jpg', '@mob.com', GETDATE(), GETDATE(), 'System', 'System'),
(6, 'Green Wheels', 'GW06', 'Berlin', 'hallo@green.com', 1, 'U06', 'https://img.freepik.com/vecteurs-libre/lettre-coloree-creation-logo-degrade_474888-2309.jpg', '@green.com', GETDATE(), GETDATE(), 'System', 'System'),
(7, 'Urban Ride', 'URB07', 'Marseille', 'contact@urbanride.com', 1, 'U07', 'https://img.pikbest.com/png-images/20240912/happy-pongal-festival-wishes-you_10830361.png!f305cw', '@urbanride.com', GETDATE(), GETDATE(), 'System', 'System'),
(8, 'Swiss Cycle', 'SWI08', 'Genève', 'info@swisscycle.com', 1, 'U08', 'https://img.pikbest.com/png-images/20241022/hacker-gaming-logo_10991508.png!w700wp', '@swisscycle.com', GETDATE(), GETDATE(), 'System', 'System'),
(9, 'Soft Ride SAS', 'SOFT09', 'Puteaux', 'it@softride.com', 1, 'U09', 'https://img.pikbest.com/png-images/20240617/lion-logo-vector-illustration_10621866.png!f305cw', '@softride.com', GETDATE(), GETDATE(), 'System', 'System'),
(10, 'Alpha Fleet', 'ALP10', 'Nice', 'fleet@alpha.com', 1, 'U10', 'https://img.pikbest.com/png-images/20240611/cricket-sport-logo-design-isolated-on-transparent-background-png_10607363.png!f305cw', '@alpha.com', GETDATE(), GETDATE(), 'System', 'System');

SET IDENTITY_INSERT [Organisations] OFF;
GO

-----------------------------------------------------------
-- 3. [AspNetRoles]
-----------------------------------------------------------
INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES
('R1', 'Admin', 'ADMIN', NEWID()), 
('R2', 'Manager', 'MANAGER', NEWID()), 
('R3', 'User', 'USER', NEWID()), 
('R4', 'Tech', 'TECH', NEWID()), 
('R5', 'RH', 'RH', NEWID()),
('R6', 'Guest', 'GUEST', NEWID()), 
('R7', 'Support', 'SUPPORT', NEWID()), 
('R8', 'Fleet', 'FLEET', NEWID()), 
('R9', 'SuperAdmin', 'SUPERADMIN', NEWID()), 
('R10', 'Audit', 'AUDIT', NEWID());
GO

-----------------------------------------------------------
-- 4. [AspNetUsers]
-----------------------------------------------------------
INSERT INTO [AspNetUsers] ([Id], [FirstName], [LastName], [OrganisationId], [Email], [NormalizedEmail], [UserName], [NormalizedUserName], [IsActif], [Role], [TailleCm], [EmailConfirmed], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount], [PasswordHash], [SecurityStamp], [ConcurrencyStamp]) VALUES
('U01','Jean','Admin',1,'jean@mojo.com','JEAN@MOJO.COM','jadmin','JADMIN',1,1,180,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash1',NEWID(),NEWID()),
('U02','Marc','Manager',1,'marc@mojo.com','MARC@MOJO.COM','mmanager','MMANAGER',1,2,175,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash2',NEWID(),NEWID()),
('U03','Sophie','RH',2,'sophie@velocite.com','SOPHIE@VELOCITE.COM','srh','SRH',1,5,165,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash3',NEWID(),NEWID()),
('U04','Luc','Client',2,'luc@gmail.com','LUC@GMAIL.COM','lclient','LCLIENT',1,3,178,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash4',NEWID(),NEWID()),
('U05','Eva','User',3,'eva@outlook.com','EVA@OUTLOOK.COM','euser','EUSER',1,3,162,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash5',NEWID(),NEWID()),
('U06','Tom','Tech',4,'tom@tech.com','TOM@TECH.COM','ttech','TTECH',1,4,185,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash6',NEWID(),NEWID()),
('U07','Lea','User',5,'lea@nantes.com','LEA@NANTES.COM','luser','LUSER',1,3,170,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash7',NEWID(),NEWID()),
('U08','Bob','Mojo',1,'bob@mojo.com','BOB@MOJO.COM','bmojo','BMOJO',1,2,182,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash8',NEWID(),NEWID()),
('U09','Kim','User',6,'kim@green.com','KIM@GREEN.COM','kuser','KUSER',1,3,160,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash9',NEWID(),NEWID()),
('U10','Ian','User',7,'ian@urban.com','IAN@URBAN.COM','iuser','IUSER',1,3,188,1,0,0,1,0,'AQAAAAIAAYagAAAAEDummyHash10',NEWID(),NEWID());
GO

-----------------------------------------------------------
-- 5. [AspNetUserRoles]
-----------------------------------------------------------
INSERT INTO [AspNetUserRoles] ([UserId], [RoleId]) VALUES
('U01','R9'), ('U02','R2'), ('U03','R5'), ('U04','R3'), ('U05','R3'), 
('U06','R4'), ('U07','R3'), ('U08','R2'), ('U09','R3'), ('U10','R3'),
('U01','R1'), ('U02','R8'), ('U03','R1'), ('U06','R7'), ('U08','R10'), 
('U01','R2'), ('U02','R5'), ('U06','R3'), ('U08','R4'), ('U10','R6');
GO

-----------------------------------------------------------
-- 6. [AspNetUserClaims]
-----------------------------------------------------------
SET IDENTITY_INSERT [AspNetUserClaims] ON;

INSERT INTO [AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES
(1, 'U01','Perm','Full'), 
(2, 'U02','Zone','FR'), 
(3, 'U03','Dept','HR'), 
(4, 'U04','Type','Std'), 
(5, 'U05','Type','Cargo'), 
(6, 'U06','Skill','Elec'), 
(7, 'U07','Portal','Yes'), 
(8, 'U08','Level','Mid'), 
(9, 'U09','Lang','DE'), 
(10, 'U10','Club','Gold');

SET IDENTITY_INSERT [AspNetUserClaims] OFF;
GO

-----------------------------------------------------------
-- 7. [AspNetUserLogins]
-----------------------------------------------------------
INSERT INTO [AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES
('Google','G1','Google','U04'), 
('MS','M2','Microsoft','U05'), 
('Apple','A1','Apple','U07'), 
('FB','F4','Facebook','U09'), 
('GitHub','GH1','Git','U01'), 
('Google','G2','Google','U02'), 
('MS','M7','Azure','U03'), 
('Twitter','T5','X','U10'), 
('LinkedIn','L8','In','U08'), 
('Google','G9','Gmail','U06');
GO

-----------------------------------------------------------
-- 8. [AspNetUserTokens]
-----------------------------------------------------------
INSERT INTO [AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES
('U01','D','Reset','T1'), 
('U02','D','Reset','T2'), 
('U03','D','Reset','T3'), 
('U04','D','Auth','T4'), 
('U05','D','Auth','T5'), 
('U06','D','Auth','T6'), 
('U07','D','Auth','T7'), 
('U08','D','Reset','T8'), 
('U09','D','Reset','T9'), 
('U10','D','Auth','T10');
GO

-----------------------------------------------------------
-- 9. [AspNetRoleClaims]
-----------------------------------------------------------
SET IDENTITY_INSERT [AspNetRoleClaims] ON;

INSERT INTO [AspNetRoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES
(1, 'R1','Power','Full'), 
(2, 'R2','Users','Manage'), 
(3, 'R3','Ride','True'), 
(4, 'R4','Parts','Edit'), 
(5, 'R5','Contract','Sign'), 
(6, 'R1','DB','Backup'), 
(7, 'R2','Audit','True'), 
(8, 'R3','Support','True'), 
(9, 'R4','Workshop','All'), 
(10, 'R5','Pay','View');

SET IDENTITY_INSERT [AspNetRoleClaims] OFF;
GO

-----------------------------------------------------------
-- 10. [Velos]
-----------------------------------------------------------
SET IDENTITY_INSERT [Velos] ON;

INSERT INTO [Velos] ([Id], [Marque], [Modele], [NumeroSerie], [PrixAchat], [Status], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 'Moustache','Lundi 27','SN2026-001',2500,1,1,GETDATE(),GETDATE(),'System','System'), 
(2, 'VanMoof','S5','SN2026-002',2900,1,1,GETDATE(),GETDATE(),'System','System'), 
(3, 'Cowboy','C4','SN2026-003',2700,1,1,GETDATE(),GETDATE(),'System','System'), 
(4, 'Giant','Explore','SN2026-004',2300,1,1,GETDATE(),GETDATE(),'System','System'), 
(5, 'Specialized','Vado','SN2026-005',3500,1,1,GETDATE(),GETDATE(),'System','System'),
(6, 'Trek','Allant','SN2026-006',3800,1,1,GETDATE(),GETDATE(),'System','System'), 
(7, 'Gazelle','Ultimate','SN2026-007',3100,0,1,GETDATE(),GETDATE(),'System','System'), 
(8, 'Decathlon','Elops','SN2026-008',1400,1,1,GETDATE(),GETDATE(),'System','System'), 
(9, 'Canyon','Precede','SN2026-009',4200,1,1,GETDATE(),GETDATE(),'System','System'), 
(10, 'Riese','Nevo','SN2026-010',5200,0,1,GETDATE(),GETDATE(),'System','System');

SET IDENTITY_INSERT [Velos] OFF;
GO

-----------------------------------------------------------
-- 11. [Contrats]
-----------------------------------------------------------
SET IDENTITY_INSERT [Contrats] ON;

INSERT INTO [Contrats] ([Id], [Ref], [VeloId], [BeneficiaireId], [UserRhId], [DateDebut], [DateFin], [Duree], [LoyerMensuelHT], [StatutContrat], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 'CTR-M01',1,'U04','U03','2026-01-01','2028-01-01',24,85,1,1,GETDATE(),GETDATE(),'System','System'), 
(2, 'CTR-V02',2,'U05','U03','2026-01-01','2028-01-01',24,95,1,1,GETDATE(),GETDATE(),'System','System'), 
(3, 'CTR-E03',3,'U07','U03','2026-01-01','2028-01-01',24,110,1,1,GETDATE(),GETDATE(),'System','System'), 
(4, 'CTR-L04',4,'U09','U03','2026-01-01','2028-01-01',24,120,1,1,GETDATE(),GETDATE(),'System','System'), 
(5, 'CTR-N05',5,'U10','U03','2026-01-01','2028-01-01',24,130,1,1,GETDATE(),GETDATE(),'System','System'),
(6, 'CTR-G06',6,'U04','U03','2026-02-01','2029-02-01',36,140,1,1,GETDATE(),GETDATE(),'System','System'), 
(7, 'CTR-U07',7,'U05','U03','2026-02-01','2029-02-01',36,150,0,1,GETDATE(),GETDATE(),'System','System'), 
(8, 'CTR-S08',8,'U07','U03','2026-02-01','2029-02-01',36,60,1,1,GETDATE(),GETDATE(),'System','System'), 
(9, 'CTR-SO09',9,'U09','U03','2026-02-01','2029-02-01',36,180,2,1,GETDATE(),GETDATE(),'System','System'), 
(10, 'CTR-A10',1,'U10','U03','2026-03-01','2029-03-01',36,85,1,1,GETDATE(),GETDATE(),'System','System');

SET IDENTITY_INSERT [Contrats] OFF;
GO

-----------------------------------------------------------
-- 12. [Amortissements]
-----------------------------------------------------------
SET IDENTITY_INSERT [Amortissements] ON;

INSERT INTO [Amortissements] ([Id], [VeloId], [DateDebut], [DureeMois], [ValeurInit], [ValeurResiduelleFinale], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 1,'2026-01-01',48,2500,500,1,GETDATE(),GETDATE(),'System','System'), 
(2, 2,'2026-01-01',48,2900,580,1,GETDATE(),GETDATE(),'System','System'), 
(3, 3,'2026-01-01',48,2700,540,1,GETDATE(),GETDATE(),'System','System'), 
(4, 4,'2026-01-01',48,2300,460,1,GETDATE(),GETDATE(),'System','System'), 
(5, 5,'2026-02-01',60,3500,700,1,GETDATE(),GETDATE(),'System','System'),
(6, 6,'2026-02-01',60,3800,760,1,GETDATE(),GETDATE(),'System','System'), 
(7, 7,'2026-02-01',60,3100,620,1,GETDATE(),GETDATE(),'System','System'), 
(8, 8,'2026-03-01',48,1400,280,1,GETDATE(),GETDATE(),'System','System'), 
(9, 9,'2026-03-01',60,4200,840,1,GETDATE(),GETDATE(),'System','System'), 
(10, 10,'2026-03-01',60,5200,1040,1,GETDATE(),GETDATE(),'System','System');

SET IDENTITY_INSERT [Amortissements] OFF;
GO

-----------------------------------------------------------
-- 13. [Discussions]
-----------------------------------------------------------
SET IDENTITY_INSERT [Discussions] ON;

INSERT INTO [Discussions] ([Id], [Objet], [ClientId], [MojoId], [Status], [IsActif], [DateCreation], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 'Panne batterie','U04','U01',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(2, 'Facture loyer','U05','U02',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(3, 'Entretien annuel','U07','U01',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(4, 'Vélo volé','U09','U01',0,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(5, 'Bruit pédalier','U10','U02',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'),
(6, 'Changement RIB','U04','U02',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(7, 'Freins lâches','U05','U06',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(8, 'Extension garantie','U07','U06',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(9, 'Usure pneus','U09','U01',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System'), 
(10, 'Clés perdues','U10','U02',1,1,GETDATE(),GETDATE(),GETDATE(),'System','System');

SET IDENTITY_INSERT [Discussions] OFF;
GO

-----------------------------------------------------------
-- 14. [Messages]
-----------------------------------------------------------
SET IDENTITY_INSERT [Messages] ON;

INSERT INTO [Messages] ([Id], [Contenu], [DiscussionId], [IsActif], [DateEnvoi], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 'Le vélo ne s''allume plus ce matin.', 1, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(2, 'Vérifiez que la batterie est bien enclenchée.', 1, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(3, 'C''est fait, mais l''écran reste noir.', 1, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(4, 'Pouvez-vous m''envoyer la facture de janvier ?', 2, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(5, 'Elle est disponible dans votre espace client.', 2, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(6, 'Mon pneu arrière est lisse.', 9, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(7, 'Nous prenons rendez-vous pour le remplacement.', 9, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(8, 'On m''a volé mon vélo à Marseille.', 4, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(9, 'Veuillez joindre le dépôt de plainte PDF.', 4, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System'),
(10, 'Voici le document scanné.', 4, 1, GETDATE(), GETDATE(), GETDATE(), 'System', 'System');

SET IDENTITY_INSERT [Messages] OFF;
GO

-----------------------------------------------------------
-- 15. [Demandes]
-----------------------------------------------------------
SET IDENTITY_INSERT [Demandes] ON;

INSERT INTO [Demandes] ([Id], [IdUser], [IdVelo], [DiscussionId], [Status], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 'U04',1,1,1,1,GETDATE(),GETDATE(),'System','System'), 
(2, 'U05',2,2,1,1,GETDATE(),GETDATE(),'System','System'), 
(3, 'U07',3,3,1,1,GETDATE(),GETDATE(),'System','System'), 
(4, 'U09',4,4,3,1,GETDATE(),GETDATE(),'System','System'), 
(5, 'U10',5,5,1,1,GETDATE(),GETDATE(),'System','System'),
(6, 'U04',6,6,1,1,GETDATE(),GETDATE(),'System','System'), 
(7, 'U05',7,7,2,1,GETDATE(),GETDATE(),'System','System'), 
(8, 'U07',8,8,2,1,GETDATE(),GETDATE(),'System','System'), 
(9, 'U09',9,9,1,1,GETDATE(),GETDATE(),'System','System'), 
(10, 'U10',10,10,1,1,GETDATE(),GETDATE(),'System','System');

SET IDENTITY_INSERT [Demandes] OFF;
GO

-----------------------------------------------------------
-- 16. [Interventions]
-----------------------------------------------------------
SET IDENTITY_INSERT [Interventions] ON;

INSERT INTO [Interventions] ([Id], [VeloId], [TypeIntervention], [Description], [DateIntervention], [Cout], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 1,'Batterie','Changement cellules',GETDATE(),450,1,GETDATE(),GETDATE(),'System','System'), 
(2, 2,'Pneus','Marathon Plus',GETDATE(),85,1,GETDATE(),GETDATE(),'System','System'), 
(3, 3,'Freins','Purge liquide',GETDATE(),50,1,GETDATE(),GETDATE(),'System','System'), 
(4, 7,'Cadre','Soudure support',GETDATE(),120,1,GETDATE(),GETDATE(),'System','System'), 
(5, 8,'Logiciel','Update V3',GETDATE(),0,1,GETDATE(),GETDATE(),'System','System'),
(6, 1,'Look','Peinture cadre',GETDATE(),60,1,GETDATE(),GETDATE(),'System','System'), 
(7, 2,'Moteur','Nettoyage capteur',GETDATE(),90,1,GETDATE(),GETDATE(),'System','System'), 
(8, 3,'Selle','Echange confort',GETDATE(),45,1,GETDATE(),GETDATE(),'System','System'), 
(9, 4,'Chaine','Graissage',GETDATE(),25,1,GETDATE(),GETDATE(),'System','System'), 
(10, 5,'Feu','Echange LED',GETDATE(),30,1,GETDATE(),GETDATE(),'System','System');

SET IDENTITY_INSERT [Interventions] OFF;
GO

-----------------------------------------------------------
-- 17. [Documents] ? AVEC NomFichier ET TypeFichier
-----------------------------------------------------------
SET IDENTITY_INSERT [Documents] ON;

INSERT INTO [Documents] ([Id], [ContratId], [Fichier], [NomFichier], [TypeFichier], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES
(1, 1, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-M01.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(2, 2, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-V02.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(3, 3, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-E03.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(4, 4, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-L04.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(5, 5, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-N05.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(6, 6, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-G06.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(7, 7, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-U07.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(8, 8, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-S08.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(9, 9, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-SO09.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(10, 10, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Contrat_CTR-A10.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
-- Documents supplémentaires pour tester plusieurs documents par contrat
(11, 1, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Assurance_CTR-M01.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(12, 1, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Facture_CTR-M01_Jan2026.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System'),
(13, 2, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, 'Facture_CTR-V02_Jan2026.pdf', 'pdf', 1, GETDATE(), GETDATE(), 'System', 'System');

SET IDENTITY_INSERT [Documents] OFF;
GO

COMMIT;
GO

PRINT 'Script exécuté avec succès ! ?';
GO
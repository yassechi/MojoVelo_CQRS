USE [MojoDb];
GO

BEGIN TRANSACTION;
GO

-----------------------------------------------------------
-- 1. [__EFMigrationsHistory]
-----------------------------------------------------------
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES 
('20250101_Init', '8.0.0'), ('20250105_Orgs', '8.0.0'), ('20250110_Users', '8.0.0'), ('20250115_Velos', '8.0.0'), ('20250120_Contrats', '8.0.0'), 
('20250125_Amort', '8.0.0'), ('20250130_Disc', '8.0.0'), ('20250201_Msg', '8.0.0'), ('20250205_Dem', '8.0.0'), ('20250206_Doc', '8.0.0');

-----------------------------------------------------------
-- 2. [Organisations] (Structure C# : LogoUrl, EmailAutorise, IdContact)
-----------------------------------------------------------
INSERT INTO [Organisations] ([Name], [Code], [Address], [ContactEmail], [IsActif], [IdContact], [LogoUrl], [EmailAutorise], [CreatedDate], [ModifiedDate]) VALUES
('Mojo Corporate', 'MOJO01', 'Paris', 'admin@mojo.com', 1, 'U01', 'https://mojovelo.be/wp-content/uploads/2020/03/cropped-mojo-logo-sans-rien-3.jpg', '@mojo.com', GETDATE(), GETDATE()),
('Velocité Lyon', 'VELO02', 'Lyon', 'contact@velocite.com', 1, 'U02', 'https://www.webador.fr/blog/wp-content/uploads/2023/09/image-4-1024x576.png', '@velocite.com', GETDATE(), GETDATE()),
('EcoBike Bordeaux', 'ECO03', 'Bordeaux', 'info@ecobike.com', 1, 'U03', 'https://img.pikbest.com/png-images/20241029/ride-your-dreams_11024247.png!w700wp', '@ecobike.com', GETDATE(), GETDATE()),
('Lille Cycles', 'LIL04', 'Lille', 'lille@cycles.com', 1, 'U04', 'https://www.webador.fr/blog/wp-content/uploads/2023/09/Evian_Logo.png', '@cycles.com', GETDATE(), GETDATE()),
('Nantes Mobilité', 'NAN05', 'Nantes', 'nantes@mob.com', 1, 'U05', 'https://mir-s3-cdn-cf.behance.net/project_modules/1400_webp/a8247e180299449.65089787f051f.jpg', '@mob.com', GETDATE(), GETDATE()),
('Green Wheels', 'GW06', 'Berlin', 'hallo@green.com', 1, 'U06', 'https://img.freepik.com/vecteurs-libre/lettre-coloree-creation-logo-degrade_474888-2309.jpg', '@green.com', GETDATE(), GETDATE()),
('Urban Ride', 'URB07', 'Marseille', 'contact@urbanride.com', 1, 'U07', 'https://img.pikbest.com/png-images/20240912/happy-pongal-festival-wishes-you_10830361.png!f305cw', '@urbanride.com', GETDATE(), GETDATE()),
('Swiss Cycle', 'SWI08', 'Genève', 'info@swisscycle.com', 1, 'U08', 'https://img.pikbest.com/png-images/20241022/hacker-gaming-logo_10991508.png!w700wp', '@swisscycle.com', GETDATE(), GETDATE()),
('Soft Ride SAS', 'SOFT09', 'Puteaux', 'it@softride.com', 1, 'U09', 'https://img.pikbest.com/png-images/20240617/lion-logo-vector-illustration_10621866.png!f305cw', '@softride.com', GETDATE(), GETDATE()),
('Alpha Fleet', 'ALP10', 'Nice', 'fleet@alpha.com', 1, 'U10', 'https://img.pikbest.com/png-images/20240611/cricket-sport-logo-design-isolated-on-transparent-background-png_10607363.png!f305cw', '@alpha.com', GETDATE(), GETDATE());

-----------------------------------------------------------
-- 3. [AspNetRoles]
-----------------------------------------------------------
INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES
('R1', 'Admin', 'ADMIN', NEWID()), ('R2', 'Manager', 'MANAGER', NEWID()), ('R3', 'User', 'USER', NEWID()), ('R4', 'Tech', 'TECH', NEWID()), ('R5', 'RH', 'RH', NEWID()),
('R6', 'Guest', 'GUEST', NEWID()), ('R7', 'Support', 'SUPPORT', NEWID()), ('R8', 'Fleet', 'FLEET', NEWID()), ('R9', 'SuperAdmin', 'SUPERADMIN', NEWID()), ('R10', 'Audit', 'AUDIT', NEWID());

-----------------------------------------------------------
-- 4. [AspNetUsers]
-----------------------------------------------------------
INSERT INTO [AspNetUsers] ([Id], [FirstName], [LastName], [OrganisationId], [Email], [NormalizedEmail], [UserName], [NormalizedUserName], [IsActif], [Role], [TailleCm], [EmailConfirmed], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount]) VALUES
('U01','Jean','Admin',1,'jean@mojo.com','JEAN@MOJO.COM','jadmin','JADMIN',1,1,180,1,0,0,1,0),
('U02','Marc','Manager',1,'marc@mojo.com','MARC@MOJO.COM','mmanager','MMANAGER',1,2,175,1,0,0,1,0),
('U03','Sophie','RH',2,'sophie@velocite.com','SOPHIE@VELOCITE.COM','srh','SRH',1,5,165,1,0,0,1,0),
('U04','Luc','Client',2,'luc@gmail.com','LUC@GMAIL.COM','lclient','LCLIENT',1,3,178,1,0,0,1,0),
('U05','Eva','User',3,'eva@outlook.com','EVA@OUTLOOK.COM','euser','EUSER',1,3,162,1,0,0,1,0),
('U06','Tom','Tech',4,'tom@tech.com','TOM@TECH.COM','ttech','TTECH',1,4,185,1,0,0,1,0),
('U07','Lea','User',5,'lea@nantes.com','LEA@NANTES.COM','luser','LUSER',1,3,170,1,0,0,1,0),
('U08','Bob','Mojo',1,'bob@mojo.com','BOB@MOJO.COM','bmojo','BMOJO',1,2,182,1,0,0,1,0),
('U09','Kim','User',6,'kim@green.com','KIM@GREEN.COM','kuser','KUSER',1,3,160,1,0,0,1,0),
('U10','Ian','User',7,'ian@urban.com','IAN@URBAN.COM','iuser','IUSER',1,3,188,1,0,0,1,0);

-----------------------------------------------------------
-- 5. [AspNetUserRoles]
-----------------------------------------------------------
INSERT INTO [AspNetUserRoles] ([UserId], [RoleId]) VALUES
('U01','R9'), ('U02','R2'), ('U03','R5'), ('U04','R3'), ('U05','R3'), ('U06','R4'), ('U07','R3'), ('U08','R2'), ('U09','R3'), ('U10','R3');

-----------------------------------------------------------
-- 6. [AspNetUserClaims]
-----------------------------------------------------------
INSERT INTO [AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES
('U01','Perm','Full'), ('U02','Zone','FR'), ('U03','Dept','HR'), ('U04','Type','Std'), ('U05','Type','Cargo'), ('U06','Skill','Elec'), ('U07','Portal','Yes'), ('U08','Level','Mid'), ('U09','Lang','DE'), ('U10','Club','Gold');

-----------------------------------------------------------
-- 7. [AspNetUserLogins]
-----------------------------------------------------------
INSERT INTO [AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES
('Google','G1','Google','U04'), ('MS','M2','Microsoft','U05'), ('Apple','A1','Apple','U07'), ('FB','F4','Facebook','U09'), ('GitHub','GH1','Git','U01'), ('Google','G2','Google','U02'), ('MS','M7','Azure','U03'), ('Twitter','T5','X','U10'), ('LinkedIn','L8','In','U08'), ('Google','G9','Gmail','U06');

-----------------------------------------------------------
-- 8. [AspNetUserTokens]
-----------------------------------------------------------
INSERT INTO [AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES
('U01','D','Reset','T1'), ('U02','D','Reset','T2'), ('U03','D','Reset','T3'), ('U04','D','Auth','T4'), ('U05','D','Auth','T5'), ('U06','D','Auth','T6'), ('U07','D','Auth','T7'), ('U08','D','Reset','T8'), ('U09','D','Reset','T9'), ('U10','D','Auth','T10');

-----------------------------------------------------------
-- 9. [AspNetRoleClaims]
-----------------------------------------------------------
INSERT INTO [AspNetRoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES
('R1','Power','Full'), ('R2','Users','Manage'), ('R3','Ride','True'), ('R4','Parts','Edit'), ('R5','Contract','Sign'), ('R1','DB','Backup'), ('R2','Audit','True'), ('R3','Support','True'), ('R4','Workshop','All'), ('R5','Pay','View');

-----------------------------------------------------------
-- 10. [Velos]
-----------------------------------------------------------
INSERT INTO [Velos] ([Marque], [Modele], [NumeroSerie], [PrixAchat], [Status], [CreatedDate], [ModifiedDate]) VALUES
('Moustache','Lundi 27','SN2026-001',2500,1,GETDATE(),GETDATE()), ('VanMoof','S5','SN2026-002',2900,1,GETDATE(),GETDATE()), ('Cowboy','C4','SN2026-003',2700,1,GETDATE(),GETDATE()), ('Giant','Explore','SN2026-004',2300,1,GETDATE(),GETDATE()), ('Specialized','Vado','SN2026-005',3500,1,GETDATE(),GETDATE()),
('Trek','Allant','SN2026-006',3800,1,GETDATE(),GETDATE()), ('Gazelle','Ultimate','SN2026-007',3100,0,GETDATE(),GETDATE()), ('Decathlon','Elops','SN2026-008',1400,1,GETDATE(),GETDATE()), ('Canyon','Precede','SN2026-009',4200,1,GETDATE(),GETDATE()), ('Riese','Nevo','SN2026-010',5200,0,GETDATE(),GETDATE());

-----------------------------------------------------------
-- 11. [Contrats]
-----------------------------------------------------------
INSERT INTO [Contrats] ([Ref], [VeloId], [BeneficiaireId], [UserRhId], [DateDebut], [DateFin], [Duree], [LoyerMensuelHT], [StatutContrat], [CreatedDate], [ModifiedDate]) VALUES
('CTR-M01',1,'U04','U03','2026-01-01','2028-01-01',24,85,1,GETDATE(),GETDATE()), ('CTR-V02',2,'U05','U03','2026-01-01','2028-01-01',24,95,1,GETDATE(),GETDATE()), ('CTR-E03',3,'U07','U03','2026-01-01','2028-01-01',24,110,1,GETDATE(),GETDATE()), ('CTR-L04',4,'U09','U03','2026-01-01','2028-01-01',24,120,1,GETDATE(),GETDATE()), ('CTR-N05',5,'U10','U03','2026-01-01','2028-01-01',24,130,1,GETDATE(),GETDATE()),
('CTR-G06',6,'U04','U03','2026-02-01','2029-02-01',36,140,1,GETDATE(),GETDATE()), ('CTR-U07',7,'U05','U03','2026-02-01','2029-02-01',36,150,0,GETDATE(),GETDATE()), ('CTR-S08',8,'U07','U03','2026-02-01','2029-02-01',36,60,1,GETDATE(),GETDATE()), ('CTR-SO09',9,'U09','U03','2026-02-01','2029-02-01',36,180,2,GETDATE(),GETDATE()), ('CTR-A10',1,'U10','U03','2026-03-01','2029-03-01',36,85,1,GETDATE(),GETDATE());

-----------------------------------------------------------
-- 12. [Amortissements]
-----------------------------------------------------------
INSERT INTO [Amortissements] ([VeloId], [DateDebut], [DureeMois], [ValeurInit], [ValeurResiduelleFinale], [CreatedDate], [ModifiedDate]) VALUES
(1,'2026-01-01',48,2500,500,GETDATE(),GETDATE()), (2,'2026-01-01',48,2900,580,GETDATE(),GETDATE()), (3,'2026-01-01',48,2700,540,GETDATE(),GETDATE()), (4,'2026-01-01',48,2300,460,GETDATE(),GETDATE()), (5,'2026-02-01',60,3500,700,GETDATE(),GETDATE()),
(6,'2026-02-01',60,3800,760,GETDATE(),GETDATE()), (7,'2026-02-01',60,3100,620,GETDATE(),GETDATE()), (8,'2026-03-01',48,1400,280,GETDATE(),GETDATE()), (9,'2026-03-01',60,4200,840,GETDATE(),GETDATE()), (10,'2026-03-01',60,5200,1040,GETDATE(),GETDATE());

-----------------------------------------------------------
-- 13. [Discussions]
-----------------------------------------------------------
INSERT INTO [Discussions] ([Objet], [ClientId], [MojoId], [Status], [DateCreation], [CreatedDate], [ModifiedDate]) VALUES
('Panne batterie','U04','U01',1,GETDATE(),GETDATE(),GETDATE()), ('Facture loyer','U05','U02',1,GETDATE(),GETDATE(),GETDATE()), ('Entretien annuel','U07','U01',1,GETDATE(),GETDATE(),GETDATE()), ('Vélo volé','U09','U01',0,GETDATE(),GETDATE(),GETDATE()), ('Bruit pédalier','U10','U02',1,GETDATE(),GETDATE(),GETDATE()),
('Changement RIB','U04','U02',1,GETDATE(),GETDATE(),GETDATE()), ('Freins lâches','U05','U06',1,GETDATE(),GETDATE(),GETDATE()), ('Extension garantie','U07','U06',1,GETDATE(),GETDATE(),GETDATE()), ('Usure pneus','U09','U01',1,GETDATE(),GETDATE(),GETDATE()), ('Clés perdues','U10','U02',1,GETDATE(),GETDATE(),GETDATE());

-----------------------------------------------------------
-- 14. [Messages]
-----------------------------------------------------------
INSERT INTO [Messages] ([Contenu], [DiscussionId], [DateEnvoi], [CreatedDate], [ModifiedDate]) VALUES
('Le vélo ne s''allume plus ce matin.', 1, GETDATE(), GETDATE(), GETDATE()),
('Vérifiez que la batterie est bien enclenchée.', 1, GETDATE(), GETDATE(), GETDATE()),
('C''est fait, mais l''écran reste noir.', 1, GETDATE(), GETDATE(), GETDATE()),
('Pouvez-vous m''envoyer la facture de janvier ?', 2, GETDATE(), GETDATE(), GETDATE()),
('Elle est disponible dans votre espace client.', 2, GETDATE(), GETDATE(), GETDATE()),
('Mon pneu arrière est lisse.', 9, GETDATE(), GETDATE(), GETDATE()),
('Nous prenons rendez-vous pour le remplacement.', 9, GETDATE(), GETDATE(), GETDATE()),
('On m''a volé mon vélo à Marseille.', 4, GETDATE(), GETDATE(), GETDATE()),
('Veuillez joindre le dépôt de plainte PDF.', 4, GETDATE(), GETDATE(), GETDATE()),
('Voici le document scanné.', 4, GETDATE(), GETDATE(), GETDATE());

-----------------------------------------------------------
-- 15. [Demandes]
-----------------------------------------------------------
INSERT INTO [Demandes] ([IdUser], [IdVelo], [DiscussionId], [Status], [CreatedDate], [ModifiedDate]) VALUES
('U04',1,1,1,GETDATE(),GETDATE()), ('U05',2,2,1,GETDATE(),GETDATE()), ('U07',3,3,1,GETDATE(),GETDATE()), ('U09',4,4,3,GETDATE(),GETDATE()), ('U10',5,5,1,GETDATE(),GETDATE()),
('U04',6,6,1,GETDATE(),GETDATE()), ('U05',7,7,2,GETDATE(),GETDATE()), ('U07',8,8,2,GETDATE(),GETDATE()), ('U09',9,9,1,GETDATE(),GETDATE()), ('U10',10,10,1,GETDATE(),GETDATE());

-----------------------------------------------------------
-- 16. [Interventions]
-----------------------------------------------------------
INSERT INTO [Interventions] ([VeloId], [TypeIntervention], [Description], [DateIntervention], [Cout], [CreatedDate], [ModifiedDate]) VALUES
(1,'Batterie','Changement cellules',GETDATE(),450,GETDATE(),GETDATE()), (2,'Pneus','Marathon Plus',GETDATE(),85,GETDATE(),GETDATE()), (3,'Freins','Purge liquide',GETDATE(),50,GETDATE(),GETDATE()), (7,'Cadre','Soudure support',GETDATE(),120,GETDATE(),GETDATE()), (8,'Logiciel','Update V3',GETDATE(),0,GETDATE(),GETDATE()),
(1,'Look','Peinture cadre',GETDATE(),60,GETDATE(),GETDATE()), (2,'Moteur','Nettoyage capteur',GETDATE(),90,GETDATE(),GETDATE()), (3,'Selle','Echange confort',GETDATE(),45,GETDATE(),GETDATE()), (4,'Chaine','Graissage',GETDATE(),25,GETDATE(),GETDATE()), (5,'Feu','Echange LED',GETDATE(),30,GETDATE(),GETDATE());

-----------------------------------------------------------
-- 17. [Documents] (PDF Binaires Variés)
-----------------------------------------------------------
-- Signature PDF standard (Variée pour simuler des fichiers différents)
INSERT INTO [Documents] ([ContratId], [Fichier], [CreatedDate], [ModifiedDate]) VALUES
(1, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(2, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(3, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(4, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(5, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(6, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(7, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(8, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(9, 0x255044462D312E340A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE()),
(10, 0x255044462D312E370A25C3A4C3BCC3B6C39F0A312030206F626A, GETDATE(), GETDATE());

-----------------------------------------------------------
-- 18. [AspNetUserRoles] (Seconde passe pour remplir les 18 tables)
-----------------------------------------------------------
INSERT INTO [AspNetUserRoles] ([UserId], [RoleId]) VALUES
('U01','R1'), ('U02','R8'), ('U03','R1'), ('U06','R7'), ('U08','R10'), ('U01','R2'), ('U02','R5'), ('U06','R3'), ('U08','R4'), ('U10','R6');

COMMIT;
GO
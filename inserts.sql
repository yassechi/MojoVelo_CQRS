BEGIN TRANSACTION;

BEGIN TRY
    -- 1. ORGANISATIONS (ID forcés de 1 à 10)
    SET IDENTITY_INSERT [Organisations] ON;
    INSERT INTO [Organisations] ([Id], [Name], [Code], [Address], [ContactEmail], [IsActif], [CreatedDate], [ModifiedDate], [CreatedBy])
    VALUES  
    (1, 'Mojo Velo HQ', 'MOJO01', 'Paris', 'contact@mojo.com', 1, GETDATE(), GETDATE(), 'System'),
    (2, 'Tech Solutions', 'TECH02', 'Lyon', 'rh@tech.fr', 1, GETDATE(), GETDATE(), 'System'),
    (3, 'Green Delivery', 'GRN03', 'Nantes', 'info@green.com', 1, GETDATE(), GETDATE(), 'System'),
    (4, 'Eco Build', 'ECO04', 'Lille', 'admin@ecobuild.fr', 1, GETDATE(), GETDATE(), 'System'),
    (5, 'Urban Move', 'URB05', 'Marseille', 'contact@urban.io', 1, GETDATE(), GETDATE(), 'System'),
    (6, 'Fast Track', 'FST06', 'Bordeaux', 'hr@fast.com', 1, GETDATE(), GETDATE(), 'System'),
    (7, 'Smart City', 'SMR07', 'Nice', 'city@smart.fr', 1, GETDATE(), GETDATE(), 'System'),
    (8, 'Future Bike', 'FUT08', 'Toulouse', 'bike@future.net', 1, GETDATE(), GETDATE(), 'System'),
    (9, 'Solar Power', 'SOL09', 'Rennes', 'solar@rennes.com', 1, GETDATE(), GETDATE(), 'System'),
    (10, 'Global Corp', 'GLO10', 'Montpellier', 'staff@global.io', 1, GETDATE(), GETDATE(), 'System');
    SET IDENTITY_INSERT [Organisations] OFF;

    -- 2. ASPNETROLES
    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES  
    ('R1', 'Admin', 'ADMIN', NEWID()), ('R2', 'Manager', 'MANAGER', NEWID()),  
    ('R3', 'User', 'USER', NEWID()), ('R4', 'Technician', 'TECHNICIAN', NEWID()),
    ('R5', 'HR', 'HR', NEWID()), ('R6', 'Guest', 'GUEST', NEWID()),
    ('R7', 'SuperAdmin', 'SUPERADMIN', NEWID()), ('R8', 'Support', 'SUPPORT', NEWID()),
    ('R9', 'Vendor', 'VENDOR', NEWID()), ('R10', 'Analyst', 'ANALYST', NEWID());

    -- 3. ASPNETROLECLAIMS
    INSERT INTO [AspNetRoleClaims] ([RoleId], [ClaimType], [ClaimValue])
    VALUES ('R1', 'Access', 'Full'), ('R2', 'Access', 'Limited'), ('R4', 'Action', 'Repair');

    -- 4. VELOS (ID forcés de 1 à 10)
    SET IDENTITY_INSERT [Velos] ON;
    INSERT INTO [Velos] ([Id], [NumeroSerie], [Marque], [Modele], [PrixAchat], [Status], [CreatedDate], [ModifiedDate], [CreatedBy])
    VALUES  
    (1, 'SN-001', 'VanMoof', 'S5', 2500, 1, GETDATE(), GETDATE(), 'System'),
    (2, 'SN-002', 'Cowboy', 'C4', 2800, 1, GETDATE(), GETDATE(), 'System'),
    (3, 'SN-003', 'Trek', 'Domane', 3200, 1, GETDATE(), GETDATE(), 'System'),
    (4, 'SN-004', 'Giant', 'Explore', 2400, 1, GETDATE(), GETDATE(), 'System'),
    (5, 'SN-005', 'Moustache', 'Lundi', 3000, 1, GETDATE(), GETDATE(), 'System'),
    (6, 'SN-006', 'Specialized', 'Turbo', 4000, 1, GETDATE(), GETDATE(), 'System'),
    (7, 'SN-007', 'Canyon', 'Roadlite', 1800, 1, GETDATE(), GETDATE(), 'System'),
    (8, 'SN-008', 'Scott', 'Sub', 2100, 1, GETDATE(), GETDATE(), 'System'),
    (9, 'SN-009', 'Cannondale', 'Neo', 3500, 1, GETDATE(), GETDATE(), 'System'),
    (10, 'SN-010', 'Rad Power', 'Mission', 1200, 1, GETDATE(), GETDATE(), 'System');
    SET IDENTITY_INSERT [Velos] OFF;

    -- 5. ASPNETUSERS
    INSERT INTO [AspNetUsers] ([Id], [FirstName], [LastName], [Role], [TailleCm], [IsActif], [OrganisationId], [UserName], [Email], [EmailConfirmed], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount])
    VALUES  
    ('U01', 'Admin', 'Mojo', 1, 180, 1, 1, 'admin', 'admin@mojo.com', 1, 1, 0, 1, 0),
    ('U02', 'Manager', 'Manager', 2, 175, 1, 2, 'marc_mgr', 'manager@manager.be', 1, 1, 0, 1, 0),
    ('U03', 'user', 'user', 5, 165, 1, 2, 'sophie_rh', 'user@user.be', 1, 1, 0, 1, 0),
    ('U04', 'Jean', 'Dupont', 3, 170, 1, 2, 'jdupont', 'j.dupont@tech.fr', 1, 1, 0, 1, 0),
    ('U05', 'Lucie', 'Martin', 3, 160, 1, 3, 'lmartin', 'l.martin@green.com', 1, 1, 0, 1, 0),
    ('U06', 'Thomas', 'Petit', 3, 185, 1, 3, 'tpetit', 't.petit@green.com', 1, 1, 0, 1, 0),
    ('U07', 'Emma', 'Leroy', 3, 168, 1, 4, 'eleroy', 'e.leroy@eco.fr', 1, 1, 0, 1, 0),
    ('U08', 'Kevin', 'Moreau', 3, 172, 1, 4, 'kmoreau', 'k.moreau@eco.fr', 1, 1, 0, 1, 0),
    ('U09', 'Julie', 'Bernard', 3, 162, 1, 5, 'jbernard', 'j.bernard@urban.io', 1, 1, 0, 1, 0),
    ('U10', 'Paul', 'Simon', 4, 177, 1, 1, 'psimon_tech', 'paul@mojo.com', 1, 1, 0, 1, 0);

    -- 6. ASPNETUSERROLES
    INSERT INTO [AspNetUserRoles] ([UserId], [RoleId]) VALUES ('U01','R1'), ('U02','R2'), ('U03','R5'), ('U04','R3'), ('U10','R4');

    -- 7. ASPNETUSERCLAIMS
    INSERT INTO [AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES ('U01','Dev','All');

    -- 8. ASPNETUSERLOGINS & TOKENS
    INSERT INTO [AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES ('Local','K1','Pwd','U04');
    INSERT INTO [AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES ('U04','Default','AToken','XYZ');

    -- 9. CONTRATS
    INSERT INTO [Contrats] ([DateDebut], [DateFin], [LoyerMensuelHT], [StatutContrat], [VeloId], [BeneficiaireId], [UserRhId], [CreatedDate], [ModifiedDate])
    VALUES  
    (GETDATE(), DATEADD(year,1,GETDATE()), 150, 1, 1, 'U04', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 160, 1, 2, 'U05', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 170, 1, 3, 'U06', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 180, 1, 4, 'U07', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 155, 1, 5, 'U08', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 200, 1, 6, 'U09', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 150, 1, 7, 'U04', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 140, 1, 8, 'U05', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 190, 1, 9, 'U06', 'U03', GETDATE(), GETDATE()),
    (GETDATE(), DATEADD(year,1,GETDATE()), 130, 0, 10, 'U07', 'U03', GETDATE(), GETDATE());

    -- 10. INTERVENTIONS
    INSERT INTO [Interventions] ([DateIntervention], [TypeIntervention], [Description], [Cout], [VeloId], [CreatedDate], [ModifiedDate])
    VALUES  
    (DATEADD(month, -6, GETDATE()), 'Révision', 'Initiale', 0, 1, GETDATE(), GETDATE()),
    (DATEADD(month, -1, GETDATE()), 'Révision', 'Contrôle freins', 45, 1, GETDATE(), GETDATE()),
    (DATEADD(month, -2, GETDATE()), 'Révision', 'Graissage chaîne', 20, 2, GETDATE(), GETDATE()),
    (GETDATE(), 'Freins', 'Changement plaquettes', 65, 3, GETDATE(), GETDATE()),
    (DATEADD(month, -1, GETDATE()), 'Cadre', 'Peinture retouche', 120, 6, GETDATE(), GETDATE()),
    (GETDATE(), 'Batterie', 'Test capacité', 50, 6, GETDATE(), GETDATE()),
    (GETDATE(), 'Révision', 'Contrôle hiver', 45, 8, GETDATE(), GETDATE());

    -- 11. AMORTISSEMENTS
    INSERT INTO [Amortissements] ([DateDebut], [ValeurInit], [DureeMois], [ValeurResiduelleFinale], [VeloId], [CreatedDate], [ModifiedDate])
    VALUES  
    (DATEADD(year, -1, GETDATE()), 2500, 36, 500, 1, GETDATE(), GETDATE()),
    (DATEADD(year, -1, GETDATE()), 2800, 36, 600, 2, GETDATE(), GETDATE()),
    (DATEADD(month, -6, GETDATE()), 3200, 48, 800, 3, GETDATE(), GETDATE()),
    (GETDATE(), 2400, 36, 400, 4, GETDATE(), GETDATE()),
    (GETDATE(), 3000, 36, 500, 5, GETDATE(), GETDATE()),
    (DATEADD(year, -2, GETDATE()), 4000, 60, 1000, 6, GETDATE(), GETDATE()),
    (GETDATE(), 1800, 24, 200, 7, GETDATE(), GETDATE()),
    (GETDATE(), 2100, 36, 400, 8, GETDATE(), GETDATE()),
    (GETDATE(), 3500, 48, 700, 9, GETDATE(), GETDATE()),
    (DATEADD(month, -24, GETDATE()), 1200, 24, 100, 10, GETDATE(), GETDATE());

    -- 12. DISCUSSIONS (ID forcés de 1 à 15)
    SET IDENTITY_INSERT [Discussions] ON;
    INSERT INTO [Discussions] ([Id], [Objet], [Status], [DateCreation], [ClientId], [MojoId], [CreatedDate], [ModifiedDate])
    VALUES  
    (1, 'Vélo volé', 1, GETDATE(), 'U04', 'U01', GETDATE(), GETDATE()),
    (2, 'Demande de casque', 1, GETDATE(), 'U05', 'U01', GETDATE(), GETDATE()),
    (3, 'Batterie HS', 1, GETDATE(), 'U06', 'U01', GETDATE(), GETDATE()),
    (4, 'Facture incorrecte', 1, GETDATE(), 'U08', 'U01', GETDATE(), GETDATE()),
    (5, 'Pneu crevé', 1, GETDATE(), 'U04', 'U01', GETDATE(), GETDATE()),
    (6, 'Changement de taille', 1, GETDATE(), 'U07', 'U01', GETDATE(), GETDATE()),
    (7, 'Bruit suspect pédalier', 1, GETDATE(), 'U09', 'U01', GETDATE(), GETDATE()),
    (8, 'Rappel maintenance', 1, GETDATE(), 'U05', 'U01', GETDATE(), GETDATE()),
    (9, 'Code cadenas perdu', 1, GETDATE(), 'U06', 'U01', GETDATE(), GETDATE()),
    (10, 'Panier avant cassé', 1, GETDATE(), 'U08', 'U01', GETDATE(), GETDATE()),
    (11, 'Question assurance vol', 1, GETDATE(), 'U04', 'U01', GETDATE(), GETDATE()),
    (12, 'Livraison accessoires', 1, GETDATE(), 'U05', 'U01', GETDATE(), GETDATE()),
    (13, 'Freins qui sifflent', 1, GETDATE(), 'U09', 'U01', GETDATE(), GETDATE()),
    (14, 'Selle inconfortable', 1, GETDATE(), 'U06', 'U01', GETDATE(), GETDATE()),
    (15, 'Demande de rachat vélo', 0, GETDATE(), 'U04', 'U01', GETDATE(), GETDATE());
    SET IDENTITY_INSERT [Discussions] OFF;

    -- 13. MESSAGES
    INSERT INTO [Messages] ([Contenu], [DateEnvoi], [DiscussionId], [CreatedDate], [ModifiedDate])
    VALUES  
    ('On m a volé mon vélo ce matin', GETDATE(), 1, GETDATE(), GETDATE()),
    ('Désolé de l apprendre. Avez-vous le PV ?', GETDATE(), 1, GETDATE(), GETDATE()),
    ('Oui je l envoie en pièce jointe', GETDATE(), 1, GETDATE(), GETDATE()),
    ('Bien reçu, nous traitons le dossier', GETDATE(), 1, GETDATE(), GETDATE()),
    ('Puis-je avoir un casque bleu ?', GETDATE(), 2, GETDATE(), GETDATE()),
    ('Oui, c est en stock.', GETDATE(), 2, GETDATE(), GETDATE()),
    ('Ma batterie ne charge plus', GETDATE(), 3, GETDATE(), GETDATE()),
    ('Le technicien passe demain', GETDATE(), 3, GETDATE(), GETDATE()),
    ('Il y a une erreur de 10 euros', GETDATE(), 4, GETDATE(), GETDATE()),
    ('Nous vérifions vos prélèvements', GETDATE(), 4, GETDATE(), GETDATE()),
    ('Pneu arrière à plat !', GETDATE(), 5, GETDATE(), GETDATE()),
    ('Utilisez le kit de secours en attendant', GETDATE(), 5, GETDATE(), GETDATE()),
    ('Ça claque quand je pédale vite', GETDATE(), 7, GETDATE(), GETDATE()),
    ('Je ne me souviens plus de mon code', GETDATE(), 9, GETDATE(), GETDATE()),
    ('C est le 1234 par défaut', GETDATE(), 9, GETDATE(), GETDATE()),
    ('Les freins font un bruit de métal', GETDATE(), 13, GETDATE(), GETDATE()),
    ('Ma selle est trop dure', GETDATE(), 14, GETDATE(), GETDATE()),
    ('On peut vous installer une selle gel', GETDATE(), 14, GETDATE(), GETDATE()),
    ('Génial, merci Mojo !', GETDATE(), 14, GETDATE(), GETDATE()),
    ('Je voudrais racheter mon vélo actuel', GETDATE(), 15, GETDATE(), GETDATE());

    COMMIT TRANSACTION;
    PRINT 'Script INTÉGRAL exécuté avec succès !';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Erreur rencontrée : ' + ERROR_MESSAGE();
    THROW;
END CATCH
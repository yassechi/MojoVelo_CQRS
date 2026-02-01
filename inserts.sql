BEGIN TRANSACTION;

BEGIN TRY
    -- 1. ORGANISATIONS (10)
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

    -- 2. ASPNETROLES (3 Rôles demandés)
    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES 
    ('R1', 'Mojo', 'MOJO', NEWID()), 
    ('R2', 'Negociateur', 'NEGOCIATEUR', NEWID()), 
    ('R3', 'User', 'USER', NEWID());

    -- 3. ASPNETROLECLAIMS (10)
    INSERT INTO [AspNetRoleClaims] ([RoleId], [ClaimType], [ClaimValue])
    VALUES 
    ('R1', 'Permission', 'All'), ('R1', 'Access', 'Full'), 
    ('R2', 'Permission', 'Negotiate'), ('R2', 'Access', 'Advanced'),
    ('R3', 'Permission', 'View'), ('R3', 'Access', 'Limited'),
    ('R1', 'Module', 'Admin'), ('R2', 'Module', 'Sales'), 
    ('R3', 'Module', 'Client'), ('R1', 'System', 'Root');

    -- 4. VELOS (10)
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

    -- 5. ASPNETUSERS (10) - Password: "123456"
    DECLARE @PwdHash NVARCHAR(MAX) = 'AQAAAAEAACcQAAAAEPv/5jL7Hj0G8Y5O0zVvI/E3L/v9wO0jK0Y5S7m1vX8Z9L0M0N0O0P0Q0R0S0T0U==';

    INSERT INTO [AspNetUsers] ([Id], [FirstName], [LastName], [Role], [TailleCm], [IsActif], [OrganisationId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount], [SecurityStamp], [PasswordHash])
    VALUES 
    ('U01', 'Admin', 'Mojo', 1, 180, 1, 1, 'admin', 'ADMIN', 'admin@mojo.com', 'ADMIN@MOJO.COM', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U02', 'Marc', 'Nego', 2, 175, 1, 2, 'marc_nego', 'MARC_NEGO', 'marc@tech.fr', 'MARC@TECH.FR', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U03', 'Sophie', 'Nego', 2, 165, 1, 2, 'sophie_nego', 'SOPHIE_NEGO', 'sophie@tech.fr', 'SOPHIE@TECH.FR', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U04', 'Jean', 'User', 3, 170, 1, 2, 'juser', 'JUSER', 'j.dupont@tech.fr', 'J.DUPONT@TECH.FR', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U05', 'Lucie', 'User', 3, 160, 1, 3, 'luser', 'LUSER', 'l.martin@green.com', 'L.MARTIN@GREEN.COM', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U06', 'Thomas', 'User', 3, 185, 1, 3, 'tuser', 'TUSER', 't.petit@green.com', 'T.PETIT@GREEN.COM', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U07', 'Emma', 'User', 3, 168, 1, 4, 'euser', 'EUSER', 'e.leroy@eco.fr', 'E.LEROY@ECO.FR', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U08', 'Kevin', 'User', 3, 172, 1, 4, 'kuser', 'KUSER', 'k.moreau@eco.fr', 'K.MOREAU@ECO.FR', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U09', 'Julie', 'User', 3, 162, 1, 5, 'july', 'JULY', 'j.bernard@urban.io', 'J.BERNARD@URBAN.IO', 1, 1, 0, 1, 0, NEWID(), @PwdHash),
    ('U10', 'Paul', 'User', 3, 177, 1, 1, 'paul_u', 'PAUL_U', 'paul@mojo.com', 'PAUL@MOJO.COM', 1, 1, 0, 1, 0, NEWID(), @PwdHash);

    -- 6. ASPNETUSERROLES (Répartition sur les 3 nouveaux rôles)
    INSERT INTO [AspNetUserRoles] ([UserId], [RoleId]) VALUES 
    ('U01','R1'), ('U02','R2'), ('U03','R2'), ('U04','R3'), ('U05','R3'), 
    ('U06','R3'), ('U07','R3'), ('U08','R3'), ('U09','R3'), ('U10','R3');

    -- 7. ASPNETUSERCLAIMS (10)
    INSERT INTO [AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES 
    ('U01','Dev','All'), ('U02','Nego','Level1'), ('U03','Nego','Level2'), ('U04','Client','Basic'),
    ('U05','Client','Basic'), ('U06','Client','Basic'), ('U07','Client','Basic'),
    ('U08','Client','Basic'), ('U09','Client','Basic'), ('U10','Client','Basic');

    -- 8. ASPNETUSERLOGINS (10)
    INSERT INTO [AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES 
    ('Local','K1','Pwd','U01'), ('Local','K2','Pwd','U02'), ('Local','K3','Pwd','U03'),
    ('Local','K4','Pwd','U04'), ('Local','K5','Pwd','U05'), ('Local','K6','Pwd','U06'),
    ('Local','K7','Pwd','U07'), ('Local','K8','Pwd','U08'), ('Local','K9','Pwd','U09'), ('Local','K10','Pwd','U10');

    -- 9. ASPNETUSERTOKENS (10)
    INSERT INTO [AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES 
    ('U01','Default','T1','V1'), ('U02','Default','T2','V2'), ('U03','Default','T3','V3'),
    ('U04','Default','T4','V4'), ('U05','Default','T5','V5'), ('U06','Default','T6','V6'),
    ('U07','Default','T7','V7'), ('U08','Default','T8','V8'), ('U09','Default','T9','V9'), ('U10','Default','T10','V10');

    -- 10. CONTRATS (10)
    INSERT INTO [Contrats] ([DateDebut], [DateFin], [LoyerMensuelHT], [StatutContrat], [VeloId], [BeneficiaireId], [UserRhId], [CreatedDate], [ModifiedDate])
    VALUES 
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 150, 1, 1, 'U04', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 160, 1, 2, 'U05', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 170, 1, 3, 'U06', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 180, 1, 4, 'U07', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 155, 1, 5, 'U08', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 200, 1, 6, 'U09', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 150, 1, 7, 'U04', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 140, 1, 8, 'U05', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 190, 1, 9, 'U06', 'U03', GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), CAST(DATEADD(year,1,GETDATE()) AS DATE), 130, 0, 10, 'U07', 'U03', GETDATE(), GETDATE());

    -- 11. INTERVENTIONS (10)
    INSERT INTO [Interventions] ([DateIntervention], [TypeIntervention], [Description], [Cout], [VeloId], [CreatedDate], [ModifiedDate])
    VALUES 
    (GETDATE(), 'Révision', 'OK', 0, 1, GETDATE(), GETDATE()), (GETDATE(), 'Freins', 'Plaquettes', 45, 1, GETDATE(), GETDATE()),
    (GETDATE(), 'Chaîne', 'Graissage', 20, 2, GETDATE(), GETDATE()), (GETDATE(), 'Pneus', 'Changement', 60, 3, GETDATE(), GETDATE()),
    (GETDATE(), 'Batterie', 'Test', 10, 6, GETDATE(), GETDATE()), (GETDATE(), 'Selle', 'Fixation', 5, 5, GETDATE(), GETDATE()),
    (GETDATE(), 'Lumière', 'Pile', 15, 8, GETDATE(), GETDATE()), (GETDATE(), 'Cadre', 'Nettoyage', 10, 9, GETDATE(), GETDATE()),
    (GETDATE(), 'Dérailleur', 'Réglage', 30, 4, GETDATE(), GETDATE()), (GETDATE(), 'Pédales', 'Serrage', 12, 7, GETDATE(), GETDATE());

    -- 12. AMORTISSEMENTS (10)
    INSERT INTO [Amortissements] ([DateDebut], [ValeurInit], [DureeMois], [ValeurResiduelleFinale], [VeloId], [CreatedDate], [ModifiedDate])
    VALUES 
    (CAST(GETDATE() AS DATE), 2500, 36, 500, 1, GETDATE(), GETDATE()), (CAST(GETDATE() AS DATE), 2800, 36, 600, 2, GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), 3200, 48, 800, 3, GETDATE(), GETDATE()), (CAST(GETDATE() AS DATE), 2400, 36, 400, 4, GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), 3000, 36, 500, 5, GETDATE(), GETDATE()), (CAST(GETDATE() AS DATE), 4000, 60, 1000, 6, GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), 1800, 24, 200, 7, GETDATE(), GETDATE()), (CAST(GETDATE() AS DATE), 2100, 36, 400, 8, GETDATE(), GETDATE()),
    (CAST(GETDATE() AS DATE), 3500, 48, 700, 9, GETDATE(), GETDATE()), (CAST(GETDATE() AS DATE), 1200, 24, 100, 10, GETDATE(), GETDATE());

    -- 13. DISCUSSIONS (10)
    SET IDENTITY_INSERT [Discussions] ON;
    INSERT INTO [Discussions] ([Id], [Objet], [Status], [DateCreation], [ClientId], [MojoId], [CreatedDate], [ModifiedDate])
    VALUES 
    (1, 'Freins', 1, GETDATE(), 'U04', 'U01', GETDATE(), GETDATE()), (2, 'Loyer', 1, GETDATE(), 'U05', 'U01', GETDATE(), GETDATE()),
    (3, 'Panne', 1, GETDATE(), 'U06', 'U01', GETDATE(), GETDATE()), (4, 'Retour', 0, GETDATE(), 'U07', 'U01', GETDATE(), GETDATE()),
    (5, 'Accessoire', 1, GETDATE(), 'U08', 'U01', GETDATE(), GETDATE()), (6, 'Taille', 1, GETDATE(), 'U09', 'U01', GETDATE(), GETDATE()),
    (7, 'Contrat', 0, GETDATE(), 'U04', 'U02', GETDATE(), GETDATE()), (8, 'Facture', 1, GETDATE(), 'U02', 'U01', GETDATE(), GETDATE()),
    (9, 'Vol', 1, GETDATE(), 'U05', 'U01', GETDATE(), GETDATE()), (10, 'Entretien', 0, GETDATE(), 'U06', 'U01', GETDATE(), GETDATE());
    SET IDENTITY_INSERT [Discussions] OFF;

    -- 14. MESSAGES (10)
    INSERT INTO [Messages] ([Contenu], [DateEnvoi], [DiscussionId], [CreatedDate], [ModifiedDate])
    VALUES 
    ('Bonjour', GETDATE(), 1, GETDATE(), GETDATE()), ('Salut', GETDATE(), 1, GETDATE(), GETDATE()),
    ('Question loyer', GETDATE(), 2, GETDATE(), GETDATE()), ('Réponse loyer', GETDATE(), 2, GETDATE(), GETDATE()),
    ('Vélo en panne', GETDATE(), 3, GETDATE(), GETDATE()), ('On arrive', GETDATE(), 3, GETDATE(), GETDATE()),
    ('Rendu vélo', GETDATE(), 4, GETDATE(), GETDATE()), ('OK reçu', GETDATE(), 4, GETDATE(), GETDATE()),
    ('Merci', GETDATE(), 5, GETDATE(), GETDATE()), ('De rien', GETDATE(), 5, GETDATE(), GETDATE());

    COMMIT TRANSACTION;
    PRINT 'Script final exécuté avec 3 rôles et mot de passe 123456.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'ERREUR : ' + ERROR_MESSAGE();
END CATCH;





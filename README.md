# 🚲 MojoVelo — Plateforme SaaS de Leasing Vélo

MojoVelo est une plateforme SaaS de leasing de vélos destinée aux entreprises.
Elle connecte les employés, les ressources humaines (RH) et MojoVelo via une application moderne, sécurisée et temps réel.

Objectif : digitaliser l’ensemble du cycle de leasing vélo, de la sélection à la maintenance, avec une forte orientation UX, sécurité et scalabilité.

---

## Auteur du projet
- Nom : Yassine Echiguer
- Rôle : Full-Stack Developer (.NET / Angular)
- Organisation : Mojo Application
- Localisation : Belgique

---

## Fonctionnalités principales

### Parcours Employé
- Diagnostic intelligent (questionnaire de 6 questions)
- Analyse des besoins (distance, terrain, usage)
- Catalogue multi-marques (Lapierre, Gazelle, Ridley, etc.)
- Filtres dynamiques
- Configuration personnalisée (vélo + accessoires)
- Calcul du loyer mensuel en temps réel
- Suivi du statut de la demande

### Gestion RH (Administration)
- Dashboard décisionnel
- KPIs : budget engagé, contrats actifs, demandes en attente
- Workflow : validation, refus, demande de modification
- Notifications temps réel
- Suivi financier
- Tableaux d’amortissement automatiques
- Historique et traçabilité des décisions

### Support & Maintenance (MojoVelo)
- Gestion du parc vélo
- Suivi des stocks par numéro de série unique
- Gestion du catalogue
- SAV intégré
- Messagerie instantanée temps réel
- Suivi des incidents

---

## Architecture & Stack Technique

### Backend — .NET 9
- Clean Architecture (Onion)
- Domain / Application / Infrastructure / API
- CQRS + MediatR
- FluentValidation
- Entity Framework Core
- SQL Server
- SignalR
- ASP.NET Identity + JWT

### Frontend — Angular
- Angular (dernière version stable)
- Architecture modulaire
- UI moderne et responsive
- Optimisé mobile & tablette
- RxJS
- Intégration SignalR

---

## Sécurité & Conformité
- Authentification JWT
- Isolation des données par organisation
- Restriction par domaine professionnel
- Configuration CORS
- Conformité RGPD
- Logs d’audit

---

## Installation & Lancement

### Cloner le dépôt
```bash
git clone https://github.com/yassechi/mojovelo.git
cd mojovelo
```

### Backend (.NET)
Configurer la chaîne de connexion SQL dans :
```bash
appsettings.json
```

Appliquer les migrations :
```bash
dotnet ef database update
```
Lancer l’API :
```bash
dotnet run
```
### Frontend (Angular)
```bash
cd client
npm install
ng serve
```
---

## Structure du projet
```bash
/src
 ├── MojoVelo.Domain
 ├── MojoVelo.Application
 ├── MojoVelo.Infrastructure
 ├── MojoVelo.API
/client
 ├── src/app
 ├── services
 ├── components
```
---

## Concepts & Bonnes Pratiques
```bash
- Clean Architecture
- CQRS
- Domain-Driven Design
- Séparation des responsabilités
- Temps réel (SignalR)
- Sécurité JWT
- Scalabilité SaaS
```
---

## Statut du projet
En cours de développement — vers un SaaS robuste et prêt pour l’échelle.

---

## Contact

Développeur :
- Nom : Yassine Echiguer
- Téléphone : +32 499 41 33 17
- Email : yassechi@gmail.com

Entreprise :
- Mojo Vélo
- Rue d’Ecolys 5, 5020 Suarlée, Belgique

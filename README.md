# MojoVelo - API (CQRS)

Plateforme de leasing de velos pour entreprises. Cette API expose la logique metier (CQRS + Clean Architecture) et alimente le front Angular.

**Stack**
- ASP.NET Core (.NET 9)
- EF Core + SQL Server
- Identity + JWT
- MediatR (CQRS)
- Semantic Kernel + Ollama (RAG)

**Prerequis**
- .NET SDK 9
- SQL Server (LocalDB / Express / Docker)
- Node 18+ et Angular CLI 17+ (pour le front)
- Ollama (optionnel, pour l'IA)

**Configuration**
Edite `src/API/Mojo.API/appsettings.json` ou utilise les User Secrets.
- `ConnectionStrings:DefaultConnection`
- `JwtSettings:Key` (min 32 caracteres)
- `EmailSettings:*` (si besoin)
- `Ollama:Endpoint` (ex: http://localhost:11434)
- `AI:AdminPdfsDir` et `AI:ClientPdfsDir`

Exemple User Secrets (placeholders) :
```powershell
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:Key" "YOUR_SECRET_KEY_MIN_32_CHARS"
dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.example.com"
dotnet user-secrets set "EmailSettings:SmtpUsername" "user@example.com"
dotnet user-secrets set "EmailSettings:SmtpPassword" "YOUR_PASSWORD"
```

**Lancer l'API**
1. Installer les outils EF (si besoin) :
```powershell
dotnet tool install --global dotnet-ef
```
2. Appliquer les migrations :
```powershell
cd src\API\Mojo.API
dotnet ef database update --project ..\..\infrastructure\Mojo.Persistence\Mojo.Persistence.csproj --startup-project .\Mojo.API.csproj
```
3. Lancer l'API :
```powershell
dotnet run
```

**URLs utiles**
- API: https://localhost:7000
- Swagger: https://localhost:7000/swagger

**IA / RAG (optionnel)**
L'IA utilise Ollama. Modeles attendus :
- `llama3.2:1b` (chat)
- `nomic-embed-text` (embeddings)
- `gemma3:4b` (juge)

Installation rapide :
```powershell
ollama serve
ollama pull llama3.2:1b
ollama pull nomic-embed-text
ollama pull gemma3:4b
```

Les PDFs sont stockes ici :
- Admin: `src/API/Mojo.API/pdfs/admin`
- Client: `src/API/Mojo.API/pdfs/client`

**Frontend (repo separe)**
Le front Angular est dans `MojoVelo_Angular`.
```powershell
cd ..\..\..\MojoVelo_Angular
npm install
ng serve
```
L'API doit etre en HTTPS (https://localhost:7000) pour coller aux URLs du front.

**Donnees de test**
Des scripts SQL sont disponibles a la racine :
- `inserts.sql`
- `DropTables.sql`

**Structure**
- `src/API` : Controllers + Program.cs
- `src/Application` : CQRS, DTOs, Validators
- `src/Domain` : Entites et enums
- `src/Infrastructure` : Services externes (IA, email)
- `src/Persistence` : EF Core + Migrations

# TokenAuth
Following codes creates project <br>

dotnet new webapi -o TokenAuthApi<br>
cd TokenAuthApi<br>
dotnet add package Microsoft.EntityFrameworkCore.InMemory<br>
code -r ../TokenAuthApi<br>

Trust the HTTPS development certificate by running the following command<br>

dotnet dev-certs https --trust<br>

Scaffold a controller<br>

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design<br>
dotnet add package Microsoft.EntityFrameworkCore.Design<br>
dotnet add package Microsoft.EntityFrameworkCore.SqlServer<br>
dotnet tool install -g dotnet-aspnet-codegenerator<br>
dotnet aspnet-codegenerator controller -name TokenAuthController -async -api -m TodoItem -dc TokenAuthContext -outDir Controllers<br>

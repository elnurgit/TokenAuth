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

With following tool you can create migrate and update database<br>

dotnet tool install --global dotnet-ef<br>
Before you can use the tools on a specific project, you'll need to add the <strong>Microsoft.EntityFrameworkCore.Design</strong> package to it.

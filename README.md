### PMC Reverse eng

Scaffold-DbContext 'Server = MUSTAFABRLS; Database = ETRADE; Trusted_Connection = true; TrustServerCertificate = True;' Microsoft.EntityFrameworkCore.SqlServer -Tables Customers, Orders [-Context ETradeContextDb] or  [ -ContextDir Contexts  -OutputDir Entities]  -Namespace Example.Entities  -ContextNamespace Example.Contexts

### CLI Reverse eng

dotnet ef dbcontext Scaffold-DbContext 'Server = MUSTAFABRLS; Database = ETRADE; Trusted_Connection = true; TrustServerCertificate = True;' Microsoft.EntityFrameworkCore.SqlServer —table orders, —table customers  [—Context ETradeContextDb] or  [ —Context-dir Contexts  -Output-dir Entities]  —namespace Example.Entities  —context-namespace Example.Contexts

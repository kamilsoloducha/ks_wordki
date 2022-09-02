Model Generation:
dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;" Npgsql.EntityFrameworkCore.PostgreSQL --schema "users" -o Model/Users -c UsersContext

dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;" Npgsql.EntityFrameworkCore.PostgreSQL --schema "cards" -o Model/Cards -c CardsContext

dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;" Npgsql.EntityFrameworkCore.PostgreSQL --schema "lessons" -o Model/Lessons -c LessonsContext
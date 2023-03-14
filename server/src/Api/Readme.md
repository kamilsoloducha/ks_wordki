dotnet ef migrations add test -c UsersContext -p ./src/Modules/Users/Infrastructure/Users.Infrastructure.csproj -s ./src/Api/Api.csproj
dotnet ef migrations script -c UsersContext -p ./src/Modules/Users/Infrastructure/Users.Infrastructure.csproj -s ./src/Api/Api.csproj -o users.sql

dotnet ef migrations add test -c CardsContext -p ./src/Modules/Cards/Infrastructure/Cards.Infrastructure.csproj -s ./src/Api/Api.csproj
dotnet ef migrations script -c CardsContext -p ./src/Modules/Cards/Infrastructure/Cards.Infrastructure.csproj -s ./src/Api/Api.csproj -o cards.sql

dotnet ef migrations add test -c LessonsContext -p ./src/Modules/Lessons/Infrastructure/Lessons.Infrastructure.csproj -s ./src/Api/Api.csproj
dotnet ef migrations script -c LessonsContext -p ./src/Modules/Lessons/Infrastructure/Lessons.Infrastructure.csproj -s ./src/Api/Api.csproj -o lessons.sql
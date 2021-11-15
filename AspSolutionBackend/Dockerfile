
FROM mcr.microsoft.com/dotnet/sdk:5.0 as BUILD
WORKDIR /source

COPY *.sln .
COPY Directory.Build.props .


#timezone problem
RUN apt-get update -y
RUN apt-get install -y tzdata
ENV TZ Europe/Tallinn


# Copy all the project files
# Base projects
COPY Contracts.BLL.Base/*.csproj ./Contracts.BLL.Base/
COPY Contracts.DAL.Base/*.csproj ./Contracts.DAL.Base/
COPY Contracts.DAL.Domain/*.csproj ./Contracts.DAL.Domain/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/
COPY Domain.Base/*.csproj ./Domain.Base/
COPY Graph/*.csproj ./Graph/
COPY Utils/*.csproj ./Utils/

#App projects
COPY App.Domain/*.csproj ./App.Domain/
COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY PublicApiDTO/*.csproj ./PublicApiDTO/
COPY Services/*.csproj ./Services/
COPY WebApp/*.csproj ./WebApp/

#Test
COPY Tests/*.csproj ./Tests/

#restore all the nuget packages
RUN dotnet restore

# copy over source code

# Copy all the project files
# Base projects
COPY Contracts.BLL.Base/. ./Contracts.BLL.Base/
COPY Contracts.DAL.Base/. ./Contracts.DAL.Base/
COPY Contracts.DAL.Domain/. ./Contracts.DAL.Domain/
COPY DAL.Base.EF/. ./DAL.Base.EF/
COPY Domain.Base/. ./Domain.Base/
COPY Graph/. ./Graph/
COPY Utils/. ./Utils/

# App projects
COPY App.Domain/. ./App.Domain/
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY PublicApiDTO/. ./PublicApiDTO/
COPY Services/. ./Services/
COPY WebApp/. ./WebApp/

# Tests
COPY Tests/. ./Tests


WORKDIR /source/WebApp

RUN dotnet publish -c Release -o out

RUN ls out -al

#create a new image from runtime and copy everything to app (no compilers)
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime

WORKDIR /app
COPY --from=BUILD /source/WebApp/out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]
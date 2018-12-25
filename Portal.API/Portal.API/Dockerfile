FROM microsoft/dotnet:2.2-sdk AS publish
WORKDIR /src
COPY . .
WORKDIR "/src/Portal.API"
RUN dotnet publish "Portal.API.csproj" -c Release -o /app

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Portal.API.dll"]
# kintella-localization

## Installation of React and Rest service
Write the following in a terminal in root of the React client, for installing react stuff :)
``` 
npm install react-script@latest
```
Write the following in a terminal in the rest-service, for installting the nuget packages.
```
npm install
```

## Notes
The following are notes of using EF (EntityFramework) tool 
- For adding your first migration. Right click on your project select "EF Core Power Tools" then "Migration Tool".
- When making a change to the migration later on, you can open up "Package Manager Console" (Tools => NuGet Package Manager => Package Manager Console) and write the following
  ``` 
  Add-Migration "Your migration name"
  ```
  If it looks good. Then write the following
  ```
  Update-Database
  ```

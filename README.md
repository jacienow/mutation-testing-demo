Code coverage tools:

`dotnet tool install -g dotnet-coverage`

`dotnet tool install -g dotnet-reportgenerator-globaltool`

Run tests with coverage:
`dotnet test --collect:"XPlat Code Coverage"`

Generate cobertura report:
`reportgenerator -reports:"xml_path" -targetdir:"coverageReport" -reporttypes:html`

Stryker:
`dotnet tool install -g dotnet-stryker`
Write-Host "Starting AuthService..."
Start-Process "dotnet" -ArgumentList "run --project ../AuthService/AuthService.csproj"

Write-Host "Starting ConsultantService..."
Start-Process "dotnet" -ArgumentList "run --project ./ConsultantService/ConsultantService.csproj"

Write-Host "Starting ApiGateway..."
Start-Process "dotnet" -ArgumentList "run --project ./Gateway/Gateway.csproj"

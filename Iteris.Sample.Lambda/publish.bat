@ECHO OFF
REM Limpar as pastas utilização na compilação
dotnet clean Iteris.Sample.Lambda.csproj

REM Realizar o publish do projeto
dotnet lambda package
	

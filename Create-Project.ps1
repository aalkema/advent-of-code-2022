$dayNums = ls | ? {$_ -match "day*" } | % {[int]$_.replace("day","")} | Sort-Object
$latestDay = $dayNums[-1]
$latestDay++
$newSlnName = "day$latestDay"
Write-Host "New project name: $newSlnName"

mkdir $newSlnName
cd $newSlnName
dotnet new sln
dotnet new console -o "$newSlnName.console"
dotnet sln "./$newSlnName.sln" add "$newSlnName.console/$newSlnName.console.csproj"
cd "$newSlnName.console"
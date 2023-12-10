az webapp list --resource-group $resourceGroupName --query "[?state=='Running'].name"

# store the web app name in a variable
$webAppName=$( az webapp list --resource-group $resourceGroupName --query "[?state=='Running'].name | [0]" --output tsv )

echo $webAppName

az webapp config appsettings list --name $webAppName --resource-group $resourceGroupName

# cosmos endpoint and key
# https://mpoed62fl4wem-cosmos-nosql.documents.azure.com:443/
# 9bDOTYlQhsQ9wlzqYlQ9BpWBvsXpXLXOYjasAthTnY8TP6xpYVJNdeUzsMtGSyb89w4Aq136iebFACDbDos4Bw==


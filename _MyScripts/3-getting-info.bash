# login to azure using az cli
az login

# set the subscription to use using az cli
az account set --subscription "2edd29f5-689f-48c5-b93e-93723216ea91"

resourceGroupName="mslearn-cosmos-openai6"

az webapp list --resource-group $resourceGroupName --query "[?state=='Running'].name"

webAppName=$( az webapp list --resource-group $resourceGroupName --query "[?state=='Running'].name | [0]" --output tsv )

echo $webAppName

az webapp config appsettings list --name $webAppName --resource-group $resourceGroupName


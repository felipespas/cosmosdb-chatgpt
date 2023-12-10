# login to azure using az cli
az login

# set the subscription to use using az cli
az account set --subscription "2edd29f5-689f-48c5-b93e-93723216ea91"

az bicep build --file azuredeploy.bicep

$resourceGroupName="mslearn-cosmos-openai5"

az group create --name $resourceGroupName --location "eastus"

az deployment group create `
    --resource-group $resourceGroupName `
    --name zero-touch-deployment `
    --template-uri https://raw.githubusercontent.com/felipespas/cosmosdb-chatgpt/main/azuredeploy.json
    ## --template-uri https://raw.githubusercontent.com/Azure-Samples/cosmosdb-chatgpt/main/azuredeploy.json


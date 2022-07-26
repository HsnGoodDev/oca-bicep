
//필요한 디펜던시 
@description('name of service owner')
@minLength(1)
param name string
param location string = resourceGroup().location
param env string = 'dev'
param loc string ='krc'
@description('email of service owner')
@minLength(1)
param publisherEmail string
@description('name of service owner')
@minLength(1)
param publisherName string

param sku string = 'Consumption'

param skuCount int = 0 

var rg = 'rg-${name}-${env}-${loc}' //* 파라미터 베어리어블 리소스 아웃풋 
var appsvcname = 'appsvc-${name}-${env}-${loc}' 

// Azure Stroge Account
resource st 'Microsoft.Storage/storageAccounts@2021-09-01' = {
  name : 'st${name}${loc}'
  location : location
  kind : 'StorageV2'
  sku :{
    name :'Standard_LRS'
  }
}

//Azure Api management
resource apiManigement 'Microsoft.ApiManagement/service@2021-08-01' ={
  name: 'api${name}${loc}'
  location : location
  sku :{
    name :sku
    capacity: skuCount
  }
  properties:{
    publisherEmail: publisherEmail
    publisherName: publisherName
  }
}

//Azure app service plan
resource csplan 'Microsoft.Web/serverFarms@2021-03-01' = {
  name :'csplan-${name}-${loc}'
  location : location
  kind : 'functionapp'
  sku :{
    name :'Y1'
    tier : 'Dynamic'
    size: 'Y1'
    family :'Y'
    capacity : 0
  }
  properties :{
    reserved : false
  }
}

//Azure function
resource appsvc 'Microsoft.Web/sites@2021-03-01' = {
  name : appsvcname
  location : location
  kind :'functionapp'
  properties: {
    serverFarmId: csplan.id
    siteConfig:{
      appSettings:[
        {
          name: 'AzureWebJobsStorage'
          value:  'DefaultEndpointsProtocol=http;AccountName=${st.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listkeys(st.id , '2021-09-01').keys[0].value}'
        }
      ]
    }
    httpsOnly: true
  }
}
//Azure log Analitics workspace 
resource wrkAnal 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name :'wrkAnal-${name}-${loc}'
  location : location
}
//Azure application insight
resource appInsight 'Microsoft.Insights/components@2020-02-02' = {
  name :'appInsight-${name}-${loc}'
  location : location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: wrkAnal.id
  }
}



output rn string = rg

// az deployment group create -n ocapractice -g  rg-oca-krc -f .\main.bicep -p name=ocaHsn

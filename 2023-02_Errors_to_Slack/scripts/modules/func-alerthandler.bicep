param azureResourceName string
param location string
param appiInstrumentationKey string

resource planAlertHandler 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'plan-alerthandler'
  location: location
  kind: 'linux'  
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
  }
  properties: {
    reserved: true
  }
}

resource stAlertHandler 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: uniqueString(resourceGroup().id)
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource funcAlertHandler 'Microsoft.Web/sites@2022-03-01' = {
  name: azureResourceName
  location: location
  kind: 'functionapp'
  properties: {
    httpsOnly: true
    serverFarmId: planAlertHandler.id
    clientAffinityEnabled: true    
    siteConfig: {
      use32BitWorkerProcess: false
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appiInstrumentationKey
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${stAlertHandler.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(stAlertHandler.id, stAlertHandler.apiVersion).keys[0].value}'
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet-isolated'
        }
        {
          name: 'SlackExceptionsWebHook'
          value: '<The webhook you create for your Slack App> -> /services/Txxxxxx/xxxxxxx/xxxxxxx'
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
        {
          name: 'WEBSITE_TIME_ZONE'
          value: 'W. Europe Standard Time'
        }
      ]
      linuxFxVersion: 'DOTNET-ISOLATED|7.0'
    }
  }
}

output defaultHostName string = funcAlertHandler.properties.defaultHostName
output id string = funcAlertHandler.id

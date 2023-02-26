param azureResourceName string
param location string
param appiInstrumentationKey string

resource planAlertOrigin 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'plan-alertorigin'
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
  kind: 'linux'
}

resource appAlertOrigin 'Microsoft.Web/sites@2022-03-01' = {
  name: azureResourceName
  location: location 
  properties: {
    siteConfig: {
        appSettings: [
          {
            name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
            value: appiInstrumentationKey
          }
        ]
    }
    serverFarmId: planAlertOrigin.id
  }
}

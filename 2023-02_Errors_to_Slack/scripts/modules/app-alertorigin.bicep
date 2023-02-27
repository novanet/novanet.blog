param azureResourceName string
param location string
param appiInstrumentationKey string

// App service plan
resource planAlertOrigin 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'plan-alertorigin'
  location: location  
  kind: 'linux'
  sku: {
    name: 'B2'
    tier: 'Basic'
  }
  properties: {    
    reserved: true
  }
}

// App
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

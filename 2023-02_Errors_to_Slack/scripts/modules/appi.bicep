param location string
param azureResourceName string

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: azureResourceName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

output id string = appInsights.id
output instrumentationKey string = appInsights.properties.InstrumentationKey
